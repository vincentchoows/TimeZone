using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BCrypt.Net;

namespace TimeZone_Assign
{
    public static class Encryption
    {
        // Define the salt and pepper values
        private const string Salt = "L5f5a5j3aMy58gGQ2wNwKQ==";
        private const string Pepper = "lSYNlCWTbAysIW+z9IluWQ==";

        // Hashes a plain-text password using a salt and pepper
        public static string HashPassword(string password)
        {
            // Combine the password, salt, and pepper values into one string
            string saltedPassword = string.Concat(password, Salt, Pepper);

            // Generate a new salt value using a cryptographically secure random number generator
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            

            // Hash the salted password using the bcrypt algorithm
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(saltedPassword, salt);

            return hashedPassword;
        }

        // Verifies a plain-text password against a stored hash value
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            try
            {
                // Combine the password, salt, and pepper values into one string
                string saltedPassword = string.Concat(password, Salt, Pepper);

                // Verify the salted password against the stored hash value using BCrypt
                bool passwordMatches = BCrypt.Net.BCrypt.Verify(saltedPassword, hashedPassword);

                return passwordMatches;
            }
            catch(Exception e)
            {
                return false;
            }
            


            
        }
    }


}