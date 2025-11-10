
using LibraryBackend_CleanArchitecture.Entities;
using LibraryBackend_CleanArchitecture.Model;

namespace LibraryBackend_CleanArchitecture.Services
{
    public interface IAuthService
    {
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);
        Task<User?> RegisterAsync(UserDto request);
    }
}