using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Syroot.NintenTools.Byaml.Dynamic;
using EveryFileExplorer;
using System.IO;
using RedCarpet.Gfx;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using static RedCarpet.Object;
using Syroot.NintenTools.Bfres;
using Polenter.Serialization;
using System.Xml.Linq;
using System.Reflection;
using System.Diagnostics;

/* -- RedCarpet --
 * MasterF0x
 * OatmealDome
 * Ray Koopa
 * Exelix :D
 * Simon
 */

namespace RedCarpet
{
    public partial class Form1 : Form
    {
        private SmShaderProgram shaderProgram;
        public SmCamera camera = new SmCamera();
        public Dictionary<string, SmModel> modelDict = new Dictionary<string, SmModel>();
        private List<string> InvalidNames = new List<string>(); //Models not found, avoid searching multiple times the same models
        public Matrix4 projectionMatrix;

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

        private int SelectedIndex
        {
            get { return objectsList.SelectedIndex; }
            set { objectsList.SelectedIndex = value; }
        }

        private string SelectedSectionName
        {
            get { return SectionSelect.Text; }
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
        public int LevelHighestId = 0;
        MapObject Item;

        //-------------------------------------------- Undo And Redo Variables --------------------------------------------

        private bool DoClearUndoAct3 = false;
        private bool StopUndo;
        private bool StopRedo;
        private int UndoInt = 0;
        private int RedoInt = 0;
        private string Undo = null;
        private string Redo = null;
        private GridItem UndoShareItem = null;
        private string UndoSharePropertyName = null;
        private GridItem RedoShareItem = null;
        private string RedoSharePropertyName = null;
        private object UndoObject = null;
        private object RedoObject = null;
        private int UndoInt2;
        private int RedoInt2;
        private bool UndoBool;
        private bool RedoBool;
        private Vector3 UndoVector;
        private Vector3 RedoVector;
        private bool UndoBool2;
        private bool RedoBool2;
        private bool UndoBool3;
        private bool RedoBool3;
        private RedCarpet.Object.MapObject UndoMapObject;
        private RedCarpet.Object.MapObject RedoMapObject;
        private string UndoString;
        private string RedoString;
        private bool StopUndo2;
        private bool StopRedo2;
        private int UndoInt21 = 0;
        private int RedoInt21 = 0;
        private string Undo2 = null;
        private string Redo2 = null;
        private GridItem UndoShareItem2 = null;
        private string UndoSharePropertyName2 = null;
        private GridItem RedoShareItem2 = null;
        private string RedoSharePropertyName2 = null;
        private object UndoObject2 = null;
        private object RedoObject2 = null;
        private int UndoInt22;
        private int RedoInt22;
        private bool UndoBool21;
        private bool RedoBool21;
        private Vector3 UndoVector2;
        private Vector3 RedoVector2;
        private bool UndoBool22;
        private bool RedoBool22;
        private bool UndoBool23;
        private bool RedoBool23;
        private RedCarpet.Object.MapObject UndoMapObject2;
        private RedCarpet.Object.MapObject RedoMapObject2;
        private string UndoString2;
        private string RedoString2;
        private bool StopUndo3;
        private bool StopRedo3;
        private int UndoInt31 = 0;
        private int RedoInt31 = 0;
        private string Undo3 = null;
        private string Redo3 = null;
        private GridItem UndoShareItem3 = null;
        private string UndoSharePropertyName3 = null;
        private GridItem RedoShareItem3 = null;
        private string RedoSharePropertyName3 = null;
        private object UndoObject3 = null;
        private object RedoObject3 = null;
        private int UndoInt32;
        private int RedoInt32;
        private bool UndoBool31;
        private bool RedoBool31;
        private Vector3 UndoVector3;
        private Vector3 RedoVector3;
        private bool UndoBool32;
        private bool RedoBool32;
        private bool UndoBool33;
        private bool RedoBool33;
        private RedCarpet.Object.MapObject UndoMapObject3;
        private RedCarpet.Object.MapObject RedoMapObject3;
        private string UndoString3;
        private string RedoString3;
        //Need to use different bools in order to make the undo work correctly.

        //-----------------------------------------------------------------------------------------------------------------
        public Form1()
        {
            InitializeComponent();
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

        void MakeByml()
        {
            LoadedByml = new Dictionary<string, dynamic>();
            LoadedByml.Add("Objs", new List<dynamic>()); //Looks like the Objs section contains a copy of every object of the other sections 
            foreach (string k in SectionSelect.Items)
            {
                if (!LoadedByml.ContainsKey(k)) LoadedByml.Add(k, new List<dynamic>());
                foreach (MapObject m in loadedMap.mobjs[k])
                {
                    ((List<dynamic>)LoadedByml[k]).Add(m.AllProperties);
                    ((List<dynamic>)LoadedByml["Objs"]).Add(m.AllProperties);
                }
            }
            LoadedByml.Add("FilePath", cpath.Text);
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
                if (!(LoadedByml[k] is List<dynamic>) || k == "Objs") continue;
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
                int ID = int.Parse(Tmp_mpobj.objectID.Remove(0, 3));
                if (ID > LevelHighestId) LevelHighestId = ID;

                LoadModelToObj(Tmp_mpobj);

                loadedMap.mobjs[section].Add(Tmp_mpobj);
            }
        }

        private void LoadModelToObj(MapObject obj)
        {
            if (obj.unitConfigName == "RouteDokan" || obj.unitConfigName == "RouteDokanLauncher")//Load clear pipes pieces
            {
                foreach (dynamic tmp in obj.AllProperties["Links"]["Parts"])
                {
                    LoadModel(tmp["ModelName"]);
                }
            }

            // Load the model
            SmModel model;
            if (obj.modelName != null && !obj.Equals(""))
            {
                model = LoadModel(obj.modelName);
            }
            else
            {
                model = LoadModel(obj.unitConfigName);
            }

            // Set the object's bounding box if one is found
            if (model != null)
            {
                obj.boundingBox = model.boundingBox;
            }

        }

        private SmModel LoadModel(string modelName)
        {
            if (InvalidNames.Contains(modelName)) return null;
            // todo: don't hardcode
            string modelPath = Properties.Settings.Default.GamePath + @"ObjectData\";
            string stagePath = Properties.Settings.Default.GamePath + @"stageModel\";

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
            InvalidNames.Add(modelName);
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
                bool isSelectedSection = k.Equals(SelectedSectionName);
                for (int i = 0; i < loadedMap.mobjs[k].Count; i++)
                {
                    MapObject mapObject = loadedMap.mobjs[k][i];
                    RenderMapObject(mapObject, (isSelectedSection && SelectedIndex == i), modelLocation, colorLocation);
                }
            }

            // Swap buffers
            glControl1.SwapBuffers();
        }

