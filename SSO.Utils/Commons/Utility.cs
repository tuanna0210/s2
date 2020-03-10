using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSO.Utils
{
    public static class Common
    {
        public static string DateFomat(DateTime dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" <label class=\"date\">29</label>", dt.Day);
            switch (dt.Month)
            {
                case 1:
                    sb.AppendFormat("<label class=\"month\">Th một</label>");
                    break;
                case 2:
                    sb.AppendFormat("<label class=\"month\">Th hai</label>");
                    break;
                case 3:
                    sb.AppendFormat("<label class=\"month\">Th ba</label>");
                    break;
                case 4:
                    sb.AppendFormat("<label class=\"month\">Th tư</label>");
                    break;
                case 5:
                    sb.AppendFormat("<label class=\"month\">Th năm</label>");
                    break;
                case 6:
                    sb.AppendFormat("<label class=\"month\">Th sáu</label>");
                    break;
                case 7:
                    sb.AppendFormat("<label class=\"month\">Th bảy</label>");
                    break;
                case 8:
                    sb.AppendFormat("<label class=\"month\">Th tám</label>");
                    break;
                case 9:
                    sb.AppendFormat("<label class=\"month\">Th chín</label>");
                    break;
                case 10:
                    sb.AppendFormat("<label class=\"month\">Th mười</label>");
                    break;
                case 11:
                    sb.AppendFormat("<label class=\"month\">Th mười một</label>");
                    break;
                default:
                    sb.AppendFormat("<label class=\"month\">Th mười hai</label>");
                    break;
            }

            return sb.ToString();
        }
        //temp
        //public static string SubstringWithdot(string input, int length)
        //{
        //    try
        //    {
        //        if (input.Length > length)
        //            return input.Substring(0, length) + "...";
        //        else return input;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogMan.Instance.WriteErrorToLog(ex);
        //    }
        //    return string.Empty;
        //}

        public static int GetNumber(string input)
        {
            try
            {
                return Convert.ToInt32(Regex.Replace(input, "\\D", string.Empty));
            }
            catch (Exception ex)
            {
                LogMan.Instance.WriteErrorToLog(ex);
                return 0;
            }
        }

        /// <summary>
        /// Chuyển danh sách từ xâu sang mảng int.
        /// Các mảng int có định dạng được ngăn cách nhau bằng dấu ,
        /// VD: 1,2,3,4
        /// </summary>
        /// <param name="LtsSourceValues">Xâu chứa mảng int</param>
        /// <returns>Mảng int</returns>
        /// <modified>
        /// Author				    created date					comments
        /// DuyNV					04/07/2013				        Tạo mới
        ///</modified>
        /////temp
        //public static List<int> String2ListInt(string LtsSourceValues)
        //{
        //    List<int> LtsValues = new List<int>();
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(LtsSourceValues))
        //        {
        //            if (LtsSourceValues.Contains(","))
        //                LtsValues = LtsSourceValues.Trim(',').Split(',').Select(o => Convert.ToInt32(o)).ToList();
        //            else
        //                LtsValues.Add(Convert.ToInt32(LtsSourceValues));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogMan.Instance.WriteErrorToLog(ex);
        //    }
        //    return LtsValues;
        //}

        /// <summary>
        /// Đọc 1 file trên file vật lý
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <modified>
        /// Author				    created date					comments
        /// DuyNV					04/07/2013				        Tạo mới
        ///</modified>

        //temp    
        //public static byte[] ReadFile(string filePath)
        //{
        //    byte[] buffer;
        //    FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        //    try
        //    {
        //        int length = (int)fileStream.Length;  // get file length
        //        buffer = new byte[length];            // create buffer
        //        int count;                            // actual number of bytes read
        //        int sum = 0;                          // total number of bytes read

        //        // read until Read method returns 0 (end of the stream has been reached)
        //        while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
        //            sum += count;  // sum is a buffer offset for next reading
        //    }
        //    finally
        //    {
        //        fileStream.Close();
        //    }
        //    return buffer;
        //}
        public static string PagerPublish(string Controler, int CatId, int RowPerPage, int CurrentPage, int totalrecod)
        {
            StringBuilder sb = new StringBuilder();
            int totalpage = totalrecod % RowPerPage == 0 ? totalrecod / RowPerPage : totalrecod / RowPerPage + 1;
            sb.Append("<ul class=\"page-numbers\">");
            //sb.AppendFormat("<a class=\" btn_small ui-state-default ui-corner-all tooltip\" title=\"First Page\" {0}>", GoPage(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, 1, Keyword, SearchIn));

            sb.AppendFormat("<li><a href=\"#\">Prev</a></li>");

            //sb.Append("<span class=\"ui-icon ui-icon-arrowthickstop-1-w\"></span>");
            //sb.Append("</a>");
            //sb.AppendFormat("<a class=\" btn_small ui-state-default ui-corner-all tooltip\" title=\"Previous Page\"  {0}>", GoPage(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, CurrentPage - 1 > 0 ? CurrentPage - 1 : 1, Keyword, SearchIn));
            //sb.Append("<span class=\"ui-icon ui-icon-circle-arrow-w\"></span>");
            //sb.Append("</a>");
            //sb.AppendFormat("<select class=\"pagesize\" {0}>", GoPageChange(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, "this.value", Keyword, SearchIn));
            for (int i = 1; i <= totalpage; i++)
            {
                if (i == CurrentPage)
                {
                    sb.AppendFormat("<li><a href=\"{0}\" class=\"current\">{1}</a></li>", "#", i);
                }
                else
                {
                    sb.AppendFormat("<li><a href=\"{0}\">{1}</a></li>", "#", i);
                }
            }
            //sb.Append(" </select>");
            //sb.AppendFormat("<a class=\"btn_small ui-state-default ui-corner-all tooltip\" title=\"Next Page\" {0}>", GoPage(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, CurrentPage + 1 <= totalpage ? CurrentPage + 1 : totalpage, Keyword, SearchIn));
            //sb.Append("<span class=\"ui-icon ui-icon-circle-arrow-e\"></span>");
            //sb.Append("</a>");
            //sb.AppendFormat("<a class=\" btn_small ui-state-default ui-corner-all tooltip\" title=\"Last Page\" {0}>", GoPage(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, totalpage, Keyword, SearchIn));
            //sb.Append("<span class=\"ui-icon ui-icon-arrowthickstop-1-e\"></span>");
            //sb.Append("</a>");
            sb.AppendFormat("<li><a href=\"#\">Next</a></li>");
            sb.Append("</ul>");





            return sb.ToString();
        }
        /// <summary>
        /// Hàm phân trang
        /// </summary>
        /// <param name="Controler"></param>
        /// <param name="CatId"></param>
        /// <param name="Status"></param>
        /// <param name="FieldSort"></param>
        /// <param name="FieldOption"></param>
        /// <param name="RowPerPage"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="Keyword"></param>
        /// <param name="SearchIn"></param>
        /// <param name="totalrecod"></param>
        /// <returns></returns>
        public static string Pager(string Controler, int CatId, int Status, string FieldSort, bool FieldOption, int RowPerPage, int CurrentPage, string Keyword, string SearchIn, int totalrecod)
        {
            StringBuilder sb = new StringBuilder();
            int totalpage = totalrecod % RowPerPage == 0 ? totalrecod / RowPerPage : totalrecod / RowPerPage + 1;
            //sb.Append("<div class=\"float-right\">");
            //sb.Append("<div id=\"pager\">");
            //sb.AppendFormat("<a class=\" btn_small ui-state-default ui-corner-all tooltip\" title=\"First Page\" {0}>", GoPage(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, 1, Keyword, SearchIn));
            //sb.Append("<span class=\"ui-icon ui-icon-arrowthickstop-1-w\"></span>");
            //sb.Append("</a>");
            //sb.AppendFormat("<a class=\" btn_small ui-state-default ui-corner-all tooltip\" title=\"Previous Page\"  {0}>", GoPage(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, CurrentPage - 1 > 0 ? CurrentPage - 1 : 1, Keyword, SearchIn));
            //sb.Append("<span class=\"ui-icon ui-icon-circle-arrow-w\"></span>");
            //sb.Append("</a>");
            //sb.AppendFormat("<select class=\"pagesize\" {0}>", GoPageChange(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, "this.value", Keyword, SearchIn));
            //for (int j = 1; j <= totalpage; j++)
            //{
            //    if (j == CurrentPage)
            //    {
            //        sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">Page {0}</option>", j);
            //    }
            //    else
            //    {
            //        sb.AppendFormat("<option value=\"{0}\" >Page {0}</option>", j);
            //    }
            //}
            //sb.Append(" </select>");
            //sb.AppendFormat("<a class=\"btn_small ui-state-default ui-corner-all tooltip\" title=\"Next Page\" {0}>", GoPage(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, CurrentPage + 1 <= totalpage ? CurrentPage + 1 : totalpage, Keyword, SearchIn));
            //sb.Append("<span class=\"ui-icon ui-icon-circle-arrow-e\"></span>");
            //sb.Append("</a>");
            //sb.AppendFormat("<a class=\" btn_small ui-state-default ui-corner-all tooltip\" title=\"Last Page\" {0}>", GoPage(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, totalpage, Keyword, SearchIn));
            //sb.Append("<span class=\"ui-icon ui-icon-arrowthickstop-1-e\"></span>");
            //sb.Append("</a>");
            //sb.Append("</div>");
            //sb.Append("</div>");

            sb.Append("<div class=\"datatable-footer\">");
            sb.Append("<div class=\"dataTables_paginate paging_full_numbers\" id=\"data-table_paginate\">");

            if (CurrentPage > 1)
            {
                sb.AppendFormat("<a class=\"first paginate_button \" tabindex=\"0\" id=\"data-table_first\" {0}>First</a>", GoPage(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, 1, Keyword, SearchIn));
                sb.AppendFormat("<a class=\"previous paginate_button \" tabindex=\"0\" id=\"data-table_previous\" {0}>&lt;</a>", GoPage(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, CurrentPage - 1, Keyword, SearchIn));
            }
            else
            {
                sb.AppendFormat("<a class=\"first paginate_button paginate_button_disabled\" tabindex=\"0\" id=\"data-table_first\" {0}>First</a>", GoPage(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, 0, Keyword, SearchIn));
                sb.Append("<a class=\"previous paginate_button paginate_button_disabled\" tabindex=\"0\" id=\"data-table_previous\">&lt;</a>");
            }
            sb.Append(" <span>");
            sb.AppendFormat("<select class=\"\" name=\"select\" {0}>", GoPageChange(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, "this.value", Keyword, SearchIn));
            for (int j = 1; j <= totalpage; j++)
                if (j == CurrentPage)
                    sb.AppendFormat("<option value=\"{0}\" selected>Trang {0}</option>", j);
                else
                    sb.AppendFormat("<option value=\"{0}\">Trang {0}</option>", j); ;
            sb.Append("</select>");
            sb.Append("</span>");
            if (CurrentPage < totalpage)
            {
                sb.AppendFormat("<a class=\"next paginate_button\" tabindex=\"0\" id=\"data-table_next\" {0}>&gt;</a>", GoPage(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, CurrentPage + 1, Keyword, SearchIn));
                sb.AppendFormat("<a class=\"last paginate_button\" tabindex=\"0\" id=\"data-table_last\" {0}>Last</a></div></div>", GoPage(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, totalpage, Keyword, SearchIn));
            }
            else
            {
                sb.AppendFormat("<a class=\"next paginate_button paginate_button_disabled\" tabindex=\"0\" id=\"data-table_next\" {0}>&gt;</a>", GoPage(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, CurrentPage + 1, Keyword, SearchIn));
                sb.AppendFormat("<a class=\"last paginate_button paginate_button_disabled\" tabindex=\"0\" id=\"data-table_last\" {0}>Last</a></div></div>", GoPage(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, totalpage, Keyword, SearchIn));
            }
            return sb.ToString();
        }

        private static string GoPage(string Controler, int CatId, int Status, string FieldSort, bool FieldOption, int RowPerPage, int CurrentPage, string Keyword, string SearchIn)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("onclick=\"GoPage('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}');\"", Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, CurrentPage, Keyword, SearchIn);
            return sb.ToString();
        }

        private static string GoPageChange(string Controler, int CatId, int Status, string FieldSort, bool FieldOption, int RowPerPage, string CurrentPage, string Keyword, string SearchIn)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("onchange=\"GoPage('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}');\"", Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, CurrentPage, Keyword, SearchIn);
            return sb.ToString();
        }

        public static string DropdowlistSearch(int tabindex, string[] Field, string selected)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<select tabindex=\"3\" class=\"field text small\" name=\"ddlSearch\" id=\"ddlSearch\">");
            foreach (var item in Field)
            {
                if (item.Equals(selected))
                    sb.AppendFormat("<option value=\"{0}\" selected>{1}</option>", item, item);
                else
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", item, item);
            }
            sb.Append("</select>");
            return sb.ToString();
        }

        /// <summary>
        /// Chuyển từ kích thước file sang định dạng chuẩn bao gồm:
        /// Bytes, Kb, MB, GB
        /// </summary>
        /// <param name="bytes">Chiều dài mảng Byte[]. Thường là size.Length</param>
        /// <returns>Xâu đã format</returns>
        /// <modified>
        /// Author				    created date					comments
        /// DuyNV					04/07/2013				        Tạo mới
        ///</modified>
        public static string FormatBytes(int bytes)
        {
            const int scale = 1024;
            string[] orders = new string[] { "GB", "MB", "KB", "Bytes" };
            long max = (long)Math.Pow(scale, orders.Length - 1);

            foreach (string order in orders)
            {
                if (bytes > max)
                    return string.Format("{0:##.##} {1}", decimal.Divide(bytes, max), order);

                max /= scale;
            }
            return "0 Bytes";
        }

        /// <summary>
        /// Hàm tính toán tỷ trọng % dựa trên số lượng và tổng
        /// </summary>
        /// <param name="currentValue">Giá trị thực tế</param>
        /// <param name="totalValue">Tổng giá trị</param>
        /// <returns>Tỷ lệ % giữa giá trị thực tế và tổng giá trị</returns>
        /// <modified>
        /// Author				    created date					comments
        /// DuyNV					04/07/2013				        Tạo mới
        ///</modified>
        //temp
        //public static string GetDataPercent(int currentValue, int totalValue)
        //{
        //    try
        //    {
        //        double value = ((double)currentValue / totalValue);
        //        return (value.ToString("0.0%") == "NaN") ? "0" : value.ToString("0.0%");
        //    }
        //    catch (Exception ex)
        //    {
        //        LogMan.Instance.WriteErrorToLog(ex);
        //        return string.Empty;
        //    }
        //}

        /// <summary>
        /// Hàm chuyển một chuỗi tiếng việt có dấu thành tiếng việt không dấu
        /// </summary>
        /// <param name="Unicode">xâu tiếng việt có dấu</param>
        /// <modified>
        /// Author				    created date					comments
        /// DuyNV					04/07/2013				        Tạo mới
        ///</modified>

        //temp
        //public static string UnicodeToAscii(string Unicode)
        //{
        //    Unicode = Regex.Replace(Unicode, "[á|à|ả|ã|ạ|â|ă|ấ|ầ|ẩ|ẫ|ậ|ắ|ằ|ẳ|ẵ|ặ]", "a", RegexOptions.IgnoreCase);
        //    Unicode = Regex.Replace(Unicode, "[é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ]", "e", RegexOptions.IgnoreCase);
        //    Unicode = Regex.Replace(Unicode, "[ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự]", "u", RegexOptions.IgnoreCase);
        //    Unicode = Regex.Replace(Unicode, "[í|ì|ỉ|ĩ|ị]", "i", RegexOptions.IgnoreCase);
        //    Unicode = Regex.Replace(Unicode, "[ó|ò|ỏ|õ|ọ|ô|ơ|ố|ồ|ổ|ỗ|ộ|ớ|ờ|ở|ỡ|ợ]", "o", RegexOptions.IgnoreCase);
        //    Unicode = Regex.Replace(Unicode, "[đ|Đ]", "d", RegexOptions.IgnoreCase);
        //    Unicode = Regex.Replace(Unicode, "[ý|ỳ|ỷ|ỹ|ỵ|Ý|Ỳ|Ỷ|Ỹ|Ỵ]", "y", RegexOptions.IgnoreCase);
        //    Unicode = Regex.Replace(Unicode, "[^A-Za-z0-9-\\s]", "");
        //    return Unicode;
        //}

        public static string ConvertLink(string input, string ch, string ext)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    return "";
                }
                input = RemoveUnicode(ChuanHoaChuoi(input));
                input = System.Text.RegularExpressions.Regex.Replace(input, @"[^\w\.@-]", ch) + ext;
            }
            catch (Exception ex)
            {
                LogMan.Instance.WriteErrorToLog(ex);
            }
            return input;
        }

        public static string ChuanHoaChuoi(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    return string.Empty;
                }
                input = input.Trim();
                while (input.IndexOf("  ") != -1)
                {
                    input = input.Replace("  ", " ");
                }
            }
            catch (Exception ex)
            {
                LogMan.Instance.WriteErrorToLog(ex);
            }
            return input;
        }

        private static string charLower = "aAeEoOuUiIdDyY";
        private static string aLower = "áàạảãâấầậẩẫăắằặẳẵ";
        private static string eLower = "éèẹẻẽêếềệểễeeeeee";
        private static string oLower = "óòọỏõôốồộổỗơớờợởỡ";
        private static string uLower = "úùụủũưứừựửữuuuuuu";
        private static string iLower = "íìịỉĩiiiiiiiiiiii";
        private static string dLower = "đdddddddddddddddd";
        private static string yLower = "ýỳỵỷỹyyyyyyyyyyyy";
        private static string aUpper = "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ";
        private static string eUpper = "ÉÈẸẺẼÊẾỀỆỂỄEEEEEE";
        private static string oUpper = "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ";
        private static string uUpper = "ÚÙỤỦŨƯỨỪỰỬỮUUUUUU";
        private static string iUpper = "ÍÌỊỈĨIIIIIIIIIIII";
        private static string dUpper = "ĐDDDDDDDDDDDDDDDD";
        private static string yUpper = "ÝỲỴỶỸYYYYYYYYYYYY";

        public static string RemoveUnicode(string resource)
        {
            string[,] array = new string[14, 18];
            array = initArray();
            string result, temp;
            result = resource;

            for (int i = 1; i < 18; i++)
            {
                array[0, i] = aLower.Substring(i - 1, 1);
                array[1, i] = aUpper.Substring(i - 1, 1);
                array[2, i] = eLower.Substring(i - 1, 1);
                array[3, i] = eUpper.Substring(i - 1, 1);
                array[4, i] = oLower.Substring(i - 1, 1);
                array[5, i] = oUpper.Substring(i - 1, 1);
                array[6, i] = uLower.Substring(i - 1, 1);
                array[7, i] = uUpper.Substring(i - 1, 1);
                array[8, i] = iLower.Substring(i - 1, 1);
                array[9, i] = iUpper.Substring(i - 1, 1);
                array[10, i] = dLower.Substring(i - 1, 1);
                array[11, i] = dUpper.Substring(i - 1, 1);
                array[12, i] = yLower.Substring(i - 1, 1);
                array[13, i] = yUpper.Substring(i - 1, 1);
            }

            for (int j = 0; j < 14; j++)
            {
                for (int k = 0; k < 18; k++)
                {
                    temp = result.Replace(array[j, k], array[j, 0]);
                    result = temp;
                }
            }

            return result;
        }
        //temp
        //public static DateTime String2Datetime(string datetime, string CultureInfo = "vi-VN")
        //{
        //    try
        //    {
        //        return DateTime.Parse("24/01/2013", new CultureInfo("CultureInfo"));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogMan.Instance.WriteErrorToLog(ex);
        //        return DateTime.Now;
        //    }
        //}

        private static string[,] initArray()
        {
            string[,] array = new string[14, 18];
            for (int i = 0; i < 14; i++)
            {
                array[i, 0] = charLower.Substring(i, 1);
            }
            return array;
        }

        /// <summary>
        /// Hàm tạo mã md5
        /// </summary>
        /// <param name="str">xâu cần mã hóa</param>
        /// <modified>
        /// Author				    created date					comments
        /// duynv					20/08/2013				        Tạo mới
        ///</modified>

        public static string GetMd5x2(string str)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            bytes = provider.ComputeHash(bytes);
            StringBuilder builder = new StringBuilder();
            foreach (byte num in bytes)
            {
                builder.Append(num.ToString("x2").ToLower());
            }
            return builder.ToString();
        }

        /// <summary>
        /// Hàm tạo mã md5
        /// </summary>
        /// <param name="str">xâu cần mã hóa</param>
        /// <modified>
        /// Author				    created date					comments
        /// DuyNV					04/07/2013				        Tạo mới
        ///</modified>
        //temp
        //public static string GetMd5Sum(string str)
        //{
        //    Encoder enc = System.Text.Encoding.Unicode.GetEncoder();
        //    byte[] unicodeText = new byte[str.Length * 2];
        //    enc.GetBytes(str.ToCharArray(), 0, str.Length, unicodeText, 0, true);
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] result = md5.ComputeHash(unicodeText);
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < result.Length; i++)
        //    {
        //        sb.Append(result[i].ToString("X2"));
        //    }
        //    return sb.ToString();
        //}

        public static string PublichKey(string str)
        {
            string strCheckSum = string.Empty;


            return "";
        }
        /// <summary>
        /// Hàm mã hóa MD5 theo chuẩn function md5() của ngôn ngữ lập trình php
        /// </summary>
        /// <param name="password">Xâu cần mã hóa</param>
        /// <returns>Xâu đã được mã hóa</returns>
        /// <modified>
        /// Author				    created date					comments
        /// DuyNV					04/07/2013				        Tạo mới
        ///</modified>
        public static string GetMd5PHP(string password)
        {
            byte[] textBytes = System.Text.Encoding.Default.GetBytes(password);
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler;
                cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash = cryptHandler.ComputeHash(textBytes);
                string ret = "";
                foreach (byte a in hash)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("x");
                    else
                        ret += a.ToString("x");
                }
                return ret;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Xóa thẻ HTML
        /// </summary>
        /// <param name="source">xâu có chứa HTML</param>
        /// <modified>
        /// Author				    created date					comments
        /// DuyNV					04/07/2013				        Tạo mới
        ///</modified>
        //temp
        //public static string RemoveHTMLTag(string source)
        //{
        //    return Regex.Replace(source, "<.*?>", string.Empty);
        //}

        /// <summary>
        /// Tạo mật khẩu
        /// </summary>
        /// <param name="leng">Độ dài mật khẩu</param>
        /// <returns></returns>
        public static string RandomString(int leng)
        {
            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!-@$?()_-";
            char[] chars = new char[leng];
            var rd = new Random();
            for (int i = 0; i < leng; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            return new string(chars);
        }
    }
}
