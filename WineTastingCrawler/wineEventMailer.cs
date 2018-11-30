using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace WineTastingCrawler
{
    public class WineEventMailer
    {
        public void SendMail(WineTastingResult wineEvent, List<string> addresses)
        {
            MailMessage mail = new MailMessage("you@yourcompany.com", "user@hotmail.com");
            foreach(var email in addresses)
            {
                mail.To.Add(email);
            }

            SmtpClient client = new SmtpClient
            {
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com"
            };

            var builder = new StringBuilder();
            builder.AppendLine($"There is wine tasting on {wineEvent.Time.ToLongDateString()} at {wineEvent.Time.ToLongTimeString()}");
            builder.AppendLine($"Tema/Title: {wineEvent.Title} and price: {wineEvent.Price}");
            builder.AppendLine($"Link to the event: {wineEvent.Link}");
            builder.Append("Vi ses til arrangementet!");
            mail.Subject = "Nyt vinarrangement";
            mail.Body = builder.ToString();
            //client.Send(mail);
        }
    }
}
