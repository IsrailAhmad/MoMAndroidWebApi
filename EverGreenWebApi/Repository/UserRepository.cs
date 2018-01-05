using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Web;
using System.IO;
using System.Drawing;

namespace EverGreenWebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public UserModel Login(string phonenumber)
        {
            //ResponseStatus response = new ResponseStatus();
            UserModel user = new UserModel();
            using (mom_androidEntities context = new mom_androidEntities())
            {
                userloginmaster u = new userloginmaster();
                var data = context.userloginmasters.Where(x => x.PhoneNumber == phonenumber).FirstOrDefault();
                if (data == null)
                {
                    u.PhoneNumber = phonenumber;
                    u.OTP = SendOTP(u.PhoneNumber);
                    context.userloginmasters.Add(u);
                }
                else
                {
                    var userdata = context.userloginmasters.Where(x => x.PhoneNumber == phonenumber);
                    foreach (var item in userdata)
                    {
                        item.OTP = SendOTP(phonenumber);
                    }
                }
                var result = context.SaveChanges();
                if (result > 0)
                {
                    var userdata = context.userloginmasters.Where(x => x.PhoneNumber == phonenumber).FirstOrDefault();
                    if (userdata != null)
                    {
                        user.LoginID = Convert.ToInt32(userdata.LoginID);
                        user.Name = userdata.Name;
                        user.PhoneNumber = userdata.PhoneNumber;
                        user.OTP = (int)userdata.OTP;
                        user.RoleId = Convert.ToInt32(userdata.RoleId);
                        user.EmailID = userdata.EmailID;
                        //user.DataBaseId = Convert.ToInt32(userdata.DataBaseId);
                        user.StoreId = Convert.ToInt32(userdata.StoreId);
                        //user.ProfilePictureUrl = "http://103.233.79.234/Data/EverGreen_Android/EverGreenProfilePicture/" + userdata.LoginID + ".jpeg";
                    }
                }
            }
            return user;
        }

        public ResponseStatus Login(string phoneNumber, int loginId)
        {
            ResponseStatus response = new ResponseStatus();
            using (mom_androidEntities context = new mom_androidEntities())
            {
                userloginmaster u = new userloginmaster();
                u.PhoneNumber = phoneNumber;
                u.LoginID = loginId;
                u.OTP = SendOTP(u.PhoneNumber);
                var data = context.userloginmasters.Where(x => x.LoginID == u.LoginID && x.PhoneNumber == u.PhoneNumber).FirstOrDefault();
                if (data != null)
                {
                    context.userloginmasters.Where(p => p.LoginID == u.LoginID && p.PhoneNumber == u.PhoneNumber).ToList().ForEach(x => x.OTP = u.OTP);
                }
                var result = context.SaveChanges();
                if (result > 0)
                {
                    response.isSuccess = true;
                    response.serverResponseTime = System.DateTime.Now;
                }
                else
                {
                    response.isSuccess = false;
                    response.serverResponseTime = System.DateTime.Now;
                }
            }
            return response;
        }

        public UserModel Login(string phoneNumber, int otp, int loginId)
        {
            UserModel user = new UserModel();
            using (mom_androidEntities context = new mom_androidEntities())
            {
                var userdata = context.userloginmasters.Where(u => u.LoginID == loginId && u.PhoneNumber == phoneNumber && u.OTP == otp).FirstOrDefault();
                if (userdata != null)
                {

                    user.LoginID = Convert.ToInt32(userdata.LoginID);
                    user.Name = userdata.Name;
                    user.PhoneNumber = userdata.PhoneNumber;
                    user.OTP = (int)userdata.OTP;
                    user.RoleId = Convert.ToInt32(userdata.RoleId);
                    user.EmailID = userdata.EmailID;
                    //user.DataBaseId = Convert.ToInt32(userdata.DataBaseId);
                    user.StoreId = Convert.ToInt32(userdata.StoreId);
                    //user.ProfilePictureUrl = "http://103.233.79.234/Data/EverGreen_Android/EverGreenProfilePicture/" + result.LoginID + ".jpeg";
                }
            }
            return user;
        }
        

        //public UserModel UpdateProfile(UserModel user)
        //{
        //    UserModel resultdata = new UserModel();
        //    using (mom_androidEntities context = new mom_androidEntities())
        //    {

        //        registrationmaster u = new registrationmaster();
        //        u.LoginID = user.LoginID;
        //        u.PhoneNumber = user.PhoneNumber;
        //        u.Name = user.Name;
        //        u.EmailID = user.EmailID;
        //        u.Role = user.Role;
        //        context.Entry(u).State = System.Data.Entity.EntityState.Modified;
        //        var result = context.SaveChanges();
        //        if (result > 0)
        //        {
        //            var data = context.registrationmasters.Where(x => x.LoginID == user.LoginID).First();
        //            if (data != null)
        //            {
        //                UserModel users = new UserModel();
        //                users.LoginID = Convert.ToInt32(data.LoginID);
        //                users.Name = data.Name;
        //                users.PhoneNumber = data.PhoneNumber;
        //                users.Otp = Convert.ToInt32(data.OTP);
        //                users.Role = Convert.ToInt32(data.Role);
        //                users.EmailID = data.EmailID;
        //                users.ProfilePictureUrl = "http://103.233.79.234/Data/EverGreen_Android/EverGreenProfilePicture/" + user.LoginID + ".jpeg";
        //                resultdata = users;
        //            }
        //        }
        //    }
        //    return resultdata;
        //}

        //public UserModel GetUserProfile(int loginid)
        //{
        //    UserModel user = new UserModel();
        //    using (mom_androidEntities context = new mom_androidEntities())
        //    {
        //        var result = context.registrationmasters.Where(u => u.LoginID == loginid).FirstOrDefault();
        //        if (result.LoginID > 0)
        //        {
        //            user.LoginID = (int)result.LoginID;
        //            user.Name = result.Name;
        //            user.PhoneNumber = result.PhoneNumber;
        //            user.EmailID = result.EmailID;
        //            user.ProfilePictureUrl = "http://103.233.79.234/Data/EverGreen_Android/EverGreenProfilePicture/" + result.LoginID + ".jpeg";
        //        }
        //    }
        //    return user;
        //}

        //public UserModel IsMobileVerified(int loginid)
        //{
        //    UserModel user = new UserModel();
        //    using (mom_androidEntities context = new mom_androidEntities())
        //    {
        //        var result = context.registrationmasters.Where(u => u.LoginID == loginid).FirstOrDefault();
        //        if (result.LoginID > 0)
        //        {
        //            user.LoginID = (int)result.LoginID;
        //            user.Name = result.Name;
        //            user.PhoneNumber = result.PhoneNumber;
        //            user.EmailID = result.EmailID;
        //            user.ProfilePictureUrl = "http://103.233.79.234/Data/EverGreen_Android/EverGreenProfilePicture/" + user.LoginID + "ProfilePicture.jpg";
        //        }
        //    }
        //    return user;
        //}

        //public UserModel SocialUserLogin(UserModel model)
        //{
        //    UserModel user = new UserModel();
        //    registrationmaster us = new registrationmaster();
        //    using (mom_androidEntities context = new mom_androidEntities())
        //    {

        //        var data = context.registrationmasters.Where(x => x.PhoneNumber == model.PhoneNumber).FirstOrDefault();
        //        if (data != null)
        //        {
        //            us = context.registrationmasters.Find(data.LoginID);
        //            if (us != null)
        //            {
        //                us.Name = model.Name;
        //                us.PhoneNumber = model.PhoneNumber;
        //                us.OTP = Convert.ToString(SendOTP(model.PhoneNumber));
        //                us.Role = Convert.ToInt32(model.Role);
        //                us.FbToken = model.FbToken;
        //                us.EmailID = model.EmailID;
        //                us.DeviceId = model.DeviceId;
        //                try
        //                {
        //                    //user.ProfilePictureUrl = "http://103.233.79.234/Data/EverGreen_Android/EverGreenProfilePicture/" + us.LoginID + ".jpeg";
        //                    string path = "C:/inetpub/wwwroot/Data/EverGreen_Android/EverGreenProfilePicture/" + model.LoginID + ".jpeg";
        //                    var bytess = Convert.FromBase64String(user.ProfilePictureUrl);
        //                    using (var imageFile = new FileStream(path, FileMode.Create))
        //                    {
        //                        imageFile.Write(bytess, 0, bytess.Length);
        //                        imageFile.Flush();
        //                    }
        //                }
        //                catch (Exception ex)
        //                {

        //                }
        //                var r = context.SaveChanges();
        //                if (r > 0)
        //                {
        //                    user.LoginID = Convert.ToInt32(us.LoginID);
        //                    user.Name = us.Name;
        //                    user.PhoneNumber = us.PhoneNumber;
        //                    user.Otp = Convert.ToInt32(us.OTP);
        //                    user.Role = Convert.ToInt32(us.Role);
        //                    user.FbToken = us.FbToken;
        //                    user.EmailID = us.EmailID;
        //                    user.DeviceId = us.DeviceId;
        //                    user.ProfilePictureUrl = "http://103.233.79.234/Data/EverGreen_Android/EverGreenProfilePicture/" + us.LoginID + ".jpeg";
        //                }
        //                return user;
        //            }
        //        }
        //        else
        //        {
        //            registrationmaster u = new registrationmaster();
        //            u.Name = model.Name;
        //            u.PhoneNumber = model.PhoneNumber;
        //            u.EmailID = model.EmailID;
        //            u.FbToken = model.FbToken;
        //            u.OTP = Convert.ToString(SendOTP(model.PhoneNumber));
        //            u.DeviceId = model.DeviceId;
        //            context.registrationmasters.Add(u);
        //            var result = context.SaveChanges();
        //            if (result > 0)
        //            {
        //                var userdata = context.registrationmasters.Where(x => x.PhoneNumber == model.PhoneNumber).FirstOrDefault();
        //                if (userdata != null)
        //                {
        //                    try
        //                    {
        //                        string path = "C:/inetpub/wwwroot/Data/EverGreen_Android/EverGreenProfilePicture/" + u.LoginID + ".jpeg";
        //                        var bytess = GetBytes(model.ProfilePictureUrl);

        //                        using (var imageFile = new FileStream(path, FileMode.Create))
        //                        {
        //                            imageFile.Write(bytess, 0, bytess.Length);
        //                            imageFile.Flush();
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {

        //                    }
        //                    user.LoginID = Convert.ToInt32(userdata.LoginID);
        //                    user.Name = userdata.Name;
        //                    user.PhoneNumber = userdata.PhoneNumber;
        //                    user.Otp = Convert.ToInt32(userdata.OTP);
        //                    user.Role = Convert.ToInt32(userdata.Role);
        //                    user.FbToken = userdata.FbToken;
        //                    user.EmailID = userdata.EmailID;
        //                    user.DeviceId = us.DeviceId;
        //                    user.ProfilePictureUrl = "http://103.233.79.234/Data/EverGreen_Android/EverGreenProfilePicture/" + userdata.LoginID + ".jpeg";
        //                }
        //            }
        //        }
        //    }
        //    return user;
        //}
        //static byte[] GetBytes(string str)
        //{
        //    byte[] bytes = new byte[str.Length * sizeof(char)];
        //    System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        //    return bytes;
        //}
        public Int32 SendOTP(string phonenumber)
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            int rand = _rdm.Next(_min, _max);

            string _user = HttpUtility.UrlEncode("shamsweet"); // API user name to send SMS
            string _pass = HttpUtility.UrlEncode("12345");     // API password to send SMS
            string _route = HttpUtility.UrlEncode("transactional");
            string _senderid = HttpUtility.UrlEncode("WISHHH");
            string _recipient = HttpUtility.UrlEncode(phonenumber);  // who will receive message
            string _messageText = HttpUtility.UrlEncode("Your MOM's Password is " + Convert.ToString(rand)); // text message

            // Creating URL to send sms
            string _createURL = "http://www.smsnmedia.com/api/push?user=" + _user + "&pwd=" + _pass + "&route=" + _route + "&sender=" + _senderid + "&mobileno=91" + _recipient + "&text=" + _messageText;

            HttpWebRequest _createRequest = (HttpWebRequest)WebRequest.Create(_createURL);
            // getting response of sms
            HttpWebResponse myResp = (HttpWebResponse)_createRequest.GetResponse();
            System.IO.StreamReader _responseStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = _responseStreamReader.ReadToEnd();
            _responseStreamReader.Close();
            myResp.Close();

            return rand;
        }
    }
}