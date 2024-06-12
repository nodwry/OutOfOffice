using System;
namespace OutOfOffice.Models
{

	public enum Subdivision
	{
		QA,
		Dev,
		PeoplePartner
	}

	public enum Position
	{
		QA,
		FrontEnd,
		BackEnd,
		HR,
		PM
	}

    public enum EmployeeStatus
    {
        Active,
        Inactive
    }

	public class Employee
	{
		public int Id { get; set; }

		public string FullName { get; set; }

		public Subdivision Subdivision { get; set; }

		public Position Position { get; set; }

		public EmployeeStatus EmployeeStatus { get; set; }

        public Employee? PeoplePartner { get; set; }

        public int? PeoplePartnerId { get; set; }

        public int Balance { get; set; }

		//photo
		


	}
}

