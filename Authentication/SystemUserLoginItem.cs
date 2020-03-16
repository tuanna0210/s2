using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public class SystemUserLoginItem : IUserLogin
    {
        private bool remember = true;

        [Required(ErrorMessage = "Yêu cầu nhập!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập!")]
        [UIHint("stringPassword")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Languague { get; set; }

        [Display(Name = "Nhớ tài khoản")]
        [DefaultValue(true)]
        public bool Remember
        {
            get
            {
                return this.remember;
            }
            set
            {
                this.remember = value;
            }
        }

        [Required(ErrorMessage = "Yêu cầu nhập!")]
        [CaptchaValid(ErrorMessage = "Giá trị nhập không đúng!")] //temp
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [Display(Name = "Captcha")]
        public string Captcha { get; set; }

        public string Provider { get; set; }

        public string ReturnUrl { get; set; }
    }
}
