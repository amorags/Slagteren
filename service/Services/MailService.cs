using infrastructure.DataModels;
using MailKit.Net.Smtp;
using MimeKit;

namespace service.Services;

public class MailService
{
    public void SendMail(User user)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Din Slagter", "DinSlagterOrganisation"));

            message.To.Add(new MailboxAddress(user.FirstName, user.Email));

            message.Subject = "Velkomstbrev fra Din Slagter";

            var body = new TextPart("plain")
            {
                Text = "Hej !!" + user.FirstName +
                       ".Din konto er nu oprettet hos Din Slagter, og du har nu fået adgang til alle de gode tilbud i vores webshop ."
            };

            message.Body = body;

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);

                client.Authenticate(Environment.GetEnvironmentVariable("fromemail"),
                    Environment.GetEnvironmentVariable("frompass"));
                client.Send(message);
                client.Disconnect(true);

            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to send" + e.Message);
            throw new Exception("Could not send email");
        }
    }

}