using MimeKit;
using MailKit.Net.Smtp;
using TechnicalTest.Interfaces;

namespace TechnicalTest.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendLockoutEmailAsync(string email, string fullName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sistema de Seguridad", _configuration["SmtpSettings:Username"]!));
            message.To.Add(new MailboxAddress(fullName, email));
            message.Subject = "Notificación de Bloqueo de Cuenta";

            message.Body = new TextPart("html")
            {
                Text = $@"<h1>Hola, {fullName}</h1>
                     <p>Te informamos que tu cuenta ha sido bloqueada temporalmente por 15 minutos 
                     debido a 5 intentos fallidos de inicio de sesión.</p>"
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(
                _configuration["SmtpSettings:Host"],
                int.Parse(_configuration["SmtpSettings:Port"]!),
                MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(
                _configuration["SmtpSettings:Username"],
                _configuration["SmtpSettings:Password"]
            );
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
