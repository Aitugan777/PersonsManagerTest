using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager
{
	public class People
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public long BirthDate { get; set; }
		public Gender Gender { get; set; }
	}
	public class Person : People
	{
		public Guid TransportId { get; set; }
		public int SequenceId { get; set; }
		public string[] CreditCardNumbers { get; set; }
		public int Age { get; set; }
		public string[] Phones { get; set; }
		public double Salary { get; set; }
		public bool IsMarried { get; set; }
		public People[] Children { get; set; }
	}

	public enum Gender
	{
		Male,
		Female
	}

}
