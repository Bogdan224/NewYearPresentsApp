using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.App.Infrastructure
{
    public class AppConfig
    {
        public Company Company { get; set; } = new Company();
        public Database Database { get; set; } = new Database();
    }

    public class Database
    {
        public string? ConnectionString { get; set; }
    }

    public class Company
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? PhoneShort { get; set; }
        public string? Email { get; set; }
    }
}
