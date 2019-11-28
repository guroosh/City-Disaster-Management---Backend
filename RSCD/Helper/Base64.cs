using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSCD.Helper
{
    public class Base64
    {
        public string Encode(string plainText)
        {
            byte[] temp = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(temp);
        }

        public string Decode(string encodedData)
        {
            byte[] temp = Convert.FromBase64String(encodedData);
            return Encoding.UTF8.GetString(temp);
        }
    }
}
