using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI.WebControls;

namespace LogisticsProject.Controllers
{
    public class Mail
    {
        public Mail(string AdresatImie, string Adres)
        {
            this.AdresatImie = AdresatImie;
            this.Adres = Adres;
        }
        public string AdresatImie { get; set; }
        public string Adres { get; set; }
        public void SendConfirmationMail()
        {
            var message = new MailMessage();
            message.From = new MailAddress("logisticdbproject@gmail.com", "Logistic Project");
            message.To.Add(new MailAddress(Adres));
            message.Subject = "Akceptacja zlecenia - Logistic";
            message.IsBodyHtml = true;
            message.Body = "<h4>Witaj<i> " + AdresatImie + "</i>," +
                           "<br/><br/>Twoje zlecenie zostało zaakceptowane i przyjęte do realizacji" +
                           "<br/><br/>Pozdrawiamy, " +
                           "<br/><i>Logistic Project</i></h4>" +
                           "<br/><br/><br/><i><h5>Firma Koksów<br/> Poznań, osiedle Koksów 69 / 666 <br/>Telefon: 666 000 666</h5></i>";
            var smtp = new SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("logisticdbproject@gmail.com", "logistic");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Send(message);
        }



        // odrzucenie

            public void SendDeclineMail()
            {
                var message = new MailMessage();
                message.From = new MailAddress("logisticdbproject@gmail.com", "Logistic Project");
                message.To.Add(new MailAddress(Adres));
                message.Subject = "Odrzucenie zlecenia - Logistic";
                message.IsBodyHtml = true;
                message.Body = "<h4>Witaj<i> " + AdresatImie + "</i>," +
                               "<br/><br/>Twoje zlecenie zostało odrzucone. W celu poznania szczegółów dzwoń na nr: 601 521 523" +
                               "<br/><br/>Pozdrawiamy, " +
                               "<br/><i>Logistic Project</i></h4>" +
                               "<br/><br/><br/><i><h5>Firma Koksów<br/> Poznań, osiedle Koksów 69 / 666 <br/>Telefon: 666 000 666</h5></i>";
                var smtp = new SmtpClient("smtp.gmail.com");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("logisticdbproject@gmail.com", "logistic");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(message);
            }
        }
    }
