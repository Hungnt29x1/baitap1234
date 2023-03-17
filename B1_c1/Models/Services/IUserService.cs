namespace B1_c1.Models.Services
{
    public interface IUserService
    {
        User GetUser(string userName, string password);
    }
}
