using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.ComponentModel;
using static RedCarpet.PropertyGridTypes;
using System.Collections;
using RedCarpet.Gfx;
using System.Windows.Forms;

namespace RedCarpet
{
    public class Object
    {
        public Dictionary<string, List<MapObject>> mobjs = new Dictionary<string, List<MapObject>>();

        [Serializable]
        public class MapObject : ICloneable
        {
            static void Vector3ToValues(Vector3 vec, dynamic obj)
            {
                obj["X"] = vec.X;
                obj["Y"] = vec.Y;
                obj["Z"] = vec.Z;
            }

            static Vector3 ValuesToVector3(dynamic val)
            {
                return new Vector3(val["X"], val["Y"], val["Z"]);
            }

            public static dynamic MakeNewObject()
            {
                Dictionary<string, dynamic> obj = new Dictionary<string, dynamic>();
                form1.LevelHighestId++;
                obj.Add("Id", "obj" + form1.LevelHighestId.ToString());
                obj.Add("IsLinkDest", false);
                obj.Add("LayerConfigName", "Common");
                obj.Add("Links", new Dictionary<string, dynamic>());
                obj.Add("ModelName", null);
                obj.Add("Rotate", MakeVector3Value());
                obj.Add("Scale", MakeVector3Value(1, 1, 1));
                obj.Add("Translate", MakeVector3Value());
                obj.Add("UnitConfigName", "newObject");
                return obj;
            }

            private static Dictionary<string, dynamic> MakeVector3Value(float X = 0, float Y = 0, float Z = 0)
            {
                Dictionary<string, dynamic> node = new Dictionary<string, dynamic>();
                node.Add("X", X);
                node.Add("Y", Y);
                node.Add("Z", Z);
                return node;
            }

            private static Dictionary<string, dynamic> MakeUnitConfig()
            {
                Dictionary<string, dynamic> node = new Dictionary<string, dynamic>();
                node.Add("DisplayName", "MadeWithRedCarpetEditor");
                node.Add("DisplayRotate", MakeVector3Value());
                node.Add("DisplayScale", MakeVector3Value());
                node.Add("DisplayTranslate", MakeVector3Value());

                return node;
            }

            public dynamic this[string v]
            {
                get { return _bymlNode[v]; }
                set { _bymlNode[v] = value; }
            }

            [TypeConverter(typeof(DictionaryConverter))]
            public Dictionary<string, dynamic> AllProperties
            {
                get { return _bymlNode; }
                set { _bymlNode = value; }
            }

            [Category("Common properties")]
            public bool RequiresCustomRendering
            {
                get { return unitConfigName == "RouteDokan" || unitConfigName == "RouteDokanLauncher"; }
            }

            [Category("Common properties")]
            public string objectID
            {
                get { return _bymlNode["Id"]; }
                set { _bymlNode["Id"] = value; }
            }

            [Category("Common properties")]
            public string modelName
            {
                get { return _bymlNode["ModelName"]; }
                set { _bymlNode["ModelName"] = value; }
            }

            [Category("Common properties")]
            public string Layer
            {
                get { return _bymlNode["LayerConfigName"]; }
                set { _bymlNode["LayerConfigName"] = value; }
            }

            [Category("Common properties")]
            public string unitConfigName
            {
                get { return _bymlNode["UnitConfigName"]; }
                set { _bymlNode["UnitConfigName"] = value; }
            }
            [Category("Common properties")]
            public Dictionary<string,dynamic> Links
            {
                get { return AllProperties["Links"]; }
                set { AllProperties["Links"] = value; }
            }

            [Category("Common properties")]
            [TypeConverter(typeof(Vector3Converter))]
            public Vector3 position
            {
                get { return new Vector3(_bymlNode["Translate"]["X"], _bymlNode["Translate"]["Y"], _bymlNode["Translate"]["Z"]); }
                set
                {
                    _bymlNode["Translate"]["X"] = value.X;
                    _bymlNode["Translate"]["Y"] = value.Y;
                    _bymlNode["Translate"]["Z"] = value.Z;
                }
            }

            [Category("Common properties")]
            [TypeConverter(typeof(Vector3Converter))]
            public Vector3 rotation
            {
                get { return new Vector3(_bymlNode["Rotate"]["X"], _bymlNode["Rotate"]["Y"], _bymlNode["Rotate"]["Z"]); }
                set
                {
                    _bymlNode["Rotate"]["X"] = value.X;
                    _bymlNode["Rotate"]["Y"] = value.Y;
                    _bymlNode["Rotate"]["Z"] = value.Z;
                }
            }

            [Category("Common properties")]
            [TypeConverter(typeof(Vector3Converter))]
            public Vector3 scale
            {
                get { return new Vector3(_bymlNode["Scale"]["X"], _bymlNode["Scale"]["Y"], _bymlNode["Scale"]["Z"]); }
                set
                {
                    _bymlNode["Scale"]["X"] = value.X;
                    _bymlNode["Scale"]["Y"] = value.Y;
                    _bymlNode["Scale"]["Z"] = value.Z;
                }
            }

            static private Form1 form1
            {
                get { return (Form1)Application.OpenForms[0]; }
            }

            public int priority;
            public List<Vector3> vertices = new List<Vector3>();
            public SmBoundingBox boundingBox;

            private Dictionary<string, dynamic> _bymlNode = null;

