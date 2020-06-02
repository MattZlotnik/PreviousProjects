using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sp18Team7Final.Models;
using sp18Team7Final.Migrations;
using sp18Team7Final.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace sp18Team7Final.Migrations
{
    public class Customers
    {
        public void SeedCustomers(AppDbContext db)
        {

            //create a user manager and a role manager to use for this method
            AppUserManager UserManager = new AppUserManager(new UserStore<AppUser>(db));

            //create a role manager
            AppRoleManager RoleManager = new AppRoleManager(new RoleStore<AppRole>(db));

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

            AppUser r1 = new AppUser();
            r1.UserName = ("cbaker@example.com");
            r1.LastName = ("Baker");
            r1.FirstName = ("Christopher");
            r1.Birthday = Convert.ToDateTime("11/23/1949");
            r1.Address = ("1245 Lake Anchorage Blvd., Austin, TX 78705");
            r1.PhoneNumber = "5125550180";
            r1.Email = ("cbaker@example.com");
            r1.PopcornPoints = 110;
            var result1 = UserManager.Create(r1, "hello1");
            UserManager.AddToRole(r1.Id, "Customer");
            db.SaveChanges();

            AppUser r2 = new AppUser();
            r2.UserName = ("banker@longhorn.net");
            r2.LastName = ("Banks");
            r2.FirstName = ("Michelle");
            r2.Birthday = Convert.ToDateTime("11/27/1962");
            r2.Address = ("1300 Tall Pine Lane, Austin, TX 78712");
            r2.PhoneNumber = "5125550183";
            r2.Email = ("banker@longhorn.net");
            r2.PopcornPoints = 40;
            var result2 = UserManager.Create(r2, "potato");
            UserManager.AddToRole(r2.Id, "Customer");
            db.SaveChanges();

            AppUser r3 = new AppUser();
            r3.UserName = ("franco@example.com");
            r3.LastName = ("Broccolo");
            r3.FirstName = ("Franco");
            r3.Birthday = Convert.ToDateTime("10/11/1992");
            r3.Address = ("62 Browning Road, Austin, TX 78704");
            r3.PhoneNumber = "5125550128";
            r3.Email = ("franco@example.com");
            r3.PopcornPoints = 30;
            var result3 = UserManager.Create(r3, "painting");
            UserManager.AddToRole(r3.Id, "Customer");
            db.SaveChanges();

            AppUser r4 = new AppUser();
            r4.UserName = ("wchang@example.com");
            r4.LastName = ("Chang");
            r4.FirstName = ("Wendy");
            r4.Birthday = Convert.ToDateTime("5/16/1997");
            r4.Address = ("202 Bellmont Hall, Round Rock, TX 78681");
            r4.PhoneNumber = "5125550133";
            r4.Email = ("wchang@example.com");
            r4.PopcornPoints = 0;
            var result4 = UserManager.Create(r4, "texas1");
            UserManager.AddToRole(r4.Id, "Customer");
            db.SaveChanges();

            AppUser r5 = new AppUser();
            r5.UserName = ("limchou@gogle.com");
            r5.LastName = ("Chou");
            r5.FirstName = ("Lim");
            r5.Birthday = Convert.ToDateTime("4/6/1970");
            r5.Address = ("1600 Teresa Lane, Austin, TX 78705");
            r5.PhoneNumber = "5125550102";
            r5.Email = ("limchou@gogle.com");
            r5.PopcornPoints = 40;
            var result5 = UserManager.Create(r5, "Anchorage");
            UserManager.AddToRole(r5.Id, "Customer");
            db.SaveChanges();

            AppUser r6 = new AppUser();
            r6.UserName = ("shdixon@aoll.com");
            r6.LastName = ("Dixon");
            r6.FirstName = ("Shan");
            r6.Birthday = Convert.ToDateTime("1/12/1984");
            r6.Address = ("234 Holston Circle, Austin, TX 78712");
            r6.PhoneNumber = "5125550146";
            r6.Email = ("shdixon@aoll.com");
            r6.PopcornPoints = 20;
            var result6 = UserManager.Create(r6, "pepperoni");
            UserManager.AddToRole(r6.Id, "Customer");
            db.SaveChanges();

            AppUser r7 = new AppUser();
            r7.UserName = ("j.b.evans@aheca.org");
            r7.LastName = ("Evans");
            r7.FirstName = ("Jim Bob");
            r7.Birthday = Convert.ToDateTime("9/9/1959");
            r7.Address = ("506 Farrell Circle, Georgetown, TX 78628");
            r7.PhoneNumber = "5125550170";
            r7.Email = ("j.b.evans@aheca.org");
            r7.PopcornPoints = 50;
            var result7 = UserManager.Create(r7, "longhorns");
            UserManager.AddToRole(r7.Id, "Customer");
            db.SaveChanges();

            AppUser r8 = new AppUser();
            r8.UserName = ("feeley@penguin.org");
            r8.LastName = ("Feeley");
            r8.FirstName = ("Lou Ann");
            r8.Birthday = Convert.ToDateTime("1/12/2001");
            r8.Address = ("600 S 8th Street W, Austin, TX 78746");
            r8.PhoneNumber = "5125550105";
            r8.Email = ("feeley@penguin.org");
            r8.PopcornPoints = 170;
            var result8 = UserManager.Create(r8, "aggies");
            UserManager.AddToRole(r8.Id, "Customer");
            db.SaveChanges();

            AppUser r9 = new AppUser();
            r9.UserName = ("tfreeley@minnetonka.ci.us");
            r9.LastName = ("Freeley");
            r9.FirstName = ("Tesa");
            r9.Birthday = Convert.ToDateTime("2/4/1991");
            r9.Address = ("4448 Fairview Ave., Horseshoe Bay, TX 78657");
            r9.PhoneNumber = "5125550114";
            r9.Email = ("tfreeley@minnetonka.ci.us");
            r9.PopcornPoints = 160;
            var result9 = UserManager.Create(r9, "raiders");
            UserManager.AddToRole(r9.Id, "Customer");
            db.SaveChanges();

            AppUser r10 = new AppUser();
            r10.UserName = ("mgarcia@gogle.com");
            r10.LastName = ("Garcia");
            r10.FirstName = ("Margaret");
            r10.Birthday = Convert.ToDateTime("10/2/1991");
            r10.Address = ("594 Longview, Austin, TX 78727");
            r10.PhoneNumber = "5125550155";
            r10.Email = ("mgarcia@gogle.com");
            r10.PopcornPoints = 10;
            var result10 = UserManager.Create(r10, "mustangs");
            UserManager.AddToRole(r10.Id, "Customer");
            db.SaveChanges();

            AppUser r11 = new AppUser();
            r11.UserName = ("chaley@thug.com");
            r11.LastName = ("Haley");
            r11.FirstName = ("Charles");
            r11.Birthday = Convert.ToDateTime("7/10/1974");
            r11.Address = ("One Cowboy Pkwy, Austin, TX 78712");
            r11.PhoneNumber = "5125550116";
            r11.Email = ("chaley@thug.com");
            r11.PopcornPoints = 40;
            var result11 = UserManager.Create(r11, "onetime");
            UserManager.AddToRole(r11.Id, "Customer");
            db.SaveChanges();

            AppUser r12 = new AppUser();
            r12.UserName = ("jeffh@sonic.com");
            r12.LastName = ("Hampton");
            r12.FirstName = ("Jeffrey");
            r12.Birthday = Convert.ToDateTime("3/10/2004");
            r12.Address = ("337 38th St., San Marcos, TX 78666");
            r12.PhoneNumber = "5125550150";
            r12.Email = ("jeffh@sonic.com");
            r12.PopcornPoints = 150;
            var result12 = UserManager.Create(r12, "hampton1");
            UserManager.AddToRole(r12.Id, "Customer");
            db.SaveChanges();

            AppUser r13 = new AppUser();
            r13.UserName = ("wjhearniii@umich.org");
            r13.LastName = ("Hearn");
            r13.FirstName = ("John");
            r13.Birthday = Convert.ToDateTime("8/5/1950");
            r13.Address = ("4225 North First, Austin, TX 78705");
            r13.PhoneNumber = "5125550196";
            r13.Email = ("wjhearniii@umich.org");
            r13.PopcornPoints = 0;
            var result13 = UserManager.Create(r13, "jhearn22");
            UserManager.AddToRole(r13.Id, "Customer");
            db.SaveChanges();

            AppUser r14 = new AppUser();
            r14.UserName = ("ahick@yaho.com");
            r14.LastName = ("Hicks");
            r14.FirstName = ("Anthony");
            r14.Birthday = Convert.ToDateTime("12/8/2004");
            r14.Address = ("32 NE Garden Ln., Ste 910, Austin, TX 78712");
            r14.PhoneNumber = "5125550188";
            r14.Email = ("ahick@yaho.com");
            r14.PopcornPoints = 60;
            var result14 = UserManager.Create(r14, "hickhickup");
            UserManager.AddToRole(r14.Id, "Customer");
            db.SaveChanges();

            AppUser r15 = new AppUser();
            r15.UserName = ("ingram@jack.com");
            r15.LastName = ("Ingram");
            r15.FirstName = ("Brad");
            r15.Birthday = Convert.ToDateTime("9/5/2001");
            r15.Address = ("6548 La Posada Ct., New York, NY 10101");
            r15.PhoneNumber = "5125550116";
            r15.Email = ("ingram@jack.com");
            r15.PopcornPoints = 20;
            var result15 = UserManager.Create(r15, "ingram2015");
            UserManager.AddToRole(r15.Id, "Customer");
            db.SaveChanges();

            AppUser r16 = new AppUser();
            r16.UserName = ("toddj@yourmom.com");
            r16.LastName = ("Jacobs");
            r16.FirstName = ("Todd");
            r16.Birthday = Convert.ToDateTime("1/20/1999");
            r16.Address = ("4564 Elm St., Austin, TX 78729");
            r16.PhoneNumber = "5125550166";
            r16.Email = ("toddj@yourmom.com");
            r16.PopcornPoints = 170;
            var result16 = UserManager.Create(r16, "toddy25");
            UserManager.AddToRole(r16.Id, "Customer");
            db.SaveChanges();

            AppUser r17 = new AppUser();
            r17.UserName = ("thequeen@aska.net");
            r17.LastName = ("Lawrence");
            r17.FirstName = ("Victoria");
            r17.Birthday = Convert.ToDateTime("4/14/2000");
            r17.Address = ("6639 Butterfly Ln., Beverly Hills, CA 90210");
            r17.PhoneNumber = "5125550173";
            r17.Email = ("thequeen@aska.net");
            r17.PopcornPoints = 130;
            var result17 = UserManager.Create(r17, "something");
            UserManager.AddToRole(r17.Id, "Customer");
            db.SaveChanges();

            AppUser r18 = new AppUser();
            r18.UserName = ("linebacker@gogle.com");
            r18.LastName = ("Lineback");
            r18.FirstName = ("Erik");
            r18.Birthday = Convert.ToDateTime("12/2/2003");
            r18.Address = ("1300 Netherland St, Austin, TX 78758");
            r18.PhoneNumber = "5125550167";
            r18.Email = ("linebacker@gogle.com");
            r18.PopcornPoints = 60;
            var result18 = UserManager.Create(r18, "Password1");
            UserManager.AddToRole(r18.Id, "Customer");
            db.SaveChanges();

            AppUser r19 = new AppUser();
            r19.UserName = ("elowe@netscare.net");
            r19.LastName = ("Lowe");
            r19.FirstName = ("Ernest");
            r19.Birthday = Convert.ToDateTime("12/7/1977");
            r19.Address = ("3201 Pine Drive, New Braunfels, TX 78130");
            r19.PhoneNumber = "5125550187";
            r19.Email = ("elowe@netscare.net");
            r19.PopcornPoints = 20;
            var result19 = UserManager.Create(r19, "aclfest2017");
            UserManager.AddToRole(r19.Id, "Customer");
            db.SaveChanges();

            AppUser r20 = new AppUser();
            r20.UserName = ("cluce@gogle.com");
            r20.LastName = ("Luce");
            r20.FirstName = ("Chuck");
            r20.Birthday = Convert.ToDateTime("3/16/1949");
            r20.Address = ("2345 Rolling Clouds, Cactus, TX 79013");
            r20.PhoneNumber = "5125550141";
            r20.Email = ("cluce@gogle.com");
            r20.PopcornPoints = 180;
            var result20 = UserManager.Create(r20, "nothinggood");
            UserManager.AddToRole(r20.Id, "Customer");
            db.SaveChanges();

            AppUser r21 = new AppUser();
            r21.UserName = ("mackcloud@george.com");
            r21.LastName = ("MacLeod");
            r21.FirstName = ("Jennifer");
            r21.Birthday = Convert.ToDateTime("2/21/1947");
            r21.Address = ("2504 Far West Blvd., Marble Falls, TX 78654");
            r21.PhoneNumber = "5125550185";
            r21.Email = ("mackcloud@george.com");
            r21.PopcornPoints = 170;
            var result21 = UserManager.Create(r21, "whatever");
            UserManager.AddToRole(r21.Id, "Customer");
            db.SaveChanges();

            AppUser r22 = new AppUser();
            r22.UserName = ("cmartin@beets.com");
            r22.LastName = ("Markham");
            r22.FirstName = ("Elizabeth");
            r22.Birthday = Convert.ToDateTime("3/20/1972");
            r22.Address = ("7861 Chevy Chase, Kissimmee, FL 34741");
            r22.PhoneNumber = "5125550134";
            r22.Email = ("cmartin@beets.com");
            r22.PopcornPoints = 100;
            var result22 = UserManager.Create(r22, "whocares");
            UserManager.AddToRole(r22.Id, "Customer");
            db.SaveChanges();

            AppUser r23 = new AppUser();
            r23.UserName = ("clarence@yoho.com");
            r23.LastName = ("Martin");
            r23.FirstName = ("Clarence");
            r23.Birthday = Convert.ToDateTime("7/19/1992");
            r23.Address = ("87 Alcedo St., Austin, TX 78709");
            r23.PhoneNumber = "5125550151";
            r23.Email = ("clarence@yoho.com");
            r23.PopcornPoints = 130;
            var result23 = UserManager.Create(r23, "xcellent");
            UserManager.AddToRole(r23.Id, "Customer");
            db.SaveChanges();

            AppUser r24 = new AppUser();
            r24.UserName = ("gregmartinez@drdre.com");
            r24.LastName = ("Martinez");
            r24.FirstName = ("Gregory");
            r24.Birthday = Convert.ToDateTime("5/28/1947");
            r24.Address = ("8295 Sunset Blvd., Red Rock, TX 78662");
            r24.PhoneNumber = "5125550120";
            r24.Email = ("gregmartinez@drdre.com");
            r24.PopcornPoints = 20;
            var result24 = UserManager.Create(r24, "snowsnow");
            UserManager.AddToRole(r24.Id, "Customer");
            db.SaveChanges();

            AppUser r25 = new AppUser();
            r25.UserName = ("cmiller@bob.com");
            r25.LastName = ("Miller");
            r25.FirstName = ("Charles");
            r25.Birthday = Convert.ToDateTime("10/15/1990");
            r25.Address = ("8962 Main St., South Padre Island, TX 78597");
            r25.PhoneNumber = "5125550198";
            r25.Email = ("cmiller@bob.com");
            r25.PopcornPoints = 20;
            var result25 = UserManager.Create(r25, "mydogspot");
            UserManager.AddToRole(r25.Id, "Customer");
            db.SaveChanges();

            AppUser r26 = new AppUser();
            r26.UserName = ("knelson@aoll.com");
            r26.LastName = ("Nelson");
            r26.FirstName = ("Kelly");
            r26.Birthday = Convert.ToDateTime("7/13/1971");
            r26.Address = ("2601 Red River, Disney, OK 74340");
            r26.PhoneNumber = "5125550177";
            r26.Email = ("knelson@aoll.com");
            r26.PopcornPoints = 110;
            var result26 = UserManager.Create(r26, "spotmydog");
            UserManager.AddToRole(r26.Id, "Customer");
            db.SaveChanges();

            AppUser r27 = new AppUser();
            r27.UserName = ("joewin@xfactor.com");
            r27.LastName = ("Nguyen");
            r27.FirstName = ("Joe");
            r27.Birthday = Convert.ToDateTime("3/17/1984");
            r27.Address = ("1249 4th SW St., Del Rio, TX 78841");
            r27.PhoneNumber = "5125550174";
            r27.Email = ("joewin@xfactor.com");
            r27.PopcornPoints = 150;
            var result27 = UserManager.Create(r27, "joejoejoe");
            UserManager.AddToRole(r27.Id, "Customer");
            db.SaveChanges();

            AppUser r28 = new AppUser();
            r28.UserName = ("orielly@foxnews.cnn");
            r28.LastName = ("O'Reilly");
            r28.FirstName = ("Bill");
            r28.Birthday = Convert.ToDateTime("7/8/1959");
            r28.Address = ("8800 Gringo Drive, Austin, TX 78746");
            r28.PhoneNumber = "5125550167";
            r28.Email = ("orielly@foxnews.cnn");
            r28.PopcornPoints = 190;
            var result28 = UserManager.Create(r28, "billyboy");
            UserManager.AddToRole(r28.Id, "Customer");
            db.SaveChanges();

            AppUser r29 = new AppUser();
            r29.UserName = ("ankaisrad@gogle.com");
            r29.LastName = ("Radkovich");
            r29.FirstName = ("Anka");
            r29.Birthday = Convert.ToDateTime("5/19/1966");
            r29.Address = ("1300 Elliott Pl, Austin, TX 78712");
            r29.PhoneNumber = "5125550151";
            r29.Email = ("ankaisrad@gogle.com");
            r29.PopcornPoints = 120;
            var result29 = UserManager.Create(r29, "radgirl");
            UserManager.AddToRole(r29.Id, "Customer");
            db.SaveChanges();

            AppUser r30 = new AppUser();
            r30.UserName = ("megrhodes@freserve.co.uk");
            r30.LastName = ("Rhodes");
            r30.FirstName = ("Megan");
            r30.Birthday = Convert.ToDateTime("3/12/1965");
            r30.Address = ("4587 Enfield Rd., Austin, TX 78705");
            r30.PhoneNumber = "5125550133";
            r30.Email = ("megrhodes@freserve.co.uk");
            r30.PopcornPoints = 190;
            var result30 = UserManager.Create(r30, "meganr34");
            UserManager.AddToRole(r30.Id, "Customer");
            db.SaveChanges();

            AppUser r31 = new AppUser();
            r31.UserName = ("erynrice@aoll.com");
            r31.LastName = ("Rice");
            r31.FirstName = ("Eryn");
            r31.Birthday = Convert.ToDateTime("4/28/1975");
            r31.Address = ("3405 Rio Grande, Austin, TX 78785");
            r31.PhoneNumber = "5125550196";
            r31.Email = ("erynrice@aoll.com");
            r31.PopcornPoints = 190;
            var result31 = UserManager.Create(r31, "ricearoni");
            UserManager.AddToRole(r31.Id, "Customer");
            db.SaveChanges();

            AppUser r32 = new AppUser();
            r32.UserName = ("jorge@noclue.com");
            r32.LastName = ("Rodriguez");
            r32.FirstName = ("Jorge");
            r32.Birthday = Convert.ToDateTime("12/8/1953");
            r32.Address = ("6788 Cotter Street, Littlefield, TX 79339");
            r32.PhoneNumber = "5125550141";
            r32.Email = ("jorge@noclue.com");
            r32.PopcornPoints = 20;
            var result32 = UserManager.Create(r32, "jrod2017");
            UserManager.AddToRole(r32.Id, "Customer");
            db.SaveChanges();

            AppUser r33 = new AppUser();
            r33.UserName = ("mrrogers@lovelyday.com");
            r33.LastName = ("Rogers");
            r33.FirstName = ("Allen");
            r33.Birthday = Convert.ToDateTime("4/22/1973");
            r33.Address = ("4965 Oak Hill, Austin, TX 78733");
            r33.PhoneNumber = "5125550189";
            r33.Email = ("mrrogers@lovelyday.com");
            r33.PopcornPoints = 100;
            var result33 = UserManager.Create(r33, "rogerthat");
            UserManager.AddToRole(r33.Id, "Customer");
            db.SaveChanges();

            AppUser r34 = new AppUser();
            r34.UserName = ("stjean@athome.com");
            r34.LastName = ("Saint-Jean");
            r34.FirstName = ("Olivier");
            r34.Birthday = Convert.ToDateTime("2/19/1995");
            r34.Address = ("255 Toncray Dr., Austin, TX 78755");
            r34.PhoneNumber = "5125550152";
            r34.Email = ("stjean@athome.com");
            r34.PopcornPoints = 250;
            var result34 = UserManager.Create(r34, "bunnyhop");
            UserManager.AddToRole(r34.Id, "Customer");
            db.SaveChanges();

            AppUser r35 = new AppUser();
            r35.UserName = ("saunders@pen.com");
            r35.LastName = ("Saunders");
            r35.FirstName = ("Sarah");
            r35.Birthday = Convert.ToDateTime("2/19/1978");
            r35.Address = ("332 Avenue C, Austin, TX 78701");
            r35.PhoneNumber = "5125550146";
            r35.Email = ("saunders@pen.com");
            r35.PopcornPoints = 40;
            var result35 = UserManager.Create(r35, "penguin12");
            UserManager.AddToRole(r35.Id, "Customer");
            db.SaveChanges();

            AppUser r36 = new AppUser();
            r36.UserName = ("willsheff@email.com");
            r36.LastName = ("Sewell");
            r36.FirstName = ("William");
            r36.Birthday = Convert.ToDateTime("12/23/2004");
            r36.Address = ("2365 51st St., El Paso, TX 79953");
            r36.PhoneNumber = "5125550192";
            r36.Email = ("willsheff@email.com");
            r36.PopcornPoints = 200;
            var result36 = UserManager.Create(r36, "alaskaboy");
            UserManager.AddToRole(r36.Id, "Customer");
            db.SaveChanges();

            AppUser r37 = new AppUser();
            r37.UserName = ("sheffiled@gogle.com");
            r37.LastName = ("Sheffield");
            r37.FirstName = ("Martin");
            r37.Birthday = Convert.ToDateTime("5/8/1960");
            r37.Address = ("3886 Avenue A, Balmorhea, TX 79718");
            r37.PhoneNumber = "5125550131";
            r37.Email = ("sheffiled@gogle.com");
            r37.PopcornPoints = 130;
            var result37 = UserManager.Create(r37, "martin1234");
            UserManager.AddToRole(r37.Id, "Customer");
            db.SaveChanges();

            AppUser r38 = new AppUser();
            r38.UserName = ("johnsmith187@aoll.com");
            r38.LastName = ("Smith");
            r38.FirstName = ("John");
            r38.Birthday = Convert.ToDateTime("6/25/1955");
            r38.Address = ("23 Hidden Forge Dr., Austin, TX 78760");
            r38.PhoneNumber = "5125550190";
            r38.Email = ("johnsmith187@aoll.com");
            r38.PopcornPoints = 130;
            var result38 = UserManager.Create(r38, "smitty444");
            UserManager.AddToRole(r38.Id, "Customer");
            db.SaveChanges();

            AppUser r39 = new AppUser();
            r39.UserName = ("dustroud@mail.com");
            r39.LastName = ("Stroud");
            r39.FirstName = ("Dustin");
            r39.Birthday = Convert.ToDateTime("7/26/1967");
            r39.Address = ("1212 Rita Rd, Austin, TX 78734");
            r39.PhoneNumber = "5125550157";
            r39.Email = ("dustroud@mail.com");
            r39.PopcornPoints = 90;
            var result39 = UserManager.Create(r39, "dustydusty");
            UserManager.AddToRole(r39.Id, "Customer");
            db.SaveChanges();

            AppUser r40 = new AppUser();
            r40.UserName = ("estuart@anchor.net");
            r40.LastName = ("Stuart");
            r40.FirstName = ("Eric");
            r40.Birthday = Convert.ToDateTime("12/4/1947");
            r40.Address = ("5576 Toro Ring, Kyle, TX 78640");
            r40.PhoneNumber = "5125550191";
            r40.Email = ("estuart@anchor.net");
            r40.PopcornPoints = 170;
            var result40 = UserManager.Create(r40, "stewball");
            UserManager.AddToRole(r40.Id, "Customer");
            db.SaveChanges();

            AppUser r41 = new AppUser();
            r41.UserName = ("peterstump@noclue.com");
            r41.LastName = ("Stump");
            r41.FirstName = ("Peter");
            r41.Birthday = Convert.ToDateTime("7/10/1974");
            r41.Address = ("1300 Kellen Circle, Philadelphia, PA 19123");
            r41.PhoneNumber = "5125550136";
            r41.Email = ("peterstump@noclue.com");
            r41.PopcornPoints = 50;
            var result41 = UserManager.Create(r41, "slowwind");
            UserManager.AddToRole(r41.Id, "Customer");
            db.SaveChanges();

            AppUser r42 = new AppUser();
            r42.UserName = ("jtanner@mustang.net");
            r42.LastName = ("Tanner");
            r42.FirstName = ("Jeremy");
            r42.Birthday = Convert.ToDateTime("1/11/1944");
            r42.Address = ("4347 Almstead, Austin, TX 78747");
            r42.PhoneNumber = "5125550170";
            r42.Email = ("jtanner@mustang.net");
            r42.PopcornPoints = 190;
            var result42 = UserManager.Create(r42, "tanner5454");
            UserManager.AddToRole(r42.Id, "Customer");
            db.SaveChanges();

            AppUser r43 = new AppUser();
            r43.UserName = ("taylordjay@aoll.com");
            r43.LastName = ("Taylor");
            r43.FirstName = ("Allison");
            r43.Birthday = Convert.ToDateTime("11/14/1990");
            r43.Address = ("467 Nueces St., Austin, TX 78712");
            r43.PhoneNumber = "5125550160";
            r43.Email = ("taylordjay@aoll.com");
            r43.PopcornPoints = 110;
            var result43 = UserManager.Create(r43, "allyrally");
            UserManager.AddToRole(r43.Id, "Customer");
            db.SaveChanges();

            AppUser r44 = new AppUser();
            r44.UserName = ("rtaylor@gogle.com");
            r44.LastName = ("Taylor");
            r44.FirstName = ("Rachel");
            r44.Birthday = Convert.ToDateTime("1/18/1976");
            r44.Address = ("345 Longview Dr., Austin, TX 78758");
            r44.PhoneNumber = "5125550127";
            r44.Email = ("rtaylor@gogle.com");
            r44.PopcornPoints = 160;
            var result44 = UserManager.Create(r44, "taylorbaylor");
            UserManager.AddToRole(r44.Id, "Customer");
            db.SaveChanges();

            AppUser r45 = new AppUser();
            r45.UserName = ("teefrank@noclue.com");
            r45.LastName = ("Tee");
            r45.FirstName = ("Frank");
            r45.Birthday = Convert.ToDateTime("9/6/1998");
            r45.Address = ("5590 Lavell Dr, Austin, TX 78729");
            r45.PhoneNumber = "5125550161";
            r45.Email = ("teefrank@noclue.com");
            r45.PopcornPoints = 70;
            var result45 = UserManager.Create(r45, "teeoff22");
            UserManager.AddToRole(r45.Id, "Customer");
            db.SaveChanges();

            AppUser r46 = new AppUser();
            r46.UserName = ("ctucker@alphabet.co.uk");
            r46.LastName = ("Tucker");
            r46.FirstName = ("Clent");
            r46.Birthday = Convert.ToDateTime("2/25/1943");
            r46.Address = ("312 Main St., Round Rock, TX 78665");
            r46.PhoneNumber = "5125550106";
            r46.Email = ("ctucker@alphabet.co.uk");
            r46.PopcornPoints = 150;
            var result46 = UserManager.Create(r46, "tucksack1");
            UserManager.AddToRole(r46.Id, "Customer");
            db.SaveChanges();

            AppUser r47 = new AppUser();
            r47.UserName = ("avelasco@yoho.com");
            r47.LastName = ("Velasco");
            r47.FirstName = ("Allen");
            r47.Birthday = Convert.ToDateTime("9/10/1985");
            r47.Address = ("679 W. 4th, Cedar Park, TX 78613");
            r47.PhoneNumber = "5125550170";
            r47.Email = ("avelasco@yoho.com");
            r47.PopcornPoints = 0;
            var result47 = UserManager.Create(r47, "meow88");
            UserManager.AddToRole(r47.Id, "Customer");
            db.SaveChanges();

            AppUser r48 = new AppUser();
            r48.UserName = ("vinovino@grapes.com");
            r48.LastName = ("Vino");
            r48.FirstName = ("Janet");
            r48.Birthday = Convert.ToDateTime("2/7/1985");
            r48.Address = ("189 Grape Road, Lockhart, TX 78644");
            r48.PhoneNumber = "5125550128";
            r48.Email = ("vinovino@grapes.com");
            r48.PopcornPoints = 160;
            var result48 = UserManager.Create(r48, "vinovino");
            UserManager.AddToRole(r48.Id, "Customer");
            db.SaveChanges();

            AppUser r49 = new AppUser();
            r49.UserName = ("westj@pioneer.net");
            r49.LastName = ("West");
            r49.FirstName = ("Jake");
            r49.Birthday = Convert.ToDateTime("1/9/1976");
            r49.Address = ("RR 3287, Austin, TX 78705");
            r49.PhoneNumber = "2025550170";
            r49.Email = ("westj@pioneer.net");
            r49.PopcornPoints = 70;
            var result49 = UserManager.Create(r49, "gowest");
            UserManager.AddToRole(r49.Id, "Customer");
            db.SaveChanges();

            AppUser r50 = new AppUser();
            r50.UserName = ("winner@hootmail.com");
            r50.LastName = ("Winthorpe");
            r50.FirstName = ("Louis");
            r50.Birthday = Convert.ToDateTime("4/19/1953");
            r50.Address = ("2500 Padre Blvd, Austin, TX 78747");
            r50.PhoneNumber = "2025550141";
            r50.Email = ("winner@hootmail.com");
            r50.PopcornPoints = 150;
            var result50 = UserManager.Create(r50, "louielouie");
            UserManager.AddToRole(r50.Id, "Customer");
            db.SaveChanges();

            AppUser r51 = new AppUser();
            r51.UserName = ("rwood@voyager.net");
            r51.LastName = ("Wood");
            r51.FirstName = ("Reagan");
            r51.Birthday = Convert.ToDateTime("12/28/2002");
            r51.Address = ("447 Westlake Dr., Austin, TX 78753");
            r51.PhoneNumber = "2025550128";
            r51.Email = ("rwood@voyager.net");
            r51.PopcornPoints = 20;
            var result51 = UserManager.Create(r51, "louielouie");
            UserManager.AddToRole(r51.Id, "Customer");
            db.SaveChanges();







        }
    }
}