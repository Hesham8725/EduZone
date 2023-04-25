using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public static class RandomPasswordCode
    {
        static string code;
        static RandomPasswordCode()
        {
            code = "";
        }
        public static string GetCode()
        {
            Random random = new Random();

            for(int i = 0; i < 8; i++)
            {
               int value = random.Next(0, 10);
                code += value.ToString();
            }
            return code;
        }
    }
}