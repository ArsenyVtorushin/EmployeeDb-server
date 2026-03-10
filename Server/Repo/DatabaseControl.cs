using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Repo
{
    public static class DatabaseControl
    {
        public static List<Employee> GetEmployeesList()
        {
            using (AppDbContext context = new AppDbContext())
            {
                return context.Employees.ToList();
            }
        }
    }
}
