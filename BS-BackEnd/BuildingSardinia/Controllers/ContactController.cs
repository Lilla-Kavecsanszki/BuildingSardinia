using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Threading.Tasks;
using BuildingSardinia.Models;

namespace BuildingSardinia.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendContactForm(ContactForm form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(form.Email),
                        Subject = form.Subject,
                        Body = $"Name: {form.Name}\nEmail: {form.Email}\nSubject: {form.Subject}\nMessage: {form.Message}",
                        IsBodyHtml = false
                    };

                    mailMessage.To.Add("manager@buildingsardinia.com");

                    using (var smtpClient = new SmtpClient("smtp.your-email-provider.com"))
                    {
                        smtpClient.Port = 587; // Use the port your email provider uses
                        smtpClient.Credentials = new System.Net.NetworkCredential("your-username", "your-password");
                        smtpClient.EnableSsl = true;
                        await smtpClient.SendMailAsync(mailMessage);
                    }

                    ViewBag.Message = "Your message has been sent successfully.";
                    return View("Contact");
                }
                catch
                {
                    // Handle error without using the exception variable
                    ViewBag.Message = "There was an error sending your message.";
                }
            }

            return View("Contact", form);
        }
    }
}
