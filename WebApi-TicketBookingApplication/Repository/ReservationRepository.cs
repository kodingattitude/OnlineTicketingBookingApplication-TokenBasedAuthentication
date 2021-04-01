using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApi_TicketBookingApplication.Models;

namespace WebApi_TicketBookingApplication.Repository
{
    public class ReservationRepository
    {
        public static async Task<string> UserReservation(UserReservationModel reservation, string UserId)
        {

            try
            {
                string key = System.Configuration.ConfigurationManager.AppSettings["KeyId"];
                string secret = System.Configuration.ConfigurationManager.AppSettings["KeySecret"];

                // Initialize RazorPay Payment Gateway
                RazorpayClient client = new RazorpayClient(key, secret);

                //Get Payment Using Id
                // Payment rPayment = client.Payment.Fetch(reservation.PaymentId);

                //Capture a payment
                //Dictionary<string, object> options = new Dictionary<string, object>();

                //options.Add("amount", reservation.Amount);

                //Payment payment = rPayment.Capture(options);

                //To refund a payment
                //Refund refund = payment.createRefund();

                //Fetch All Refunds for a payment
                //List<Refund> refunds = payment.getAllRefunds();

                //Fetch One Refund for a payment using refund id
                //Refund refund = payment.fetchRefund(id);

                //Accessing the payment attributes
                //var result = new System.Collections.Specialized.NameValueCollection();

                //foreach (var att in payment.Attributes)
                //{
                //    if (att.Name != "notes")
                //    {

                //        if (att.HasValues)
                //        {
                //            if (att.Value.HasValues)
                //            {
                //                result.Add(att.Name, att.Value.Value.ToString());
                //            }
                //            else
                //            {
                //                result.Add(att.Name, att.Value.ToString());
                //            }
                //        }

                //    }
                //    else
                //    {

                //        foreach (var note in att.Value)
                //        {

                //            if (note.HasValues)
                //            {
                //                if (note.Value.HasValues)
                //                {
                //                    result.Add(note.Name, note.Value.Value.ToString());
                //                }
                //                else
                //                {
                //                    result.Add(note.Name, note.Value.ToString());
                //                }
                //            }

                //        }

                //    }

                //}

                //var xml = result.ToXml().ToString();
                long userId = Convert.ToInt64(UserId);
                using (TicketBookingDB db = new TicketBookingDB())
                {
                   
                    var user = db.Users.Where(x => x.Id == userId).FirstOrDefault();
                    
                    if (user != null)
                    {
                        db.Reservations.RemoveRange(user.Reservations);

                        foreach (var seat in reservation.SelectedSeats)
                        {
                            Reservation reserve = new Reservation();
                            reserve.SeatNo = seat;
                            reserve.User = user;
                            reserve.CreatedBy = user.UserName;
                            reserve.CreatedDate = DateTime.Now;
                            db.Reservations.Add(reserve);

                        }
                        db.SaveChanges();

                        return await Task.FromResult("Saved Seats Successfully");
                    }

                    return await Task.FromResult("No user available");
                }
            }
            catch (Exception ex)
            {
                return "Fail to Register";
            }

        }
        public static async Task<List<SeatModel>> GetUserReservationInfo(string UserId)
        {
           
            //try
            //{
            using (TicketBookingDB db = new TicketBookingDB())
            {
                var reservation = db.Reservations.ToList();
                List<SeatModel> seatList = new List<SeatModel>();
                foreach (var res in reservation)
                {
                    SeatModel sm = new SeatModel();
                    sm.SelectedSeats = res.SeatNo;
                    sm.UserName = res.User.Email;
                    sm.IsDisabled = (res.User.Id == Convert.ToInt64(UserId)) ? false : true;
                    sm.selected = (res.User.Id == Convert.ToInt64(UserId)) ? true : false;
                    seatList.Add(sm);
                }
                
                return await Task.FromResult(seatList);


            }
            //}
            //catch (Exception ex)
            //{
            //   // return null;
            //}
            //return null;
        }
    }
}