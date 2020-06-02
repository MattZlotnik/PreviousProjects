using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sp18Team7Final.Models;
using sp18Team7Final.Migrations;
using sp18Team7Final.DAL;
using System.Data.Entity.Migrations;

namespace sp18Team7Final.Migrations
{
    public class MovieSchedule
    {
        public void SeedShowtimes(AppDbContext db)
        {
            Schedule DaySchedule1 = new Schedule();
            DaySchedule1.ScheduleDate = Convert.ToDateTime("05/04/2018");
            DaySchedule1.Completed = true;
            db.SaveChanges();

            Schedule DaySchedule2 = new Schedule();
            DaySchedule2.ScheduleDate = Convert.ToDateTime("05/05/2018");
            DaySchedule2.Completed = true;
            db.SaveChanges();

            Schedule DaySchedule3 = new Schedule();
            DaySchedule3.ScheduleDate = Convert.ToDateTime("05/06/2018");
            DaySchedule3.Completed = true;
            db.SaveChanges();

            Schedule DaySchedule4 = new Schedule();
            DaySchedule4.ScheduleDate = Convert.ToDateTime("05/07/2018");
            DaySchedule4.Completed = true;
            db.SaveChanges();

            Schedule DaySchedule5 = new Schedule();
            DaySchedule5.ScheduleDate = Convert.ToDateTime("05/08/2018");
            DaySchedule5.Completed = true;
            db.SaveChanges();

            Schedule DaySchedule6 = new Schedule();
            DaySchedule6.ScheduleDate = Convert.ToDateTime("05/09/2018");
            DaySchedule6.Completed = true;
            db.SaveChanges();

            Schedule DaySchedule7 = new Schedule();
            DaySchedule7.ScheduleDate = Convert.ToDateTime("05/10/2018");
            DaySchedule7.Completed = true;
            db.SaveChanges();

            Showtime r1 = new Showtime();
            r1.Theater = 1;
            r1.StartTime = Convert.ToDateTime("05/04/2018 09:05");
            r1.Schedule = DaySchedule1;
            var query1 = from mov in db.Movies
                        select mov;
            query1 = query1.Where(mov => mov.Title == "The Sting");
            List<Movie> SelectedMovies = query1.ToList();
            Movie wantedMovie1 = SelectedMovies.First();
            r1.Movie = wantedMovie1;
            Utilities.CreateSeats.InstantiateSeats(r1);
            r1.EndTime = r1.StartTime.AddMinutes(r1.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r => r.ShowtimeID, r1);
            db.SaveChanges();

            Showtime r2 = new Showtime();
            r2.Theater = 1;
            r2.StartTime = Convert.ToDateTime("05/04/2018 11:45");
            r2.Schedule = DaySchedule1;
            var query2 = from mov in db.Movies
                         select mov;
            query2 = query2.Where(mov => mov.Title == "WarGames");
            SelectedMovies = query2.ToList();
            Movie wantedMovie2 = SelectedMovies.First();
            r2.Movie = wantedMovie2;
            Utilities.CreateSeats.InstantiateSeats(r2);
            r2.EndTime = r2.StartTime.AddMinutes(r2.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r => r.ShowtimeID, r2);
            db.SaveChanges();

            Showtime r3 = new Showtime();
            r3.Theater = 1;
            r3.StartTime = Convert.ToDateTime("05/04/2018 14:10");
            r3.Schedule = DaySchedule1;
            var query3 = from mov in db.Movies
                         select mov;
            query3 = query3.Where(mov => mov.Title == "Office Space");
            SelectedMovies = query3.ToList();
            Movie wantedMovie3 = SelectedMovies.First();
            r3.Movie = wantedMovie3;
            Utilities.CreateSeats.InstantiateSeats(r3);
            r3.EndTime = r3.StartTime.AddMinutes(r3.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r => r.ShowtimeID, r3);
            db.SaveChanges();

            Showtime r4 = new Showtime();
            r4.Theater = 1;
            r4.StartTime = Convert.ToDateTime("05/04/2018 16:15");
            r4.Schedule = DaySchedule1;
            var query4 = from mov in db.Movies
                         select mov;
            query4 = query4.Where(mov => mov.Title == "Diamonds are Forever");
            SelectedMovies = query4.ToList();
            r4.Movie = SelectedMovies.First();
            Movie wantedMovie4 = SelectedMovies.First();
            r4.Movie = wantedMovie4;
            Utilities.CreateSeats.InstantiateSeats(r4);
            r4.EndTime = r4.StartTime.AddMinutes(r4.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r => r.ShowtimeID, r4);
            db.SaveChanges();

            Showtime r5 = new Showtime();
            r5.Theater = 1;
            r5.StartTime = Convert.ToDateTime("05/04/2018 18:40");
            r5.Schedule = DaySchedule1;
            var query5 = from mov in db.Movies
                         select mov;
            query5 = query5.Where(mov => mov.Title == "West Side Story");
            SelectedMovies = query5.ToList();
            r5.Movie = SelectedMovies.First();
            Movie wantedMovie5 = SelectedMovies.First();
            r5.Movie = wantedMovie5;
            Utilities.CreateSeats.InstantiateSeats(r5);
            r5.EndTime = r5.StartTime.AddMinutes(r5.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r => r.ShowtimeID, r5);
            db.SaveChanges();

            Showtime r6 = new Showtime();
            r6.Theater = 1;
            r6.StartTime = Convert.ToDateTime("05/04/2018 21:55");
            r6.Schedule = DaySchedule1;
            var query6 = from mov in db.Movies
                         select mov;
            query6 = query6.Where(mov => mov.Title == "Psycho");
            SelectedMovies = query6.ToList();
            Movie wantedMovie6 = SelectedMovies.First();
            r6.Movie = wantedMovie6;
            r6.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r6);
            r6.EndTime = r6.StartTime.AddMinutes(r6.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r => r.ShowtimeID, r6);
            db.SaveChanges();

            Showtime r7 = new Showtime();
            r7.Theater = 2;
            r7.StartTime = Convert.ToDateTime("05/04/2018 09:10");
            r7.Schedule = DaySchedule1;
            var query7 = from mov in db.Movies
                         select mov;
            query7 = query7.Where(mov => mov.Title == "Mary Poppins");
            SelectedMovies = query7.ToList();
            Movie wantedMovie7 = SelectedMovies.First();
            r7.Movie = wantedMovie7;
            r7.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r7);
            r7.EndTime = r7.StartTime.AddMinutes(r7.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r => r.ShowtimeID, r7);
            db.SaveChanges();

