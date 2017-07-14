using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.ComponentModel;
using static RedCarpet.PropertyGridTypes;
using System.Collections;

namespace RedCarpet
{
    public class Object 
    {
        public List<MapObject> mobjs = new List<MapObject>();

        public class MapObject 
        {            

            public dynamic this[string v]
            {
                get { return _bymlNode[v];}
                set { _bymlNode[v] = value; }
            }

            [TypeConverter (typeof(DictionaryConverter))]
            public Dictionary<string, dynamic> AllProperties
            {
                get { return _bymlNode; }
                set { _bymlNode = value; }
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

            /*public string Template
            {
                get { return _bymlNode; }
                set { _bymlNode = value; }
            }*/

            public int priority;
            public List<Vector3> vertices = new List<Vector3>();
            public Vector3 bbMin;
            public Vector3 bbMax;

            private Dictionary<string, dynamic> _bymlNode = null;

            public MapObject(dynamic _obj)
            {
                if (!(_obj is Dictionary<string, dynamic>)) throw new Exception("Game object node not supported");
                _bymlNode = _obj;
            }

            public Vector3 calcBBMin()
            {
                Vector3 minTemp = new Vector3();
                List<Vector3> verts = vertices;
                for (int i = 0; i < verts.Count; i++)
                {
                    if (verts[i].X < minTemp.X) minTemp.X = verts[i].X;
                    if (verts[i].Y < minTemp.Y) minTemp.Y = verts[i].Y;
                    if (verts[i].Z < minTemp.Z) minTemp.Z = verts[i].Z;
                }
                bbMin = minTemp;
                return minTemp;
            }

            public Vector3 calcBBMax()
            {
                Vector3 maxTemp = new Vector3();
                List<Vector3> verts = vertices;
                for (int i = 0; i < verts.Count; i++)
                {
                    if (verts[i].X > maxTemp.X) maxTemp.X = verts[i].X;
                    if (verts[i].Y > maxTemp.Y) maxTemp.Y = verts[i].Y;
                    if (verts[i].Z > maxTemp.Z) maxTemp.Z = verts[i].Z;
                }
                bbMax = maxTemp;
                return maxTemp;
            }
        }
    }
}
