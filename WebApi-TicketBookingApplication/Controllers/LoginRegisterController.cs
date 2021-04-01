using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_TicketBookingApplication.Repository;
using static WebApi_TicketBookingApplication.Models.LoginRegisterModel;

namespace WebApi_TicketBookingApplication.Controllers
{
    public class LoginRegisterController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/LoginRegister/Register")]
        public async Task<IHttpActionResult> Register(Register registerDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await LoginRepository.RegisterUser(registerDetails);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/LoginRegister/Login")]
        public async Task<IHttpActionResult> Login(string UserName, string Password)
        {
            if (UserName == "" || UserName == null || UserName == "undefined")
            {
                return BadRequest(UserName);
            }
            var result = await LoginRepository.LoginUser(UserName, Password);
           
            return Ok(result);
        }

    }
}
