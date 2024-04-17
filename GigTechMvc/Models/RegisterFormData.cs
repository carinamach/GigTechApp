using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

public class RegisterFormData
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    public string Username { get; set; }
    [Required]
    public string ProfileImage { get; set; }

    [Required]
    public string PasswordHASH { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public int CustomerId { get; set; }
}