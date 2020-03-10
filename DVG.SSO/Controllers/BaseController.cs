using MvcAuthenication;
using SSO.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace DVG.SSO.Controllers
{
    public class BaseController : BaseSystemController
    {
        public BaseController()
        {

            request = System.Web.HttpContext.Current.Request;
            if (request.Url.Segments.Length > 4) { id = Common.GetNumber(request.Url.Segments[4]); ViewBag.id = id; } else { id = 0; ViewBag.id = 0; }
            if (!string.IsNullOrEmpty(request["surveyid"])) Surveyid = Common.GetNumber(request["surveyid"]); else Surveyid = 0; ViewBag.Surveyid = Surveyid;
            if (!string.IsNullOrEmpty(request["status"])) { Status = Common.GetNumber(request["status"]); } else Status = 1; ViewBag.Status = Status;
            if (!string.IsNullOrEmpty(request["ParentId"])) ParentId = Common.GetNumber(request["ParentId"]); else ParentId = 0;
            if (!string.IsNullOrEmpty(request["CatId"])) CatId = Common.GetNumber(request["CatId"]); else CatId = 0; ViewBag.CatId = CatId;
            if (!string.IsNullOrEmpty(request["CurrentPage"])) CurrentPage = Common.GetNumber(request["CurrentPage"]); else CurrentPage = 1;
            if (!string.IsNullOrEmpty(request["RowPerPage"])) RowPerPage = Common.GetNumber(request["RowPerPage"]); else RowPerPage = 10;
            if (!string.IsNullOrEmpty(request["FieldSort"])) FieldSort = request["FieldSort"]; else FieldSort = "ID";
            if (!string.IsNullOrEmpty(request["FieldOption"])) FieldOption = (request["FieldOption"].ToLower().Equals("true")) ? true : false; else FieldOption = true;
            if (!string.IsNullOrEmpty(request["PageStep"])) PageStep = Common.GetNumber(request["PageStep"]); else PageStep = 3;
            if (!string.IsNullOrEmpty(request["SearchIn"]))
            {
                if (request["SearchIn"].Contains(","))
                    SearchInFiled = request["SearchIn"].Split(',').ToList();
                else
                {
                    SearchInFiled = new List<string>();
                    SearchInFiled.Add(request["SearchIn"].ToString());
                }
            }
            else
                SearchInFiled = new List<string>();
            if (!string.IsNullOrEmpty(request["Keyword"]))
            {
                Keyword = request["Keyword"].Trim();
                Keyword = Regex.Replace(Keyword, @"\s+", " ");
            }

            objMsg = new Message() { Error = false, ID = id };
            MyApplicationId = Guid.Parse(ConfigurationManager.AppSettings["ApplicationId"]);
        }

        private HttpRequest request;
        public string etc { get; set; }

        public int id { get; set; }

        //Trang hiện tại
        public int CurrentPage { get; set; }

        //Survey id
        public int Surveyid { get; set; }

        //Trạng thái
        public int Status { get; set; }

        //danh mục cha id
        public int ParentId { get; set; }

        //danh mục id
        public int CatId { get; set; }

        //Số bản ghi trên trang
        public int RowPerPage { get; set; }

        // Cột sắp xếp
        public string FieldSort { get; set; }

        // Từ khóa tìm kiếm
        public string Keyword { get; set; }

        // Số trang hiển thị
        public int PageStep { get; set; }

        // Trường search
        public List<string> SearchInFiled { get; set; }

        public bool FieldOption { get; set; }

        /// <summary>
        /// ApplicationId
        /// </summary>
        public Guid MyApplicationId { get; set; }

        public string ControlerName;

        public string GetTextLink()
        {
            return Common.ConvertLink(Request.Params["name"], "-", "-" + DateTime.Now.ToString("ddMMyy") + ".html");
        }

        public string GetTextLinkExt()
        {
            return Common.ConvertLink(Request.Params["name"], "-", ".html");
        }
        public Message Msg
        {
            get
            {
                return objMsg;
            }
            set
            {
                objMsg = value;
            }
        }

        private Message objMsg;
        #region Authenticate

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            ControlerName = filterContext.Controller.GetType().Name.Replace("Controller", "");
            ViewBag.ControllerName = ControlerName;
            base.OnAuthorization(filterContext);

            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
        }

        #endregion Authenticate
    }
}