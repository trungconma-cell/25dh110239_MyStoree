using System.ComponentModel.DataAnnotations;

namespace _25dh110239.Models.ViewModel
{
    public class RegisterVM
{
    [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
    [Display(Name = "Tên đăng nhập")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
    [DataType(DataType.Password)]
    [Display(Name = "Xác nhận mật khẩu")]
    [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Họ tên không được để trống")]
    [Display(Name = "Họ tên")]
    public string CustomerName { get; set; }

    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    [Display(Name = "Số điện thoại")]
    [DataType(DataType.PhoneNumber)]
    public string CustomerPhone { get; set; }

    [Required(ErrorMessage = "Email không được để trống")]
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    public string CustomerEmail { get; set; }

    [Required(ErrorMessage = "Địa chỉ không được để trống")]
    [Display(Name = "Địa chỉ")]
    public string CustomerAddress { get; set; }
}
}