using System;
using BCrypt.Net;

namespace EncuestasAcme.Helpers
{
    public static class PasswordHelpers
    {
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("La contraseña es obligatoria.");
            }

            return BCrypt.Net.BCrypt.HashPassword(password.Trim());
        }

        public static bool VerifyPassword(string password, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordHash))
            {
                return false;
            }

            try
            {
                return BCrypt.Net.BCrypt.Verify(password.Trim(), passwordHash);
            }
            catch
            {
                return false;
            }
        }
    }
}