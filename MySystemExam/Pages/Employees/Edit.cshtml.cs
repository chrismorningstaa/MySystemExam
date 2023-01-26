using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MySystemExam.Pages.Employees
{
    public class EditModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            employeeInfo.id = Request.Query["id"];
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=LAPTOP-BOM23NO2\\SQLEXPRESS;User ID=sa;Password=1;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM employees WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
              
                                employeeInfo.firstname = reader.GetString(1);
                                employeeInfo.lastname = reader.GetString(2);
                                employeeInfo.gender = reader.GetString(3);
                                employeeInfo.birthday = reader.GetDateTime(4).ToString("yyyy-MM-dd");
                                employeeInfo.position = reader.GetString(5);
                  
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            employeeInfo.id = Request.Form["id"];
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

            try
            {
                String connectionString = "Data Source=LAPTOP-BOM23NO2\\SQLEXPRESS;User ID=sa;Password=1;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE employees " +
                                 "SET firstname=@firstname, lastname=@lastname, gender=@gender, birthday=@birthday, position=@position " + 
                                 "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@firstname", employeeInfo.firstname);
                        command.Parameters.AddWithValue("@lastname", employeeInfo.lastname);
                        command.Parameters.AddWithValue("@gender", employeeInfo.gender);
                        command.Parameters.AddWithValue("@birthday", employeeInfo.birthday);
                        command.Parameters.AddWithValue("@position", employeeInfo.position);
                        command.Parameters.AddWithValue("@id", employeeInfo.id);

                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("Index");
        }
    }
}
