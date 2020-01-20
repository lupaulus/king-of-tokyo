
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ServeurKoT.Controleur{
    public class Utilitaire {

        public Utilitaire() {
        }

        public static byte[] Serializer(object _object)
        {
            byte[] bytes;
            using (var _MemoryStream = new MemoryStream())
            {
                IFormatter _BinaryFormatter = new BinaryFormatter();
                _BinaryFormatter.Serialize(_MemoryStream, _object);
                bytes = _MemoryStream.ToArray();
            }
            return bytes;
        }

        public static T Deserializer<T>(byte[] _byteArray)
        {
            T ReturnValue;
            using (var _MemoryStream = new MemoryStream(_byteArray))
            {
                IFormatter _BinaryFormatter = new BinaryFormatter();
                ReturnValue = (T)_BinaryFormatter.Deserialize(_MemoryStream);
            }
            return ReturnValue;
        }
    }
}