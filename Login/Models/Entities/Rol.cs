namespace Login.Models.Entities;

public class Rol
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public ICollection<User> Users { get; set; } = new HashSet<User>();
    public ICollection<UserRol>? UsersRols { get; set; }
}
