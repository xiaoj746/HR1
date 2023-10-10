using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class salary_grant_detailsXJ
    {
        //[grd_id], [salary_grant_id], [human_id], [human_name], [bouns_sum], [sale_sum], [deduct_sum], [salary_standard_sum], [salary_paid_sum]
        public int grd_id { get; set; }
        public string salary_grant_id { get; set; }
        public string human_id { get; set; }
        public string human_name { get; set; }
        public string bouns_sum { get; set; }
        public string sale_sum { get; set; }
        public string deduct_sum { get; set; }
        public string salary_standard_sum_sgd { get; set; }
        public string salary_paid_sum_sgd { get; set; }
    }
}
