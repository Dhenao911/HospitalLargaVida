namespace HospitalLargaVida.Backend.DAL.Dtos.UserDto
{
    public class ChangesPasswordDto
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmNewPassword { get; set; }
    }
}