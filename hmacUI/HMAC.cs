using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace hmacUI
{
    

    internal class HMAC
    {
        static byte[] itterativeXor(byte[] array, byte x)
        {
            byte[] outp = new byte[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                outp[i] = (byte)((int)array[i] ^ (int)x);

            }

            return outp;
        }
        public static string ComputeHMAC(string preferedAlgorithm, string input, string key)
        {
            byte[] data = Encoding.UTF8.GetBytes(input);
            byte[] inkey = Encoding.UTF8.GetBytes(key);
            HashAlgorithm hashAlgorithm = HashAlgorithm.Create(preferedAlgorithm);
            ushort blockSize = 64;
            //adjust key length to blocksize
            if (preferedAlgorithm == "SHA384" || preferedAlgorithm == "SHA512")
            {
                blockSize = 128;
            }
            
            //key padding with zeros
            byte[] padkey = new byte[blockSize];
            if (inkey.Length > blockSize)
            {
                padkey = hashAlgorithm.ComputeHash(inkey);
            }
            else
            {
                for (int i = 0; i < inkey.Length; i++)
                {
                    padkey[i] = inkey[i];
                }
            }

            //2 keys derived from initial key by using xor
            byte[] o_key = itterativeXor(padkey, 0x5c);
            byte[] i_key = itterativeXor(padkey, 0x36);

            //inner 
            //hash(data + inner key) 
            byte[] hash = hashAlgorithm.ComputeHash(i_key.Concat(data).ToArray());

            //outer
            //hash of outer is hash(data, hashinner)
            hash = hashAlgorithm.ComputeHash(o_key.Concat(hash).ToArray());

            hashAlgorithm.Dispose();
            //hash bytes in hex
            return BitConverter.ToString(hash).Replace("-", "").ToLower();



            
        }




    }
}
