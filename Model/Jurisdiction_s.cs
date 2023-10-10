using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Jurisdiction_s
    {
        public int id { get; set; }

        public string Name { get; set; }
        public int groupID_s { get; set; }
        public string url { get; set; }
        public List<Jurisdiction_s> children { get; set; }
    }
}
