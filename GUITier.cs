namespace FinalProject;
using System.Data;
using MySql.Data.MySqlClient;
class GuiTier{
    User user = new User();
    DataTier database = new DataTier();

    // print login page
     public User Login(){
        Console.WriteLine("------Package Management System------");
        Console.WriteLine("Please Enter Username: ");
        user.userID = Console.ReadLine()!;
        Console.WriteLine("Please Enter password: ");
        user.userPassword = Console.ReadLine()!;
        return user;
    }

    // print Dashboard after user logs in successfully
    public int Dashboard(User user){
        DateTime localDate = DateTime.Now;
        Console.WriteLine("---------------User Dashboard-------------------");
        Console.WriteLine("Please select an option to continue:");
        Console.WriteLine("1. Send Email to Resident");
        Console.WriteLine("2. Show Package records");
        Console.WriteLine("3. Log Out");
        int option = Convert.ToInt16(Console.ReadLine());
        return option;
    }

    // Add new package records returned from database
    public void DisplayResidents(DataTable tableResidents){
        Console.WriteLine("---------------Package Record List-------------------");
        foreach(DataRow row in tableResidents.Rows){
           Console.WriteLine($"Resident ID: {row["id"]} \t Full Name: {row["full_name"]} \t Email:{row["email"]} \t Unit Number:{row["unit_number"]}");
        }
    }
    public void DisplayEmail(DataTable tableEmail){
        Console.WriteLine("---------------List-------------------");
        foreach(DataRow row in tableEmail.Rows){
           Console.WriteLine($"Package ID: {row["packageID"]} \t Agency: {row["agency"]} \t Status:{row["pStatus"]}");
        }
    }

}
