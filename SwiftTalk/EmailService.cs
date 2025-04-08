using MimeKit;
using MailKit.Net.Smtp;
using System;

namespace SwiftTalk
{
    public class EmailService
    {
        private readonly string _smtpHost = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "myswifttalk@gmail.com";
        private readonly string _smtpPass = "yith qood zocb lxcl";

        public string SendVerificationCode(string email)
        {
            try
            {
                var code = new Random().Next(100000, 999999).ToString();

                var mailMessage = new MimeMessage();
                mailMessage.From.Add(new MailboxAddress("SwiftTalk", _smtpUser));
                mailMessage.To.Add(new MailboxAddress("", email));
                mailMessage.Subject = "Код для регистрации в SwiftTalk";

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $@"
                        <html>
                        <body style='font-family: Arial, sans-serif; color: #333;'>
                        <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                        <div style='text-align: center;'>
                        <h1 style='color: #4A90E2;'>SwiftTalk</h1>
                        </div>
                        <p>Здравствуйте,</p>
                        <p>Чтобы активировать ваш новый аккаунт SwiftTalk, скопируйте и вставьте этот 6-значный код подтверждения в приложение:</p>
                        <div style='text-align: center; background-color: #f5f5f5; padding: 20px; margin: 20px 0; border-radius: 5px;'>
                        <h2 style='font-size: 36px; margin: 0; letter-spacing: 5px;'>{code}</h2>
                        </div>
                        <p>Если вы не можете найти, куда ввести код, пожалуйста, свяжитесь с нами.</p>
                        <p style='margin-top: 40px;'>Команда SwiftTalk</p>
                        <p style='font-size: 12px; color: #777;'>Нужна помощь? <a href='mailto:support@swifttalk.com' style='color: #4A90E2;'>Свяжитесь с нами</a>.</p>
                        </div>
                        </body>
                        </html>"
                };
                mailMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(_smtpHost, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(_smtpUser, _smtpPass);
                    client.Send(mailMessage);
                    client.Disconnect(true);
                }

                return code;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при отправке email: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
            }
        }
    }
}