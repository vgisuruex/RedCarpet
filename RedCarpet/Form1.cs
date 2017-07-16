using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syroot.NintenTools.Byaml;
using Syroot.NintenTools.Byaml.Dynamic;
using EveryFileExplorer;
using System.IO;
using RedCarpet.Gfx;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using static RedCarpet.Object;
using Syroot.NintenTools.Bfres;
using System.Diagnostics;

/* -- RedCarpet --
 * MasterF0x
 * OatmealDome
 * Ray Koopa
 * Exelix :D
 */

namespace RedCarpet
{
    public partial class Form1 : Form
    {
        private SmShaderProgram shaderProgram;
        private SmCamera camera = new SmCamera();
        private Dictionary<string, SmModel> modelDict = new Dictionary<string, SmModel>();
        private Matrix4 projectionMatrix;

        private Point FormCenter
        {
            get
            {
                return new Point(this.Location.X + this.Width, this.Location.Y + this.Height);
            }
        }

        private List<MapObject> SelectedSection
        {
            get { return loadedMap.mobjs[SectionSelect.Text]; }
        }

        private Vector3 MoveDir;
        private int MouseAxis;
        private Point MouseStart;
        private Point MouseLast;

        private int prevMouseX;
        private int prevMouseY;
        
        private Object loadedMap = null; //Load every section of the byml in dictionary

        private static Vector4 blackColor = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
        private static Vector4 whiteColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        private static Vector4 orangeColor = new Vector4(1.0f, 0.5f, 0.2f, 1.0f);
        private static Vector4 blueColor = new Vector4(75.0f / 255.0f, 184.0f / 255.0f, 251.0f / 255.0f, 1.0f);

        private Dictionary<string, byte[]> LoadedSarc = null; 
        string loadedSarcFileName = "";
        string loadedBymlFileName = ""; //inside the sarc
        Dictionary<string, dynamic> LoadedByml = null;

        //static string BASEPATH = @"C:\Users\ronal\Desktop\3DWorldKit\SM3DW\content\"; //no need to put the editor in the game's folder, \ at the end matters !!!
        //static string BASEPATH = @"C:\HAX\WIIU\SUPER MARIO 3D WORLD (EUR)\content\";
        static string BASEPATH = @"D:\Archive\Nintendo\WiiU\SM3DW\sm3dw_game\content\";

        public Form1()
        {
            InitializeComponent();
            if (!Directory.Exists(BASEPATH)) throw new Exception("set BASEPATH to the game's folder");
        }

        private void titleDemo00StageDesign1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadLevel(BASEPATH + "StageData/" + sender.ToString() + ".szs");
        }

        public void DisposeCurrentLevel()
        {
            SectionSelect.Items.Clear();
            btn_openBymlView.Enabled = false;
            loadedSarcFileName = "";
            loadedBymlFileName = "";
            LoadedByml = null;
            modelDict.Clear();
            if (LoadedSarc != null) LoadedSarc.Clear();
            LoadedSarc = null;
            cpath.Text = "";
            objectsList.Items.Clear();
            if (loadedMap != null) loadedMap.mobjs.Clear();
            loadedMap = null;
            glControl1.Invalidate();
            GC.Collect();
        }

        public void LoadLevel(string filename)
        {
            DisposeCurrentLevel();
            //both yaz0 decompression and sarc unpacking are done in ram, this avoids useless wirtes to disk, faster level loading
            SARC sarc = new SARC();            
            LoadedSarc = sarc.unpackRam(YAZ0.Decompress(filename)); //the current level files are now stored in LoadedSarc

            loadedSarcFileName = filename;
            /*Yaz0Compression.Decompress(BASEPATH + "StageData/" + "TitleDemo00StageDesign1" + ".szs", "stageDesign.sarc");
            sarc.unpack("stageDesign");
            Yaz0Compression.Decompress(BASEPATH + "ObjectData/" + "TitleDemoStepA" + ".szs", "stageModel.sarc");
            sarc.unpack("stageModel"); Not needed for now*/

            //removed stage model loading, every stage include the model name in the byml

            // parse byaml
            parseBYML(Path.GetFileNameWithoutExtension(filename) + ".byml");

            btn_openBymlView.Enabled = true;
            // force render
            glControl1.Invalidate();
        }

