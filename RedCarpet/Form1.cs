﻿using System;
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

        bool StopUndo;
        bool StopRedo;
        int UndoInt = 0;
        int RedoInt = 0;
        string Undo = null;
        string Redo = null;
        GridItem UndoShareItem = null;
        string UndoSharePropertyName = null;
        GridItem RedoShareItem = null;
        string RedoSharePropertyName = null;
        object UndoObject = null;
        object RedoObject = null;
        int UndoInt2;
        int RedoInt2;
        bool UndoBool;
        bool RedoBool;
        Vector3 UndoVector;
        Vector3 RedoVector;
        bool UndoBool2;
        bool RedoBool2;
        bool UndoBool3;
        bool RedoBool3;
        MapObject UndoMapObject;
        MapObject RedoMapObject;
        string UndoString;
        string RedoString;
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
            else if (e.Button == MouseButtons.Left)
            {
                if (UndoBool2 == false)
                {
                    if (SelectedIndex != -1)
                    {
                        Undo = "ObjectMoveGl";
                        UndoInt = SelectedIndex;
                        UndoVector = SelectedSection[SelectedIndex].position;
                        UndoBool2 = true;
                    }
                }
                UndoBool3 = true; //Used to know if a object is moved using the gl. if not,
                                  //the undo always think that the last undo move was moving the object cause
                                  //the mouse move and no mouse click is always triggered and fired
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

                if (SelectedIndex != -1)
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
                    if (SelectedSection[SelectedIndex].RequiresCustomRendering)
                        SelectedSection[SelectedIndex].Drag(MoveDir * dif / 24, e.X, e.Y);
                    else
                        SelectedSection[SelectedIndex].position += MoveDir * dif / 24;
                }
                glControl1.Invalidate();
            }
            if (e.Button != MouseButtons.Left)
            {
                if (UndoBool3 == true)
                {
                    UndoBool2 = false;
                    UndoBool3 = false;
                }
            }
            else
            {
                if (MouseAxis != 0)
                {
                    MouseAxis = 0;
                    if (SelectedIndex > 0 && SelectedSection[SelectedIndex].RequiresCustomRendering) SelectedSection[SelectedIndex].StopDragging();
                }
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

        void selectObject(int Objindex)
        {
            Undo = "selectObject";
            UndoInt = SelectedIndex;
            SelectedIndex = Objindex;
            glControl1.Invalidate();
        }

        private void objectsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StopUndo == false)
            {
                if (UndoBool == false)
                {
                    UndoBool = true;
                    UndoInt2 = objectsList.SelectedIndex;
                }
                if (UndoBool == true)
                {
                    if (UndoInt2 != objectsList.SelectedIndex)
                    {
                        Undo = "ObjectListSelectedIndex";
                        UndoInt = UndoInt2;
                        UndoInt2 = objectsList.SelectedIndex;
                    }
                }
                propertyGrid1.SelectedObject = null;
                if (SelectedIndex != -1)
                    propertyGrid1.SelectedObject = SelectedSection[SelectedIndex];

                SelectedIndex = objectsList.SelectedIndex;
                glControl1.Invalidate();
            }
            else if (StopUndo == true)
            {
                StopUndo = false;
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
            StopUndo = true;
            Undo = "ObjectDelete";
            UndoString = section;
            UndoMapObject = loadedMap.mobjs[section].ElementAt(index);
            if (propertyGrid1.SelectedObject == loadedMap.mobjs[section][index]) propertyGrid1.SelectedObject = null;
            loadedMap.mobjs[section].RemoveAt(index);
            if (SelectedSection == loadedMap.mobjs[section]) objectsList.Items.RemoveAt(index);
            glControl1.Invalidate();
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
            if (Undo == "selectObject")
            {
                SelectedIndex = UndoInt;// this was a peace of cake
                glControl1.Invalidate();
            }
            if (Undo == "propertyValueChanged")
            {
                PropertyInfo pi = propertyGrid1.SelectedObject.GetType().GetProperty(UndoSharePropertyName);
                pi.SetValue(propertyGrid1.SelectedObject, UndoObject, null); //I Spent about an two days for finding a way to do this lol

                propertyGrid1.Refresh();
                Undo = null;
            }
            if (Undo == "ObjectListSelectedIndex")
            {
                objectsList.SelectedIndex = UndoInt; //Also a piece of cake
                Undo = null;
            }
            if (Undo == "ObjectMoveGl")
            {
                SelectedSection[UndoInt].position = UndoVector; // a little overcomplicated
                glControl1.Refresh();
                Undo = null;
            }
            if (Undo == "ObjectDelete")
            {
                AddObject(UndoMapObject, UndoString);
                glControl1.Invalidate();
                Undo = null;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                undoToolStripMenuItem.PerformClick(); //For an unkonw reason this only fires if the z button is pressed
                                                      //after the control (ctrl) button.tried writing it backward but no luck.
            }
        }

        private void debugTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            debugTestToolStripMenuItem.Text = Undo;
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
    }
}
