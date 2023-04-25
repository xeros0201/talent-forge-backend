
using TFBackend.Entities.Dto.Staff;
using TFBackend.Models;

namespace TFBackend.Entities.Dto.Login
{
    public class LoginResponse
    {
        public StaffDto User { get; set; }
        public string  accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
