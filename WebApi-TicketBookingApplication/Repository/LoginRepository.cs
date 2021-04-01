using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApi_TicketBookingApplication;
using static WebApi_TicketBookingApplication.Models.LoginRegisterModel;

namespace WebApi_TicketBookingApplication.Repository
{
    public class LoginRepository
    {
        public static async Task<string> RegisterUser(Register registerDetails)
        {

            try
            {
                var pwdEncoded = Common.Base64Encode(registerDetails.Password);
                using (TicketBookingDB db = new TicketBookingDB())
                {
                    var b = db.Users.Select(x => x).Where(y => y.Email == registerDetails.Email).FirstOrDefault();
                    var c = db.Users.Select(x => x).Where(y => y.Mobile == registerDetails.Mobile).FirstOrDefault();

                    if (b != null)
                    {
                        return "Given Email ID is Already available";
                    }
                    else if (c != null)
                    {
                        return "Given Mobile No. is Already available";
                    }
                    else
                    {
                        var UserDtl = (from v in db.Users where (v.Email == registerDetails.Email || v.Mobile == registerDetails.Email) select v).FirstOrDefault();
                        if (UserDtl == null)
                        {
                            UserDtl = new User();
                            UserDtl.Email = registerDetails.Email;
                            UserDtl.UserName = registerDetails.UserName;
                            UserDtl.Mobile = registerDetails.Mobile;
                            UserDtl.CreatedBy = registerDetails.UserName;
                            UserDtl.CreatedDate = DateTime.Now;
                            UserDtl.Password = pwdEncoded;
                            db.Users.Add(UserDtl);
                            db.SaveChanges();
                            return await Task.FromResult("Registered Successfully");
                        }

                        return await Task.FromResult("Already Registered");
                    }
                }
            }
            catch (Exception ex)
            {
                return "Fail to Register";
            }

        }

        public static async Task<string> LoginUser(string UserName, string Password)
        {
            try
            {
                var pwdEncoded = Common.Base64Encode(Password);
                using (TicketBookingDB db = new TicketBookingDB())
                {
                    var a = db.Users
                         .Where(b => (b.Email == UserName || b.Mobile == UserName) && b.Password == pwdEncoded)
                         .Select(c => new
                         {
                             message = "Login Successfull",
                             c.Email,
                             c.Mobile

                         })
                         .FirstOrDefault();
                    if (a != null)
                    {
                        return await Task.FromResult("Login Successfull");
                    }
                    else
                    {
                        return await Task.FromResult("Incorrect UserId/Password");
                    }

                }
            }
            catch
            {
                return "Fail to Validate";
            }

        }

        public static async Task<Register> FindUser(string UserName, string Password)
        {
            try
            {
                using (TicketBookingDB db = new TicketBookingDB())
                {
                    var pwdEncoded = Common.Base64Encode(Password);
                    var result = db.Users
                            .Where(b => (b.Email == UserName || b.Mobile == UserName) && b.Password == pwdEncoded)
                            .Select(c => new Register
                            {
                                UserName = c.UserName,
                                Email = c.Email,
                                Mobile = c.Mobile,
                                Id = c.Id,
                                Password = c.Password,
                                Gender = c.Gender,
                                CreatedBy = c.CreatedBy,
                                CreatedDate = c.CreatedDate

                            })
                            .FirstOrDefault();
                    return await Task.FromResult(result);
                }
            
             

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}