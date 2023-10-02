using MessagePack;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DOTNetCoreADONETFramworkDemo.Models
{
    public class EmployeeDataAccessLayer
    {
      
        string connectionString = "Server=SANDIPP;Database=DotNetCoreMVCDemo.Data;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true";
        //To View all Employee details      
        public IEnumerable<Employee> GetAllEmployee()
        {
            List<Employee> empList = new List<Employee>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Employee emp = new Employee();

                    emp.Id = Guid.Parse(rdr["Id"].ToString());
                    emp.Name = rdr["Name"].ToString();
                    emp.City = rdr["City"].ToString();

                    empList.Add(emp);
                }
                con.Close();
            }
            return empList;
        }

        //To Add new Employee record      
        public void AddEmployee(Employee Employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddemployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", Employee.Name);
                cmd.Parameters.AddWithValue("@City", Employee.City);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //To Update the records of a individual Employee    
        public void UpdateEmployee(Employee Employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateemployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Employee.Id);
                cmd.Parameters.AddWithValue("@Name", Employee.Name);
                cmd.Parameters.AddWithValue("@City", Employee.City);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Get the details of a individual Employee    
        public Employee GetEmployeeData(Guid? id)
        {
            Employee Employee = new Employee();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM employee WHERE id= '" + id + "'";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText=sqlQuery;
                cmd.Connection = con;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Employee.Id = Guid.Parse(rdr["Id"].ToString());
                    Employee.Name = rdr["Name"].ToString();
                    Employee.City = rdr["City"].ToString();
                }
                con.Close();
                
            }
            return Employee;
        }

        //To Delete the record on a particular Employee    
        public void DeleteEmployee(Guid? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteemployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
