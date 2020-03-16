using MvcAuthenication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DVG.SSO.Models
{
    public class SystemUserLoginItemExtend : SystemUserLoginItem
    {
        public string SSOType { get; set; }
        public string SecretKey { get; set; } //key để check khi login sd SSO VN
        [Required(ErrorMessage = "Yêu cầu nhập!")]
        public string OTP { get; set; }
    }
}