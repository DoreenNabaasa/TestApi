using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestApi.Controllers
{
    public class CustomerController : ApiController
    {
        public string Post(Customer emp)
        {
            try
            {
                DataTable table = new DataTable();

             string query = @"
                                insert into CustomerDetails
                                    (FullName, PhoneNo, Email, Password)
                                values(
                                    '" + emp.FullName + @"',
                                    " + emp.PhoneNo + @",
                                    '" + emp.Email + @"',
                                    '" + emp.Password + @"'
                                
                                      )
                                ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added Successfully";
            }
            catch (Exception ex)
            {
                return "Failed to Add " + ex;
            }
        }



        //GET METHOD FOR PERSONALIZATION
        /*public HttpResponseMessage Get(Employee emp)
        {
            //emp.customerID= "doryn@gmail.com";
            string id = "doryn@gmail.com";
            DataTable table = new DataTable();

            string query = @"
                             select ProductID, DocumentType, ProductName, StartDate, EndDate, ReminderDate,Description from BillDetails
                             where BillDetails.CustomerID= '" + myId+"'" ;
          

            string query = @"
                               select ProductID, DocumentType, ProductName, StartDate, EndDate, ReminderDate,Description from BillDetails where BillDetails.CustomerID ='" 
                               + id + @"'";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }*/
    }
}
