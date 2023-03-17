using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Threading.Tasks;
using B3_C1.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using QuickMailer;
using System;

namespace B3_C1.Controllers
{
    public class Send_MailController : Controller
    {
        private readonly ISendMailService _emailSender;

        public Send_MailController(ISendMailService emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Sendmail(EmailViewModel model)
        {
            await _emailSender.SendEmailAsync(model.ToEmail, model.Subject, model.Body);
            return View();
        }




        //----------------------------------------------------------
        public IActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendEmail(EmailViewModel model)
        {
            string mgs = "Fail";

            try
            {
                Email email = new Email();
                bool isSend = email.SendEmail(model.ToEmail, Credential.Email, Credential.Password, model.Subject, model.Body);
                if (isSend)
                {
                    mgs = "Success.";
                }
                ViewBag.Mgs = mgs;
                return View();
            }
            catch (Exception e)
            {
                mgs = "Failed.";
                ViewBag.Mgs = mgs;
                return View();
            }
        }


        //[HttpPost]
        //public IActionResult SendEmail(EmailViewModel model)
        //{
        //    GuiMail(model.ToEmail,model.Subject,model.Body);
        //    return View(model);
        //}

        //public void GuiMail(string to,string subject,string body)
        //{
        //     string fromEmail = "jinbi2k1@gmail.com";
        //     string password = "Myfunction!@";

        //    MailMessage message = new MailMessage();
        //    message.From = new MailAddress(fromEmail);
        //    message.Subject = subject;
        //    message.To.Add(new MailAddress(to));
        //    message.Body = body;
        //    message.IsBodyHtml = true;

        //    var smtpClient = new SmtpClient()
        //    {
        //        Host = "smtp.gmail.com",
        //        Port = 587,// cổng kết nối smtp
        //        Credentials = new NetworkCredential(fromEmail,password),
        //        EnableSsl = true, // mã hóa dữ liệu khi gửi mail
        //        DeliveryMethod = SmtpDeliveryMethod.Network,//Đặt phthuc gửi mail là Network
        //        UseDefaultCredentials= false,


        //    };
        //    smtpClient.Send(message);

        //}

        public static class Credential
        {
            public static string Email = "jinbi2k1@gmail.com";
            public static string Password = "Myfunction!@";
        }

        //public ActionResult Send()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Send(EmailViewModel model)
        //{
        //    try
        //    {
        //        Email email = new Email();
        //        email.SendEmail(model.ToEmail, model.Email, model.Password, model.Subject, model.Body);
        //        return View();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }

        //}


    }
}
