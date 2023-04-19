using BL.Core;
using FCBankBasicHelper.Models;
using System.Text.RegularExpressions;

namespace BL
{
    public class Validation
    {
        private readonly ConfigBl config;
        public Validation(ConfigBl config)
        {
                this.config = config;
        }
        public bool EmailValidation(string mail)
        {
            try
            {
                string pattern = config.GetBykey(ConfigType.MailRegex);
                if (Regex.IsMatch(mail, pattern))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }
        public bool AgeValidation(DateTime age)
        {
            try
            {
                int? ageLimitFlor = int.Parse(config.GetBykey(ConfigType.AgeLimitFlor));
                int? ageLimitCeiling = int.Parse(config.GetBykey(ConfigType.AgeLimitCeiling));
                if (ageLimitFlor < DateTime.Now.Year - age.Year && DateTime.Now.Year - age.Year < ageLimitCeiling) 
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }
        public bool PhoneValidation(string number)
        {
            try
            {
                string phoneValid = config.GetBykey(ConfigType.PhoneRegex);
                if (Regex.IsMatch(number, phoneValid))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool PasswordValidation(string password)
        {
            try
            {
                string passwordValid = config.GetBykey(ConfigType.PasswordRegex);
                if (Regex.IsMatch(password, passwordValid))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }
        public bool NameValidation(string firstName, string lastName)
        {
            string nameValid = config.GetBykey(ConfigType.NameRegex);
            if (Regex.IsMatch(firstName, nameValid) && Regex.IsMatch(lastName, nameValid))
            {
                return true;
            }

           return false;
        }
        public bool UserValidation(User userInfo)
        {
            return NameValidation(userInfo.FirstName, userInfo.LastName) &&
               PasswordValidation(userInfo.Password) &&
               EmailValidation(userInfo.Email) &&
               AgeValidation(userInfo.Birthday);
        }
        public bool CustomerValidation(Customer customerInfo)
        {
            return NameValidation(customerInfo.FirstName, customerInfo.LastName) &&
               EmailValidation(customerInfo.Email) &&
               AgeValidation(customerInfo.Birthday);
        }
    }
}