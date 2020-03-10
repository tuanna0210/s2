using SSO.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data
{
    public class RolesDA : BaseSystemDA<cms_Roles, int>, IBaseSystemDA<cms_Roles>
    {

        public RolesDA(bool syn = false) : base(syn) { }

        #region Tạo cây thư mục

        public string Tree(IEnumerable<cms_Roles> lstCategory, bool ShowQuickTool = true)
        {
            return BuilTree(0, lstCategory, ShowQuickTool);
        }

        public string TreeCheckBox(IEnumerable<cms_Roles> lstCategory, List<int> lstChecked)
        {
            return BuilTreeCheckBox(0, lstCategory, lstChecked);
        }

        private string BuilTree(int parentid, IEnumerable<cms_Roles> lstDanhMuc, bool ShowQuickTool)
        {
            string rs = "";
            List<cms_Roles> _subcategory = lstDanhMuc.Where(p => p.ParentID == parentid).ToList();
            if (_subcategory.Count < 1)
                return rs;
            foreach (cms_Roles item in _subcategory)
            {
                if (lstDanhMuc.Where(p => p.ParentID == item.ID).ToList().Count() > 0)
                {
                    rs += "<li id=\"" + item.ID + "\" class=\"unselect collapsable\">";
                    rs += "<div class=\"hitarea unselect-hitarea expandable-hitarea\"></div>";
                    if (ShowQuickTool)
                        rs += "<span class=\"folder\"><a onclick=\"ShowQuickTool('qt" + item.ID + "');\" class=\"tool\">" + "(" + item.ID + ") " + item.Name + "</a>" + QuickTool(item.ID, item.Name) + "</span>";
                    else rs += "<span class=\"folder\"><a class=\"tool\">" + "(" + item.ID + ") " + item.Name + "</a></span>";
                    rs += "<ul>";
                    rs += BuilTree(item.ID, lstDanhMuc, ShowQuickTool);
                    rs += "</ul>";
                    rs += " </li>";
                }
                else
                {
                    if (ShowQuickTool)
                        rs += " <li class=\"unselect\" id=\"" + item.ID + "\"><span class=\"file\"><a onclick=\"ShowQuickTool('qt" + item.ID + "');\" class=\"tool\">" + "(" + item.ID + ") " + item.Name + "</a>" + QuickTool(item.ID, item.Name) + "</span></li>";
                    else rs += " <li class=\"unselect\" id=\"" + item.ID + "\"><span class=\"file\"><a class=\"tool\">" + "(" + item.ID + ") " + item.Name + "</a></span></li>";
                }
            }
            return rs;
        }

        private string BuilTreeCheckBox(int parentid, IEnumerable<cms_Roles> lstDanhMuc, List<int> lstChecked)
        {
            string rs = "";
            List<cms_Roles> _subcategory = lstDanhMuc.Where(p => p.ParentID == parentid).ToList();
            if (_subcategory.Count < 1)
                return rs;
            foreach (cms_Roles item in _subcategory)
            {
                if (lstDanhMuc.Where(p => p.ParentID == item.ID).ToList().Count() > 0)
                {
                    rs += "<li id=\"" + item.ID + "\" class=\"unselect collapsable\">";
                    rs += "<div class=\"hitarea unselect-hitarea expandable-hitarea\"></div>";
                    if (lstChecked.Contains(item.ID)) rs += "<span class=\"folder\">" + CheckBox(item.ID, item.Name, true) + "<a  class=\"tool\">" + item.Name + "</a></span>";
                    else rs += "<span class=\"folder\">" + CheckBox(item.ID, item.Name, false) + "<a  class=\"tool\">" + item.Name + "</a></span>";
                    rs += "<ul>";
                    rs += BuilTreeCheckBox(item.ID, lstDanhMuc, lstChecked);
                    rs += "</ul>";
                    rs += " </li>";
                }
                else
                {
                    if (lstChecked.Contains(item.ID)) rs += " <li class=\"unselect\" id=\"" + item.ID + "\"><span class=\"file\">" + CheckBox(item.ID, item.Name, true) + "<a  class=\"tool\">" + item.Name + "</a></span></li>";
                    else rs += " <li class=\"unselect\" id=\"" + item.ID + "\"><span class=\"file\">" + CheckBox(item.ID, item.Name, false) + "<a  class=\"tool\">" + item.Name + "</a></span></li>";
                }
            }
            return rs;
        }

        private string CheckBox(int id, string name, bool check)
        {
            if (check) return "<input type=\"checkbox\" class=\"checkboxTree\" checked=\"checked\" value=\"\" name=\"" + name + "\" id=\"" + id + "\" />";
            else return "<input type=\"checkbox\" class=\"checkboxTree\" value=\"\" name=\"" + name + "\" id=\"" + id + "\" />";
        }

        private string QuickTool(int id, string title)
        {
            string rs = string.Empty;
            rs += "<div class=\"quickTool\" id=\"qt" + id + "\">";
            rs += "<a class=\"dialog_link btn_small ui-state-default ui-corner-all tooltip\" onclick=\"AddRole('" + id + "','" + title + "');\" class=\"thickbox_2\">";
            rs += "<span class=\"ui-icon ui-icon-circle-plus\"></span></a>";

            rs += "<a class=\"dialog_link btn_small ui-state-default ui-corner-all tooltip\" onclick=\"AddRoleSystem('" + id + "','" + title + "');\" class=\"thickbox_2\">";
            rs += "<span class=\"ui-icon ui-icon-circle-plus\"></span></a>";

            rs += "<a class=\"dialog_link btn_small ui-state-default ui-corner-all tooltip\" onclick=\"Edit('" + id + "');\" class=\"thickbox_2\">";
            rs += "<span class=\"ui-icon ui-icon-pencil\"></span></a>";

            rs += "<a class=\"dialog_link btn_small ui-state-default ui-corner-all tooltip\" onclick=\"Delete('" + id + "','" + title + "');\">";
            rs += "<span class=\"ui-icon ui-icon-circle-close\"></span>";
            rs += "</a>";

            rs += "<a class=\"dialog_link btn_small ui-state-default ui-corner-all tooltip\" onclick=\"Sort('" + id + "','" + title + "');\">";
            rs += "<span class=\"ui-icon ui-icon-triangle-2-n-s\"></span>";
            rs += "</a>";
            rs += "</div>";
            return rs;
        }

        #endregion Tạo cây thư mục

        #region Tạo dropdowlist

        public string Dropdowlist(IEnumerable<cms_Roles> lstCategory, string selected)
        {
            return "<option value=\"0\" >Rool</option>" + BuilTree(0, lstCategory, "", selected);
        }
        public string DropdowlistSimple(IEnumerable<cms_Roles> lstCategory, string selected)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            var lstGroupCat = lstCategory.Select(p => new { p.Group }).Distinct();
            foreach (var item in lstGroupCat)
            {
                sb.AppendFormat("<option value=\"{0}\">{1}</option>", item.Group, item.Group);
            }
            return sb.ToString();
        }
        private string BuilTree(int parentid, IEnumerable<cms_Roles> lstDanhMuc, string tab, string selectedvalue)
        {
            string rs = "|";
            tab += "--";
            List<cms_Roles> _subcategory = lstDanhMuc.Where(p => p.ParentID == parentid).ToList();
            if (_subcategory.Count < 1)
                return rs;
            foreach (cms_Roles item in _subcategory)
            {
                if (lstDanhMuc.Where(p => p.ParentID == item.ID).ToList().Count() > 0)
                {
                    if (item.ID.ToString().Equals(selectedvalue))
                    {
                        rs += "<option value=\"" + item.ID + "\" selected >" + tab + ' ' + item.Name + "</option>";
                        rs += BuilTree(item.ID, lstDanhMuc, tab, selectedvalue);
                    }
                    else
                    {
                        rs += "<option value=\"" + item.ID + "\" >" + tab + ' ' + item.Name + "</option>";
                        rs += BuilTree(item.ID, lstDanhMuc, tab, selectedvalue);
                    }
                }
                else
                {
                    if (item.ID.ToString().Equals(selectedvalue))
                    {
                        rs += "<option value=\"" + item.ID + "\" selected >" + tab + ' ' + item.Name + "</option>";
                    }
                    else
                    {
                        rs += "<option value=\"" + item.ID + "\" >" + tab + ' ' + item.Name + "</option>";
                    }
                }
            }
            return rs;
        }

        #endregion Tạo dropdowlist
    }
}