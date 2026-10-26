using System.ComponentModel.DataAnnotations;

namespace _25dh110239.Models.ViewModel
{
    public class LoginVM
{
    [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
    [Display(Name = "Tên đăng nhập")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; }
}
}