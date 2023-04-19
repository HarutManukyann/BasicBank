
using BL.Hashing;
using BL.MailConfirmation;
using BL.Repository;
using FCBankBasicHelper.Models;
using Models.BaseType;
using Models.DTO;
using Models.Mappers;
using Models.RestorePassword;

namespace BL.Core
{
    public class UserBL
    {
        private readonly UserRepository userRepository;
        private readonly Validation validation;
        private readonly MailSender mailsend;
        private readonly Encryption encryption;
        public UserBL(Validation validation, MailSender mailsend , Encryption encryption , UserRepository userRepository)
        {
            this.mailsend = mailsend;
            this.validation =validation;
            this.encryption= encryption;
            this.userRepository=userRepository;
        }
        private string[] GetRolesForUser(string username)
        {
            string[] userRoles;
            username = encryption.Encrypt(username);
            using (FcbankBasicContext context = new FcbankBasicContext())
            {
                userRoles = (from user in context.Users
                             join roleMapping in context.UserRolesMappings
                             on user.Id equals roleMapping.UserId
                             join role in context.Roles
                             on roleMapping.RoleId equals role.Id
                             where user.Username == username
                             select role.RoleName).ToArray();
            }
            return userRoles;
        }
        public bool HasPermission(string username, params string[] roles)
        {
            var userRoles = GetRolesForUser(username);
            var innerjoin = userRoles.Join(roles, str1 => str1,
                      str2 => str2,
                      (str1, str2) => str1);
            if (innerjoin.Count() == 0) return false;

            return true;
        }
        public ResponseBase<UserModel> InsertUser(UserModel model)
        {
                try
                {
                    User user = Mapper<UserModel, User>.Map(model);
                    if (user == null && !validation.UserValidation(user)) throw new Exception("Invalid User");
                    encryption.EncryptData(user);
                    user.Password = PasswordHash.Hashed_Password(user.Password);
                    userRepository.Add(user);
                    return new ResponseBase<UserModel>(true, "User added Successfully", model);
                }
                catch (Exception ex)
                {
                    return new ResponseBase<UserModel>(false, ex.Message);
                }
        }
        public ResponseBase<UserModel> GetUser(string mail, string password)
        {
            try
            {
                User login = userRepository.Login(mail, password);
                if (login is null) throw new Exception("User not found");
                encryption.DecryptData(login);
                var loginmodel = Mapper<User, UserModel>.Map(login);
                return new ResponseBase<UserModel>(true, "User getted Successfully", loginmodel);
            }
            catch (Exception ex)
            {
                return new ResponseBase<UserModel>(false, ex.Message);
            }
        }
        public ResponseBase<UserModel> RemoveUser(string username)
        {
                try
                {
                    userRepository.RemoveUser(encryption.Encrypt(username));
                    return new ResponseBase<UserModel>(true, "User removed Successfully");
                }
                catch (Exception ex)
                {
                    return new ResponseBase<UserModel>(false, ex.Message);
                }
        }
        public ResponseBase<UserModel> UpdateUser(UserModel model)
        {
                try
                {
                    User user = Mapper<UserModel, User>.Map(model);
                    if (user == null && !validation.UserValidation(user)) throw new Exception("Invalid user");
                    encryption.EncryptData(user);
                    user.Password = PasswordHash.Hashed_Password(user.Password);
                    userRepository.Update(user);
                    return new ResponseBase<UserModel>(true, "User ipdated Successfully", model);
                }
                catch (Exception ex)
                {
                    return new ResponseBase<UserModel>(false, ex.Message);
                }
        }
        public bool UserExists(string username)
        {
            try
            {
                return userRepository.UserExists(encryption.Encrypt(username));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ResponseBase<UserModel> GetUserByEmail(string mail)
        {
            try
            {
                mail = encryption.Encrypt(mail);
                var result = userRepository.GetUserByEmail(mail);
                encryption.DecryptData(result);
                return new ResponseBase<UserModel>(true, "User ipdated Successfully", Mapper<User, UserModel>.Map(result));
            }
            catch (Exception ex)
            {
                return new ResponseBase<UserModel>(false, ex.Message);
            }
        }
        public void UpdatePassword(RestorePassword changePassword)
        {
                try
                {
                    User user = Mapper<RestorePassword, User>.Map(changePassword);
                    userRepository.Update(user);
                }
                catch (Exception)
                {
                    throw;
                }
        }
        public void UpdatePass(string newPassword, int userId)
        {
                try
                {
                    object userObject = new { newPassword, userId };
                    User user = Mapper<object, User>.Map(userObject);
                    userRepository.Update(user);
                }
                catch (Exception)
                {
                    throw;
                }
        }       
        public ResponseBase<UserModel> GetUserById(int id)
        {
            try
            {
                var result = userRepository.GetUserById(id);
                if (result == null) throw new Exception("User not found");
                encryption.DecryptData(result);
                return new ResponseBase<UserModel>(true, "User getted Successfully", Mapper<User, UserModel>.Map(result));
            }
            catch (Exception ex)
            {
                return new ResponseBase<UserModel>(false, ex.Message);
            }
        }
    }
}