        public void parseBYML(string name)
        {
            //calling it Object wasn't a great idea, i stared at the code for half hour before realizing that it's a custom class lol
            loadedMap = new Object();
            if (name.EndsWith("Map1.byml"))  //the szs name always ends with 1, but the map byml doesn't, this seems to be true for every level
                loadedBymlFileName = name.Replace("Map1.byml", "Map.byml");
            else if (name.EndsWith("Design1.byml"))
                loadedBymlFileName = name.Replace("Design1.byml", "Design.byml");
            else if (name.EndsWith("Sound1.byml"))
                loadedBymlFileName = name.Replace("Sound1.byml", "Sound.byml");
            else loadedBymlFileName = name;

            LoadedByml = ByamlFile.Load(new MemoryStream(LoadedSarc[loadedBymlFileName]));
            foreach (string k in LoadedByml.Keys)
            {
                if (!(LoadedByml[k] is List<dynamic>)) continue;
                SectionSelect.Items.Add(k);
                loadedMap.mobjs.Add(k, new List<MapObject>());
                LoadObjectsSection(k);
            }

            if (SectionSelect.Items.Contains("Objs")) SectionSelect.SelectedItem = "Objs";
            else if (SectionSelect.Items.Contains("ObjectList")) SectionSelect.SelectedItem = "ObjectList";
            else SectionSelect.SelectedIndex = 0;

            cpath.Text = LoadedByml["FilePath"];
        }

        private void LoadObjectsSection(string section)
        {

            IList<dynamic> objs = LoadedByml[section]; // ObjectList works as well
            for (int i = 0; i < objs.Count; i++)
            {
                MapObject Tmp_mpobj = new Object.MapObject(objs[i]);
                loadedMap.mobjs[section].Add(Tmp_mpobj);

                // Load the model
                SmModel model;
                if (Tmp_mpobj.modelName != null && !Tmp_mpobj.Equals(""))
                {
                    model = LoadModel(Tmp_mpobj.modelName);
                }
                else
                {
                    model = LoadModel(Tmp_mpobj.unitConfigName);
                }

                // Set the object's bounding box if one is found
                if (model != null)
                {
                    Tmp_mpobj.boundingBox = model.boundingBox;
                }
            }
        }

        private SmModel LoadModel(string modelName)
        {
            // todo: don't hardcode
            string modelPath = BASEPATH + @"ObjectData\";
            string stagePath = BASEPATH + @"stageModel\";

            if (!Directory.Exists("Models")) Directory.CreateDirectory("Models"); //Models once unpacked will be saved in the editor's directory instead of the game folder

            // Check if the model is loaded first
            SmModel model;
            modelDict.TryGetValue(modelName, out model);
            if (model != null)
            {
                return model;
            }

            if (LoadModelWithBase(modelPath, modelName))
            {
                return modelDict[modelName];
            }
            else if (LoadModelWithBase(stagePath, modelName))
            {
                return modelDict[modelName];
            }

            Console.WriteLine("WARN: Could not load a model for " + modelName);
            return null;
        }
        
        private bool LoadModelWithBase(string basePath, string modelName)
        {
            // Attempt to load the bfres that contains the model
            string modelPath = "Models\\" + modelName + ".bfres";
            if (File.Exists(modelPath))
            {
                // Load the bfres
                LoadBfres(modelPath);
                return true;
            }

            // Check if the szs archive that contains the bfres exists
            string szsPath = basePath + modelName + ".szs";
            if (File.Exists(szsPath))
            {
                // Decompress the szs into a sarc archive
                string sarcPath = basePath + modelName;
                SARC sarc = new SARC();
                var unpackedmodel = sarc.unpackRam(YAZ0.Decompress(szsPath));
                if (!unpackedmodel.ContainsKey(modelName + ".bfres"))
                    return false;

                File.WriteAllBytes(modelPath, unpackedmodel[modelName + ".bfres"]);
                // Load the bfres
                LoadBfres(modelPath);

                return true;
            }

            return false;
        }

        private void LoadBfres(string path)
        {
            ResFile resFile = new ResFile(path);
            foreach (String key in resFile.Models.Keys)
            {
                Model model = resFile.Models[key];

                //Console.WriteLine("loading fmdl @ " + resFile.Models.IndexOf(key) + ": " + key);
                if (modelDict.ContainsKey(key))
                {
                    Console.WriteLine("WARN: Duplicated FMDL " + key + ", skipping");
                    return;
                }

                modelDict.Add(key, new SmModel(model, resFile.ByteOrder));
            }
        }

