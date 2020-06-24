using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Research.SEAL;

namespace SEALServer
{
    public static class Utilities
    {
        static public string SerializeSEAL(object srcObject)
        {
            MemoryStream str;
            if (srcObject is Ciphertext)
            {
                str = new MemoryStream();
                ((Ciphertext)srcObject).Save(str);
                byte[] buffer = new byte[str.Length];
                str.Seek(0, SeekOrigin.Begin);
                str.Read(buffer, 0, (int)str.Length);
                return System.Convert.ToBase64String(buffer);
            }
            else if (srcObject is Plaintext)
            {
                str = new MemoryStream();
                ((Plaintext)srcObject).Save(str);
                byte[] buffer = new byte[str.Length];
                str.Seek(0, SeekOrigin.Begin);
                str.Read(buffer, 0, (int)str.Length);
                return System.Convert.ToBase64String(buffer);
            }
            else if (srcObject is EncryptionParameters)
            {
                str = new MemoryStream();
                ((EncryptionParameters)srcObject).Save(str);
                byte[] buffer = new byte[str.Length];
                str.Seek(0, SeekOrigin.Begin);
                str.Read(buffer, 0, (int)str.Length);
                return System.Convert.ToBase64String(buffer);
            }
            return null;
        }
    }
}
