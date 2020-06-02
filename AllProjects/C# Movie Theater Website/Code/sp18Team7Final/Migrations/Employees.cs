using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using sp18Team7Final.Models;
using sp18Team7Final.Migrations;
using sp18Team7Final.DAL;

namespace sp18Team7Final.Migrations
{
    public class Employees
    {

        public void SeedEmployees(AppDbContext db)
        {
            //create a user manager and a role manager to use for this method
            AppUserManager UserManager = new AppUserManager(new UserStore<AppUser>(db));

            //create a role manager
            AppRoleManager RoleManager = new AppRoleManager(new RoleStore<AppRole>(db));

            //check to see if the manager has been added
            AppUser manager = db.Users.FirstOrDefault(u => u.Email == "admin@example.com");

            //if manager hasn't been created, then add them
            if (manager == null)
            {
                //TODO: Add any additional fields for user here
                manager = new AppUser();
                manager.UserName = "admin@example.com";
                manager.FirstName = "Admin";
                manager.LastName = "Admin";
                manager.Email = "admin@example.com";
                manager.PhoneNumber = "(512)555-5555";
                manager.Birthday = Convert.ToDateTime("12/25/1970");

                var result = UserManager.Create(manager, "Abc123!");
                db.SaveChanges();
                manager = db.Users.First(u => u.UserName == "admin@example.com");
            }

            //TODO: Add the needed roles
            //if role doesn't exist, add it
            if (RoleManager.RoleExists("Manager") == false)
            {
                RoleManager.Create(new AppRole("Manager"));
            }

            if (RoleManager.RoleExists("Employee") == false)
            {
                RoleManager.Create(new AppRole("Employee"));
            }

            if (RoleManager.RoleExists("Customer") == false)
            {
                RoleManager.Create(new AppRole("Customer"));
            }

            if (RoleManager.RoleExists("Fired") == false)
            {
                RoleManager.Create(new AppRole("Fired"));
            }

            //make sure user is in role
            if (UserManager.IsInRole(manager.Id, "Manager") == false)
            {
                UserManager.AddToRole(manager.Id, "Manager");
            }

            //save changes
            db.SaveChanges();

            AppUser r1 = new AppUser();
            r1.UserName = ("t.jacobs@longhorncinema.com");
            r1.LastName = ("Jacobs");
            r1.FirstName = ("Todd");
            r1.Birthday = Convert.ToDateTime("4/25/1958");
            r1.Address = ("4564 Elm St., Georgetown, TX 78628");
            r1.PhoneNumber = "9074653365";
            r1.Email = ("t.jacobs@longhorncinema.com");
            var result1 = UserManager.Create(r1, "society");
            UserManager.AddToRole(r1.Id, "Employee");
            db.SaveChanges();

            AppUser r2 = new AppUser();
            r2.UserName = ("e.rice@longhorncinema.com");
            r2.LastName = ("Rice");
            r2.FirstName = ("Eryn");
            r2.Birthday = Convert.ToDateTime("7/2/1959");
            r2.Address = ("3405 Rio Grande, Austin, TX 78746");
            r2.PhoneNumber = "9073876657";
            r2.Email = ("e.rice@longhorncinema.com");
            var result2 = UserManager.Create(r2, "ricearoni");
            UserManager.AddToRole(r2.Id, "Manager");
            db.SaveChanges();

            AppUser r3 = new AppUser();
            r3.UserName = ("b.ingram@longhorncinema.com");
            r3.LastName = ("Ingram");
            r3.FirstName = ("Brad");
            r3.Birthday = Convert.ToDateTime("8/25/1962");
            r3.Address = ("6548 La Posada Ct., Austin, TX 78705");
            r3.PhoneNumber = "9074678821";
            r3.Email = ("b.ingram@longhorncinema.com");
            var result3 = UserManager.Create(r3, "ingram45");
            UserManager.AddToRole(r3.Id, "Employee");
            db.SaveChanges();

            AppUser r4 = new AppUser();
            r4.UserName = ("a.taylor@longhorncinema.com");
            r4.LastName = ("Taylor");
            r4.FirstName = ("Allison");
            r4.Birthday = Convert.ToDateTime("9/2/1964");
            r4.Address = ("467 Nueces St., Austin, TX 78727");
            r4.PhoneNumber = "9074748452";
            r4.Email = ("a.taylor@longhorncinema.com");
            var result4 = UserManager.Create(r4, "nostalgic");
            UserManager.AddToRole(r4.Id, "Employee");
            db.SaveChanges();

            AppUser r5 = new AppUser();
            r5.UserName = ("g.martinez@longhorncinema.com");
            r5.LastName = ("Martinez");
            r5.FirstName = ("Gregory");
            r5.Birthday = Convert.ToDateTime("3/30/1992");
            r5.Address = ("8295 Sunset Blvd., Austin, TX 78712");
            r5.PhoneNumber = "9078746718";
            r5.Email = ("g.martinez@longhorncinema.com");
            var result5 = UserManager.Create(r5, "fungus");
            UserManager.AddToRole(r5.Id, "Employee");
            db.SaveChanges();

            AppUser r6 = new AppUser();
            r6.UserName = ("m.sheffield@longhorncinema.com");
            r6.LastName = ("Sheffield");
            r6.FirstName = ("Martin");
            r6.Birthday = Convert.ToDateTime("12/29/1996");
            r6.Address = ("3886 Avenue A, San Marcos, TX 78666");
            r6.PhoneNumber = "9075479167";
            r6.Email = ("m.sheffield@longhorncinema.com");
            var result6 = UserManager.Create(r6, "longhorns");
            UserManager.AddToRole(r6.Id, "Employee");
            db.SaveChanges();

            AppUser r7 = new AppUser();
            r7.UserName = ("j.macleod@longhorncinema.com");
            r7.LastName = ("MacLeod");
            r7.FirstName = ("Jennifer");
            r7.Birthday = Convert.ToDateTime("6/10/1997");
            r7.Address = ("2504 Far West Blvd., Austin, TX 78705");
            r7.PhoneNumber = "9074748138";
            r7.Email = ("j.macleod@longhorncinema.com");
            var result7 = UserManager.Create(r7, "smitty");
            UserManager.AddToRole(r7.Id, "Employee");
            db.SaveChanges();

            AppUser r8 = new AppUser();
            r8.UserName = ("j.tanner@longhorncinema.com");
            r8.LastName = ("Tanner");
            r8.FirstName = ("Jeremy");
            r8.Birthday = Convert.ToDateTime("8/12/1970");
            r8.Address = ("4347 Almstead, Austin, TX 78712");
            r8.PhoneNumber = "9074590929";
            r8.Email = ("j.tanner@longhorncinema.com");
            var result8 = UserManager.Create(r8, "tanman");
            UserManager.AddToRole(r8.Id, "Employee");
            db.SaveChanges();

            AppUser r9 = new AppUser();
            r9.UserName = ("m.rhodes@longhorncinema.com");
            r9.LastName = ("Rhodes");
            r9.FirstName = ("Megan");
            r9.Birthday = Convert.ToDateTime("12/18/1970");
            r9.Address = ("4587 Enfield Rd., Austin, TX 78729");
            r9.PhoneNumber = "9073744746";
            r9.Email = ("m.rhodes@longhorncinema.com");
            var result9 = UserManager.Create(r9, "countryrhodes");
            UserManager.AddToRole(r9.Id, "Employee");
            db.SaveChanges();

            AppUser r10 = new AppUser();
            r10.UserName = ("e.stuart@longhorncinema.com");
            r10.LastName = ("Stuart");
            r10.FirstName = ("Eric");
            r10.Birthday = Convert.ToDateTime("3/11/1971");
            r10.Address = ("5576 Toro Ring, Austin, TX 78758");
            r10.PhoneNumber = "9078178335";
            r10.Email = ("e.stuart@longhorncinema.com");
            var result10 = UserManager.Create(r10, "stewboy");
            UserManager.AddToRole(r10.Id, "Employee");
            db.SaveChanges();

            AppUser r11 = new AppUser();
            r11.UserName = ("c.miller@longhorncinema.com");
            r11.LastName = ("Miller");
            r11.FirstName = ("Charles");
            r11.Birthday = Convert.ToDateTime("7/20/1972");
            r11.Address = ("8962 Main St., Austin, TX 78709");
            r11.PhoneNumber = "9077458615";
            r11.Email = ("c.miller@longhorncinema.com");
            var result11 = UserManager.Create(r11, "squirrel");
            UserManager.AddToRole(r11.Id, "Employee");
            db.SaveChanges();

            AppUser r12 = new AppUser();
            r12.UserName = ("r.taylor@longhorncinema.com");
            r12.LastName = ("Taylor");
            r12.FirstName = ("Rachel");
            r12.Birthday = Convert.ToDateTime("12/20/1972");
            r12.Address = ("345 Longview Dr., Austin, TX 78746");
            r12.PhoneNumber = "9074512631";
            r12.Email = ("r.taylor@longhorncinema.com");
            var result12 = UserManager.Create(r12, "swansong");
            UserManager.AddToRole(r12.Id, "Manager");
            db.SaveChanges();

            AppUser r13 = new AppUser();
            r13.UserName = ("v.lawrence@longhorncinema.com");
            r13.LastName = ("Lawrence");
            r13.FirstName = ("Victoria");
            r13.Birthday = Convert.ToDateTime("4/28/1973");
            r13.Address = ("6639 Butterfly Ln., Austin, TX 78712");
            r13.PhoneNumber = "9079457399";
            r13.Email = ("v.lawrence@longhorncinema.com");
            var result13 = UserManager.Create(r13, "lottery");
            UserManager.AddToRole(r13.Id, "Employee");
            db.SaveChanges();

            AppUser r14 = new AppUser();
            r14.UserName = ("a.rogers@longhorncinema.com");
            r14.LastName = ("Rogers");
            r14.FirstName = ("Allen");
            r14.Birthday = Convert.ToDateTime("6/21/1978");
            r14.Address = ("4965 Oak Hill, Austin, TX 78705");
            r14.PhoneNumber = "9078752943";
            r14.Email = ("a.rogers@longhorncinema.com");
            var result14 = UserManager.Create(r14, "evanescent");
            UserManager.AddToRole(r14.Id, "Manager");
            db.SaveChanges();

            AppUser r15 = new AppUser();
            r15.UserName = ("e.markham@longhorncinema.com");
            r15.LastName = ("Markham");
            r15.FirstName = ("Elizabeth");
            r15.Birthday = Convert.ToDateTime("5/21/1990");
            r15.Address = ("7861 Chevy Chase, Austin, TX 78785");
            r15.PhoneNumber = "9074579845";
            r15.Email = ("e.markham@longhorncinema.com");
            var result15 = UserManager.Create(r15, "monty3");
            UserManager.AddToRole(r15.Id, "Employee");
            db.SaveChanges();

            AppUser r16 = new AppUser();
            r16.UserName = ("c.baker@longhorncinema.com");
            r16.LastName = ("Baker");
            r16.FirstName = ("Christopher");
            r16.Birthday = Convert.ToDateTime("3/16/1993");
            r16.Address = ("1245 Lake Anchorage Blvd., Cedar Park, TX 78613");
            r16.PhoneNumber = "9075571146";
            r16.Email = ("c.baker@longhorncinema.com");
            var result16 = UserManager.Create(r16, "hecktour");
            UserManager.AddToRole(r16.Id, "Employee");
            db.SaveChanges();

            AppUser r17 = new AppUser();
            r17.UserName = ("s.saunders@longhorncinema.com");
            r17.LastName = ("Saunders");
            r17.FirstName = ("Sarah");
            r17.Birthday = Convert.ToDateTime("1/5/1997");
            r17.Address = ("332 Avenue C, Austin, TX 78733");
            r17.PhoneNumber = "9073497810";
            r17.Email = ("s.saunders@longhorncinema.com");
            var result17 = UserManager.Create(r17, "rankmary");
            UserManager.AddToRole(r17.Id, "Employee");
            db.SaveChanges();

            AppUser r18 = new AppUser();
            r18.UserName = ("w.sewell@longhorncinema.com");
            r18.LastName = ("Sewell");
            r18.FirstName = ("William");
            r18.Birthday = Convert.ToDateTime("5/25/1986");
            r18.Address = ("2365 51st St., Austin, TX 78755");
            r18.PhoneNumber = "9074510084";
            r18.Email = ("w.sewell@longhorncinema.com");
            var result18 = UserManager.Create(r18, "walkamile");
            UserManager.AddToRole(r18.Id, "Manager");
            db.SaveChanges();

            AppUser r19 = new AppUser();
            r19.UserName = ("j.mason@longhorncinema.com");
            r19.LastName = ("Mason");
            r19.FirstName = ("Jack");
            r19.Birthday = Convert.ToDateTime("6/6/1986");
            r19.Address = ("444 45th St, Austin, TX 78701");
            r19.PhoneNumber = "9018833432";
            r19.Email = ("j.mason@longhorncinema.com");
            var result19 = UserManager.Create(r19, "changalang");
            UserManager.AddToRole(r19.Id, "Employee");
            db.SaveChanges();

            AppUser r20 = new AppUser();
            r20.UserName = ("j.jackson@longhorncinema.com");
            r20.LastName = ("Jackson");
            r20.FirstName = ("Jack");
            r20.Birthday = Convert.ToDateTime("10/16/1986");
            r20.Address = ("222 Main, Austin, TX 78760");
            r20.PhoneNumber = "9075554545";
            r20.Email = ("j.jackson@longhorncinema.com");
            var result20 = UserManager.Create(r20, "offbeat");
            UserManager.AddToRole(r20.Id, "Employee");
            db.SaveChanges();

            AppUser r21 = new AppUser();
            r21.UserName = ("m.nguyen@longhorncinema.com");
            r21.LastName = ("Nguyen");
            r21.FirstName = ("Mary");
            r21.Birthday = Convert.ToDateTime("4/5/1988");
            r21.Address = ("465 N. Bear Cub, Austin, TX 78734");
            r21.PhoneNumber = "9075524141";
            r21.Email = ("m.nguyen@longhorncinema.com");
            var result21 = UserManager.Create(r21, "landus");
            UserManager.AddToRole(r21.Id, "Employee");
            db.SaveChanges();

            AppUser r22 = new AppUser();
            r22.UserName = ("s.barnes@longhorncinema.com");
            r22.LastName = ("Barnes");
            r22.FirstName = ("Susan");
            r22.Birthday = Convert.ToDateTime("2/22/1993");
            r22.Address = ("888 S. Main, Kyle, TX 78640");
            r22.PhoneNumber = "9556662323";
            r22.Email = ("s.barnes@longhorncinema.com");
            var result22 = UserManager.Create(r22, "rhythm");
            UserManager.AddToRole(r22.Id, "Employee");
            db.SaveChanges();

            AppUser r23 = new AppUser();
            r23.UserName = ("l.jones@longhorncinema.com");
            r23.LastName = ("Jones");
            r23.FirstName = ("Lester");
            r23.Birthday = Convert.ToDateTime("6/29/1996");
            r23.Address = ("999 LeBlat, Austin, TX 78747");
            r23.PhoneNumber = "9886662222";
            r23.Email = ("l.jones@longhorncinema.com");
            var result23 = UserManager.Create(r23, "kindly");
            UserManager.AddToRole(r23.Id, "Employee");
            db.SaveChanges();

            AppUser r24 = new AppUser();
            r24.UserName = ("h.garcia@longhorncinema.com");
            r24.LastName = ("Garcia");
            r24.FirstName = ("Hector");
            r24.Birthday = Convert.ToDateTime("5/13/1997");
            r24.Address = ("777 PBR Drive, Austin, TX 78712");
            r24.PhoneNumber = "9221114444";
            r24.Email = ("h.garcia@longhorncinema.com");
            var result24 = UserManager.Create(r24, "instrument");
            UserManager.AddToRole(r24.Id, "Employee");
            db.SaveChanges();

            AppUser r25 = new AppUser();
            r25.UserName = ("c.silva@longhorncinema.com");
            r25.LastName = ("Silva");
            r25.FirstName = ("Cindy");
            r25.Birthday = Convert.ToDateTime("12/29/1997");
            r25.Address = ("900 4th St, Austin, TX 78758");
            r25.PhoneNumber = "9221113333";
            r25.Email = ("c.silva@longhorncinema.com");
            var result25 = UserManager.Create(r25, "arched");
            UserManager.AddToRole(r25.Id, "Employee");
            db.SaveChanges();

            AppUser r26 = new AppUser();
            r26.UserName = ("m.lopez@longhorncinema.com");
            r26.LastName = ("Lopez");
            r26.FirstName = ("Marshall");
            r26.Birthday = Convert.ToDateTime("11/4/1996");
            r26.Address = ("90 SW North St, Austin, TX 78729");
            r26.PhoneNumber = "9234442222";
            r26.Email = ("m.lopez@longhorncinema.com");
            var result26 = UserManager.Create(r26, "median");
            UserManager.AddToRole(r26.Id, "Employee");
            db.SaveChanges();

            AppUser r27 = new AppUser();
            r27.UserName = ("b.larson@longhorncinema.com");
            r27.LastName = ("Larson");
            r27.FirstName = ("Bill");
            r27.Birthday = Convert.ToDateTime("11/14/1999");
            r27.Address = ("1212 N. First Ave, Round Rock, TX 78665");
            r27.PhoneNumber = "9795554444";
            r27.Email = ("b.larson@longhorncinema.com");
            var result27 = UserManager.Create(r27, "approval");
            UserManager.AddToRole(r27.Id, "Employee");
            db.SaveChanges();

            AppUser r28 = new AppUser();
            r28.UserName = ("s.rankin@longhorncinema.com");
            r28.LastName = ("Rankin");
            r28.FirstName = ("Suzie");
            r28.Birthday = Convert.ToDateTime("12/17/1999");
            r28.Address = ("23 Polar Bear Road, Austin, TX 78712");
            r28.PhoneNumber = "9893336666";
            r28.Email = ("s.rankin@longhorncinema.com");
            var result28 = UserManager.Create(r28, "approval");
            UserManager.AddToRole(r28.Id, "Employee");
            db.SaveChanges();




        }
    }
}