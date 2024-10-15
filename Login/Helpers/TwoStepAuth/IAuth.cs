using Login.Models.Entities;

namespace Login.Helpers.TwoStepAuth;

public interface IAuth
{
    byte[] CreateQR(ref User user);
    bool VerifyCode(string secret, string code);
    Task SendEmail(User User, byte[] QR);
}
