namespace HospitalLargaVida.Backend.Helpers
{
    public static class CalculateAge
    {

        public static int calcularEdad(DateTime dateOfBirth)
        {
            var today = DateTime.UtcNow;
            var age = today.Year - dateOfBirth.Year;

            if (dateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
