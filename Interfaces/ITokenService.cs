using WebAPI.Models;

namespace WebAPI.Interfaces;

public interface ITokenService {
    public string CreateToken(User user);
}