using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_TicketBookingApplication.Models;
using WebApi_TicketBookingApplication.Repository;

namespace WebApi_TicketBookingApplication.Controllers
{
    public class ReservationController : ApiController
    {
        //[AllowAnonymous]
        [Authorize]
        [HttpGet]
        [Route("api/Reservation/GetUserReservationInfo")]
        //public async Task<IHttpActionResult> GetUserReservationInfo(string UserName)
        public async Task<IHttpActionResult> GetUserReservationInfo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = RequestContext.Principal.Identity.GetUserId();
            var result = await ReservationRepository.GetUserReservationInfo(userId);
            
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("api/Reservation/UserReservation")]
        public async Task<IHttpActionResult> UserReservation(UserReservationModel reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = RequestContext.Principal.Identity.GetUserId();
            var result = await ReservationRepository.UserReservation(reservation,userId);

            return Ok(result);
        }
        
        
    }
}
