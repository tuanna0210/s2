//using Authentication;
using DVS.Algorithm;
using MvcAuthenication;
using SSO.Data;
using SSO.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DVG.SSO.Controllers
{
    public class AccountController : BaseSystemController
    {
        public ActionResult Captcha(string prefix, bool effect = true)
        {
            Captcha security = new Captcha();
            return security.CreateCaptcha(prefix, effect);
        }
        public ActionResult Login(string returnUrl)

        {
            if (User != null)
                return Redirect(ConfigurationManager.AppSettings["DefaultReturnUrl"]);
            ViewBag.ReturnUrl = returnUrl;
            SystemUserLoginItem item = new SystemUserLoginItem() { Remember = true };
            return View(item);
        }
        public ActionResult LoginCallback(string returnUrl)
        {
            SystemUserLoginItem item = new SystemUserLoginItem() { Remember = true, ReturnUrl = returnUrl, Provider = GetDomain(returnUrl) };
            if (User != null)
            {
                SystemUserLoginItem user = new SystemUserLoginItem();
                user.UserName = User.Account;
                user.Provider = GetDomain(returnUrl);
                #region Xử lý nếu User đã đăng xuất tại client
                var clientDa = new ClientDA();
                var client = clientDa.GetByQuery(m => m.Domain == user.Provider);
                if (client != null)
                {
                    var userClientDa = new UserClientDA();
                    var userClient = userClientDa.GetListByUsernameAndClientId(User.Account, client.ID);//temp, cần test lại
                    if (userClient != null)
                    {
                        if (userClient.IsLogin == false)
                        {
                            ViewBag.ReturnUrl = returnUrl;
                            return View(item);
                        }
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                #endregion
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(string.Format("{0}{1}{2}", returnUrl, "?data=", HttpUtility.UrlEncode(GetReturnData(user))));
                }
                else
                {
                    return Redirect(ConfigurationManager.AppSettings["DefaultReturnUrl"]);
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(item);
        }
        [HttpPost]
        public JsonResult Login(SystemUserLoginItem user, string returnUrl)
        {
            try
            {
                if (FormsAuthentication.CookiesSupported)
                {
                    if (ModelState.IsValid)
                    {
                        user.Password = user.Password.ToMD5(); 
                        var error = SystemAuthenticate.Login(user);
                        if (error == SystemCommon.Error.LoginSuccess)
                        {
                            ////Cập nhật trường IsLogin trong bảng Userclient
                            var userClientDa = new UserClientDA();
                            var lstUserClient = userClientDa.GetListByUsername(user.UserName).ToList();
                            if (lstUserClient.Count(m => m.IsLogin) == 0)
                            {
                                foreach (var item in lstUserClient)
                                {
                                    item.IsLogin = true;
                                    userClientDa.Update(item);
                                }
                            }

                            if (string.IsNullOrEmpty(returnUrl))
                                objMsg.Title = ConfigurationManager.AppSettings["DefaultReturnUrl"];
                            else
                                objMsg.Title = returnUrl;
                        }
                        else
                        {
                            objMsg.Title = getMessageError(error);
                            objMsg.Error = true;
                        }
                    }
                    else
                    {
                        objMsg.Error = true;
                        objMsg.Title = string.Join("</br>", ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {

                objMsg.Error = true;
                objMsg.Title = ex.Message;
            }

            return Json(objMsg);
        }
           
        [HttpPost]
        public JsonResult Logincallback(SystemUserLoginItem user)
        {
            //Nếu như user chưa đăng nhập bất cứ phần mềm nào = đã logout tất cả phần mềm và SSO đã bị logout thì bật tất cả các
            var flag = (User == null);//SSO bị logout
            if (FormsAuthentication.CookiesSupported)
            {
                if (ModelState.IsValid)
                {
                    user.Password = user.Password.ToMD5();
                    var error = SystemAuthenticate.Login(user);
                    if (error == SystemCommon.Error.LoginSuccess)
                    {
                        var userClientDA = new UserClientDA();//temp
                        if (flag)
                        {
                            //var lstUserClient = userClientDa.GetAll(m => m.Username == user.UserName).ToList();
                            var lstUserClient = userClientDA.GetListByUsername(user.UserName); //temp
                            if (lstUserClient.Count(m => m.IsLogin) == 0)
                            {
                                foreach (var item in lstUserClient)
                                {
                                    item.IsLogin = true;
                                    //userClientDa.UpdateAndSubmit(item);//temp
                                    userClientDA.Update(item);
                                }
                            }
                        }
                        else
                        {
                            var clientDa = new ClientDA();
                            var domain = GetDomain(user.ReturnUrl);
                            var client = clientDa.GetByQuery(m => m.Domain == domain);
                            var userClient = userClientDA.GetListByUsernameAndClientId(user.UserName, client.ID);
                            if (userClient != null)
                            {
                                userClient.IsLogin = true;
                                //userClientDa.UpdateAndSubmit(userClient);//temp
                                userClientDA.Update(userClient);
                            }
                        }

                        if (string.IsNullOrEmpty(user.ReturnUrl))
                            objMsg.Title = ConfigurationManager.AppSettings["DefaultReturnUrl"];
                        else
                        {
                            objMsg.Title = string.Format("{0}{1}{2}", user.ReturnUrl, "?data=", HttpUtility.UrlEncode(GetReturnData(user)));
                        }

                    }
                    else
                    {
                        objMsg.Title = getMessageError(error);
                        objMsg.Error = true;
                    }
                }
                else
                {
                    objMsg.Error = true;
                    objMsg.Title = string.Join("</br>", ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage));
                }
            }
            return Json(objMsg);
        }
        private string GetReturnData(SystemUserLoginItem user)
        {
            var lstProvider = ConfigurationManager.AppSettings["DefaultDomain"].Split(',').ToList();
            if (lstProvider.Contains(user.Provider))
            {
                string token = ConfigurationManager.AppSettings["DefaultToken"];
                string timeLogin = DateTime.Now.ToString();
                string hashCode = CryptoEngine.Md5x2(user.UserName + timeLogin + token);
                string returnData = CryptorEngine.Encrypt(string.Format("{0}#{1}#{2}#{3}", 100, user.UserName, timeLogin, hashCode), token);
                return returnData;
            }
            else
            {
                var userClientInfomation = objUserClientIdDA.GetListByUsernameAndDomain(user.UserName,user.Provider);
                if (userClientInfomation.Count() < 1)
                {
                    return "101";
                }
                var lstUserName = string.Join(",", userClientInfomation.Select(p => p.UserIDClient));
                string token = userClientInfomation.FirstOrDefault().Token;
                string timeLogin = DateTime.Now.ToString();
                string hashCode = CryptoEngine.Md5x2(lstUserName + timeLogin + token);
                string returnData = CryptorEngine.Encrypt(string.Format("{0}#{1}#{2}#{3}", 100, lstUserName, timeLogin, hashCode), token);
                return returnData;
            }

        }
        private string GetDomain(string urlReturn = "")
        {
            Match match = Regex.Match(urlReturn, @"^(?:\w+://)?([^/?]*)");
            return match.Groups[1].Value;
        }
        private string getMessageError(SystemCommon.Error error)
        {
            string message = string.Empty;
            switch (error)
            {
                case SystemCommon.Error.NotSupportCookie: message = Notify.CookiesNotSupport; break;
                case SystemCommon.Error.InfoIncorrect: message = Notify.LoginInfoIncorrect; break;
                case SystemCommon.Error.NotActivated: message = Notify.LoginNotActivated; break;
                case SystemCommon.Error.CodeIncorrect: message = Notify.LoginCodeIncorrect; break;
                case SystemCommon.Error.AccountExist: message = Notify.AccountExist; break;
                case SystemCommon.Error.RegisterSuccess: message = Notify.RegisterSuccess; break;
                case SystemCommon.Error.RegisterFail: message = Notify.RegisterFail; break;
                case SystemCommon.Error.LoginSuccess: message = Notify.LoginSuccess; break;
                case SystemCommon.Error.LoginFail: message = Notify.LoginFail; break;
                case SystemCommon.Error.ActivateFail: message = Notify.ActivateFail; break;
                case SystemCommon.Error.ActivateSuccess: message = Notify.RecoverPasswordSuccess; break;
            }
            return message;
        }
        private UserClientIdDA objUserClientIdDA = new UserClientIdDA();
        private Message objMsg = new Message() { Error = false, Title = "Login:" };
    }

}