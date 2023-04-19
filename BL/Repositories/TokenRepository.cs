using BL.Core;
using BL.Repositories.Interfaceis;
using BL.Repository;
using FCBankBasicHelper.Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class TokenRepository : Repository<Token>, ITokenRepository
    {
        public TokenRepository(FcbankBasicContext context) : base(context)
        {

        }
        public void InsertUser()
        {
            using (var scope = TransactionBL.CreateTransactionScope())
            {
                try
                {
                    context.SaveChanges();
                    scope.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public Token Tokens(string? username)
        {
            return context.Tokens.LastOrDefault(u => u.Username == username);
        }
    }
}
