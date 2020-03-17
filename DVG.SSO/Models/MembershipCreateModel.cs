using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DVG.SSO.Models
{
    public class MembershipCreateModel
    {
        [Required(ErrorMessage = "Yêu cầu nhập!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập!")]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập!")]
        public string Email { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public string Comment { get; set; }
    }
}