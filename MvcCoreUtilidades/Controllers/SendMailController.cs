using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace MvcCoreUtilidades.Controllers
{
   

    public class SendMailController : Controller
    {
        private IConfiguration configuration;
        public SendMailController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // GET: SendMailController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string to,string subject,string body)
        {
            string user = this.configuration.GetValue<string>("MailSettings:Credentials:User");
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(user);
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;

            //RECUPERAMOS LOS DATOS
            string password = this.configuration.GetValue<string>("MailSettings:Credentials:Password");
            string host = this.configuration.GetValue<string>("MailSettings:Smtp:Host");
            int port = this.configuration.GetValue<int>("MailSettings:Smtp:Port"); 
            bool ssl = this.configuration.GetValue<bool>("MailSettings:Smtp:Ssl");
            bool defaultCredentials = this.configuration.GetValue<bool>("MailSettings:Smtp:DefaultCredentials");
            SmtpClient client = new SmtpClient();
            client.Host = host;
            client.Port = port;
            client.EnableSsl = ssl;
            client.UseDefaultCredentials = defaultCredentials;
            NetworkCredential credentials = new NetworkCredential(user, password);
            client.Credentials = credentials;
            await client.SendMailAsync(mail);
            ViewData["MENSAJE"] = "Cuerpo : " + body;

            return View();
        }

        

    }
}
