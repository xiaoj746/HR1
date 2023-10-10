using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class config_file_third_kind_cader
    {
        public string value { get; set; }

        public string label { get; set; }

        public string value2 { get; set; }

        public string label2 { get; set; }

        public List<config_file_second_kind_cader> children { get; set; }
    }
}
