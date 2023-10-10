using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Config_major_cader
    {
        public string value { get; set; }

        public string label { get; set; }


        public List<Config_major_cader> children { get; set; }
    }
}