            Showtime r8 = new Showtime();
            r8.Theater = 2;
            r8.StartTime = Convert.ToDateTime("05/04/2018 12:00");
            r8.Schedule = DaySchedule1;
            var query8 = from mov in db.Movies
                         select mov;
            query8 = query8.Where(mov => mov.Title == "The Muppet Movie");
            SelectedMovies = query8.ToList();
            Movie wantedMovie8 = SelectedMovies.First();
            r8.Movie = wantedMovie8;
            r8.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r8);
            r8.EndTime = r8.StartTime.AddMinutes(r8.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r => r.ShowtimeID, r8);
            db.SaveChanges();

            Showtime r9 = new Showtime();
            r9.Theater = 2;
            r9.StartTime = Convert.ToDateTime("05/04/2018 14:20");
            r9.Schedule = DaySchedule1;
            var query9 = from mov in db.Movies
                         select mov;
            query9 = query9.Where(mov => mov.Title == "The Princess Bride");
            SelectedMovies = query9.ToList();
            Movie wantedMovie9 = SelectedMovies.First();
            r9.Movie = wantedMovie9;
            r9.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r9);
            r9.EndTime = r9.StartTime.AddMinutes(r9.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r => r.ShowtimeID, r9);
            db.SaveChanges();

            Showtime r10 = new Showtime();
            r10.Theater = 2;
            r10.StartTime = Convert.ToDateTime("05/04/2018 16:30");
            r10.Schedule = DaySchedule1;
            var query10 = from mov in db.Movies
                          select mov;
            query10 = query10.Where(mov => mov.Title == "The Lego Movie");
            SelectedMovies = query10.ToList();
            Movie wantedMovie10 = SelectedMovies.First();
            r10.Movie = wantedMovie10;
            r10.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r10);
            r10.EndTime = r10.StartTime.AddMinutes(r10.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r => r.ShowtimeID, r10);
            db.SaveChanges();

            Showtime r11 = new Showtime();
            r11.Theater = 2;
            r11.StartTime = Convert.ToDateTime("05/04/2018 18:45");
            r11.Schedule = DaySchedule1;
            var query11 = from mov in db.Movies
                          select mov;
            query11 = query11.Where(mov => mov.Title == "Finding Nemo");
            SelectedMovies = query11.ToList();
            Movie wantedMovie11 = SelectedMovies.First();
            r11.Movie = wantedMovie11;
            r11.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r11);
            r11.EndTime = r11.StartTime.AddMinutes(r11.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r => r.ShowtimeID, r11);
            db.SaveChanges();

            Showtime r12 = new Showtime();
            r12.Theater = 2;
            r12.StartTime = Convert.ToDateTime("05/04/2018 20:55");
            r12.Schedule = DaySchedule1;
            var query12 = from mov in db.Movies
                          select mov;
            query12 = query12.Where(mov => mov.Title == "Harry Potter and the Goblet of Fire");
            SelectedMovies = query12.ToList();
            Movie wantedMovie12 = SelectedMovies.First();
            r12.Movie = wantedMovie12;
            r12.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r12);
            r12.EndTime = r12.StartTime.AddMinutes(r12.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r => r.ShowtimeID, r12);
            db.SaveChanges();

            Showtime r13 = new Showtime();
            r13.Theater = 1;
            r13.StartTime = Convert.ToDateTime("05/05/2018 09:05");
            r13.Schedule = DaySchedule2;
            var query13 = from mov in db.Movies
                          select mov;
            query13 = query13.Where(mov => mov.Title == "The Sting");
            SelectedMovies = query13.ToList();
            Movie wantedMovie13 = SelectedMovies.First();
            r13.Movie = wantedMovie13;
            r13.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r13);
            r13.EndTime = r13.StartTime.AddMinutes(r13.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r13);
            db.SaveChanges();

            Showtime r14 = new Showtime();
            r14.Theater = 1;
            r14.StartTime = Convert.ToDateTime("05/05/2018 11:45");
            r14.Schedule = DaySchedule2;
            var query14 = from mov in db.Movies
                          select mov;
            query14 = query14.Where(mov => mov.Title == "WarGames");
            SelectedMovies = query14.ToList();
            Movie wantedMovie14 = SelectedMovies.First();
            r14.Movie = wantedMovie14;
            r14.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r14);
            r14.EndTime = r14.StartTime.AddMinutes(r14.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r14);
            db.SaveChanges();

            Showtime r15 = new Showtime();
            r15.Theater = 1;
            r15.StartTime = Convert.ToDateTime("05/05/2018 14:10");
            r15.Schedule = DaySchedule2;
            var query15 = from mov in db.Movies
                          select mov;
            query15 = query15.Where(mov => mov.Title == "Office Space");
            SelectedMovies = query15.ToList();
            Movie wantedMovie15 = SelectedMovies.First();
            r15.Movie = wantedMovie15;
            r15.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r15);
            r15.EndTime = r15.StartTime.AddMinutes(r15.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r15);
            db.SaveChanges();

            Showtime r16 = new Showtime();
            r16.Theater = 1;
            r16.StartTime = Convert.ToDateTime("05/05/2018 16:15");
            r16.Schedule = DaySchedule2;
            var query16 = from mov in db.Movies
                          select mov;
            query16 = query16.Where(mov => mov.Title == "Diamonds are Forever");
            SelectedMovies = query16.ToList();
            Movie wantedMovie16 = SelectedMovies.First();
            r16.Movie = wantedMovie16;
            r16.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r16);
            r16.EndTime = r16.StartTime.AddMinutes(r16.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r16);
            db.SaveChanges();

            Showtime r17 = new Showtime();
            r17.Theater = 1;
            r17.StartTime = Convert.ToDateTime("05/05/2018 18:40");
            r17.Schedule = DaySchedule2;
            var query17 = from mov in db.Movies
                          select mov;
            query17 = query17.Where(mov => mov.Title == "West Side Story");
            SelectedMovies = query17.ToList();
            Movie wantedMovie17 = SelectedMovies.First();
            r17.Movie = wantedMovie17;
            r17.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r17);
            r17.EndTime = r17.StartTime.AddMinutes(r17.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r17);
            db.SaveChanges();

            Showtime r18 = new Showtime();
            r18.Theater = 1;
            r18.StartTime = Convert.ToDateTime("05/05/2018 21:55");
            r18.Schedule = DaySchedule2;
            var query18 = from mov in db.Movies
                          select mov;
            query18 = query18.Where(mov => mov.Title == "Psycho");
            SelectedMovies = query18.ToList();
            Movie wantedMovie18 = SelectedMovies.First();
            r18.Movie = wantedMovie18;
            r18.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r18);
            r18.EndTime = r18.StartTime.AddMinutes(r18.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r18);
            db.SaveChanges();

            Showtime r19 = new Showtime();
            r19.Theater = 2;
            r19.StartTime = Convert.ToDateTime("05/05/2018 09:10");
            r19.Schedule = DaySchedule2;
            var query19 = from mov in db.Movies
                          select mov;
            query19 = query19.Where(mov => mov.Title == "Mary Poppins");
            SelectedMovies = query19.ToList();
            Movie wantedMovie19 = SelectedMovies.First();
            r19.Movie = wantedMovie19;
            r19.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r19);
            r19.EndTime = r19.StartTime.AddMinutes(r19.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r19);
            db.SaveChanges();

            Showtime r20 = new Showtime();
            r20.Theater = 2;
            r20.StartTime = Convert.ToDateTime("05/05/2018 12:00");
            r20.Schedule = DaySchedule2;
            var query20 = from mov in db.Movies
                          select mov;
            query20 = query20.Where(mov => mov.Title == "The Muppet Movie");
            SelectedMovies = query20.ToList();
            Movie wantedMovie20 = SelectedMovies.First();
            r20.Movie = wantedMovie20;
            r20.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r20);
            r20.EndTime = r20.StartTime.AddMinutes(r20.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r20);
            db.SaveChanges();

            Showtime r21 = new Showtime();
            r21.Theater = 2;
            r21.StartTime = Convert.ToDateTime("05/05/2018 14:20");
            r21.Schedule = DaySchedule2;
            var query21 = from mov in db.Movies
                          select mov;
            query21 = query21.Where(mov => mov.Title == "The Princess Bride");
            SelectedMovies = query21.ToList();
            Movie wantedMovie21 = SelectedMovies.First();
            r21.Movie = wantedMovie21;
            r21.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r21);
            r21.EndTime = r21.StartTime.AddMinutes(r21.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r21);
            db.SaveChanges();

            Showtime r22 = new Showtime();
            r22.Theater = 2;
            r22.StartTime = Convert.ToDateTime("05/05/2018 16:30");
            r22.Schedule = DaySchedule2;
            var query22 = from mov in db.Movies
                          select mov;
            query22 = query22.Where(mov => mov.Title == "The Lego Movie");
            SelectedMovies = query22.ToList();
            Movie wantedMovie22 = SelectedMovies.First();
            r22.Movie = wantedMovie22;
            r22.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r22);
            r22.EndTime = r22.StartTime.AddMinutes(r22.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r22);
            db.SaveChanges();

            Showtime r23 = new Showtime();
            r23.Theater = 2;
            r23.StartTime = Convert.ToDateTime("05/05/2018 18:45");
            r23.Schedule = DaySchedule2;
            var query23 = from mov in db.Movies
                          select mov;
            query23 = query23.Where(mov => mov.Title == "Finding Nemo");
            SelectedMovies = query23.ToList();
            Movie wantedMovie23 = SelectedMovies.First();
            r23.Movie = wantedMovie23;
            r23.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r23);
            r23.EndTime = r23.StartTime.AddMinutes(r23.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r23);
            db.SaveChanges();

            Showtime r24 = new Showtime();
            r24.Theater = 2;
            r24.StartTime = Convert.ToDateTime("05/05/2018 20:55");
            r24.Schedule = DaySchedule2;
            var query24 = from mov in db.Movies
                          select mov;
            query24 = query24.Where(mov => mov.Title == "Harry Potter and the Goblet of Fire");
            SelectedMovies = query24.ToList();
            Movie wantedMovie24 = SelectedMovies.First();
            r24.Movie = wantedMovie24;
            r24.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r24);
            r24.EndTime = r24.StartTime.AddMinutes(r24.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r24);
            db.SaveChanges();


            Showtime r25 = new Showtime();
            r25.Theater = 1;
            r25.StartTime = Convert.ToDateTime("05/06/2018 09:05");
            r25.Schedule = DaySchedule3;
            var query25 = from mov in db.Movies
                          select mov;
            query25 = query25.Where(mov => mov.Title == "The Sting");
            SelectedMovies = query25.ToList();
            Movie wantedMovie25 = SelectedMovies.First();
            r25.Movie = wantedMovie25;
            r25.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r25);
            r25.EndTime = r25.StartTime.AddMinutes(r25.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r25);
            db.SaveChanges();

            Showtime r26 = new Showtime();
            r26.Theater = 1;
            r26.StartTime = Convert.ToDateTime("05/06/2018 11:45");
            r26.Schedule = DaySchedule3;
            var query26 = from mov in db.Movies
                          select mov;
            query26 = query26.Where(mov => mov.Title == "WarGames");
            SelectedMovies = query26.ToList();
            Movie wantedMovie26 = SelectedMovies.First();
            r26.Movie = wantedMovie26;
            r26.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r26);
            r26.EndTime = r26.StartTime.AddMinutes(r26.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r26);
            db.SaveChanges();

            Showtime r27 = new Showtime();
            r27.Theater = 1;
            r27.StartTime = Convert.ToDateTime("05/06/2018 14:10");
            r27.Schedule = DaySchedule3;
            var query27 = from mov in db.Movies
                          select mov;
            query27 = query27.Where(mov => mov.Title == "Office Space");
            SelectedMovies = query27.ToList();
            Movie wantedMovie27 = SelectedMovies.First();
            r27.Movie = wantedMovie27;
            r27.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r27);
            r27.EndTime = r27.StartTime.AddMinutes(r27.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r27);
            db.SaveChanges();

            Showtime r28 = new Showtime();
            r28.Theater = 1;
            r28.StartTime = Convert.ToDateTime("05/06/2018 16:15");
            r28.Schedule = DaySchedule3;
            var query28 = from mov in db.Movies
                          select mov;
            query28 = query28.Where(mov => mov.Title == "Diamonds are Forever");
            SelectedMovies = query28.ToList();
            Movie wantedMovie28 = SelectedMovies.First();
            r28.Movie = wantedMovie28;
            r28.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r28);
            r28.EndTime = r28.StartTime.AddMinutes(r28.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r28);
            db.SaveChanges();

            Showtime r29 = new Showtime();
            r29.Theater = 1;
            r29.StartTime = Convert.ToDateTime("05/06/2018 18:40");
            r29.Schedule = DaySchedule3;
            var query29 = from mov in db.Movies
                          select mov;
            query29 = query29.Where(mov => mov.Title == "West Side Story");
            SelectedMovies = query29.ToList();
            Movie wantedMovie29 = SelectedMovies.First();
            r29.Movie = wantedMovie29;
            r29.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r29);
            r29.EndTime = r29.StartTime.AddMinutes(r29.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r29);
            db.SaveChanges();

            Showtime r30 = new Showtime();
            r30.Theater = 1;
            r30.StartTime = Convert.ToDateTime("05/06/2018 21:55");
            r30.Schedule = DaySchedule3;
            var query30 = from mov in db.Movies
                          select mov;
            query30 = query30.Where(mov => mov.Title == "Psycho");
            SelectedMovies = query30.ToList();
            Movie wantedMovie30 = SelectedMovies.First();
            r30.Movie = wantedMovie30;
            r30.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r30);
            r30.EndTime = r30.StartTime.AddMinutes(r30.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r30);
            db.SaveChanges();

            Showtime r31 = new Showtime();
            r31.Theater = 2;
            r31.StartTime = Convert.ToDateTime("05/06/2018 09:10");
            r31.Schedule = DaySchedule3;
            var query31 = from mov in db.Movies
                          select mov;
            query31 = query31.Where(mov => mov.Title == "Mary Poppins");
            SelectedMovies = query31.ToList();
            Movie wantedMovie31 = SelectedMovies.First();
            r31.Movie = wantedMovie31;
            r31.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r31);
            r31.EndTime = r31.StartTime.AddMinutes(r31.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r31);
            db.SaveChanges();

            Showtime r32 = new Showtime();
            r32.Theater = 2;
            r32.StartTime = Convert.ToDateTime("05/06/2018 12:00");
            r32.Schedule = DaySchedule3;
            var query32 = from mov in db.Movies
                          select mov;
            query32 = query32.Where(mov => mov.Title == "The Muppet Movie");
            SelectedMovies = query32.ToList();
            Movie wantedMovie32 = SelectedMovies.First();
            r32.Movie = wantedMovie32;
            r32.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r32);
            r32.EndTime = r32.StartTime.AddMinutes(r32.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r32);
            db.SaveChanges();

            Showtime r33 = new Showtime();
            r33.Theater = 2;
            r33.StartTime = Convert.ToDateTime("05/06/2018 14:20");
            r33.Schedule = DaySchedule3;
            var query33 = from mov in db.Movies
                          select mov;
            query33 = query33.Where(mov => mov.Title == "The Princess Bride");
            SelectedMovies = query33.ToList();
            Movie wantedMovie33 = SelectedMovies.First();
            r33.Movie = wantedMovie33;
            r33.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r33);
            r33.EndTime = r33.StartTime.AddMinutes(r33.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r33);
            db.SaveChanges();

            Showtime r34 = new Showtime();
            r34.Theater = 2;
            r34.StartTime = Convert.ToDateTime("05/06/2018 16:30");
            r34.Schedule = DaySchedule3;
            var query34 = from mov in db.Movies
                          select mov;
            query34 = query34.Where(mov => mov.Title == "The Lego Movie");
            SelectedMovies = query34.ToList();
            Movie wantedMovie34 = SelectedMovies.First();
            r34.Movie = wantedMovie34;
            r34.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r34);
            r34.EndTime = r34.StartTime.AddMinutes(r34.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r34);
            db.SaveChanges();

            Showtime r35 = new Showtime();
            r35.Theater = 2;
            r35.StartTime = Convert.ToDateTime("05/06/2018 18:45");
            r35.Schedule = DaySchedule3;
            var query35 = from mov in db.Movies
                          select mov;
            query35 = query35.Where(mov => mov.Title == "Finding Nemo");
            SelectedMovies = query35.ToList();
            Movie wantedMovie35 = SelectedMovies.First();
            r35.Movie = wantedMovie35;
            r35.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r35);
            r35.EndTime = r35.StartTime.AddMinutes(r35.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r35);
            db.SaveChanges();

            Showtime r36 = new Showtime();
            r36.Theater = 2;
            r36.StartTime = Convert.ToDateTime("05/06/2018 20:55");
            r36.Schedule = DaySchedule3;
            var query36 = from mov in db.Movies
                          select mov;
            query36 = query36.Where(mov => mov.Title == "Harry Potter and the Goblet of Fire");
            SelectedMovies = query36.ToList();
            Movie wantedMovie36 = SelectedMovies.First();
            r36.Movie = wantedMovie36;
            r36.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r36);
            r36.EndTime = r36.StartTime.AddMinutes(r36.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r36);
            db.SaveChanges();


            Showtime r37 = new Showtime();
            r37.Theater = 1;
            r37.StartTime = Convert.ToDateTime("05/07/2018 09:05");
            r37.Schedule = DaySchedule4;
            var query37 = from mov in db.Movies
                          select mov;
            query37 = query37.Where(mov => mov.Title == "The Sting");
            SelectedMovies = query37.ToList();
            Movie wantedMovie37 = SelectedMovies.First();
            r37.Movie = wantedMovie37;
            r37.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r37);
            r37.EndTime = r37.StartTime.AddMinutes(r37.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r37);
            db.SaveChanges();

            Showtime r38 = new Showtime();
            r38.Theater = 1;
            r38.StartTime = Convert.ToDateTime("05/07/2018 11:45");
            r38.Schedule = DaySchedule4;
            var query38 = from mov in db.Movies
                          select mov;
            query38 = query38.Where(mov => mov.Title == "WarGames");
            SelectedMovies = query38.ToList();
            Movie wantedMovie38 = SelectedMovies.First();
            r38.Movie = wantedMovie38;
            r38.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r38);
            r38.EndTime = r38.StartTime.AddMinutes(r38.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r38);
            db.SaveChanges();

            Showtime r39 = new Showtime();
            r39.Theater = 1;
            r39.StartTime = Convert.ToDateTime("05/07/2018 14:10");
            r39.Schedule = DaySchedule4;
            var query39 = from mov in db.Movies
                          select mov;
            query39 = query39.Where(mov => mov.Title == "Office Space");
            SelectedMovies = query39.ToList();
            Movie wantedMovie39 = SelectedMovies.First();
            r39.Movie = wantedMovie39;
            r39.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r39);
            r39.EndTime = r39.StartTime.AddMinutes(r39.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r39);
            db.SaveChanges();

            Showtime r40 = new Showtime();
            r40.Theater = 1;
            r40.StartTime = Convert.ToDateTime("05/07/2018 16:15");
            r40.Schedule = DaySchedule4;
            var query40 = from mov in db.Movies
                          select mov;
            query40 = query40.Where(mov => mov.Title == "Diamonds are Forever");
            SelectedMovies = query40.ToList();
            Movie wantedMovie40 = SelectedMovies.First();
            r40.Movie = wantedMovie40;
            r40.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r40);
            r40.EndTime = r40.StartTime.AddMinutes(r40.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r40);
            db.SaveChanges();

            Showtime r41 = new Showtime();
            r41.Theater = 1;
            r41.StartTime = Convert.ToDateTime("05/07/2018 18:40");
            r41.Schedule = DaySchedule4;
            var query41 = from mov in db.Movies
                          select mov;
            query41 = query41.Where(mov => mov.Title == "West Side Story");
            SelectedMovies = query41.ToList();
            Movie wantedMovie41 = SelectedMovies.First();
            r41.Movie = wantedMovie41;
            r41.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r41);
            r41.EndTime = r41.StartTime.AddMinutes(r41.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r41);
            db.SaveChanges();

            Showtime r42 = new Showtime();
            r42.Theater = 1;
            r42.StartTime = Convert.ToDateTime("05/07/2018 21:55");
            r42.Schedule = DaySchedule4;
            var query42 = from mov in db.Movies
                          select mov;
            query42 = query42.Where(mov => mov.Title == "Psycho");
            SelectedMovies = query42.ToList();
            Movie wantedMovie42 = SelectedMovies.First();
            r42.Movie = wantedMovie42;
            r42.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r42);
            r42.EndTime = r42.StartTime.AddMinutes(r42.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r42);
            db.SaveChanges();

            Showtime r43 = new Showtime();
            r43.Theater = 2;
            r43.StartTime = Convert.ToDateTime("05/07/2018 09:10");
            r43.Schedule = DaySchedule4;
            var query43 = from mov in db.Movies
                          select mov;
            query43 = query43.Where(mov => mov.Title == "Mary Poppins");
            SelectedMovies = query43.ToList();
            Movie wantedMovie43 = SelectedMovies.First();
            r43.Movie = wantedMovie43;
            r43.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r43);
            r43.EndTime = r43.StartTime.AddMinutes(r43.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r43);
            db.SaveChanges();

            Showtime r44 = new Showtime();
            r44.Theater = 2;
            r44.StartTime = Convert.ToDateTime("05/07/2018 12:00");
            r44.Schedule = DaySchedule4;
            var query44 = from mov in db.Movies
                          select mov;
            query44 = query44.Where(mov => mov.Title == "The Muppet Movie");
            SelectedMovies = query44.ToList();
            Movie wantedMovie44 = SelectedMovies.First();
            r44.Movie = wantedMovie44;
            r44.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r44);
            r44.EndTime = r44.StartTime.AddMinutes(r44.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r44);
            db.SaveChanges();

            Showtime r45 = new Showtime();
            r45.Theater = 2;
            r45.StartTime = Convert.ToDateTime("05/07/2018 14:20");
            r45.Schedule = DaySchedule4;
            var query45 = from mov in db.Movies
                          select mov;
            query45 = query45.Where(mov => mov.Title == "The Princess Bride");
            SelectedMovies = query45.ToList();
            Movie wantedMovie45 = SelectedMovies.First();
            r45.Movie = wantedMovie45;
            r45.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r45);
            r45.EndTime = r45.StartTime.AddMinutes(r45.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r45);
            db.SaveChanges();

            Showtime r46 = new Showtime();
            r46.Theater = 2;
            r46.StartTime = Convert.ToDateTime("05/07/2018 16:30");
            r46.Schedule = DaySchedule4;
            var query46 = from mov in db.Movies
                          select mov;
            query46 = query46.Where(mov => mov.Title == "The Lego Movie");
            SelectedMovies = query46.ToList();
            Movie wantedMovie46 = SelectedMovies.First();
            r46.Movie = wantedMovie46;
            r46.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r46);
            r46.EndTime = r46.StartTime.AddMinutes(r46.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r46);
            db.SaveChanges();

            Showtime r47 = new Showtime();
            r47.Theater = 2;
            r47.StartTime = Convert.ToDateTime("05/07/2018 18:45");
            r47.Schedule = DaySchedule4;
            var query47 = from mov in db.Movies
                          select mov;
            query47 = query47.Where(mov => mov.Title == "Finding Nemo");
            SelectedMovies = query47.ToList();
            Movie wantedMovie47 = SelectedMovies.First();
            r47.Movie = wantedMovie47;
            r47.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r47);
            r47.EndTime = r47.StartTime.AddMinutes(r47.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r47);
            db.SaveChanges();

            Showtime r48 = new Showtime();
            r48.Theater = 2;
            r48.StartTime = Convert.ToDateTime("05/07/2018 20:55");
            r48.Schedule = DaySchedule4;
            var query48 = from mov in db.Movies
                          select mov;
            query48 = query48.Where(mov => mov.Title == "Harry Potter and the Goblet of Fire");
            SelectedMovies = query48.ToList();
            Movie wantedMovie48 = SelectedMovies.First();
            r48.Movie = wantedMovie48;
            r48.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r48);
            r48.EndTime = r48.StartTime.AddMinutes(r48.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r48);
            db.SaveChanges();

            Showtime r49 = new Showtime();
            r49.Theater = 1;
            r49.StartTime = Convert.ToDateTime("05/08/2018 09:05");
            r49.Schedule = DaySchedule5;
            var query49 = from mov in db.Movies
                          select mov;
            query49 = query49.Where(mov => mov.Title == "The Sting");
            SelectedMovies = query49.ToList();
            Movie wantedMovie49 = SelectedMovies.First();
            r49.Movie = wantedMovie49;
            r49.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r49);
            r49.EndTime = r49.StartTime.AddMinutes(r49.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r49);
            db.SaveChanges();

            Showtime r50 = new Showtime();
            r50.Theater = 1;
            r50.StartTime = Convert.ToDateTime("05/08/2018 11:45");
            r50.Schedule = DaySchedule5;
            var query50 = from mov in db.Movies
                          select mov;
            query50 = query50.Where(mov => mov.Title == "WarGames");
            SelectedMovies = query50.ToList();
            Movie wantedMovie50 = SelectedMovies.First();
            r50.Movie = wantedMovie50;
            r50.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r50);
            r50.EndTime = r50.StartTime.AddMinutes(r50.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r50);
            db.SaveChanges();

            Showtime r51 = new Showtime();
            r51.Theater = 1;
            r51.StartTime = Convert.ToDateTime("05/08/2018 14:10");
            r51.Schedule = DaySchedule5;
            var query51 = from mov in db.Movies
                          select mov;
            query51 = query51.Where(mov => mov.Title == "Office Space");
            SelectedMovies = query51.ToList();
            Movie wantedMovie51 = SelectedMovies.First();
            r51.Movie = wantedMovie51;
            r51.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r51);
            r51.EndTime = r51.StartTime.AddMinutes(r51.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r51);
            db.SaveChanges();

            Showtime r52 = new Showtime();
            r52.Theater = 1;
            r52.StartTime = Convert.ToDateTime("05/08/2018 16:15");
            r52.Schedule = DaySchedule5;
            var query52 = from mov in db.Movies
                          select mov;
            query52 = query52.Where(mov => mov.Title == "Diamonds are Forever");
            SelectedMovies = query52.ToList();
            Movie wantedMovie52 = SelectedMovies.First();
            r52.Movie = wantedMovie52;
            r52.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r52);
            r52.EndTime = r52.StartTime.AddMinutes(r52.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r52);
            db.SaveChanges();

            Showtime r53 = new Showtime();
            r53.Theater = 1;
            r53.StartTime = Convert.ToDateTime("05/08/2018 18:40");
            r53.Schedule = DaySchedule5;
            var query53 = from mov in db.Movies
                          select mov;
            query53 = query53.Where(mov => mov.Title == "West Side Story");
            SelectedMovies = query53.ToList();
            Movie wantedMovie53 = SelectedMovies.First();
            r53.Movie = wantedMovie53;
            r53.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r53);
            r53.EndTime = r53.StartTime.AddMinutes(r53.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r53);
            db.SaveChanges();

            Showtime r54 = new Showtime();
            r54.Theater = 1;
            r54.StartTime = Convert.ToDateTime("05/08/2018 21:55");
            r54.Schedule = DaySchedule5;
            var query54 = from mov in db.Movies
                          select mov;
            query54 = query54.Where(mov => mov.Title == "Psycho");
            SelectedMovies = query54.ToList();
            Movie wantedMovie54 = SelectedMovies.First();
            r54.Movie = wantedMovie54;
            r54.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r54);
            r54.EndTime = r54.StartTime.AddMinutes(r54.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r54);
            db.SaveChanges();

            Showtime r55 = new Showtime();
            r55.Theater = 2;
            r55.StartTime = Convert.ToDateTime("05/08/2018 09:10");
            r55.Schedule = DaySchedule5;
            var query55 = from mov in db.Movies
                          select mov;
            query55 = query55.Where(mov => mov.Title == "Mary Poppins");
            SelectedMovies = query55.ToList();
            Movie wantedMovie55 = SelectedMovies.First();
            r55.Movie = wantedMovie55;
            r55.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r55);
            r55.EndTime = r55.StartTime.AddMinutes(r55.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r55);
            db.SaveChanges();

            Showtime r56 = new Showtime();
            r56.Theater = 2;
            r56.StartTime = Convert.ToDateTime("05/08/2018 12:00");
            r56.Schedule = DaySchedule5;
            var query56 = from mov in db.Movies
                          select mov;
            query56 = query56.Where(mov => mov.Title == "The Muppet Movie");
            SelectedMovies = query56.ToList();
            Movie wantedMovie56 = SelectedMovies.First();
            r56.Movie = wantedMovie56;
            r56.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r56);
            r56.EndTime = r56.StartTime.AddMinutes(r56.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r56);
            db.SaveChanges();

            Showtime r57 = new Showtime();
            r57.Theater = 2;
            r57.StartTime = Convert.ToDateTime("05/08/2018 14:20");
            r57.Schedule = DaySchedule5;
            var query57 = from mov in db.Movies
                          select mov;
            query57 = query57.Where(mov => mov.Title == "The Princess Bride");
            SelectedMovies = query57.ToList();
            Movie wantedMovie57 = SelectedMovies.First();
            r57.Movie = wantedMovie57;
            r57.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r57);
            r57.EndTime = r57.StartTime.AddMinutes(r57.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r57);
            db.SaveChanges();

            Showtime r58 = new Showtime();
            r58.Theater = 2;
            r58.StartTime = Convert.ToDateTime("05/08/2018 16:30");
            r58.Schedule = DaySchedule5;
            var query58 = from mov in db.Movies
                          select mov;
            query58 = query58.Where(mov => mov.Title == "The Lego Movie");
            SelectedMovies = query58.ToList();
            Movie wantedMovie58 = SelectedMovies.First();
            r58.Movie = wantedMovie58;
            r58.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r58);
            r58.EndTime = r58.StartTime.AddMinutes(r58.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r58);
            db.SaveChanges();

            Showtime r59 = new Showtime();
            r59.Theater = 2;
            r59.StartTime = Convert.ToDateTime("05/08/2018 18:45");
            r59.Schedule = DaySchedule5;
            var query59 = from mov in db.Movies
                          select mov;
            query59 = query59.Where(mov => mov.Title == "Finding Nemo");
            SelectedMovies = query59.ToList();
            Movie wantedMovie59 = SelectedMovies.First();
            r59.Movie = wantedMovie59;
            r59.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r59);
            r59.EndTime = r59.StartTime.AddMinutes(r59.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r59);
            db.SaveChanges();

            Showtime r60 = new Showtime();
            r60.Theater = 2;
            r60.StartTime = Convert.ToDateTime("05/08/2018 20:55");
            r60.Schedule = DaySchedule5;
            var query60 = from mov in db.Movies
                          select mov;
            query60 = query60.Where(mov => mov.Title == "Harry Potter and the Goblet of Fire");
            SelectedMovies = query60.ToList();
            Movie wantedMovie60 = SelectedMovies.First();
            r60.Movie = wantedMovie60;
            r60.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r60);
            r60.EndTime = r60.StartTime.AddMinutes(r60.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r60);
            db.SaveChanges();


            Showtime r61 = new Showtime();
            r61.Theater = 1;
            r61.StartTime = Convert.ToDateTime("05/09/2018 09:05");
            r61.Schedule = DaySchedule6;
            var query61 = from mov in db.Movies
                          select mov;
            query61 = query61.Where(mov => mov.Title == "The Sting");
            SelectedMovies = query61.ToList();
            Movie wantedMovie61 = SelectedMovies.First();
            r61.Movie = wantedMovie61;
            r61.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r61);
            r61.EndTime = r61.StartTime.AddMinutes(r61.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r61);
            db.SaveChanges();

            Showtime r62 = new Showtime();
            r62.Theater = 1;
            r62.StartTime = Convert.ToDateTime("05/09/2018 11:45");
            r62.Schedule = DaySchedule6;
            var query62 = from mov in db.Movies
                          select mov;
            query62 = query62.Where(mov => mov.Title == "WarGames");
            SelectedMovies = query62.ToList();
            Movie wantedMovie62 = SelectedMovies.First();
            r62.Movie = wantedMovie62;
            r62.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r62);
            r62.EndTime = r62.StartTime.AddMinutes(r62.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r62);
            db.SaveChanges();

            Showtime r63 = new Showtime();
            r63.Theater = 1;
            r63.StartTime = Convert.ToDateTime("05/09/2018 14:10");
            r63.Schedule = DaySchedule6;
            var query63 = from mov in db.Movies
                          select mov;
            query63 = query63.Where(mov => mov.Title == "Office Space");
            SelectedMovies = query63.ToList();
            Movie wantedMovie63 = SelectedMovies.First();
            r63.Movie = wantedMovie63;
            r63.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r63);
            r63.EndTime = r63.StartTime.AddMinutes(r63.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r63);
            db.SaveChanges();

            Showtime r64 = new Showtime();
            r64.Theater = 1;
            r64.StartTime = Convert.ToDateTime("05/09/2018 16:15");
            r64.Schedule = DaySchedule6;
            var query64 = from mov in db.Movies
                          select mov;
            query64 = query64.Where(mov => mov.Title == "Diamonds are Forever");
            SelectedMovies = query64.ToList();
            Movie wantedMovie64 = SelectedMovies.First();
            r64.Movie = wantedMovie64;
            r64.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r64);
            r64.EndTime = r64.StartTime.AddMinutes(r64.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r64);
            db.SaveChanges();

            Showtime r65 = new Showtime();
            r65.Theater = 1;
            r65.StartTime = Convert.ToDateTime("05/09/2018 18:40");
            r65.Schedule = DaySchedule6;
            var query65 = from mov in db.Movies
                          select mov;
            query65 = query65.Where(mov => mov.Title == "West Side Story");
            SelectedMovies = query65.ToList();
            Movie wantedMovie65 = SelectedMovies.First();
            r65.Movie = wantedMovie65;
            r65.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r65);
            r65.EndTime = r65.StartTime.AddMinutes(r65.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r65);
            db.SaveChanges();

            Showtime r66 = new Showtime();
            r66.Theater = 1;
            r66.StartTime = Convert.ToDateTime("05/09/2018 21:55");
            r66.Schedule = DaySchedule6;
            var query66 = from mov in db.Movies
                          select mov;
            query66 = query66.Where(mov => mov.Title == "Psycho");
            SelectedMovies = query66.ToList();
            Movie wantedMovie66 = SelectedMovies.First();
            r66.Movie = wantedMovie66;
            r66.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r66);
            r66.EndTime = r66.StartTime.AddMinutes(r66.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r66);
            db.SaveChanges();

            Showtime r67 = new Showtime();
            r67.Theater = 2;
            r67.StartTime = Convert.ToDateTime("05/09/2018 09:10");
            r67.Schedule = DaySchedule6;
            var query67 = from mov in db.Movies
                          select mov;
            query67 = query67.Where(mov => mov.Title == "Mary Poppins");
            SelectedMovies = query67.ToList();
            Movie wantedMovie67 = SelectedMovies.First();
            r67.Movie = wantedMovie67;
            r67.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r67);
            r67.EndTime = r67.StartTime.AddMinutes(r67.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r67);
            db.SaveChanges();

            Showtime r68 = new Showtime();
            r68.Theater = 2;
            r68.StartTime = Convert.ToDateTime("05/09/2018 12:00");
            r68.Schedule = DaySchedule6;
            var query68 = from mov in db.Movies
                          select mov;
            query68 = query68.Where(mov => mov.Title == "The Muppet Movie");
            SelectedMovies = query68.ToList();
            Movie wantedMovie68 = SelectedMovies.First();
            r68.Movie = wantedMovie68;
            r68.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r68);
            r68.EndTime = r68.StartTime.AddMinutes(r68.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r68);
            db.SaveChanges();

            Showtime r69 = new Showtime();
            r69.Theater = 2;
            r69.StartTime = Convert.ToDateTime("05/09/2018 14:20");
            r69.Schedule = DaySchedule6;
            var query69 = from mov in db.Movies
                          select mov;
            query69 = query69.Where(mov => mov.Title == "The Princess Bride");
            SelectedMovies = query69.ToList();
            Movie wantedMovie69 = SelectedMovies.First();
            r69.Movie = wantedMovie69;
            r69.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r69);
            r69.EndTime = r69.StartTime.AddMinutes(r69.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r69);
            db.SaveChanges();

            Showtime r70 = new Showtime();
            r70.Theater = 2;
            r70.StartTime = Convert.ToDateTime("05/09/2018 16:30");
            r70.Schedule = DaySchedule6;
            var query70 = from mov in db.Movies
                          select mov;
            query70 = query70.Where(mov => mov.Title == "The Lego Movie");
            SelectedMovies = query70.ToList();
            Movie wantedMovie70 = SelectedMovies.First();
            r70.Movie = wantedMovie70;
            r70.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r70);
            r70.EndTime = r70.StartTime.AddMinutes(r70.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r70);
            db.SaveChanges();

            Showtime r71 = new Showtime();
            r71.Theater = 2;
            r71.StartTime = Convert.ToDateTime("05/09/2018 18:45");
            r71.Schedule = DaySchedule6;
            var query71 = from mov in db.Movies
                          select mov;
            query71 = query71.Where(mov => mov.Title == "Finding Nemo");
            SelectedMovies = query71.ToList();
            Movie wantedMovie71 = SelectedMovies.First();
            r71.Movie = wantedMovie71;
            r71.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r71);
            r71.EndTime = r71.StartTime.AddMinutes(r71.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r71);
            db.SaveChanges();

            Showtime r72 = new Showtime();
            r72.Theater = 2;
            r72.StartTime = Convert.ToDateTime("05/09/2018 20:55");
            r72.Schedule = DaySchedule6;
            var query72 = from mov in db.Movies
                          select mov;
            query72 = query72.Where(mov => mov.Title == "Harry Potter and the Goblet of Fire");
            SelectedMovies = query72.ToList();
            Movie wantedMovie72 = SelectedMovies.First();
            r72.Movie = wantedMovie72;
            r72.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r72);
            r72.EndTime = r72.StartTime.AddMinutes(r72.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r72);
            db.SaveChanges();


            Showtime r73 = new Showtime();
            r73.Theater = 1;
            r73.StartTime = Convert.ToDateTime("05/10/2018 09:05");
            r73.Schedule = DaySchedule7;
            var query73 = from mov in db.Movies
                          select mov;
            query73 = query73.Where(mov => mov.Title == "The Sting");
            SelectedMovies = query73.ToList();
            Movie wantedMovie73 = SelectedMovies.First();
            r73.Movie = wantedMovie73;
            r73.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r73);
            r73.EndTime = r73.StartTime.AddMinutes(r73.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r73);
            db.SaveChanges();

            Showtime r74 = new Showtime();
            r74.Theater = 1;
            r74.StartTime = Convert.ToDateTime("05/10/2018 11:45");
            r74.Schedule = DaySchedule7;
            var query74 = from mov in db.Movies
                          select mov;
            query74 = query74.Where(mov => mov.Title == "WarGames");
            SelectedMovies = query74.ToList();
            Movie wantedMovie74 = SelectedMovies.First();
            r74.Movie = wantedMovie74;
            r74.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r74);
            r74.EndTime = r74.StartTime.AddMinutes(r74.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r74);
            db.SaveChanges();

            Showtime r75 = new Showtime();
            r75.Theater = 1;
            r75.StartTime = Convert.ToDateTime("05/10/2018 14:10");
            r75.Schedule = DaySchedule7;
            var query75 = from mov in db.Movies
                          select mov;
            query75 = query75.Where(mov => mov.Title == "Office Space");
            SelectedMovies = query75.ToList();
            Movie wantedMovie75 = SelectedMovies.First();
            r75.Movie = wantedMovie75;
            r75.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r75);
            r75.EndTime = r75.StartTime.AddMinutes(r75.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r75);
            db.SaveChanges();

            Showtime r76 = new Showtime();
            r76.Theater = 1;
            r76.StartTime = Convert.ToDateTime("05/10/2018 16:15");
            r76.Schedule = DaySchedule7;
            var query76 = from mov in db.Movies
                          select mov;
            query76 = query76.Where(mov => mov.Title == "Diamonds are Forever");
            SelectedMovies = query76.ToList();
            Movie wantedMovie76 = SelectedMovies.First();
            r76.Movie = wantedMovie76;
            r76.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r76);
            r76.EndTime = r76.StartTime.AddMinutes(r76.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r76);
            db.SaveChanges();

            Showtime r77 = new Showtime();
            r77.Theater = 1;
            r77.StartTime = Convert.ToDateTime("05/10/2018 18:40");
            r77.Schedule = DaySchedule7;
            var query77 = from mov in db.Movies
                          select mov;
            query77 = query77.Where(mov => mov.Title == "West Side Story");
            SelectedMovies = query77.ToList();
            Movie wantedMovie77 = SelectedMovies.First();
            r77.Movie = wantedMovie77;
            r77.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r77);
            r77.EndTime = r77.StartTime.AddMinutes(r77.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r77);
            db.SaveChanges();

            Showtime r78 = new Showtime();
            r78.Theater = 1;
            r78.StartTime = Convert.ToDateTime("05/10/2018 21:55");
            r78.Schedule = DaySchedule7;
            var query78 = from mov in db.Movies
                          select mov;
            query78 = query78.Where(mov => mov.Title == "Psycho");
            SelectedMovies = query78.ToList();
            Movie wantedMovie78 = SelectedMovies.First();
            r78.Movie = wantedMovie78;
            r78.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r78);
            r78.EndTime = r78.StartTime.AddMinutes(r78.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r78);
            db.SaveChanges();

            Showtime r79 = new Showtime();
            r79.Theater = 2;
            r79.StartTime = Convert.ToDateTime("05/10/2018 09:10");
            r79.Schedule = DaySchedule7;
            var query79 = from mov in db.Movies
                          select mov;
            query79 = query79.Where(mov => mov.Title == "Mary Poppins");
            SelectedMovies = query79.ToList();
            Movie wantedMovie79 = SelectedMovies.First();
            r79.Movie = wantedMovie79;
            r79.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r79);
            r79.EndTime = r79.StartTime.AddMinutes(r79.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r79);
            db.SaveChanges();

            Showtime r80 = new Showtime();
            r80.Theater = 2;
            r80.StartTime = Convert.ToDateTime("05/10/2018 12:00");
            r80.Schedule = DaySchedule7;
            var query80 = from mov in db.Movies
                          select mov;
            query80 = query80.Where(mov => mov.Title == "The Muppet Movie");
            SelectedMovies = query80.ToList();
            Movie wantedMovie80 = SelectedMovies.First();
            r80.Movie = wantedMovie80;
            r80.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r80);
            r80.EndTime = r80.StartTime.AddMinutes(r80.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r80);
            db.SaveChanges();

            Showtime r81 = new Showtime();
            r81.Theater = 2;
            r81.StartTime = Convert.ToDateTime("05/10/2018 14:20");
            r81.Schedule = DaySchedule7;
            var query81 = from mov in db.Movies
                          select mov;
            query81 = query81.Where(mov => mov.Title == "The Princess Bride");
            SelectedMovies = query81.ToList();
            Movie wantedMovie81 = SelectedMovies.First();
            r81.Movie = wantedMovie81;
            r81.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r81);
            r81.EndTime = r81.StartTime.AddMinutes(r81.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r81);
            db.SaveChanges();

            Showtime r82 = new Showtime();
            r82.Theater = 2;
            r82.StartTime = Convert.ToDateTime("05/10/2018 16:30");
            r82.Schedule = DaySchedule7;
            var query82 = from mov in db.Movies
                          select mov;
            query82 = query82.Where(mov => mov.Title == "The Lego Movie");
            SelectedMovies = query82.ToList();
            Movie wantedMovie82 = SelectedMovies.First();
            r82.Movie = wantedMovie82;
            r82.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r82);
            r82.EndTime = r82.StartTime.AddMinutes(r82.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r82);
            db.SaveChanges();

            Showtime r83 = new Showtime();
            r83.Theater = 2;
            r83.StartTime = Convert.ToDateTime("05/10/2018 18:45");
            r83.Schedule = DaySchedule7;
            var query83 = from mov in db.Movies
                          select mov;
            query83 = query83.Where(mov => mov.Title == "Finding Nemo");
            SelectedMovies = query83.ToList(); Movie wantedMovie83 = SelectedMovies.First();
            r83.Movie = wantedMovie83;
            r83.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r83);
            r83.EndTime = r83.StartTime.AddMinutes(r83.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r83);
            db.SaveChanges();

            Showtime r84 = new Showtime();
            r84.Theater = 2;
            r84.StartTime = Convert.ToDateTime("05/10/2018 20:55");
            r84.Schedule = DaySchedule7;
            var query84 = from mov in db.Movies
                          select mov;
            query84 = query84.Where(mov => mov.Title == "Harry Potter and the Goblet of Fire");
            SelectedMovies = query84.ToList();
            Movie wantedMovie84 = SelectedMovies.First();
            r84.Movie = wantedMovie84;
            r84.Movie = SelectedMovies.First();
            Utilities.CreateSeats.InstantiateSeats(r84);
            r84.EndTime = r84.StartTime.AddMinutes(r84.Movie.Runtime);
            db.Showtimes.AddOrUpdate(r84);
            db.SaveChanges();
        }
    }
}