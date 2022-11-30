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
                    // 
                    case 1:
                        DataTable tableResidents = database.CheckEnrollment(user);
                        if(tableResidents != null)
                            appGUI.DisplayResidents(tableResidents);
                        break;
                    // Add A Course  
                    case 2:
                    DataTable TableShowCourse = database.ShowCourse();
                        if(TableShowCourse !=null)
                            appGUI.DisplayShowCourses(TableShowCourse);
                            database.AddCourse(user);
                            tableResidents = database.CheckEnrollment(user);
                        if(tableResidents !=null)
                            appGUI.DisplayResidents(tableResidents);
                        break;
                    // Drop A Course
                    case 3:
                        tableEnrollment = database.CheckEnrollment(user);
                        if(tableEnrollment != null)
                        appGUI.DisplayEnrollment(tableEnrollment);
                        database.DropCourse(user);
                        tableEnrollment = database.CheckEnrollment(user);
                        if(tableEnrollment != null);
                        appGUI.DisplayEnrollment(tableEnrollment!);
                        break;
                    // Log Out
                    case 4:
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
