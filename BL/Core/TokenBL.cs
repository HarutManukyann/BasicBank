using BL.Hashing;
using BL.Repositories;
using FCBankBasicHelper.Models;
using Models.DTO;
using Models.Mappers;

namespace BL.Core
{
    public class TokenBL
    {
        private readonly TokenRepository tokenRepository;
        private readonly Validation validation;
        private readonly Encryption encryption;
        public TokenBL(Encryption encryption , Validation validation , TokenRepository tokenRepository)
        {
            this.tokenRepository = tokenRepository;
            this.validation = validation;
            this.encryption = encryption;
        }
        public void Save(string refreshToken, string userName)
        {
                try
                {
                    var tok = new Token
                    {
                        Username = encryption.Encrypt( userName),
                        RefreshToken = refreshToken,
                        RefreshTokenExpire = DateTime.UtcNow.AddDays(7)
                    };
                     tokenRepository.Add(tok);
                }
                catch (Exception)
                {
                    throw;
                }
        }
        public void Save()
        {
                try
                {
                    tokenRepository.InsertUser();
                }
                catch (Exception)
                {
                    throw;
                }
        }
        public Token GetTokens(string? username)
        {
            try
            {
                username= encryption.Decrypt( username);
                return tokenRepository.Tokens(username);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UserValid(UserModel userModel)
        {
            User user = Mapper<UserModel, User>.Map(userModel);
            return validation.UserValidation(user);
        }
    }
}
