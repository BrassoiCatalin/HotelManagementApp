using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelManagementApp.Services
{
    internal static class UserValidator
    {
        public static void CheckEmail(string email)
        {
            if (email == null)
            {
                throw new ArgumentNullException("Email cannot be null!");
            }
            else if (email.Length < 8)
            {
                throw new Exception("Email must have atleast 8 characters!");
            }
            else
            {
                var mailWasCreated = MailAddress.TryCreate(email, out _);
                if (!mailWasCreated)
                {
                    throw new Exception("Mail not valid!");
                }
            }
        }

        public static void CheckPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("Password cannot be null!");
            }
            else if (password.Length < 8)
            {
                throw new Exception("Password must have atleast 8 characters!");
            }
        }
    }
}
