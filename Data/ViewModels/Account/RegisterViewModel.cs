using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels.Account; 

public class RegisterViewModel {
    [Required]
    [Display(Name = "Username")]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.EmailAddress), Display(Name = "Email address")]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password), Display(Name = "Confirm password")]
    [Compare(nameof(Password), ErrorMessage = "Passwords does not match")]
    public string PasswordConfirm { get; set; }
}