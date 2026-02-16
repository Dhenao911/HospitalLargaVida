using HospitalLargaVida.Backend.DAL.Dtos.UserDto;
using HospitalLargaVida.Backend.DAL.Models;
using HospitalLargaVida.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HospitalLargaVida.Backend.Services.Implementaciones
{
    public class UserService 
    {
        private readonly UserManager<User> _userManager;
        public UserService(UserManager<User>userManager)
        {
            _userManager = userManager;
        }

       /* public async Task<IdentityResult> ChangesPasswordAsync(string id, ChangesPasswordDto dto)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            var oldPassWord=dto.CurrentPassword.Trim();
            var newPassword=dto.NewPassword.Trim();
            var confirmNewPassword=dto.ConfirmNewPassword.Trim();

            if (newPassword != confirmNewPassword)
            {
                return IdentityResult.Failed(new IdentityError { Description = "The new password and confirmation do not match." });
            }

            if (newPassword == oldPassWord)
            {
                return IdentityResult.Failed(new IdentityError { Description = "The new password must be different from the current password." });
            }

            var newChangePassword= await _userManager.ChangePasswordAsync(user, oldPassWord, newPassword);

            if (newChangePassword.Succeeded)
            {

            }

        }

        public Task<IdentityResult> RegisterUserAsync(RegisterUserDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdapteUserAsync(string id, UpdateUserDto dto)
        {
            throw new NotImplementedException();
        }*/
    }
}
