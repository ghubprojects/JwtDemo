using JwtDemo.Entities;

namespace JwtDemo.Repositories;

public interface IUserRepository {
    User? GetById(Guid id);
    User? GetByEmail(string email);
    bool IsEmailUnique(string email);
    void Add(User user);
}

public class UserRepository : IUserRepository {
    private readonly List<User> _users;

    public UserRepository() { _users = []; }

    public User? GetById(Guid id) => _users.FirstOrDefault(x => x.Id == id);

    public User? GetByEmail(string email) => _users.FirstOrDefault(x => x.Email == email);

    public bool IsEmailUnique(string email) => !_users.Any(x => x.Email == email);

    public void Add(User user) => _users.Add(user);
}
