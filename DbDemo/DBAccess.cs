using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDemo
{
    public class DBAccess
    {
        #region constring
        private string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AspITLabDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        #endregion

        public List<Company> GetAllCompanies()
        {
            string query = "select * from companies";

            DataSet ds = ExecuteQuery(query);

            return DataSetToCompanyList(ds);
        }
        
        public List<Company> GetAllCompaniesWithEmployees()
        {
            List<Employee> employees = GetAllEmployees();
            List<Company> companies = GetAllCompanies();

            foreach (var company in companies)
            {
                company.Employees.AddRange(employees.Where(x => x.CompanyId == company.Id));
            }

            return companies;
        }

        public List<Employee> GetEmployeesByCompanyId(int id)
        {
            string q = $"select * from employees where companyid = {id}";

            List<Employee> employees = new List<Employee>();
            DataSet ds = ExecuteQuery(q);

            return DataSetToEmployeeList(ds);
        }

        public List<Employee> GetAllEmployees()
        {
            string query = "select * from employees";

            DataSet ds = ExecuteQuery(query);

            return DataSetToEmployeeList(ds);            
        }

        private List<Company> DataSetToCompanyList(DataSet ds)
        {
            List<Company> companies = new List<Company>();

            if (ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    companies.Add(new Company()
                    {
                        Name = row["name"].ToString(),
                        Id = int.Parse(row["id"].ToString())
                    });
                }
            }
            else
            {
                throw new Exception("There are no companies in the companies table");
            }

            return companies;
        }
        private List<Employee> DataSetToEmployeeList(DataSet ds)
        {
            List<Employee> employees = new List<Employee>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    employees.Add(new Employee()
                    {
                        Id = int.Parse(row["Id"].ToString()),
                        Name = row["name"].ToString(),
                        CompanyId = int.Parse(row["companyId"].ToString())

                    });
                }
            }
            else
            {
                throw new Exception("There are no employees :(");
            }

            return employees;
        }

        public DataSet ExecuteQuery(string query)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlDataAdapter dap = new SqlDataAdapter(query, con))
            {
                dap.Fill(ds);
            }

            return ds;
        }

        public int ExecuteNonQuery(string query)
        {
            int affectedRows = 0;

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand com = new SqlCommand(query,con))
            {
                affectedRows = com.ExecuteNonQuery();
            }

            return affectedRows;
        }
    }
}
