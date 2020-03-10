using SSO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVG.SSO.Ultility
{
    public class TypeData
    {

        public static System.Collections.Generic.List<ComboboxStringItem> lstCulture = new System.Collections.Generic.List<ComboboxStringItem>() {
        new ComboboxStringItem (){ID = "vi-VN", Name = "Tiếng Việt"},
        new ComboboxStringItem (){ID = "en-US", Name = "Tiếng Anh"}
       };
    }
}