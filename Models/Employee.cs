using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Models;

[Table("tb_m_employess")]

public class Employee
{ 
    [Key, Column("nik", TypeName = "nchar(5)")]
    public string NIK { get; set; }
    [Required, Column("first_name"), MaxLength(50)]
    public string FirstName { get; set; }
    [Column("last_name"), MaxLength(50)]
    public string? LastName { get; set; }
    [Required, Column("birthdate")]
    public DateTime Birthdate { get; set; }
    [Required, Column("gender")]
    public GenderEnum Gender { get; set; }
    [Required, Column("hiring_date")]
    public DateTime HiringDate { get; set; } = DateTime.Now;
    [Required, Column("email"), MaxLength(50)]
    public string Email { get; set; }
    [Column("phone_number"), MaxLength(20)]
    public string? PhoneNumber { get; set; }
    [Column("manager_id", TypeName = "nchar(5)")]
    public string? ManagerId { get; set; }


    // Cardinality
    public ICollection<Profilling>? Profillings { get; set; }
    public Account? Account { get; set; }
    // Many
    public ICollection<Employee>? Employees { get; set; }
    // One
    public  Employee? Manager { get; set; }

    public enum GenderEnum
    {
        Male,
        Female
    }
}