            public MapObject(dynamic _obj)
            {
                if (!(_obj is Dictionary<string, dynamic>)) throw new Exception("Game object node not supported");
                _bymlNode = _obj;
            }

            public object Clone()
            {
                MapObject o = new MapObject(CloneDict(_bymlNode));
                o.priority = priority;
                o.vertices = new List<Vector3>();
                foreach (Vector3 v in vertices) o.vertices.Add(v);
                o.boundingBox = boundingBox;
                return o;
            }

            Dictionary<string, dynamic> CloneDict(Dictionary<string, dynamic> dict)
            {
                Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();
                foreach (string k in dict.Keys)
                {
                    if (dict[k] is Dictionary<string, dynamic> && k != "Links") res.Add(k, CloneDict(dict[k])); //Links aren't cloned
                    else if (dict[k] is ICloneable) res.Add(k, ((ICloneable)dict[k]).Clone());
                    else
                    {
                        if (dict[k] != null) System.Diagnostics.Debug.WriteLine("WARNING: CloneDict - " + k + " of type " + ((Type)dict[k].GetType()).ToString() + " is not ICloneable");
                        res.Add(k, dict[k]);
                    }
                }
                return res;
            }

            int SubObjSelectedIndex = -1;
            public void Render(Action<SmModel,Vector3,Vector3,Vector3,bool> render)
            {
                if (!RequiresCustomRendering) throw new Exception("Should not call this method");
                if (unitConfigName == "RouteDokan" || unitConfigName == "RouteDokanLauncher") //Clear pipes rendering
                {
                    int index = 0;
                    foreach (dynamic obj in AllProperties["Links"]["Parts"])
                    {
                        SmModel model;
                        form1.modelDict.TryGetValue(obj["ModelName"], out model);
                        if (model != null)
                        {
                            Vector3 pos = ValuesToVector3(obj["Translate"]);
                            Vector3 rot = ValuesToVector3(obj["Rotate"]);
                            Vector3 scale = ValuesToVector3(obj["Scale"]);
                            render(model, pos, rot, scale, SubObjSelectedIndex == -1 ? true : SubObjSelectedIndex == index);
                        }
                        index++;
                    }

                    if (unitConfigName == "RouteDokanLauncher") //Launcher model itself
                    {
                        SmModel model;
                        form1.modelDict.TryGetValue(unitConfigName, out model);
                        render(model, position, rotation, scale, SubObjSelectedIndex == -1 ? true : false);
                    }

                }
                else throw new Exception("Unsupported object");
            }

            //For raycasting, returns an array of the bounding box of every sub object and (if not null) it's own bounding box
            public SubObjectBoundingBox[] GetSubObjectMeshes()
            {
                if (!RequiresCustomRendering) throw new Exception("Should not call this method");
                List<SubObjectBoundingBox> res = new List<SubObjectBoundingBox>();
                if (unitConfigName == "RouteDokan" || unitConfigName == "RouteDokanLauncher")
                {
                    foreach (dynamic obj in AllProperties["Links"]["Parts"])
                    {
                        SmModel model;
                        form1.modelDict.TryGetValue(obj["ModelName"], out model);
                        SubObjectBoundingBox boundingBox;
                        boundingBox.box = model == null ? null : model.boundingBox;
                        boundingBox.Position = ValuesToVector3(obj["Translate"]);
                        res.Add(boundingBox);
                    }
                }
                else throw new Exception("Unsupported object");
                if (boundingBox != null)
                {
                    SubObjectBoundingBox box;
                    box.box = boundingBox;
                    box.Position = position;
                    res.Add(box);
                }
                return res.ToArray();
            }

            public void Drag(Vector3 v, int mouseX,int mouseY)
            {
                if (!RequiresCustomRendering) throw new Exception("Should not call this method");
                if (SubObjSelectedIndex == -1) //Get selected sub object
                {
                    SubObjSelectedIndex = form1.camera.CastRayToSubObjects(mouseX,mouseY,form1.glControl1.Width,form1.glControl1.Height,form1.projectionMatrix,GetSubObjectMeshes());
                    if (SubObjSelectedIndex == -1) return;
                }

                if (unitConfigName == "RouteDokan" || unitConfigName == "RouteDokanLauncher")
                {
                    if (SubObjSelectedIndex >= ((IList)AllProperties["Links"]["Parts"]).Count) //RouteDokanLauncher has a bounding box, so the SubObjSelectedIndex can be AllProperties["Links"]["Parts"].Count 
                    {
                        if (unitConfigName == "RouteDokan") return; //RouteDokan doesn't have a bounding box, this shouldn't happen, but we handle it
                        else position += v;
                    }
                    else
                    {
                        Vector3 pos = ValuesToVector3(AllProperties["Links"]["Parts"][SubObjSelectedIndex]["Translate"]);
                        pos += v;
                        Vector3ToValues(pos, AllProperties["Links"]["Parts"][SubObjSelectedIndex]["Translate"]);
                    }
                }
                else throw new Exception("Unsupported object");
            }

            public void StopDragging()
            {
                SubObjSelectedIndex = -1;
            }

            public override string ToString()
            {
                return unitConfigName;
            }
        }

        public struct SubObjectBoundingBox
        {
           public SmBoundingBox box;
           public Vector3 Position;
        }
    }
}
