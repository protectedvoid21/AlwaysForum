using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels.Account; 

public class ChangePasswordViewModel {
    [Required]
    [Display(Name = "Current password")]
    public string CurrentPassword { get; set; }

    [Required]
    [Display(Name = "New password")]
    public string NewPassword { get; set; }

    [Required]
    [Compare(nameof(NewPassword))]
    [Display(Name = "Confirm password")]
    public string ConfirmPassword { get; set; }
}