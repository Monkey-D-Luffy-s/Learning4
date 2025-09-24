
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning4.Models.Employees
{

    public class AddEmployeeModel
    {

        public string? EmployeeId { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "RoleId is Required")]
        public string RoleId { get; set; }
        [Required(ErrorMessage = "Salary is Required")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "DateOfJoining is Required")]
        public DateTime DateOfJoining { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "PhoneNumber is Required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "CollegeId is Required")]
        public string? CollegeId { get; set; }
        [Required(ErrorMessage = "DistrictID is Required")]
        public string? DistrictID { get; set; }
        [Required(ErrorMessage = "AdhaarNumber is Required")]
        [StringLength(12, ErrorMessage = "AdhaarNumber must be 12 digits")]
        public string? AdhaarNumber { get; set; }
        [Required(ErrorMessage = "PANNumber is Required")]
        [StringLength(10, ErrorMessage = "PAN number must be 12 digits")]
        public string? PANNumber { get; set; }
        public string? Passport_DocumentPath { get; set; }
        [Required(ErrorMessage = "Passport Photo is Required")]
        [NotMapped]
        public IFormFile? Passport_Document { get; set; }
        [Required(ErrorMessage = "BloodGroup is Required")]
        public string BloodGroup { get; set; }
        [Required(ErrorMessage = "Singnature Document is Required")]
        [NotMapped]
        public IFormFile? Singnature_Document { get; set; }
        public string? Singnature_DocumentPath { get; set; }
    }


}
