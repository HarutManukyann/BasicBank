using BL.Core;
using BL.Hashing;
using BL.Repositories.Interfaceis;
using FCBankBasicHelper.Models;
using Microsoft.EntityFrameworkCore;

namespace BL.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {        
        public UserRepository(FcbankBasicContext context) : base(context)
        {

        }
        public User GetUserByEmail(string mail)
        {
            var user = context?.Users.AsNoTracking().FirstOrDefault(u => u.Email == mail);
            return user;
        }
        public User GetUserById(int id)
        {
            var user = context?.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
            return user;
        }
        public User Login(string email, string password)
        {
            try
            {
                var user = context.Users.AsNoTracking().FirstOrDefault(x => x.Email == email);
                password = PasswordHash.Hashed_Password(password).ToString();
                if (user != null && password == user.Password)
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }
        public void RemoveUser(string username)
        {
            using (var scope = TransactionBL.CreateTransactionScope())
            {
                try
                {
                    var user = context.Users.AsNoTracking().FirstOrDefault(x => x.Username == username);
                    user.IsActive = false;
                    context.Users.Update(user);
                    context.SaveChanges();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.StackTrace);
                }
            }
        }
        public void UpdateUser(User model)
        {
            using (var scope= TransactionBL.CreateTransactionScope())
            {
                try
                {
                    var user = context.Users.AsNoTracking().FirstOrDefault(x => x.Id == model.Id);
                    if (user != null)
                    {
                        context.Users.Update(model);
                        context.SaveChanges();
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.StackTrace);
                }
            }
        }
        public bool UserExists(string username)
        {           
            var user = context?.Users.AsNoTracking().FirstAsync(item=>item.Username==username);     
            return user == null ? false : true;
        }       
    }
}
