using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Authentication
{
    public class CaptchaValid : ValidationAttribute
    {
        protected override ValidationResult IsValid(
          object value,
          ValidationContext validationContext)
        {
            return value != null && (HttpContext.Current.Session["Captcha" + validationContext.DisplayName] == null || HttpContext.Current.Session["Captcha" + validationContext.DisplayName].ToString() != value.ToString()) ? new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName)) : ValidationResult.Success;
        }
    }
}
