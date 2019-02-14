using Aeon.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace Pipeline
{
    /// <summary>
    /// A helper for serialization.
    /// </summary>
    public static class SerializationHelper
    {
        public static void GetParameters(string key, XmlDocument input, out AeonKeyValuePair<string, ParameterCollection> kvp)
        {
            string id = "";
            int paramCount = 0;

            foreach (XmlNode node in input.GetElementsByTagName(key)[0].ChildNodes)
                switch (node.Name)
                {
                    case "ID":
                        id = node.InnerText;
                        break;

                    case "Parameters":
                        int.TryParse(node.InnerText, out paramCount);
                        break;
                }

            ParameterCollection parameters = new ParameterCollection();

            if (paramCount > 0)
                for (int i = 0; i < paramCount; i++)
                {
                    foreach (XmlNode node in input.GetElementsByTagName(key + "Param" + i)[0].ChildNodes)
                    {
                        string text = "";

                        switch (node.Name)
                        {
                            case "Value":
                                text = node.InnerText;
                                break;
                        }

                        object obj = null;

                        GetTypeValue(text, out obj);
                        parameters.Add(obj);
                    }
                }

            kvp = new AeonKeyValuePair<string, ParameterCollection>();

            kvp.Key = id;

            if (paramCount > 0)
                kvp.Value = parameters;
            else
                kvp.Value = null;
        }

        public static void GetTypeValue(string text, out object output)
        {
            string[] values = text.Split(new[]
			{
				'_', ' '
			}, StringSplitOptions.RemoveEmptyEntries);

            output = null;

            if (values.Length == 2)
            {
                Type t = Type.GetType(values[0]);

                if (t != null)
                    output = GetValueFromType(t, values[1]);
                else
                    switch (values[0])
                    {
                        case "ADS2":
                            {
                                AeonDictionary<string, string> dict =
                                    new AeonDictionary<string, string>();

                                string[] keys = text.Split(new[]
                                {
                                    ',', ' '
                                }, StringSplitOptions.RemoveEmptyEntries);



                                int count = 0;
                                int.TryParse(keys[1], out count);

                                int range = (count - 2) / 2;

                                if (range > 0)
                                    for (int i = 0; i < range; i++)
                                        dict.Add(keys[i + 2], keys[i + 1 + range]);

                                output = dict;
                            }
                            break;

                        case "TimeSpan":
                            {
                                TimeSpan time = TimeSpan.Zero;
                                GetTimeSpan(values[1], out time);
                                output = time;
                            }
                            break;

                        case "Rectangle":
                            {
                                Rectangle rect = new Rectangle();
                                GetRectangle(values[1], out rect);
                                output = rect;
                            }
                            break;

                        case "Colour":
                            {
                                Color colour = new Color();
                                GetColour(values[1], out colour);
                                output = colour;
                            }
                            break;

                        case "Vector2":
                            {
                                Vector2 vec = Vector2.Zero;
                                GetVector2(values[1], out vec);
                                output = vec;
                            }
                            break;

                        case "Vector3":
                            {
                                Vector3 vec = Vector3.Zero;
                                GetVector3(values[1], out vec);
                                output = vec;
                            }
                            break;

                        case "Vector4":
                            {
                                Color col = Color.White;
                                GetColour(values[1], out col);

                                output = col.ToVector4();
                            }
                            break;
                    }
            }
        }

        public static object GetValueFromType(Type type, string valueText)
        {
            if (type == typeof(TimeSpan))
            {
                TimeSpan time = TimeSpan.Zero;
                GetTimeSpan(valueText, out time);

                return time;
            }
            else if (type == typeof(int))
            {
                int i = 0;
                int.TryParse(valueText, out i);

                return i;
            }
            else if (type == typeof(float))
            {
                float f = 0;
                float.TryParse(valueText, out f);

                return f;
            }
            else if (type == typeof(string))
                return valueText;
            else
                return null;
        }

        public static XmlDocument LoadXmlFile(string filename, ContentImporterContext context)
        {
            XmlDocument file = new XmlDocument();

            try
            {
                file.Load(filename);
            }
            catch (Exception e)
            {
                context.Logger.LogImportantMessage
                    ("The file " + filename + " is not valid: " + e.Message);

                throw e;
            }

            return file;
        }

        public static void Get2DArray(string text, out List<List<int>> tiles)
        {
            string[] values = text.Split(new[]
			{
				'_', ' '
			}, StringSplitOptions.RemoveEmptyEntries);

            tiles = new List<List<int>>();

            for (int y = 0; y < values.Length; y++)
            {
                tiles.Add(new List<int>());

                string[] temp = values[y].Split(new[]
                        {
                            ',', ' '
                        }, StringSplitOptions.RemoveEmptyEntries);

                for (int x = 0; x < temp.Length; x++)
                {
                    int val = 0;
                    int.TryParse(temp[x], out val);
                    tiles[y].Add(val);
                }
            }

            tiles.Reverse();
        }

        /// <summary>
        /// Finds an enum value from a string.
        /// </summary>
        /// <typeparam name="T">The type of enum.</typeparam>
        /// <param name="text">The text to be converted.</param>
        /// <returns>The value found</returns>
        public static T GetEnumValue<T>(string text)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), text);
            }
            catch
            {
                throw new ArgumentException("Invalid type given as an enum.");
            }
        }

        /// <summary>
        /// Returns a Rectangle from a string.
        /// </summary>
        /// <param name="text">The string to get the infomation from.</param>
        /// <param name="rect">The Rectangle to be set.</param>
        public static void GetRectangle(string text, out Rectangle rect)
        {
            string[] values = Extract(text);

            if (values.Length == 4)
                rect = new Rectangle(Convert.ToInt32(values[0]),
                    Convert.ToInt32(values[1]), Convert.ToInt32(values[2]),
                    Convert.ToInt32(values[3]));
            else
                rect = Rectangle.Empty;
        }

        /// <summary>
        /// Returns a TimeSpan from a string.
        /// </summary>
        /// <param name="text">The string to get the infomation from.</param>
        /// <param name="time">The TimeSpan to use.</param>
        public static void GetTimeSpan(string text, out TimeSpan time)
        {
            string[] values = Extract(text);

            switch (values.Length)
            {
                case 2:
                    time = new TimeSpan(0, Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
                    break;
                case 3:
                    time = new TimeSpan(Convert.ToInt32(values[0]),
                        Convert.ToInt32(values[1]), Convert.ToInt32(values[2]));
                    break;
                case 4:
                    time = new TimeSpan(Convert.ToInt32(values[0]),
                        Convert.ToInt32(values[1]), Convert.ToInt32(values[2]),
                        Convert.ToInt32(values[3]));
                    break;
                default:
                    time = TimeSpan.FromMilliseconds(Convert.ToDouble(values[0]));
                    break;
            }
        }

        /// <summary>
        /// Returns a Matrix from a string.
        /// </summary>
        /// <param name="text">The string to get the infomation from.</param>
        /// <param name="rect">The Matrix to be set.</param>
        public static void GetMatrix(string text, out Matrix matrix)
        {
            string[] values = Extract(text);

            if (values.Length == 16)
                matrix = new Matrix(Convert.ToSingle(values[0]),
                    Convert.ToSingle(values[1]),
                    Convert.ToSingle(values[2]),
                    Convert.ToSingle(values[3]),
                    Convert.ToSingle(values[4]),
                    Convert.ToSingle(values[5]),
                    Convert.ToSingle(values[6]),
                    Convert.ToSingle(values[7]),
                    Convert.ToSingle(values[8]),
                    Convert.ToSingle(values[9]),
                    Convert.ToSingle(values[10]),
                    Convert.ToSingle(values[11]),
                    Convert.ToSingle(values[12]),
                    Convert.ToSingle(values[13]),
                    Convert.ToSingle(values[14]),
                    Convert.ToSingle(values[15]));
            else
                matrix = Matrix.Identity;
        }

        /// <summary>
        /// Returns a Vector2 from a given string.
        /// </summary>
        /// <param name="text">The text to get the values from.</param>
        /// <param name="vec">The generated Vector2.</param>
        public static void GetVector2(string text, out Vector2 vec)
        {
            string[] values = Extract(text);

            if (values.Length == 2)
                vec = new Vector2(Convert.ToSingle(values[0]),
                    Convert.ToSingle(values[1]));
            else
                vec = Vector2.Zero;
        }

        /// <summary>
        /// Returns a Vector3 from a given string.
        /// </summary>
        /// <param name="text">The text to get the values from.</param>
        /// <param name="vec">The generated Vector3.</param>
        public static void GetVector3(string text, out Vector3 vec)
        {
            string[] values = Extract(text);

            if (values.Length == 3)
                vec = new Vector3(Convert.ToSingle(values[0]),
                    Convert.ToSingle(values[1]), Convert.ToSingle(values[2]));
            else
                vec = Vector3.Zero;
        }

        /// <summary>
        /// Returns a Vector4 from a given string.
        /// </summary>
        /// <param name="text">The text to get the values from.</param>
        /// <param name="vec">The generated Vector4.</param>
        public static void GetVector4(string text, out Vector4 vec)
        {
            string[] values = Extract(text);

            if (values.Length == 4)
                vec = new Vector4(Convert.ToSingle(values[0]),
                    Convert.ToSingle(values[1]), Convert.ToSingle(values[2]),
                    Convert.ToSingle(values[3]));
            else
                vec = Vector4.Zero;
        }

        /// <summary>
        /// Returns a Color from a given string.
        /// </summary>
        /// <param name="text">The text to get the values from.</param>
        /// <param name="colour">The generated Color.</param>
        public static void GetColour(string text, out Color colour)
        {
            string[] values = Extract(text);

            if (values.Length == 4)
                colour = new Color(Convert.ToByte(values[0]),
                    Convert.ToByte(values[1]),
                    Convert.ToByte(values[2]),
                    Convert.ToByte(values[3]));
            else if (values.Length == 3)
                colour = new Color(Convert.ToByte(values[0]),
                    Convert.ToByte(values[1]),
                    Convert.ToByte(values[2]));
            else
                colour = Color.White;
        }

        public static string[] Extract(string text)
        {
            text = text.Replace("(", "");
            text = text.Replace(")", "");
            text = text.Replace("{", "");
            text = text.Replace("}", "");

            string[] values = text.Split(new[]
			{
				',', ' '
			},
                StringSplitOptions.RemoveEmptyEntries);

            return values;
        }

        /// <summary>
        /// Converts a string to a number.
        /// </summary>
        /// <param name="text">The text to be converted.</param>
        /// <param name="value">The generated number.</param>
        public static void StringToNumber(string text, out int value)
        {
            value = Convert.ToInt32(text, CultureInfo.InvariantCulture);
        }
    }
}
