using System.Text.Json.Serialization;

namespace Login.Models.Dtos.User
{
    public class UserDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        [JsonIgnore]
        public string? TwoStepSecret { get; set; }
    }
}
