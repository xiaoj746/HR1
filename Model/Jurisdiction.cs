using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Jurisdiction
    {
        //[JuriID], [JurName], [GroupID], [JurAddress], [Pid]
        public int JuriID { get; set; }
        public string JurName { get; set; }
        public int GroupID { get; set; }
        public string JurAddress { get; set; }
        public int Pid { get; set; }
    }
}
