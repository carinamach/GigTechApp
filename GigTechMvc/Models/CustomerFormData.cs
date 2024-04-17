using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

public class CustomerFormData
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

    public string ProfileImage { get; set; }
}