using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace projectC.Mail
{
    public class MailProduct
    {
        public static void PurchaseMail(string email, string Productname)
        {
            try
            {
                // Alibaby account
                var credentials = new NetworkCredential("ProjectCalibaby@gmail.com", "aliwalibi666");

                var mail = new MailMessage()
                {
                    From = new MailAddress(email),
                    Subject = Productname,
                    Body = "Bedankt voor het kopen van " + Productname + ". Deze mail is verstuurd ter bevestiging van uw aankoop."
                };

                mail.To.Add(new MailAddress(email));
                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in sending email: " + ex.Message);
                //Console.ReadKey();
                return;
            }
        }
    }
    public class MailRegister
    {
        public static void RegisterMail(string email, string firstname, string lastname)
        {
            try
            {
                // Alibaby account
                var credentials = new NetworkCredential("ProjectCalibaby@gmail.com", "aliwalibi666");

                var mail = new MailMessage()
                {
                    From = new MailAddress(email),
                    Subject = "Registratie succesvol",
                    Body = "Beste " + firstname + " " + lastname + ",\n" + "Bedankt voor het registreren bij onze studentenshop."
                };

                mail.To.Add(new MailAddress(email));
                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in sending email: " + ex.Message);
                //Console.ReadKey();
                return;
            }
        }
    }
}