        #region GlControl events
        private void glControl1_Load(object sender, EventArgs e)
        {
            // Enable depth test
            GL.Enable(EnableCap.DepthTest);

            // Set the viewport
            GL.Viewport(glControl1.ClientRectangle);

            // Compile and link shaders
            string vertexShaderSrc = Encoding.ASCII.GetString(Properties.Resources.VertexShader);
            string fragmentShaderSrc = Encoding.ASCII.GetString(Properties.Resources.FragmentShader);
            shaderProgram = new SmShaderProgram(vertexShaderSrc, fragmentShaderSrc);

            // Construct the projection matrix
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)glControl1.Width / (float)glControl1.Height, 1.0f, 20000.0f);

            // Check the maximum line width supported
            float[] widthRange = new float[2];
            GL.GetFloat(GetPName.LineWidthRange, widthRange);
            if (widthRange[1] < 3.0f)
            {
                throw new Exception("Graphics card or driver does not support required line width");
            }
        }

        private void glControl1_resize(object sender, EventArgs e)
        {
            // Update the viewport
            GL.Viewport(glControl1.ClientRectangle);
            // Construct the projection matrix
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)glControl1.Width / (float)glControl1.Height, 1.0f, 20000.0f);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            // Do standard clearing
            GL.ClearColor(Color.Turquoise);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Don't do anything else if there is no map loaded
            if (loadedMap == null)
            {
                glControl1.SwapBuffers();
                return;
            }

            // Get uniform locations
            int modelLocation = shaderProgram.GetUniformLocation("model");
            int viewLocation = shaderProgram.GetUniformLocation("view");
            int projectionLocation = shaderProgram.GetUniformLocation("projection");
            int colorLocation = shaderProgram.GetUniformLocation("colorVec");

            // Set uniforms
            Matrix4 viewMatrix = camera.CalculateLookAt();

            shaderProgram.Use();
            GL.UniformMatrix4(viewLocation, false, ref viewMatrix);
            GL.UniformMatrix4(projectionLocation, false, ref projectionMatrix);

            // Render all map objects
            foreach (string k in loadedMap.mobjs.Keys.ToArray())
            {
                bool isSelectedSection = k.Equals(SectionSelect.Text);
                for (int i = 0; i < loadedMap.mobjs[k].Count; i++)
                {
                    MapObject mapObject = loadedMap.mobjs[k][i];
                    RenderMapObject(mapObject, (isSelectedSection && objectsList.SelectedIndex == i), modelLocation, colorLocation);
                }
            }

            // Swap buffers
            glControl1.SwapBuffers();
        }

        private void RenderMapObject(MapObject mapObject, bool selected, int modelLocation, int colorLocation) //TODO: add scale and rotation
        {
            // Try to get the model via the UnitConfigName or ModelName
            SmModel model;
            if (!modelDict.TryGetValue(mapObject.unitConfigName, out model))
            {
                if (mapObject.modelName == null || !modelDict.TryGetValue(mapObject.modelName, out model))
                {
                    // Give up
                    return;
                }
            }

            // Get the position and rotation of the object
            Matrix4 positionMat = Matrix4.CreateTranslation(mapObject.position);
            Matrix4 rotXMat = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(mapObject.rotation.X));
            Matrix4 rotYMat = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(mapObject.rotation.Y));
            Matrix4 rotZMat = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(mapObject.rotation.Z));
            Matrix4 scaleMat = Matrix4.CreateScale(mapObject.scale);
            Matrix4 finalMat = scaleMat * (rotXMat * rotYMat * rotZMat) * positionMat;

            // Set the position in the shader
            GL.UniformMatrix4(modelLocation, false, ref positionMat);

            // Render filled triangles
            GL.Uniform4(colorLocation, mapObject.objectID.Equals("stageObject") ? whiteColor : whiteColor);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.Enable(EnableCap.PolygonOffsetFill);
            GL.PolygonOffset(1, 1);

            model.Render();

            // Render outlined triangles
            GL.Disable(EnableCap.PolygonOffsetFill);
            GL.Uniform4(colorLocation, blackColor);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            GL.Enable(EnableCap.PolygonOffsetLine);
            GL.PolygonOffset(-1, -1);

            model.Render();

            // Check if this is the currently selected object
            if (selected)
            {
                // Render the bounding box
                GL.Uniform4(colorLocation, blueColor);
                GL.LineWidth(3.0f);

                model.boundingBox.Render();
            }

            GL.Disable(EnableCap.PolygonOffsetLine);
            GL.LineWidth(1.0f);
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (loadedMap == null || LoadedByml == null) return;
            // OpenGL's Y-origin starts at the bottom left, unlike WinForms
            int newY = glControl1.Height - e.Y;

            if (e.Button == MouseButtons.Right)
            {
                float deltaX = ((float)e.X - (float)prevMouseX) / 100;
                float deltaY = ((float)newY - (float)prevMouseY) / 100;

                camera.yaw += deltaX;
                camera.pitch += deltaY;

                glControl1.Invalidate();
            }

            if (e.Button == MouseButtons.Left)
            {
                Point relMouse = glControl1.PointToClient(Cursor.Position);
                if (MouseAxis == 0)
                {
                    Vector3 unitm = new Vector3(0f, 0f, 0f);
                    if (Math.Abs(MouseStart.X - relMouse.X) > 45)
                    {
                        Vector3 r = new Vector3((float)(Math.Cos(camera.yaw + Math.PI / 2f) * (float)Math.Cos(camera.pitch)), (float)Math.Sin(camera.pitch),
                        (float)(Math.Sin(camera.yaw + Math.PI / 2f) * (float)Math.Cos(camera.pitch)));

                        if (Math.Abs(r.X) > Math.Abs(r.Y) && Math.Abs(r.X) > Math.Abs(r.Z))
                            if (r.X > 0) MoveDir = -Vector3.UnitX; else MoveDir = Vector3.UnitX;
                        else if (Math.Abs(r.Y) > Math.Abs(r.X) && Math.Abs(r.Y) > Math.Abs(r.Z))
                            if (r.Y > 0) MoveDir = -Vector3.UnitY; else MoveDir = Vector3.UnitY;
                        else if (Math.Abs(r.Z) > Math.Abs(r.Y) && Math.Abs(r.Z) > Math.Abs(r.X))
                            if (r.Z > 0) MoveDir = -Vector3.UnitZ; else MoveDir = Vector3.UnitZ;

                        MouseAxis = 1;
                    }
                    else if (Math.Abs(MouseStart.Y - relMouse.Y) > 45)
                    {
                        Vector3 u = new Vector3((float)(Math.Cos(camera.yaw) * (float)Math.Cos(camera.pitch - Math.PI / 2f)), (float)Math.Sin(camera.pitch + Math.PI / 2f),
                        (float)(Math.Sin(camera.yaw) * (float)Math.Cos(camera.pitch + Math.PI / 2f)));

                        if (Math.Abs(u.X) > Math.Abs(u.Y) && Math.Abs(u.X) > Math.Abs(u.Z))
                            if (u.X > 0) MoveDir = -Vector3.UnitX; else MoveDir = Vector3.UnitX;
                        else if (Math.Abs(u.Y) > Math.Abs(u.X) && Math.Abs(u.Y) > Math.Abs(u.Z))
                            if (u.Y > 0) MoveDir = Vector3.UnitY; else MoveDir = -Vector3.UnitY;
                        else if (Math.Abs(u.Z) > Math.Abs(u.Y) && Math.Abs(u.Z) > Math.Abs(u.X))
                            if (u.Z > 0) MoveDir = Vector3.UnitZ; else MoveDir = -Vector3.UnitZ;

                        MouseAxis = 2;
                    }
                }
                else
                {
                    float dif = 0.0f;
                    switch (Math.Abs(MouseAxis))
                    {
                        case 1:
                            dif = MouseLast.X - relMouse.X;
                            break;
                        case 2:
                            dif = MouseLast.Y - relMouse.Y;
                            break;
                    }
                    Vector3 old = SelectedSection[objectsList.SelectedIndex].position;
                    old += MoveDir * dif / 24;
                    SelectedSection[objectsList.SelectedIndex].position = old;
                }
                glControl1.Invalidate();
            }
            else
            {
                MouseAxis = 0;
            }

            prevMouseX = e.X;
            prevMouseY = newY;
        }

        private void glControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            // Move the camera towards where it's facing
            camera.cameraPosition += camera.cameraFront * e.Delta;

            glControl1.Invalidate();
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (loadedMap == null || loadedMap.mobjs == null) return;
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = MouseLast = MouseStart = glControl1.PointToClient(Cursor.Position);

                int iY = glControl1.Height - e.Y;
                var obj = camera.castRay(e.X, e.Y, glControl1.Width, glControl1.Height, projectionMatrix, loadedMap.mobjs);
                if (obj == null) return;
                SectionSelect.Text = obj.Item1;
                selectObject(obj.Item2);
            }
        }
        #endregion
        
        void selectObject(int Objindex)
        {
            objectsList.SelectedIndex = Objindex;
        }

        private void objectsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = SelectedSection[objectsList.SelectedIndex];
        }

        private void propertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            glControl1_Paint(null,null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bymlViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opn = new OpenFileDialog();
            opn.InitialDirectory = BASEPATH + "StageData";
            opn.Filter = "byml files, szs files |*.byml;*.szs";
            if (opn.ShowDialog() != DialogResult.OK) return;
            dynamic byml = null;
            if (opn.FileName.EndsWith("byml")) byml = ByamlFile.Load(opn.FileName);
            else if (opn.FileName.EndsWith("szs"))
            {
                SARC sarc = new SARC();
                var unpackedsarc = sarc.unpackRam(YAZ0.Decompress(opn.FileName));
                string bymlName = Path.GetFileNameWithoutExtension(opn.FileName) + ".byml";
                if (bymlName.EndsWith("Map1.byml"))  //the szs name always ends with 1, but the map byml doesn't, this seems to be true for every level
                    bymlName = bymlName.Replace("Map1.byml", "Map.byml");
                else if (bymlName.EndsWith("Design1.byml"))
                    bymlName = bymlName.Replace("Design1.byml", "Design.byml");
                else if (bymlName.EndsWith("Sound1.byml"))
                    bymlName = bymlName.Replace("Sound1.byml", "Sound.byml");
                byml = ByamlFile.Load(new MemoryStream(unpackedsarc[bymlName]));
            }
            else throw new Exception("Not supported");
            if (byml is Dictionary<string, dynamic>) new ByamlViewer(byml).Show(); else throw new Exception("Not supported");
        }

        private void openLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opn = new OpenFileDialog();
            opn.InitialDirectory = BASEPATH + "StageData";
            opn.Filter = "szs files | *.szs";
            if (opn.ShowDialog() != DialogResult.OK) return;
            LoadLevel(opn.FileName);
        }

        private void closeCurrentLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisposeCurrentLevel();
        }

        private void btn_openBymlView_Click(object sender, EventArgs e)
        {
            if (LoadedByml is Dictionary<string, dynamic>) new ByamlViewer(LoadedByml).Show(); else throw new Exception("Not supported");
        }

        private void actorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TO DO
            
            /* IList<dynamic> objectList = LoadedByml["Objs"];
            Dictionary<string, dynamic> test = new Dictionary<string, dynamic>();
            test.Add("CollisionCheckDist", 300);
            test.Add("Comment", null);
            test.Add("Id", "f0xCoin");
            test.Add("IsConnectToCollision", false);
            test.Add("IsDropShadowActorDown", false);
            test.Add("IsLinkDest", false);
            test.Add("IsPlacementInRouteDokan", false);
            test.Add("LayerConfigName", "Common");
            objectList.Add(test); */
        }

        private void SectionSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            objectsList.Items.Clear();
            propertyGrid1.SelectedObject = null;
            foreach (MapObject m in loadedMap.mobjs[SectionSelect.Text])
                objectsList.Items.Add(m.unitConfigName);
        }

        private void objectsList_doubleClick(object sender, EventArgs e)
        {
            if (objectsList.SelectedItem != null)
            {
                camera.cameraPosition = SelectedSection[objectsList.SelectedIndex].position + new Vector3(100,100,100);
                glControl1.Invalidate();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedByml == null) return;
            MemoryStream mem = new MemoryStream();
            ByamlFile.Save(mem, LoadedByml);
            LoadedSarc[loadedBymlFileName] = mem.ToArray();
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "szs file|*.szs";
            if (s.ShowDialog() == DialogResult.OK) File.WriteAllBytes(s.FileName, YAZ0.Compress(SARC.pack(LoadedSarc)));
        }
    }
}
