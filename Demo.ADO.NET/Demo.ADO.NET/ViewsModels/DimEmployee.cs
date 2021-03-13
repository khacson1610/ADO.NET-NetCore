using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.ADO.NET.ViewsModels
{
    public class DimEmployee
    {
        //FirstName,LastName,MiddleName,NameStyle,Title,BirthDate,EmailAddress,Phone
        public int EmployeeKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string NameStyle { get; set; }
        public string Title { get; set; }
        public string BirthDate { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
    }
}
