using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MySystemExam.Pages.Employees
{
    public class CreateModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            employeeInfo.firstname = Request.Form["firstname"];
            employeeInfo.lastname = Request.Form["lastname"];
            employeeInfo.gender = Request.Form["gender"];
            employeeInfo.birthday = Request.Form["birthday"];
            employeeInfo.position = Request.Form["position"];

            if (employeeInfo.firstname.Length == 0 || employeeInfo.lastname.Length == 0 ||
                employeeInfo.gender.Length == 0 || employeeInfo.birthday.Length == 0 || employeeInfo.position.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }
                // saving new Employee in Database

            try
            {
                String connectionString = "Data Source=LAPTOP-BOM23NO2\\SQLEXPRESS;User ID=sa;Password=1;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO employees" +
                                 "(firstname, lastname, gender, birthday, position) VALUES " +
                                 "(@firstname, @lastname, @gender, @birthday, @position);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@firstname", employeeInfo.firstname);
                        command.Parameters.AddWithValue("@lastname", employeeInfo.lastname);
                        command.Parameters.AddWithValue("@gender", employeeInfo.gender);
                        command.Parameters.AddWithValue("@birthday", employeeInfo.birthday);
                        command.Parameters.AddWithValue("@position", employeeInfo.position);

                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            employeeInfo.firstname = ""; employeeInfo.lastname = ""; employeeInfo.gender = ""; employeeInfo.birthday = ""; employeeInfo.position = "";
            successMessage = "New Employee Added Correctly";

            Response.Redirect("Index");

        }
    }
}
