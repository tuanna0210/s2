//using Authentication;
using DVG.SSO.Models;
using DVS.Algorithm;
using MvcAuthenication;
using Newtonsoft.Json;
using SSO.Data;
using SSO.Utils;
using SSO.Utils.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private UserClientIdDA objUserClientIdDA = new UserClientIdDA();
        private MembershipDA userDA = new MembershipDA();
        private Message objMsg = new Message() { Error = false, Title = "Login:" };
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
            SystemUserLoginItemExtend item = new SystemUserLoginItemExtend() { Remember = true };
            return View(item);
        }
        public ActionResult LoginCallback(string returnUrl)
        {
            SystemUserLoginItemExtend item = new SystemUserLoginItemExtend() { Remember = true, ReturnUrl = returnUrl, Provider = GetDomain(returnUrl) };
            if (User != null)
            {
                SystemUserLoginItem user = new SystemUserLoginItem();
                user.UserName = User.Account;
                user.Provider = GetDomain(returnUrl);
                #region Xử lý nếu User đã đăng xuất tại client
                var clientDA = new ClientDA();
                var client = clientDA.GetByDomain(user.Provider); //lấy ra client mà user đang sử dụng
                if (client != null)
                {
                    var userClientDa = new UserClientDA();
                    var userClient = userClientDa.GetListByUsernameAndClientId(User.Account, client.Id);
                    if (userClient != null) //check nếu user có account của client đấy không
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
        public JsonResult Login(SystemUserLoginItemExtend user, string returnUrl)
        {
            try
            {
                if (FormsAuthentication.CookiesSupported)
                {
                    if (ModelState.IsValid)
                    {
                        //user.Password = user.Password.ToMD5(); 
                        var error = SystemAuthenticate.Login(user);
                        if (error == SystemCommon.Error.LoginSuccess)
                        {
                            //Check OTP
                            var userInfo = userDA.GetListByUsername(user.UserName).FirstOrDefault();
                            var secretkey = ConfigurationManager.AppSettings["OTPSecretKey"] + userInfo.OtpPrivateKey;
                            if (!GoogleTOTP.IsVaLid(secretkey, user.OTP))
                            {
                                objMsg.Error = true;
                                objMsg.Title = getMessageError(SystemCommon.Error.InfoIncorrect);
                                //xóa cookie (check OTP phải xử lý sau khi login, vì login xử lý ở dll,nên nếu OTP ko chính xác thì phải xóa cookie
                                FormsAuthentication.SignOut();
                                return Json(objMsg);
                            }

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
        public JsonResult Logincallback(SystemUserLoginItemExtend user)
        {
            //Nếu như user chưa đăng nhập bất cứ phần mềm nào = đã logout tất cả phần mềm và SSO đã bị logout thì bật tất cả các
            var flag = (User == null);//SSO bị logout
            if (FormsAuthentication.CookiesSupported)
            {
                if (ModelState.IsValid)
                {
                    if(user.SSOType == "Global")
                    {
                        //user.Password = user.Password.ToMD5();
                        var error = SystemAuthenticate.Login(user);
                        if (error == SystemCommon.Error.LoginSuccess)
                        {
                            //Check OTP
                            var userInfo = userDA.GetListByUsername(user.UserName).FirstOrDefault();
                            var secretkey = ConfigurationManager.AppSettings["OTPSecretKey"] + userInfo.OtpPrivateKey;
                            if (!GoogleTOTP.IsVaLid(secretkey, user.OTP))
                            {
                                objMsg.Error = true;
                                objMsg.Title = getMessageError(SystemCommon.Error.InfoIncorrect);
                                //xóa cookie (check OTP phải xử lý sau khi login, vì login xử lý ở dll,nên nếu OTP ko chính xác thì phải xóa cookie
                                FormsAuthentication.SignOut();
                                return Json(objMsg);
                            }

                            var userClientDA = new UserClientDA();
                            if (flag) //(*)
                            {
                                var lstUserClient = userClientDA.GetListByUsername(user.UserName);
                                if (lstUserClient.Count(m => m.IsLogin) == 0)
                                {
                                    foreach (var item in lstUserClient)
                                    {
                                        item.IsLogin = true;
                                        userClientDA.Update(item);
                                    }
                                }
                            }
                            else //(**)
                            {
                                var clientDA = new ClientDA();
                                var domain = GetDomain(user.ReturnUrl);
                                var client = clientDA.GetByDomain(domain);
                                var userClient = userClientDA.GetListByUsernameAndClientId(user.UserName, client.Id);
                                if (userClient != null)
                                {
                                    userClient.IsLogin = true;
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
                        var url = ConfigurationManager.AppSettings["SSOVN"];
                        user.SecretKey = Security.CreateKey();
                        var ssoVNResponse = JsonConvert.DeserializeObject<Message>(HttpUtils.MakePostRequest(url, JsonConvert.SerializeObject(user), "application/json"));
                        objMsg = ssoVNResponse;
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
        public ActionResult Logout()
        {
            SystemAuthenticate.Logout();
            var userClientDa = new UserClientDA();
            var userClient = userClientDa.GetListByUsername(User.Account).ToList();
            foreach (var item in userClient)
            {
                item.IsLogin = false;
                userClientDa.Update(item);
            }
            return Redirect(SystemAuthenticate.LoginUrl);
        }
        public ActionResult LogoutCallback(string returnUrl)
        {
            //Cập nhật trường IsLogin trong bảng Userclient
            var clientDa = new ClientDA();
            var domain = GetDomain(returnUrl);
            var client = clientDa.GetByDomain(domain);
            if (client != null)
            {
                if (User != null)
                {
                    var userClientDa = new UserClientDA();
                    var userClient = userClientDa.GetListByUsernameAndClientId(User.Account, client.Id);
                    if (userClient != null)
                    {
                        userClient.IsLogin = false;
                        userClientDa.Update(userClient);
                    }
                }
            }

            if (string.IsNullOrEmpty(returnUrl))
            {
                Uri myReferrer = Request.UrlReferrer;
                string actual = myReferrer.ToString();
                return Redirect(actual);
            }
            else
            {
                return Redirect("/account/logincallback?returnUrl=" + returnUrl);
            }

        }
        [HttpPost]
        public ActionResult AllocateOTP1(int userId)
        {
            return Json("AAAAAAAAAAAAAA");
        }
        public ActionResult AllocateOTP(int userId)
        {
            // Cập nhật lại OTP private key cho nhân viên này
            var user = userDA.GetById(userId);
            // Sinh mã OTP private key trong bảng user
            var randomString = StringUtils.RandomString(5);
            userDA.UpdateOTPPrivateKey(userId, randomString);

            var secretKey = ConfigurationManager.AppSettings["OTPSecretKey"] + randomString;
            var dataInfo = new
            {
                UserName = user.Username,
                SecretKey = secretKey,
                ExpiredTime = System.DateTime.Now.AddMinutes(5)
            };
            //var data = HttpUtility.UrlEncode(JsonConvert.SerializeObject(dataInfo).Encrypt());
            var data = JsonConvert.SerializeObject(dataInfo).Encrypt();
            //var link = string.Format("/User/ShowQRCode?data={0}", data);
            //var fullLink = AppSettings.Instance.GetString("ServerDomain").ToString() + link;
            //data = "%2bZr86lkPS9rsBTjBmkW9kRVQm%2f4N%2b7JapCI9l0HNN2EVjKb0VM351xz20ImAlJrRv0TVXwL2FW9mXuVtLijOXo8%2b87jARoNRMAIr5LFQYKfX95STJdD%2f2q8qttKlWIrYJ%2b4Kf7%2bupHZtEAwV%2b%2bWp%2bv9aNV%2b8QH14VHB1A27WVDo%3d";
            var test = data.Decrypt();
            var dataDecrypt = JsonConvert.DeserializeObject<dynamic>(data.Decrypt());
            var username = (string)dataDecrypt.UserName;
            var secretKey1 = (string)dataDecrypt.SecretKey;
            var expiredTime = (System.DateTime)dataDecrypt.ExpiredTime;
            //Sinh ra ảnh QR code
            var googleOPTAuthenticator = new GoogleTOTP();
            var qRCodeImage = googleOPTAuthenticator.GenerateImage(secretKey, "AUTOPORTAL.CRM.NIGE-" + "temp");

            string data1 = "";
            //var retVal = new OTPManagerModel();
            if (expiredTime > System.DateTime.Now)
            {
                data1 = qRCodeImage;
            }
            else
            {
                data1 = "The QRCode is expired.Please contact IT to be supported.";
            }

            return View("AllocateOTP", null,data1);
        }
        
    }  
}