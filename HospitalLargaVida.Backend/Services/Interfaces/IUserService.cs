using HospitalLargaVida.Backend.DAL.Dtos.UserDto;
using Microsoft.AspNetCore.Identity;

namespace HospitalLargaVida.Backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterUserDto dto);

        Task<IdentityResult> UpdapteUserAsync(string id, UpdateUserDto dto);

        Task<IdentityResult> ChangesPasswordAsync(string id, ChangesPasswordDto dto);
    }
}