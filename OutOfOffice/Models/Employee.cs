using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OutOfOffice.Models
{

	public enum Subdivision
	{
		QA,
		Dev,
        [Display(Name = "People Partner")]
        PeoplePartner
	}

	public enum Position
	{
		QA,
        [Display(Name = "Front End")]
        FrontEnd,
        [Display(Name = "Back End")]
        BackEnd,
		HR,
        [Display(Name = "Project Manager")]
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

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Please enter a full name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please select a subdivision")]
        public Subdivision Subdivision { get; set; }

        [Required(ErrorMessage = "Please select a position")]
        public Position Position { get; set; }

        [Display(Name = "Employee Status")]
        [Required(ErrorMessage = "Please select a status")]
        public EmployeeStatus EmployeeStatus { get; set; }

        public Employee? PeoplePartner { get; set; }

        [Display(Name = "People Partner")]
        [Required(ErrorMessage = "Please select a People Partner")]
        public int? PeoplePartnerId { get; set; }

        [Required(ErrorMessage = "Please enter the balance")]
        public int Balance { get; set; }

		//photo
		


	}
}

