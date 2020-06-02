using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sp18Team7Final.DAL;
using sp18Team7Final.Models;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

//TODO: Post on piazza "Who is the revenue attributed to?"
//TODO: ask katie exactly what gifting !!entails entire order must be gifted!! !!Purchaser can be searched for revenue!! 
//TODO: how do we keep track of an order and how do we let customers search for another movie and add it to their bag?? Shopping cart??
//TODO: Ask katie if our ordering thoughts make sense. (Only 1 incomplete order, query by incomplete and add ticket to buy to that order)
namespace sp18Team7Final.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public class EmailMessaging
        {
            public static void SendEmail(String toEmailAddress, String emailSubject, String emailBody)
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("team7sp18@utexas.edu", "abc.1234"),
                    EnableSsl = true
                };

                String finalMessage = emailBody + "\n\n Thank you for choosing Longhorn Cinema!";

                MailAddress senderEmail = new MailAddress("team7sp18@utexas.edu", "abc.1234");

                MailMessage mm = new MailMessage();
                mm.Subject = "Team 7 - " + emailSubject;
                mm.Sender = senderEmail;
                mm.From = senderEmail;
                mm.To.Add(new MailAddress(toEmailAddress));
                mm.Body = finalMessage;
                client.Send(mm);
            }
        }

        // GET: Orders
        public ActionResult Index()
        {
            if (User.IsInRole("Customer") == true)
            {
                AppUser thisuser = db.Users.Find(User.Identity.GetUserId());
                return View(thisuser.Orders.ToList());
            }
            
            return View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        public ActionResult AddToCustomerOrder(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            return View(movie);
        }

        [HttpPost]
        public ActionResult AddToCustomerOrder(int? id, String useremail)
        {
            Movie thismovie = db.Movies.Find(id);
            var query = from usersi in db.Users
                        select usersi;
            query = query.Where(us => us.Email == useremail);
            List<AppUser> SelectedUsers = query.ToList();
            if(SelectedUsers.Count() == 0)
            {
                ViewBag.CustomerError = "There is no customer in our database with this email";
                return View(thismovie);
            }
            AppUser selecteduser = SelectedUsers.First();


            var query2 = from ord in selecteduser.Orders
                         select ord;
            query2 = query2.Where(or => or.CompletedOrder == false);
            List<Order> incompleteorder = query2.ToList();

            //If the user already has an incomplete order
            if(incompleteorder.Count() == 1)
            {
                Order neworder = incompleteorder.First();
                neworder.AppUser = selecteduser;
                neworder.EmployeeMade = true;
                ViewBag.Cart = "We noticed you had a pending cart. We will add to that cart";
                db.Entry(selecteduser).State = EntityState.Modified;
                db.Entry(neworder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AddToOrder", new { id = thismovie.MovieID });
            }
            else //Make a new order and add it to the customer
            {
                Order neworder = new Order();
                neworder.OrderNumber = Utilities.NextOrderNumber.GetNextOrderNumber();
                neworder.OrderDate = DateTime.Now;
                neworder.EmployeeMade = true;
                db.Orders.Add(neworder);
                selecteduser.Orders.Add(neworder);
                db.Entry(selecteduser).State = EntityState.Modified;
                db.SaveChanges();
                String passstring = thismovie.MovieID.ToString() + "@#$%^&~" + selecteduser.Id;
                return RedirectToAction("AddToOrder", new { id = thismovie.MovieID });

            }
            

        }


        public ActionResult AddToOrder(int? id)
        {
            //TODO: should return an error if below age
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.Movie = movie.Title;
            List<Showtime> showtimeslist = (movie.Showtimes.ToList());
            SelectList showtimes = new SelectList(showtimeslist, "ShowtimeID", "StartTime");
            ViewBag.showtimes = showtimes;
            AppUser user = db.Users.Find(User.Identity.GetUserId());
            var hailquery = from ord in db.Orders
                            select ord;
            hailquery = hailquery.Where(or => or.EmployeeMade == true);
            List<Order> EmployeeOrders = hailquery.ToList();
            if(EmployeeOrders.Count() == 1)
            {
                Order EmployeeOrder = EmployeeOrders.First();
                return View(EmployeeOrder);
            }


            var query = from ord in user.Orders
                        select ord;

            query = query.Where(ord => ord.CompletedOrder == false);

            List<Order> orders = query.ToList();

            Order NewOrder = new Order();

            if (orders.Count() == 0)
            {
                NewOrder.OrderNumber = Utilities.NextOrderNumber.GetNextOrderNumber();
                NewOrder.OrderDate = DateTime.Now;
                db.Orders.Add(NewOrder);
                user.Orders.Add(NewOrder);
                db.Entry(user).State = EntityState.Modified;                               
                db.SaveChanges();
            }
            else
            {
                NewOrder = orders.First();
            }
            return View(NewOrder);
        }

        [HttpPost]
        public ActionResult AddToOrder(Order ord, int? SelectedShowtime)
        {
            Showtime thisshowtime = db.Showtimes.Find(SelectedShowtime);
            Movie movie = db.Movies.Find(thisshowtime.Movie.MovieID);
            List<Showtime> showtimeslist = (movie.Showtimes.ToList());
            SelectList showtimes = new SelectList(showtimeslist, "ShowtimeID", "StartTime");
            ViewBag.showtimes = showtimes;
            if (SelectedShowtime == null)
            {
                ViewBag.Error = "You must select a showtime";
                return View(ord);
            }
            Order order = db.Orders.Find(ord.OrderID);
            Showtime showtime = db.Showtimes.Find(SelectedShowtime);

            if (Utilities.Eligibility.ViewerEligibility(order.AppUser, showtime) == false)
            {
                ViewBag.Error = "You are not old enough to buy tickets to this movie.";
                return View(ord);
            }

            if(showtime.StartTime < DateTime.Now)
            {
                ViewBag.Error = "This showtime has already started/ended";
                return View(ord);
            }

            foreach(Ticket tick in order.Tickets)
            {
                if((tick.Showtime.Movie.Title == showtime.Movie.Title) && (tick.Showtime.ShowtimeID != showtime.ShowtimeID))
                {
                    ViewBag.DuplicateMovie = "You may not purchase tickets to two different showtimes of the same movie";
                    return View(ord);
                }
                if(((thisshowtime.StartTime >= tick.Showtime.StartTime && thisshowtime.StartTime <= tick.Showtime.EndTime)||(thisshowtime.EndTime >= tick.Showtime.StartTime && thisshowtime.EndTime <= tick.Showtime.EndTime) || (thisshowtime.StartTime < tick.Showtime.StartTime && thisshowtime.EndTime > tick.Showtime.EndTime)) && thisshowtime.ShowtimeID != tick.Showtime.ShowtimeID)
                {
                    ViewBag.DuplicateMovie = "You are already seeing a movie at this time";
                    return View(ord);
                }
            }
            
            return RedirectToAction("SeatSelection", "Orders", new { id = showtime.ShowtimeID});
        }

        public ActionResult SeatSelection(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Showtime st = db.Showtimes.Find(id);
            if (st == null)
            {
                return HttpNotFound();
            }

            var qry = from t in st.Tickets
                        select t;

            qry = qry.Where(t => t.Taken == false);

            List<Ticket> AvailableTicketsList = qry.ToList();
            MultiSelectList AvailableTickets = new MultiSelectList(AvailableTicketsList, "TicketID", "Seat");
            ViewBag.AvailableTickets = AvailableTickets;
            return View(st);
        }

        [HttpPost]
        public ActionResult SeatSelection(List<int> SelectedSeats)
        {
            Order NewOrder = new Order();
            // search for order
            var hailquery = from ord in db.Orders
                            select ord;
            hailquery = hailquery.Where(or => or.EmployeeMade == true);
            List<Order> EmployeeOrders = hailquery.ToList();
            if(EmployeeOrders.Count() == 1)
            {
                NewOrder = EmployeeOrders.First();
            }
            else
            {
                AppUser user = db.Users.Find(User.Identity.GetUserId());
                var query = from ord in user.Orders
                            select ord;
                query = query.Where(ord => ord.CompletedOrder == false);
                List<Order> orders = query.ToList();
                NewOrder = orders.First();
            }

            

            foreach (int ticketId in SelectedSeats)
            {
                // Find ticket
                Ticket t = db.Tickets.Find(ticketId);

                // Add to customer order
                NewOrder.Tickets.Add(t);

                // Mark as taken
                t.Taken = true;
                db.Entry(t).State = EntityState.Modified;

            }
            db.Entry(NewOrder).State = EntityState.Modified;
            db.SaveChanges();
            return View("Edit", NewOrder);
        }

        // TODO: validation, completing order (Date + payment method set), calculating prices based off of price at payment (needs to be set), set other properties such as cancelled and gift 

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,OrderDate,PaymentMethod,Subtotal,Tax,Total,DiscountAmount,CancelledOrder,Gift,DiscountType")] Order order, AppUser user)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            AppUser currentuser = db.Users.Find(order.AppUser.Id);
            if (order == null)
            {
                return HttpNotFound();
            }


            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,OrderDate,PaymentMethod,Subtotal,Tax,Total,DiscountAmount,CancelledOrder,Gift,DiscountType")] Order orderi)
        {
            Order order = db.Orders.Find(orderi.OrderID);
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ReportsByCustomer(String strsearchstring)
        {
            if(strsearchstring == null || strsearchstring == "")
            {
                var query1 = from user in db.Users
                            select user;
                List<AppUser> usersitoshow = query1.ToList();
                List<AppUser> realusersi = new List<AppUser>();
                foreach (AppUser useri in usersitoshow)
                {
                    AppUser realuser = db.Users.Find(useri.Id);
                    realusersi.Add(realuser);
                }
                return View(realusersi);
            }
            var query = from user in db.Users
                        select user;
            query = query.Where(us => us.Email.Contains(strsearchstring) || us.FirstName.Contains(strsearchstring) || us.LastName.Contains(strsearchstring));
            List<AppUser> userstoshow = query.ToList();
            List<AppUser> realusers = new List<AppUser>();
            foreach(AppUser useri in userstoshow)
            {
                AppUser realuser = db.Users.Find(useri.Id);
                realusers.Add(realuser);
            }
            return View(realusers);
        }

        public ActionResult ByCustomerSummary(String id)
        {
            AppUser thisuser = db.Users.Find(id);
            var query = from ord in thisuser.Orders
                        select ord;
            List<Order> OrdersToShow = query.ToList();
            return View(OrdersToShow);
        }


        public ActionResult Checkout(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            AppUser currentuser = db.Users.Find(order.AppUser.Id);
            if (order == null)
            {
                return HttpNotFound();
            }

            if(order.Tickets.Count() == 0)
            {
                ViewBag.EmptyOrder = "You must select tickets in order to checkout";
                return View("Edit", order);
            }

            List<CreditCard> CreditCards = new List<CreditCard>();
            foreach(CreditCard card in order.AppUser.CreditCards)
            {
                CreditCards.Add(card);
            }
            CreditCard SelectNone = new CreditCard() { CreditCardID = 0, DisplayString = "Select Card" };
            CreditCards.Add(SelectNone);
            SelectList AllCardios = new SelectList(CreditCards.OrderBy(c => c.CreditCardID), "CreditCardID", "DisplayString");
            ViewBag.AllCards = AllCardios;

            List<String> namesofdiscounts = Utilities.Pricing.GetTicketPrice(order, currentuser);
            Order editedorder = db.Orders.Find(order.OrderID);
            editedorder.DiscountNames = namesofdiscounts;


            return View(editedorder);
        }

        [HttpPost]
        public ActionResult Checkout(int? id, String PaymentMethod, String NewCardNumber, Int32? SelectedCard, String GiftEmail)
        {
            Order order = db.Orders.Find(id);
            AppUser currentuser = db.Users.Find(order.AppUser.Id);
            //TODO: Give $1 per popcorn point
            List<CreditCard> CreditCards = new List<CreditCard>();
            foreach (CreditCard card in order.AppUser.CreditCards)
            {
                CreditCards.Add(card);
            }
            CreditCard SelectNone = new CreditCard() { CreditCardID = 0, DisplayString = "Select Card" };
            CreditCards.Add(SelectNone);
            SelectList AllCardios = new SelectList(CreditCards.OrderBy(c => c.CreditCardID), "CreditCardID", "DisplayString");
            ViewBag.AllCards = AllCardios;

            if (GiftEmail != "")
            {
                var query = from usersi in db.Users
                            select usersi;

                query = query.Where(u => u.Email == GiftEmail);
                List<AppUser> GiftUsers = query.ToList();
                if(GiftUsers.Count() == 0)
                {
                    ViewBag.PaymentError = "We're sorry, there is no user with that email in our database";

                    return View(order);
                }
                AppUser GiftUser = GiftUsers.First();

                foreach (Ticket t in order.Tickets)
                {
                    if (Utilities.Eligibility.ViewerEligibility(GiftUser, t.Showtime) == false)
                    {
                        ViewBag.PaymentError = "The gift recipient is not eligible to watch all of the movies within this order.";
                        return View(order);
                    }
                }
                //TODO: add this call to eligibilty to check if user himself is eligible when adding ticket to order
                order.DiscountType = GiftUser.Id;

                Order giftorder = new Order();

                List<Ticket> tickets = new List<Ticket>();
                foreach(Ticket tick in order.Tickets)
                {
                    tickets.Add(tick);
                }
                foreach(Ticket tick in tickets)
                {
                    Ticket tickettotake = db.Tickets.Find(tick.TicketID);
                    order.Tickets.Remove(tickettotake);
                    giftorder.Tickets.Add(tickettotake);
                    db.Entry(tickettotake).State = EntityState.Modified;
                }

                giftorder.AppUser = GiftUser;
                order.Gift = true;
                giftorder.Gift = true;
                giftorder.OrderNumber = order.OrderNumber;
                giftorder.OrderDate = order.OrderDate;
                giftorder.CompletedOrder = true;
                ViewBag.RecipientEmail = giftorder.AppUser.Email;
                db.Orders.Add(giftorder);

            }
            if (PaymentMethod != null)
            {
                if (PaymentMethod == "NewCard")
                {
                    bool cardcheck = Utilities.ValidateCard.Validation(NewCardNumber);
                    if(cardcheck == false)
                    {
                        ViewBag.PaymentError = "This is not a valid card number, please try again";
                        return View(order);
                    }
                    String displaycardnumber = Utilities.ValidateCard.CreateHiddenNumberString(NewCardNumber);
                    ViewBag.HiddenCard = displaycardnumber;
                    order.PaymentMethod = Models.PaymentMethod.Card;
                }
                else if(PaymentMethod == "ExistingCard")
                {
                    if(SelectedCard == 0)
                    {
                        ViewBag.PaymentError = "You must select a Credit Card";
                        return View(order);
                    }
                    order.PaymentMethod = Models.PaymentMethod.Card;
                    CreditCard currentcard = db.CreditCards.Find(SelectedCard);
                    ViewBag.HiddenCard = currentcard.DisplayString;
                }
                else if (PaymentMethod == "PopcornPoints")
                {
                    foreach (Ticket tk in order.Tickets)
                    {
                        if (tk.Showtime.SpecialEvent == true)
                        {
                            ViewBag.PaymentError = "Your order contains tickets to a special event. Unforunately you cannot pay for special events with Popcorn Points.";
                            return View(order);
                        }
                    }
                    int PopcornPrice = order.Tickets.Count() * 100;
                    if (PopcornPrice > currentuser.PopcornPoints)
                    {
                        ViewBag.PaymentError = "You do not have enough Popcorn Points to complete this order";
                        return View(order);
                    }
                    order.PaymentMethod = Models.PaymentMethod.PopcornPoints;
                    currentuser.PopcornPoints -= PopcornPrice;
                    db.Entry(currentuser).State = EntityState.Modified;
                }
            }
            else
            {
                ViewBag.PaymentError = "Please select a payment method";
                return View(order);
            }

            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            
            return View("Confirm", order);
        }

        [HttpPost]
        public ActionResult Confirm(Order order)
        {
            Order ord = db.Orders.Find(order.OrderID);
            ord.OrderDate = DateTime.Now;

            AppUser user = db.Users.Find(ord.AppUser.Id);
            if (ord.PaymentMethod == PaymentMethod.Card)
            {
                int PointsToAdd = Convert.ToInt32(Math.Floor(ord.Total));
                ord.AppUser.PopcornPoints += PointsToAdd;
            }
            if(ord.PaymentMethod == PaymentMethod.PopcornPoints)
            {
                ord.Subtotal = 0;
            }
            ord.CompletedOrder = true;
            ord.EmployeeMade = false;
            EmailMessaging.SendEmail(user.Email, "Thank you for placing an order!", ("Date:" + order.OrderDate.ToString() + "\n\nSubtotal: " + order.Subtotal.ToString() + "\nTax: " + order.Subtotal.ToString() + "\nTotal: " + order.Total.ToString()));
            db.Entry(ord).State = EntityState.Modified;
            db.SaveChanges();
            return View("Confirmation", ord);
        }



        public ActionResult RemoveFromOrder(int? id)
        {
            Ticket tick = db.Tickets.Find(id);
            Order ord = db.Orders.Find(tick.Order.OrderID);
            AppUser thisuser = db.Users.Find(ord.AppUser.Id);
            if(tick.Showtime.StartTime.AddHours(-1) < DateTime.Now)
            {
                ViewBag.DeleteError = "You cannot cancel a ticket for a movie within an hour of starting or after it has finished";
                return View(ord);
            }
            tick.Taken = false;
            ord.Tickets.Remove(tick);
     
            db.Entry(ord).State = EntityState.Modified;
            db.Entry(tick).State = EntityState.Modified;
            db.Entry(thisuser).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = ord.OrderID });

        }
        public ActionResult CancelTicket(int? id)
        {
            Ticket tick = db.Tickets.Find(id);
            Order ord = db.Orders.Find(tick.Order.OrderID);
            AppUser thisuser = db.Users.Find(ord.AppUser.Id);
            if (tick.Showtime.StartTime.AddHours(-1) < DateTime.Now)
            {
                ViewBag.DeleteError = "You cannot cancel a ticket for a movie within an hour of starting or after it has finished";
                return View(ord);
            }
            if (ord.PaymentMethod == PaymentMethod.PopcornPoints)
            {
                thisuser.PopcornPoints += 100;
            }
            OrdersController.EmailMessaging.SendEmail(thisuser.Email, "Ticket Cancelled", "Hi! You have cancelled a ticket");
            tick.Taken = false;
            ord.Tickets.Remove(tick);

            db.Entry(ord).State = EntityState.Modified;
            db.Entry(tick).State = EntityState.Modified;
            db.Entry(thisuser).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = ord.OrderID });

        }


        public ActionResult CancelOrder(int? id)
        {
            Order ord = db.Orders.Find(id);
            AppUser thisuser = db.Users.Find(ord.AppUser.Id);
            foreach(Ticket tick in ord.Tickets)
            {
                if(tick.Showtime.StartTime.AddHours(-1) < DateTime.Now)
                {
                    ViewBag.DeleteError = "You cannot cancel this entire order: One or more showtimes have started or are about to start";
                    return View(ord);
                }
            }

            List<Ticket> TicketsToTake = new List<Ticket>();

            foreach(Ticket tick in ord.Tickets)
            {
                TicketsToTake.Add(tick);
            }
            foreach(Ticket tick in TicketsToTake)
            {
                Ticket tickettotake = db.Tickets.Find(tick.TicketID);
                tickettotake.Taken = false;
                ord.Tickets.Remove(tickettotake);
                db.Entry(tickettotake).State = EntityState.Modified;
            }
            if(ord.PaymentMethod == PaymentMethod.Card)
            {
                int PointsToTake = Convert.ToInt32(Math.Floor(ord.Total));
                thisuser.PopcornPoints -= PointsToTake;
            }
            if(ord.PaymentMethod == PaymentMethod.PopcornPoints)
            {
                int PointsToGive = ord.Tickets.Count() * 100;
                thisuser.PopcornPoints += PointsToGive;
            }
            ord.CancelledOrder = true;
            ord.Subtotal = 0;
            db.Entry(ord).State = EntityState.Modified;
            db.SaveChanges();
            EmailMessaging.SendEmail(thisuser.Email, "You have cancelled an order!", "We're sorry you didn't want to see a show with us, hopefully you place another order soon!" );
            return RedirectToAction("Index");
        }


        public ActionResult EditTicket(int? id)
        {
            Ticket tick = db.Tickets.Find(id);
            Showtime st = db.Showtimes.Find(tick.Showtime.ShowtimeID);
            var qry = from t in st.Tickets
                      select t;

            qry = qry.Where(t => t.Taken == false);

            List<Ticket> AvailableTicketsList = qry.ToList();
            SelectList AvailableTickets = new SelectList(AvailableTicketsList, "TicketID", "Seat");
            ViewBag.AvailableTickets = AvailableTickets;
            return View(tick);
        }

        [HttpPost]
        public ActionResult EditTicket(int? id, int? SelectedSeat)
        {
            Ticket oldticket = db.Tickets.Find(id);
            Order order = db.Orders.Find(oldticket.Order.OrderID);

            Showtime showtime = db.Showtimes.Find(oldticket.Showtime.ShowtimeID);

            var qry = from t in showtime.Tickets
                      select t;
            List<Ticket> AvailableTicketsList = qry.ToList();
            SelectList AvailableTickets = new SelectList(AvailableTicketsList, "TicketID", "Seat");
            ViewBag.AvailableTickets = AvailableTickets;
            
            AppUser currentuser = db.Users.Find(oldticket.Order.AppUser.Id);

            if(SelectedSeat == null)
            {
                ViewBag.SeatError = "Please select a new seat";
                return View();
            }

            Ticket newticket = db.Tickets.Find(SelectedSeat);

            order.Tickets.Remove(oldticket);
            oldticket.Taken = false;

            order.Tickets.Add(newticket);
            newticket.Taken = true;

            db.Entry(order).State = EntityState.Modified;
            db.Entry(oldticket).State = EntityState.Modified;
            db.Entry(newticket).State = EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("Edit", new { id = order.OrderID });
        }

        public ActionResult GiftSeats(int? id)
        {
            Order order = db.Orders.Find(id);
            if (order.DiscountType != "" && order.DiscountType != null)
            {

                Order GiftorUserOrder = db.Orders.Find(id);
                var query = from ord in db.Orders
                            select ord;
                query = query.Where(ord => ord.OrderNumber == GiftorUserOrder.OrderNumber);
                query = query.Where(ord => ord.OrderID != GiftorUserOrder.OrderID);
                List<Order> wantedorders = query.ToList();
                Order WantedOrder = wantedorders.First();

                return View(WantedOrder);
            }
            else
            {
                Order GifteeUserOrder = db.Orders.Find(id);
                

                return View(GifteeUserOrder);
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
