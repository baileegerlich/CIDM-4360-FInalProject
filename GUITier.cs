namespace FinalProject;
using System.Data;
using MySql.Data.MySqlClient;

class GuiTier{
    User user = new User();
    DataTier database = new DataTier();

    // print login page
     public User Login(){
        Console.WriteLine("------Welcome to Course Management System------");
        Console.WriteLine("Please input user ID (StudentID): ");
        user.userID = Convert.ToInt16(Console.ReadLine());
        Console.WriteLine("Please input password: ");
        user.userPassword = Console.ReadLine();
        return user;
    }

    // print Dashboard after user logs in successfully
    public int Dashboard(User user){
        DateTime localDate = DateTime.Now;
        Console.WriteLine("---------------User Dashboard-------------------");
        Console.WriteLine($"Hello: {user.userID}");
        Console.WriteLine("Please select an option to continue:");
        Console.WriteLine("1. Add New Package");
        Console.WriteLine("2. Send Email");
        Console.WriteLine("3. Update Package Status");
        Console.WriteLine("4. Show Package Records");
        Console.WriteLine("5. Log Out");
        int option = Convert.ToInt16(Console.ReadLine());
        return option;
    }

    // Add new package records returned from database
    public void DisplayEnrollment(DataTable tableResidents){
        Console.WriteLine("---------------Resident List-------------------");
        foreach(DataRow row in tableResidents.Rows){
           Console.WriteLine($"Resident ID: {row["id"]} \t Full Name: {row["full_name"]} \t Email:{row["email"]} \t Unit Number:{row["unit_number"]}");
        }
    }

    // display courses
    public void DisplayShowCourses(DataTable TableShowCourse){
        Console.WriteLine("---------------Course List-------------------");
        foreach(DataRow row in TableShowCourse.Rows){
            Console.WriteLine($"courseID: {row["courseID"]} \t courseName: {row["courseName"]}");
        }
    }
}
