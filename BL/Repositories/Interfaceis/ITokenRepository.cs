using FCBankBasicHelper.Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories.Interfaceis
{
    public interface ITokenRepository : IRepository<Token>
    {
        void InsertUser();
        Token Tokens(string? username);
    }
}
