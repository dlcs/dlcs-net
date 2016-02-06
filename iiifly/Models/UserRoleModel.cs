using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iiifly.Models
{
    public class UserRoleModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Affiliation { get; set; }
        public string Email { get; set; }
        public bool CanCallDlcs { get; set; }
        public bool CanApproveUsers { get; set; }
    }

    public class ToggleRoleState
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
        public bool InRole { get; set; }
    }
}
