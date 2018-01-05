using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class UserModel
    {
        public int LoginID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailID { get; set; }
        public Int32 OTP { get; set; }
        public string Password { get; set; }
        public int DataBaseId { get; set; }
        public int RoleId { get; set; }
        public int StoreId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}