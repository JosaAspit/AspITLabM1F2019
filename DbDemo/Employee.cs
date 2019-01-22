using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDemo
{
    public class Employee
    {
        private string name;
        private int id;
        private int companyId;

        public int CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}
