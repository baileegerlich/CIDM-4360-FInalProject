namespace FinalProject;
using System.Data;
using MySql.Data.MySqlClient;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;
class BusinessLogic
{
   

   //Business Logic
    static async Task Main(string[] args)
    {
        bool _continue = true;
        User user;
        GuiTier appGUI = new GuiTier();
        DataTier database = new DataTier();

        // start GUI
        user = appGUI.Login();

       
        if (database.LoginCheck(user)){

            while(_continue){
                int option  = appGUI.Dashboard(user);
                switch(option)
                {
                    //Send email
                    case 1:
                        DataTable tableEmail = database.Email();
                        if(tableEmail != null)
                            appGUI.DisplayResidents(tableEmail);
                             
                                // this serviceConnectionString is stored in the code diectly in this example for demo purpose
                                // it should be stored in the server when working for a business application.
                                // ref: https://learn.microsoft.com/en-us/azure/communication-services/quickstarts/create-communication-resource?tabs=windows&pivots=platform-azp#store-your-connection-string
                                string serviceConnectionString = "endpoint=https://bngerichweek10communicationservice.communication.azure.com/;accesskey=OXQEgcwnZem2uN/hnwRtaD812qx4Hn/2Njon3+M1SIUYLVEg/68JN+VUW2duJKvRSoKE4dJoJBpMkzOlOXwo9w==";
                                EmailClient emailClient = new EmailClient(serviceConnectionString);
                                var subject = "Package";
                                var emailContent = new EmailContent(subject);
                                // use Multiline String @ to design html content
                                emailContent.Html= @"
                                            <html>
                                                <body>
                                                    <h1 style=color:red>Legands Apartment Postal Services</h1>
                                                    <h4>Package Alert</h4>
                                                    <p>You are recieving this email, because your package has arrived. We can hold your package up to one month upon arrival.</p>
                                                </body>
                                            </html>";
                            // mailfrom domain of your email service on Azure
                                var sender = "DoNotReply@dc6e7e6e-0530-40b6-81df-f63e56c85ec4.azurecomm.net";

                                Console.WriteLine("Please input an email address: ");
                                string? inputEmail = Console.ReadLine();
                                var emailRecipients = new EmailRecipients(new List<EmailAddress> {
                                    new EmailAddress(inputEmail) { DisplayName = "Testing" },
                                });

                                var emailMessage = new EmailMessage(sender, emailContent, emailRecipients);

                                try
                                {
                                    SendEmailResult sendEmailResult = emailClient.Send(emailMessage);

                                    string messageId = sendEmailResult.MessageId;
                                    if (!string.IsNullOrEmpty(messageId))
                                    {
                                        Console.WriteLine($"Email sent, MessageId = {messageId}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Failed to send email.");
                                        return;
                                    }
                            // wait max 2 minutes to check the send status for mail.
                                    var cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(2));
                                    do
                                    {
                                        SendStatusResult sendStatus = emailClient.GetSendStatus(messageId);
                                        Console.WriteLine($"Send mail status for MessageId : <{messageId}>, Status: [{sendStatus.Status}]");

                                        if (sendStatus.Status != SendStatus.Queued)
                                        {
                                            break;
                                        }
                                        await Task.Delay(TimeSpan.FromSeconds(20));
                                    
                                    } while (!cancellationToken.IsCancellationRequested);

                                    if (cancellationToken.IsCancellationRequested)
                                    {
                                        Console.WriteLine($"Looks like we timed out for email");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error in sending email, {ex}");
                                }
                        break;

                    // Package History Records 
                    case 2:
                    DataTable TableShowRecords = database.ShowRecords();
                        if(TableShowRecords != null)
                            appGUI.DisplayResidents(TableShowRecords);
                        break;
                    // Log Out
                    case 3:
                        _continue = false;
                        Console.WriteLine("Log out, Goodbye.");
                        break;
                    // default: wrong input
                    default:
                        Console.WriteLine("Wrong Input");
                        break;
                }

            }
        }
        else{
                Console.WriteLine("Login Failed, Try Again.");
        }        
    }    
}
