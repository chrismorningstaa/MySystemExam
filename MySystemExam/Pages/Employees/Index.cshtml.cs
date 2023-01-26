using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MySystemExam.Pages.Employees
{
    public class IndexModel : PageModel
    {
        public List<EmployeeInfo> listEmployees = new List<EmployeeInfo>();

        public void OnGet()
        {  
            try
            {
                String connectionString = "Data Source=LAPTOP-BOM23NO2\\SQLEXPRESS;User ID=sa;Password=1;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM employees";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EmployeeInfo employeeInfo = new EmployeeInfo();
                                employeeInfo.id = "" + reader.GetInt32(0);
                                employeeInfo.firstname = reader.GetString(1);
                                employeeInfo.lastname = reader.GetString(2);
                                employeeInfo.gender = reader.GetString(3);
                                employeeInfo.birthday = reader.GetDateTime(4).ToString();
                                employeeInfo.position = reader.GetString(5);
                                employeeInfo.created_at = reader.GetDateTime(6).ToString();

                                listEmployees.Add(employeeInfo);
                            }
                        }
                    }
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine("Exeption : " + ex.ToString()); 
            }
        }
    }
    public class EmployeeInfo
    {
        public String id;
        public String firstname;
        public String lastname;
        public String gender;
        public String birthday;
        public String position;
        public String created_at;

    }
}
