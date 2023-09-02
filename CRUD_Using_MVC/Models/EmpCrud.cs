using System.Data.SqlClient;

namespace CRUD_Using_MVC.Models
{
    public class EmpCrud
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;

        public EmpCrud(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(configuration.GetConnectionString("defaultConnection"));
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> list = new List<Employee>();
            string qry = "select * from Employee where isActive=1";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Employee emp = new Employee();
                    emp.Id = Convert.ToInt32(dr["id"]);
                    emp.Name = Convert.ToString(dr["name"]);
                    emp.Salary = Convert.ToDouble(dr["salary"]);
                    emp.Department = Convert.ToString(dr["department"]);
                    list.Add(emp);
                }
            }
            con.Close();
            return list;
        }
        public Employee GetEmployeeById(int id)
        {
            Employee emp = new Employee();
            string qry = "select * from Employee where  id=@id";
            cmd = new SqlCommand(qry,con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr=cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    emp.Id = Convert.ToInt32(dr["id"]);
                    emp.Name = dr["name"].ToString();
                    emp.Salary = Convert.ToDouble(dr["salary"]);
                    emp.Department = dr["department"].ToString();
                }
            }
            con.Close();
            return emp;
        }
        public int AddEmployee(Employee emp)
        {
            emp.isActive = 1;
            int result = 0;
            string qry = "insert into Employee values(@name,@salary,@department,@isActive)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name",emp.Name);
            cmd.Parameters.AddWithValue("@salary",emp.Salary);
            cmd.Parameters.AddWithValue("@department",emp.Department);
            cmd.Parameters.AddWithValue("@isActive",emp.isActive);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int UpdateEmployee(Employee emp)
        {
            emp.isActive = 1;
            int result = 0;
            string qry = "update Employee set name=@name,salary=@salary,department=@department,isActive=@isActive where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", emp.Name);
            cmd.Parameters.AddWithValue("@salary", emp.Salary);
            cmd.Parameters.AddWithValue("@department", emp.Department);
            cmd.Parameters.AddWithValue("@isActive", emp.isActive);
            cmd.Parameters.AddWithValue("@id", emp.Id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int DeleteEmployee(int id)
        {
            int result = 0;
            string qry = "update Employee set isActive=0 where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}
