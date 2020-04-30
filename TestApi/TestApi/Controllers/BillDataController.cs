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
    public class BillDataController : ApiController
    {
       
        //GET METHOD TO RETRIEVE ALL THE INFO

        public HttpResponseMessage Get(Employee id)
        {
            DataTable table = new DataTable();

            string query = @"
                             select ProductID, DocumentType, ProductName, StartDate, EndDate, ReminderDate,Description from BillDetails
                              ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        //POST METHOD
        public string Post(Employee emp)
        {
            try
            {
                DataTable table = new DataTable();

                string query2 =
                    $"insert into BillDetails(DocumentType, ProductName, StartDate, EndDate, ReminderDate, Description) values(" +
                    $"'{emp.DocumentType}'," +
                    $"'{emp.ProductName}'," +
                    $"'{emp.StartDate}'," +
                    $"'{emp.EndDate}'," +
                    $"'{emp.ReminderDate}'," +
                    $"'{emp.Description}')";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
                using (var cmd = new SqlCommand(query2, con))
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

        //PUT OR UPDATE METHOD
        public string put(Employee emp)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                            update dbo.BillDetails set ProductName ='" + emp.ProductName + @"',
                                                       StartDate = '" + emp.StartDate + @"',
                                                       EndDate ='" + emp.EndDate + @"',
                                                       ReminderDate = '" + emp.ReminderDate + @"',
                                                       Description = '" + emp.Description + @"'
                            where ProductID =" + emp.ProductID + @"
                            ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Updated Successfully";
            }
            catch
            {
                return "Failed to Update";
            }
        }

        //DELETE METHOD
        public string delete(int id)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                            delete from dbo.BillDetails
                            where ProductID =" + id
                            ;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "deleted Successfully";
            }
            catch
            {
                return "Failed to delete";
            }
        }
    }
}
