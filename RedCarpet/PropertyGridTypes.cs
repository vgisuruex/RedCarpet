using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;

namespace RedCarpet
{
    class PropertyGridTypes
    {
        public class Vector3Converter : System.ComponentModel.TypeConverter
        {            
            public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(string);
            }

            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                string[] tokens = ((string)value).Split(';');
                return new Vector3(float.Parse(tokens[0]), float.Parse(tokens[1]), float.Parse(tokens[2]));
            }

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                Vector3 p = (Vector3)value;
                return p.X + ";" + p.Y + ";" + p.Z;
            }
        }

        public class NullConverter : System.ComponentModel.TypeConverter
        {
            public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
            {
                return true;
            }

            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                return null;
            }

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                return "<Null>";
            }
        }

        public class DictionaryConverter : System.ComponentModel.TypeConverter
        {
            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                ArrayList properties = new ArrayList();
                foreach (string e in ((Dictionary<string, dynamic>)value).Keys)
                {
                    properties.Add(new DictionaryPropertyDescriptor((Dictionary<string, dynamic>)value, e));
                }

                PropertyDescriptor[] props =
                    (PropertyDescriptor[])properties.ToArray(typeof(PropertyDescriptor));

                return new PropertyDescriptorCollection(props);
            }

            public class DictionaryPropertyDescriptor : PropertyDescriptor //TODO Fix JIS encoding
            {
                IDictionary<string, dynamic> _dictionary;
                string _key;

                public override TypeConverter Converter
                {
                    get
                    {
                        if (_dictionary[_key] == null) return new NullConverter();
                        else if (_dictionary[_key] is IDictionary<string, dynamic>) return new DictionaryConverter();
                        else if (_dictionary[_key] is IList<dynamic>) return new ArrayNodeConverter();
                        else return TypeDescriptor.GetConverter(_dictionary[_key]);
                    }
                }

                internal DictionaryPropertyDescriptor(IDictionary<string, dynamic> d, string key)
                    : base(key, null)
                {
                    _dictionary = d;
                    _key = key;
                }

                public override Type PropertyType
                {
                    get { return _dictionary[_key] == null ? null : _dictionary[_key].GetType(); }
                }

                public override void SetValue(object component, object value)
                {
                    _dictionary[_key] = value;
                }

                public override object GetValue(object component)
                {
                    return _dictionary[_key];
                }

                public override bool IsReadOnly
                {
                    get { return false; }
                }

                public override Type ComponentType
                {
                    get { return null; }
                }

                public override bool CanResetValue(object component)
                {
                    return false;
                }

                public override void ResetValue(object component)
                {
                }

                public override bool ShouldSerializeValue(object component)
                {
                    return false;
                }
            }

        }

        public class ArrayNodeConverter : System.ComponentModel.TypeConverter
        {
            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                ArrayList properties = new ArrayList();
                for (int i = 0; i < ((List<dynamic>)value).Count; i++)
                {
                    properties.Add(new ArrayPropertyDescriptor(((List<dynamic>)value)[i], "Item " + i.ToString() + " :"));
                }

                PropertyDescriptor[] props =
                    (PropertyDescriptor[])properties.ToArray(typeof(PropertyDescriptor));

                return new PropertyDescriptorCollection(props);
            }

            public class ArrayPropertyDescriptor : PropertyDescriptor //TODO Fix JIS encoding
            {
                dynamic _obj;
                string _key;

                public override TypeConverter Converter
                {
                    get
                    {
                        if (_obj == null) return new NullConverter();
                        else if (_obj is IDictionary<string, dynamic>) return new DictionaryConverter();
                        else if (_obj is IList<dynamic>) return new ArrayNodeConverter();
                        else return TypeDescriptor.GetConverter(_obj);
                    }
                }

                internal ArrayPropertyDescriptor(dynamic obj, string key)
                    : base(key, null)
                {
                    _obj = obj;
                    _key = key;
                }

                public override Type PropertyType
                {
                    get { return _obj == null ? null : _obj.GetType(); }
                }

                public override void SetValue(object component, object value)
                {
                    _obj = value;
                }

                public override object GetValue(object component)
                {
                    return _obj;
                }

                public override bool IsReadOnly
                {
                    get { return false; }
                }

                public override Type ComponentType
                {
                    get { return null; }
                }

                public override bool CanResetValue(object component)
                {
                    return false;
                }

                public override void ResetValue(object component)
                {
                }

                public override bool ShouldSerializeValue(object component)
                {
                    return false;
                }
            }

        }

    }


}
