using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            DBAccess dBAccess = new DBAccess();

            var com = dBAccess.GetAllCompanies();
            var emp = dBAccess.GetAllEmployees();

            var fin = dBAccess.GetAllCompaniesWithEmployees();

            var res = typeof(Employee).GetProperties().Select(x => x.Name);
        }
    } 
}
