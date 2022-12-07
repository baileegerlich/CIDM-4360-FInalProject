namespace FinalProject;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
class DataTier{
    public string connStr = "server=20.172.0.16;user=bngerlich2;database=bngerlich2;port=8080;password=bngerlich2";

    // perform login check using Stored Procedure "LoginCount" in Database based on given user' studentID and Password
    public bool LoginCheck(User user){
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {  
            conn.Open(); // connect to database
            string procedure = "LoginCount"; //run the login procedure
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure; // set the commandType as storedProcedure
            cmd.Parameters.AddWithValue("@inputUserID", user.userID);
            cmd.Parameters.AddWithValue("@inputUserPassword", user.userPassword);
            cmd.Parameters.Add("@userCount", MySqlDbType.Int32).Direction =  ParameterDirection.Output;
            MySqlDataReader rdr = cmd.ExecuteReader();
           
            int returnCount = (int) cmd.Parameters["@userCount"].Value;
            rdr.Close();
            conn.Close();

            if (returnCount ==1){
                return true;
            }
            else{
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return false;
        }
       
    }
    
    // show package history records
    public DataTable ShowRecords(){
        MySqlConnection conn = new MySqlConnection(connStr);
        
        try
        {  
            conn.Open();
            string procedure = "ShowRecords";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            // cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rdr = cmd.ExecuteReader();

            DataTable TablePackageRecord = new DataTable();
            TablePackageRecord.Load(rdr);
            rdr.Close();
            conn.Close();
            return TablePackageRecord;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return null!;
        }
    }
    public DataTable Email(){
        MySqlConnection conn = new MySqlConnection(connStr);
        
        try
        {  
            conn.Open();
            string procedure = "Email";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rdr = cmd.ExecuteReader();

            DataTable TableEmail = new DataTable();
            TableEmail.Load(rdr);
            rdr.Close();
            conn.Close();
            return TableEmail;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return null!;
        }
    }
}
