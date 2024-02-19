using System.ComponentModel.DataAnnotations;

namespace PatientManagement.Common
{
    public class UserRequest
    {
        [Required(ErrorMessage = "UserName Required!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Required!")]
        public string Password { get; set; }
    }
}
