using Microsoft.AspNetCore.Identity;

namespace HospitalLargaVida.Backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync();

        Task<IdentityResult> UpdapteUserAsync();

        Task<IdentityResult> ChangesPasswordAsync();
    }
}