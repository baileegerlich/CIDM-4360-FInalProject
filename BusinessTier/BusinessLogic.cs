namespace FinalProject;
using System.Data;
using MySql.Data.MySqlClient;
class BusinessLogic
{
   
    static void Main(string[] args)
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
                        break;
                    // Records  
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
                Console.WriteLine("Login Failed, Goodbye.");
        }        
    }    
}
