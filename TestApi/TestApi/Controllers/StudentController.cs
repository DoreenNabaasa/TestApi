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
    
    public class StudentController : ApiController
    {
       
        // GET api/<controller>
        /*public IEnumerable<Employee> Get()
        {
            using(EmployeeDBEntities enti = new EmployeeDBEntities())
            {
                return enti.Employees.ToList();
            }
        }*/

        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"
                             select * from Employees
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


        public string Post(Employee emp)
            {
            try
            {
                DataTable table = new DataTable();
                
                string query =
                    $"insert into BillDetails(DocumentType, ProductName, StartDate, EndDate, ReminderDate, Description) values(" +
                    $"'{emp.DocumentType}'," +
                    $"'{emp.ProductName}'," +
                    $"'{emp.StartDate}',"+
                    $"'{emp.EndDate}'," +
                    $"'{emp.ReminderDate}',"+
                    $"'{emp.Description}')";

               

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
            /*try
            {
                DataTable table = new DataTable();
            string query = @"
                                insert into Employees
                                    (EmployeeName, Department, MailID)
                                values(
                                    '" + emp.EmployeeName + @"',
                                '" + emp.Department + @"',
                                '" + emp.MailID + @"'
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
            catch(Exception ex)
            {
                return "Failed to Add " +ex;
            }*/
        }
            /*
            // GET api/<controller>/5
            public string Get(int id)
            {
                return "value";
            }

            // POST api/<controller>
            public void Post([FromBody]string value)
            {
            }

            // PUT api/<controller>/5
            public void Put(int id, [FromBody]string value)
            {
            }

            // DELETE api/<controller>/5
            public void Delete(int id)
            {
            }*/
        }
}