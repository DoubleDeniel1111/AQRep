using System.Net.Mail;
using System.Net;
using ProjectAnswersAndQuestions.Models;

namespace ProjectAnswersAndQuestions.Services
{
    public class VerifEmailService
    {
        public void SendConfirmationEmail(UserRegistration userRegistration)
        {
          
            var from = new MailAddress("xaqcompx@gmail.com", "AQ");
            var to = new MailAddress(userRegistration.Email);
            var mail = new MailMessage(from, to);
            mail.Subject = "Подтверждение данных";
            mail.Body = $"Добро пожаловать! " +
                $"Ваши данные для входа:\n" +
                $"- Почта: {userRegistration.Email};\n" +
                $"- Пароль: {userRegistration.UserPassword}.";
            mail.IsBodyHtml = true;


            SmtpClient smtp = new SmtpClient("smtp.gmail.com",587);
            smtp.Credentials = new NetworkCredential("xaqcompx@gmail.com", "jxlcgexknowrrger");         
            smtp.EnableSsl = true;
          
            smtp.Send(mail);
        }
    }
}