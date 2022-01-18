using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polly.Tests.SomeMethod
{
    public class SomeMethod
    {
        public string[] Do()
        {
            Random random = new Random();
            if (random.Next(1, 4) != 1)
            {
                throw new Exception();
            }
            return new string[] { "asfd", "afd", "afsd" };
        }
    }
}
