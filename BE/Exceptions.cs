using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Exceptions : Exception//טיפול בחריגות
    {
        public string message;
        public long key;
        public string name;
        public Exceptions(string s, long k, string n)
        {
            message = s;
            key = k;
            name = n;
        }
    }

}
