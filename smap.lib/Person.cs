using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smap.lib
{
    public class Address
    {
        public string City { get; set; }
        public string State { get; set; }
    }
    public interface IPerson
    {
        string Name { get; set; }
        double Salary { get; set; }
        Address Address { get; set; }

    }
    public class Person : IPerson
    {
        public string Name { get; set; }
        public double Salary { get; set; }
        public Address Address { get; set; }

    }
}
