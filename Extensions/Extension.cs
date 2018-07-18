using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TestingNNEvolution.AI.NeuralNetwork;

namespace TestingNNEvolution.Extensions
{
    public static class Extension
    {
        public static GraphicsDevice Device;
        public static Random Random = new Random();


        public static Texture2D GetColor(Color color)
        {
            Texture2D t = new Texture2D(Device, 1, 1);
            t.SetData<Color>(new Color[] { color });
            return t;
        }


        public static byte[] SerializeToByteArray(this object objectData)
        {
            byte[] bytes;
            using (var _MemoryStream = new MemoryStream())
            {
                IFormatter _BinaryFormatter = new BinaryFormatter();
                _BinaryFormatter.Serialize(_MemoryStream, objectData);
                bytes = _MemoryStream.ToArray();
            }
            return bytes;
        }
        public static dynamic DeserializeToDynamicType(this byte[] byteArray)
        {
            using (var _MemoryStream = new MemoryStream(byteArray))
            {
                IFormatter _BinaryFormatter = new BinaryFormatter();
                var ReturnValue = _BinaryFormatter.Deserialize(_MemoryStream);
                return ReturnValue;
            }
        }
        public static dynamic DeepCopy(this object objectData)
        {
            return (objectData.SerializeToByteArray()).DeserializeToDynamicType();
        }


        public static double GetDouble10Bit(string value)
        {
            char[] reversedArray = value.ToCharArray();
            bool signed = reversedArray[0] == '0';

            Array.Reverse(reversedArray);

            double result = .0d;
            for (int i = 0; i < reversedArray.Length - 1; i++)
            {
                int num = int.Parse(reversedArray[i] + "");
                result += num * Math.Pow(2, i);
            }

            return signed ? result * -1.0 : result;

        }
        public static string GetBinary10Bit(double value)
        {
            string fullBin = "";

            int num = (int)value;
            for (int i = 0; i < 10; i++)
            {
                if (num % 2 == 0)
                    fullBin += "0";
                else fullBin += '1';

                num /= 2;
            }

            if (value < 0)
                fullBin += "0";
            else fullBin += "1";

            char[] reversedArray = fullBin.ToCharArray();
            Array.Reverse(reversedArray);

            return new string(reversedArray);
        }

        public static string[] GetParts(this string input, int length)
        {
            List<string> _Parts = new List<string>();
            int _OffSet = 0;

            for (int i = 0; i < input.Length; i += length)
            {
                if (i + length >= input.Length)
                    _Parts.Add(input.Substring(_OffSet, input.Length - _OffSet));
                else _Parts.Add(input.Substring(_OffSet, length));

                _OffSet += length;
            }

            return _Parts.ToArray();

        }

        public static int ToRGB(this Color color)
        {
            return (color.R << 16) | (color.G << 8) | color.B;
        }
    }
}
