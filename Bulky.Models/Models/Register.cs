using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BulkyWeb.Models
{
    public class Register
    {
        public string? PersonName;
        public string? Email;
        public string? Password;
        public string? ConfirmedPassword;
        public string? PhoneNumber;
    }
}
