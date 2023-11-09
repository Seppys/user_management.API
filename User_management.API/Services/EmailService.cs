using Newtonsoft.Json;

namespace User_management.API.Services
{
    public class EmailService
    {
        public static async void SendEmail(string email, string subject, string body)
        {
            string emailSenderApiUrl = "https://notificatorapi.onrender.com/api/email/send";

            var data = new
            {
                address = email,
                subject = subject,
                body = body
            };

            using (HttpClient client = new HttpClient()) 
            {
                var content = new StringContent(JsonConvert.SerializeObject(data), 
                    System.Text.Encoding.UTF8, "application/json");

                await client.PostAsync(emailSenderApiUrl, content);
            }
        }
    }
}
