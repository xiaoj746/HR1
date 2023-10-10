﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class config_file_third_kind
    {
        //[ftk_id], [first_kind_id], [first_kind_name],
        //[second_kind_id], [second_kind_name], [third_kind_id],
        //[third_kind_name], [third_kind_sale_id], [third_kind_is_retail]
        public int ftk_id { get; set; }

        [Required(ErrorMessage = "I级机构名称不能为空")]

        public string first_kind_id { get; set; }

        public string first_kind_name { get; set; }
        [Required(ErrorMessage = "II级机构名称不能为空")]

        public string second_kind_id { get; set; }

        public string second_kind_name { get; set; }

        public string third_kind_id { get; set; }

        [Required(ErrorMessage = "III级机构名称不能为空")]
        public string third_kind_name { get; set; }

        public string third_kind_is_retail { get; set; }

        [Required(ErrorMessage = "销售责任人编号")]

        public string third_kind_sale_id { get; set; }

    }
}
