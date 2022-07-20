using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels.Account; 

public class RegisterViewModel {
    [Required]
    [Display(Name = "Username")]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords does not match")]
    public string PasswordConfirm { get; set; }
}