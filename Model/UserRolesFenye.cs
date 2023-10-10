using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserRolesFenye
    {
        //[UserRolesID], [UserID], [RolesID], [Expr1], [rolesName], [rolesInstruction], [rolesIf], [u_id], [u_name], [u_true_name], [u_password]
        public int UserRolesID { get; set; }
        public int UserID { get; set; }
        public int RolesID { get; set; }

        public string rolesName { get; set; }
        public string rolesInstruction { get; set; }
        public string rolesIf { get; set; }
        public int u_id { get; set; }
        public string u_name { get; set; }
        public string u_true_name { get; set; }
        public string u_password { get; set; }
    }
}
