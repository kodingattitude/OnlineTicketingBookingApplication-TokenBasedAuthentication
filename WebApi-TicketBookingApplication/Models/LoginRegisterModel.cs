using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_TicketBookingApplication.Models
{
    public class LoginRegisterModel
    {
        public class Register
        {
            public long Id { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
            public string Gender { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedDate { get; set; }
        }
    }
    public class UserReservationModel
    {
        public List<int> SelectedSeats { get; set; }
        public string UserName { get; set; }
        public string PaymentId { get; set; }
        public double Amount { get; set; }

    }
    public class ReservationModel
    {
        public List<int> SelectedSeats { get; set; }
        public string UserName { get; set; }

    }
    public class SeatReservationModel
    {
         public List<SeatModel> SelectedSeats { get; set; }
        public string UserName { get; set; }
    }
    public class SeatModel
    {
        public int SelectedSeats { get; set; }
        public bool IsDisabled { get; set; }
        public string UserName { get; set; }
        public bool selected { get; set; }
    }
}