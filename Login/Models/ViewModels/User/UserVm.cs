using Login.Models.Entities;
using System.Text.Json.Serialization;

namespace Login.Models.ViewModels.User;

public class UserVm
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    [JsonIgnore]
    public string? TwoStepSecret { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
}