        private void RenderModel(SmModel model, Vector3 position, Vector3 rotation, Vector3 scale, bool selected, int modelLocation, int colorLocation)
        {
            // Get the position and rotation of the object
            Matrix4 positionMat = Matrix4.CreateTranslation(position);
            Matrix4 rotXMat = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X));
            Matrix4 rotYMat = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y));
            Matrix4 rotZMat = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z));
            Matrix4 scaleMat = Matrix4.CreateScale(scale);
            Matrix4 finalMat = scaleMat * (rotXMat * rotYMat * rotZMat) * positionMat;

            // Set the position in the shader
            GL.UniformMatrix4(modelLocation, false, ref finalMat);

            // Render filled triangles
            GL.Uniform4(colorLocation, whiteColor);
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

        private void RenderMapObject(MapObject mapObject, bool selected, int modelLocation, int colorLocation)
        {
            if (mapObject.RequiresCustomRendering)
                mapObject.Render(
                    (SmModel model, Vector3 pos, Vector3 rot, Vector3 scale, bool sel) =>
                        RenderModel(model, pos, rot, scale, sel && selected, modelLocation, colorLocation));
            else
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
                RenderModel(model, mapObject.position, mapObject.rotation, mapObject.scale, selected, modelLocation, colorLocation);
            }
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!((this.loadedMap == null) || ReferenceEquals(this.LoadedByml, null)))
            {
                int num = this.glControl1.Height - e.Y;
                if (e.Button == MouseButtons.Right)
                {
                    float num2 = (e.X - this.prevMouseX) / 100f;
                    float num3 = (num - this.prevMouseY) / 100f;
                    this.camera.yaw += num2;
                    this.camera.pitch += num3;
                    this.glControl1.Invalidate();
                }
                else if (e.Button == MouseButtons.Left)
                {
                    if (!this.UndoBool2 && (this.SelectedIndex != -1))
                    {
                        this.PassUndoBackward();
                        this.Undo = "ObjectMoveGl";
                        this.UndoInt = this.SelectedIndex;
                        this.UndoVector = this.SelectedSection[this.SelectedIndex].position;
                        this.UndoBool2 = true;
                    }
                    this.UndoBool3 = true;
                    Point point = this.glControl1.PointToClient(Cursor.Position);
                    if (this.MouseAxis == 0)
                    {
                        Vector3 vector = new Vector3(0f, 0f, 0f);
                        if (Math.Abs((int)(this.MouseStart.X - point.X)) > 0x2d)
                        {
                            Vector3 vector2 = new Vector3((float)(Math.Cos(this.camera.yaw + 1.5707963267948966) * ((float)Math.Cos((double)this.camera.pitch))), (float)Math.Sin((double)this.camera.pitch), (float)(Math.Sin(this.camera.yaw + 1.5707963267948966) * ((float)Math.Cos((double)this.camera.pitch))));
                            if ((Math.Abs(vector2.X) > Math.Abs(vector2.Y)) && (Math.Abs(vector2.X) > Math.Abs(vector2.Z)))
                            {
                                this.MoveDir = (vector2.X <= 0f) ? Vector3.UnitX : -Vector3.UnitX;
                            }
                            else if ((Math.Abs(vector2.Y) > Math.Abs(vector2.X)) && (Math.Abs(vector2.Y) > Math.Abs(vector2.Z)))
                            {
                                this.MoveDir = (vector2.Y <= 0f) ? Vector3.UnitY : -Vector3.UnitY;
                            }
                            else if ((Math.Abs(vector2.Z) > Math.Abs(vector2.Y)) && (Math.Abs(vector2.Z) > Math.Abs(vector2.X)))
                            {
                                this.MoveDir = (vector2.Z <= 0f) ? Vector3.UnitZ : -Vector3.UnitZ;
                            }
                            this.MouseAxis = 1;
                        }
                        else if (Math.Abs((int)(this.MouseStart.Y - point.Y)) > 0x2d)
                        {
                            Vector3 vector3 = new Vector3((float)(Math.Cos((double)this.camera.yaw) * ((float)Math.Cos(this.camera.pitch - 1.5707963267948966))), (float)Math.Sin(this.camera.pitch + 1.5707963267948966), (float)(Math.Sin((double)this.camera.yaw) * ((float)Math.Cos(this.camera.pitch + 1.5707963267948966))));
                            if ((Math.Abs(vector3.X) > Math.Abs(vector3.Y)) && (Math.Abs(vector3.X) > Math.Abs(vector3.Z)))
                            {
                                this.MoveDir = (vector3.X <= 0f) ? Vector3.UnitX : -Vector3.UnitX;
                            }
                            else if ((Math.Abs(vector3.Y) > Math.Abs(vector3.X)) && (Math.Abs(vector3.Y) > Math.Abs(vector3.Z)))
                            {
                                this.MoveDir = (vector3.Y <= 0f) ? -Vector3.UnitY : Vector3.UnitY;
                            }
                            else if ((Math.Abs(vector3.Z) > Math.Abs(vector3.Y)) && (Math.Abs(vector3.Z) > Math.Abs(vector3.X)))
                            {
                                this.MoveDir = (vector3.Z <= 0f) ? -Vector3.UnitZ : Vector3.UnitZ;
                            }
                            this.MouseAxis = 2;
                        }
                    }
                    if (this.SelectedIndex != -1)
                    {
                        float num4 = 0f;
                        int num5 = Math.Abs(this.MouseAxis);
                        if (num5 == 1)
                        {
                            num4 = this.MouseLast.X - point.X;
                        }
                        else if (num5 == 2)
                        {
                            num4 = this.MouseLast.Y - point.Y;
                        }
                        if (this.SelectedSection[this.SelectedIndex].RequiresCustomRendering)
                        {
                            this.SelectedSection[this.SelectedIndex].Drag((this.MoveDir * num4) / 24f, e.X, e.Y);
                        }
                        else
                        {
                            RedCarpet.Object.MapObject local1 = this.SelectedSection[this.SelectedIndex];
                            local1.position += (this.MoveDir * num4) / 24f;
                        }
                    }
                    this.glControl1.Invalidate();
                }
                if (e.Button != MouseButtons.Left)
                {
                    if (this.UndoBool3)
                    {
                        this.UndoBool2 = false;
                        this.UndoBool3 = false;
                    }
                }
                else if (this.MouseAxis != 0)
                {
                    this.MouseAxis = 0;
                    if ((this.SelectedIndex > 0) && this.SelectedSection[this.SelectedIndex].RequiresCustomRendering)
                    {
                        this.SelectedSection[this.SelectedIndex].StopDragging();
                    }
                }
                this.prevMouseX = e.X;
                this.prevMouseY = num;
            }
        }
            private void glControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            // Move the camera towards where it's facing
            camera.cameraPosition += camera.cameraFront * e.Delta;

            glControl1.Invalidate();
        }
        public void ObjectList_PositionChanged()
        {

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
                if (SelectedIndex > 0 && SelectedSection[SelectedIndex].RequiresCustomRendering) SelectedSection[SelectedIndex].StopDragging();
                SectionSelect.Text = obj.Item1;
                selectObject(obj.Item2);
            }
        }
        #endregion

        private void selectObject(int Objindex)
        {
            this.PassUndoBackward();
            this.Undo = "selectObject";
            this.UndoInt = this.SelectedIndex;
            this.SelectedIndex = Objindex;
            this.glControl1.Invalidate();
        }

        private void objectsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.StopUndo)
            {
                if (this.StopUndo)
                {
                    this.StopUndo = false;
                }
            }
            else
            {
                if (!this.UndoBool)
                {
                    this.UndoBool = true;
                    this.UndoInt2 = this.objectsList.SelectedIndex;
                }
                if (this.UndoBool && (this.UndoInt2 != this.objectsList.SelectedIndex))
                {
                    this.PassUndoBackward();
                    this.Undo = "ObjectListSelectedIndex";
                    this.UndoInt = this.UndoInt2;
                    this.UndoInt2 = this.objectsList.SelectedIndex;
                }
                this.propertyGrid1.SelectedObject = null;
                if (this.SelectedIndex != -1)
                {
                    this.propertyGrid1.SelectedObject = this.SelectedSection[this.SelectedIndex];
                }
                this.SelectedIndex = this.objectsList.SelectedIndex;
                this.glControl1.Invalidate();
            }
        }

        private void propertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "unitConfigName" || e.ChangedItem.Label == "modelName") LoadModelToObj(SelectedSection[SelectedIndex]);

            if (objectsList.Items[SelectedIndex].ToString() != SelectedSection[SelectedIndex].unitConfigName)
                objectsList.Items[SelectedIndex] = SelectedSection[SelectedIndex].unitConfigName;

            Undo = "propertyValueChanged";
            UndoShareItem = e.ChangedItem;
            UndoSharePropertyName = UndoShareItem.PropertyDescriptor.Name;
            UndoObject = e.OldValue;

            glControl1_Paint(null, null);
        }
        private void bymlViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opn = new OpenFileDialog();
            opn.InitialDirectory = Properties.Settings.Default.GamePath + "StageData";
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
            opn.InitialDirectory = Properties.Settings.Default.GamePath + "StageData";
            opn.Filter = "szs files | *.szs";
            if (opn.ShowDialog() != DialogResult.OK) return;
            LoadLevel(opn.FileName);
        }

        private void closeCurrentLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Close current level?", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                DisposeCurrentLevel();
            }
            else
            {
                return;
            }
        }

        private void btn_openBymlView_Click(object sender, EventArgs e)
        {
            if (LoadedByml is Dictionary<string, dynamic>) new ByamlViewer(LoadedByml).Show(); else throw new Exception("Not supported");
        }

        private void SectionSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            objectsList.Items.Clear();
            propertyGrid1.SelectedObject = null;
            foreach (MapObject m in loadedMap.mobjs[SelectedSectionName])
                objectsList.Items.Add(m.unitConfigName);
        }

        private void objectsList_doubleClick(object sender, EventArgs e)
        {
            if (objectsList.SelectedItem != null)
            {
                camera.cameraPosition = SelectedSection[SelectedIndex].position + new Vector3(100, 100, 100);
                glControl1.Invalidate();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedByml == null) return;
            MakeByml();
            MemoryStream mem = new MemoryStream();
            ByamlFile.Save(mem, LoadedByml);
            LoadedSarc[loadedBymlFileName] = mem.ToArray();
            SaveFileDialog s = new SaveFileDialog();
            s.FileName = Path.GetFileName(loadedSarcFileName);
            s.Filter = "szs file|*.szs";
            if (s.ShowDialog() == DialogResult.OK) File.WriteAllBytes(s.FileName, YAZ0.Compress(SARC.pack(LoadedSarc)));
        }

        private void selectStageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StageSelectForm form = new StageSelectForm();
            form.ShowDialog(this);
        }

        private void Form1_shown(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.GamePath.Trim() == "" || !Directory.Exists(Properties.Settings.Default.GamePath))
            {
                if (!SelectGameFolder())
                {
                    MessageBox.Show("To use this editor you must have the game's files");
                    this.Close();
                }
            }
        }

        private bool SelectGameFolder()
        {
            MessageBox.Show("Select the game's content folder");
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.GamePath = (fd.SelectedPath.EndsWith("\\") || fd.SelectedPath.EndsWith("/")) ? fd.SelectedPath : fd.SelectedPath + "\\";
                Properties.Settings.Default.Save();
                return true;
            }
            return false;
        }

        private void changeGameFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectGameFolder();
        }

        private void btn_duplicate_Click(object sender, EventArgs e)
        {
            MapObject m = (MapObject)SelectedSection[SelectedIndex].Clone();
            LevelHighestId++;
            m.objectID = "obj" + LevelHighestId.ToString();
            AddObject(m, SelectedSectionName);
        }

        public void AddObject(MapObject obj, string section)
        {
            loadedMap.mobjs[section].Add(obj);
            
            if (section == SelectedSectionName)
            {
                objectsList.Items.Add(obj.unitConfigName);
                SelectedIndex = objectsList.Items.Count - 1;
            }
            glControl1.Invalidate();
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            DeleteObject(SelectedSectionName, SelectedIndex);
        }

        public void DeleteObject(string section, int index)
        {
            this.StopUndo = true;
            this.PassUndoBackward();
            this.Undo = "ObjectDelete";
            this.UndoString = section;
            this.UndoMapObject = Enumerable.ElementAt<RedCarpet.Object.MapObject>(this.loadedMap.mobjs[section], index);
            if (this.propertyGrid1.SelectedObject == this.loadedMap.mobjs[section][index])
            {
                this.propertyGrid1.SelectedObject = null;
            }
            this.loadedMap.mobjs[section].RemoveAt(index);
            if (this.SelectedSection == this.loadedMap.mobjs[section])
            {
                this.objectsList.Items.RemoveAt(index);
            }
            this.glControl1.Invalidate();
        }

        private void actorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddObject(new MapObject(MapObject.MakeNewObject()), SelectedSectionName);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }
        private static Dictionary<string, object> GetXmlData(XElement xml)
        {
            var attr = xml.Attributes().ToDictionary(d => d.Name.LocalName, d => (object)d.Value);
            if (xml.HasElements) attr.Add("_value", xml.Elements().Select(e => GetXmlData(e)));
            else if (!xml.IsEmpty) attr.Add("_value", xml.Value);
            return new Dictionary<string, object> { { xml.Name.LocalName, attr } };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (LoadedByml == null)
            {
                MessageBox.Show("No Level Loaded", "Load A Level First", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (File.Exists("XmlFile.Xml"))
            {
                if (MessageBox.Show("XmlFile.Xml Already Exists In The Exe Folder. Are You Sure You Want To Replace It?", "File Already Exists", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                {
                    return;
                }
            }
            var settings = new SharpSerializerXmlSettings();
            settings.IncludeAssemblyVersionInTypeName = false;
            settings.IncludeCultureInTypeName = false;
            settings.IncludePublicKeyTokenInTypeName = false;
            var serializer = new SharpSerializer(settings);
            serializer.Serialize(LoadedByml, "XmlFile.Xml");
            MessageBox.Show("Exported Currently Loadded Byml To XmlFile.Xml In The Exe Folder", "Export Successfull", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (File.Exists("XmlFile.Xml"))
            {
                var settings = new SharpSerializerXmlSettings();
                settings.IncludeAssemblyVersionInTypeName = false;
                settings.IncludeCultureInTypeName = false;
                settings.IncludePublicKeyTokenInTypeName = false;
                var serializer = new SharpSerializer(settings);
                object Byml = serializer.Deserialize("XmlFile.Xml");
                MemoryStream mem = new MemoryStream();
                ByamlFile.Save(mem, Byml);
                LoadedSarc[loadedBymlFileName] = mem.ToArray();
                SaveFileDialog s = new SaveFileDialog();
                s.FileName = Path.GetFileName(loadedSarcFileName);
                s.Filter = "szs file|*.szs";
                if (s.ShowDialog() == DialogResult.OK) File.WriteAllBytes(s.FileName, YAZ0.Compress(SARC.pack(LoadedSarc)));
                MessageBox.Show("Imported XmlFile.Xml In The Exe Folder To " + s.FileName, "Import Successfull", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("XmlFile.Xml Does Not Exist In The Exe Folder.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Undo == "selectObject")
            {
                this.SelectedIndex = this.UndoInt;
                this.glControl1.Invalidate();
                this.PassUndoForward();
            }
            if (this.Undo == "propertyValueChanged")
            {
                this.propertyGrid1.SelectedObject.GetType().GetProperty(this.UndoSharePropertyName).SetValue(this.propertyGrid1.SelectedObject, this.UndoObject, null);
                this.propertyGrid1.Refresh();
                this.Undo = null;
                this.PassUndoForward();
            }
            if (this.Undo == "ObjectListSelectedIndex")
            {
                this.objectsList.SelectedIndex = this.UndoInt;
                this.Undo = null;
                this.PassUndoForward();
            }
            if (this.Undo == "ObjectMoveGl")
            {
                this.SelectedSection[this.UndoInt].position = this.UndoVector;
                this.glControl1.Refresh();
                this.Undo = null;
                this.PassUndoForward();
            }
            if (this.Undo == "ObjectDelete")
            {
                this.AddObject(this.UndoMapObject, this.UndoString);
                this.glControl1.Invalidate();
                this.Undo = null;
                this.PassUndoForward();
            }
        }
        private void PassUndoBackward()
        {
            this.StopUndo3 = this.StopUndo2;
            this.StopRedo3 = this.StopRedo2;
            this.UndoInt31 = this.UndoInt21;
            this.RedoInt31 = this.RedoInt21;
            this.Undo3 = this.Undo2;
            this.Redo3 = this.Redo2;
            this.UndoShareItem3 = this.UndoShareItem2;
            this.UndoSharePropertyName3 = this.UndoSharePropertyName2;
            this.RedoShareItem3 = this.RedoShareItem2;
            this.RedoSharePropertyName3 = this.RedoSharePropertyName2;
            this.UndoObject3 = this.UndoObject2;
            this.RedoObject3 = this.RedoObject2;
            this.UndoInt32 = this.UndoInt22;
            this.RedoInt32 = this.RedoInt22;
            this.UndoBool31 = this.UndoBool21;
            this.RedoBool31 = this.RedoBool21;
            this.UndoVector3 = this.UndoVector2;
            this.RedoVector3 = this.RedoVector2;
            this.UndoBool32 = this.UndoBool22;
            this.RedoBool32 = this.RedoBool22;
            this.UndoBool33 = this.UndoBool23;
            this.RedoBool33 = this.RedoBool23;
            this.UndoMapObject3 = this.UndoMapObject2;
            this.RedoMapObject3 = this.RedoMapObject2;
            this.UndoString3 = this.UndoString2;
            this.RedoString3 = this.RedoString2;
            this.StopUndo2 = this.StopUndo;
            this.StopRedo2 = this.StopRedo;
            this.UndoInt21 = this.UndoInt;
            this.RedoInt21 = this.RedoInt;
            this.Undo2 = this.Undo;
            this.Redo2 = this.Redo;
            this.UndoShareItem2 = this.UndoShareItem;
            this.UndoSharePropertyName2 = this.UndoSharePropertyName;
            this.RedoShareItem2 = this.RedoShareItem;
            this.RedoSharePropertyName2 = this.RedoSharePropertyName;
            this.UndoObject2 = this.UndoObject;
            this.RedoObject2 = this.RedoObject;
            this.UndoInt22 = this.UndoInt2;
            this.RedoInt22 = this.RedoInt2;
            this.UndoBool21 = this.UndoBool;
            this.RedoBool21 = this.RedoBool;
            this.UndoVector2 = this.UndoVector;
            this.RedoVector2 = this.RedoVector;
            this.UndoBool22 = this.UndoBool;
            this.RedoBool22 = this.RedoBool;
            this.UndoBool23 = this.UndoBool;
            this.RedoBool23 = this.RedoBool;
            this.UndoMapObject2 = this.UndoMapObject;
            this.RedoMapObject2 = this.RedoMapObject;
            this.UndoString2 = this.UndoString;
            this.RedoString2 = this.RedoString;
        }

        private void PassUndoForward()
        {
            this.StopUndo = this.StopUndo2;
            this.StopRedo = this.StopRedo2;
            this.UndoInt = this.UndoInt21;
            this.RedoInt = this.RedoInt21;
            this.Undo = this.Undo2;
            this.Redo = this.Redo2;
            this.UndoShareItem = this.UndoShareItem2;
            this.UndoSharePropertyName = this.UndoSharePropertyName2;
            this.RedoShareItem = this.RedoShareItem2;
            this.RedoSharePropertyName = this.RedoSharePropertyName2;
            this.UndoObject = this.UndoObject2;
            this.RedoObject = this.RedoObject2;
            this.UndoInt2 = this.UndoInt22;
            this.RedoInt2 = this.RedoInt22;
            this.UndoBool = this.UndoBool21;
            this.RedoBool = this.RedoBool21;
            this.UndoVector = this.UndoVector2;
            this.RedoVector = this.RedoVector2;
            this.UndoBool2 = this.UndoBool22;
            this.RedoBool2 = this.RedoBool22;
            this.UndoBool3 = this.UndoBool23;
            this.RedoBool3 = this.RedoBool23;
            this.UndoMapObject = this.UndoMapObject2;
            this.RedoMapObject = this.RedoMapObject2;
            this.UndoString = this.UndoString2;
            this.RedoString = this.RedoString2;
            this.StopUndo2 = this.StopUndo3;
            this.StopRedo2 = this.StopRedo3;
            this.UndoInt21 = this.UndoInt31;
            this.RedoInt21 = this.RedoInt31;
            this.Undo2 = this.Undo3;
            this.Redo2 = this.Redo3;
            this.UndoShareItem2 = this.UndoShareItem3;
            this.UndoSharePropertyName2 = this.UndoSharePropertyName3;
            this.RedoShareItem2 = this.RedoShareItem3;
            this.RedoSharePropertyName2 = this.RedoSharePropertyName3;
            this.UndoObject2 = this.UndoObject3;
            this.RedoObject2 = this.RedoObject3;
            this.UndoInt22 = this.UndoInt32;
            this.RedoInt22 = this.RedoInt32;
            this.UndoBool21 = this.UndoBool31;
            this.RedoBool21 = this.RedoBool31;
            this.UndoVector2 = this.UndoVector3;
            this.RedoVector2 = this.RedoVector3;
            this.UndoBool22 = this.UndoBool32;
            this.RedoBool22 = this.RedoBool32;
            this.UndoBool23 = this.UndoBool33;
            this.RedoBool23 = this.RedoBool33;
            this.UndoMapObject2 = this.UndoMapObject3;
            this.RedoMapObject2 = this.RedoMapObject3;
            this.UndoString2 = this.UndoString3;
            this.RedoString2 = this.RedoString3;

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                undoToolStripMenuItem.PerformClick(); //For an unkonw reason this only fires if the z button is pressed
                                                      //after the control (ctrl) button.tried writing it backward but no luck.
            }
        }

        private void testCreateActorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddObject AddObject = new AddObject();
            AddObject.Show();
        }

        private void Undobut_Click(object sender, EventArgs e)
        {
            undoToolStripMenuItem.PerformClick();
        }

        private void Redobut_Click(object sender, EventArgs e)
        {
            redoToolStripMenuItem.PerformClick();
        }

        private void AddObj_Click(object sender, EventArgs e)
        {
            actorToolStripMenuItem.PerformClick();
        }

        private void DelAllObj_Click(object sender, EventArgs e)
        {
            loadedMap.mobjs["ObjectList"].Clear();
            propertyGrid1.SelectedObject = null;
            if (SelectedSectionName == "ObjectList")
            {
                objectsList.Items.Clear();
            }
            glControl1.Invalidate();
        }

        private void DelAllWithoutOne_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Do it?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (SectionSelect.Items.Contains("Objs")) SectionSelect.SelectedItem = "Objs";
                else if (SectionSelect.Items.Contains("ObjectList")) SectionSelect.SelectedItem = "ObjectList";
                else SectionSelect.SelectedIndex = 0;
                int SearchedItemIndex = objectsList.FindString(DelAllEx.Text);
                if (SearchedItemIndex != -1)
                {
                    bool Done = new bool();
                    object ListBoxItem = new object();
                    if (propertyGrid1.SelectedObject == SelectedSection[SearchedItemIndex]) propertyGrid1.SelectedObject = null;
                    Item = loadedMap.mobjs["ObjectList"].ElementAt(SearchedItemIndex);
                    ListBoxItem = objectsList.Items[SearchedItemIndex];
                    loadedMap.mobjs["ObjectList"].Clear();
                    objectsList.Items.Clear();
                    SearchedItemIndex = -1;
                    loadedMap.mobjs["ObjectList"].Add(Item);
                    objectsList.Items.Add(ListBoxItem);
                }
                else if (SearchedItemIndex == -1)
                {
                    MessageBox.Show("The name you typed doesn't match any objects.", "No Matches Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else { return; }
        }

        private void debugGetValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            object QWE = SelectedSection[SelectedIndex];
            var settings = new SharpSerializerXmlSettings();
            settings.IncludeAssemblyVersionInTypeName = false;
            settings.IncludeCultureInTypeName = false;
            settings.IncludePublicKeyTokenInTypeName = false;
            var serializer = new SharpSerializer(settings);
            serializer.Serialize(QWE, "Test.Xml");
        }

        private void objectFileManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}
