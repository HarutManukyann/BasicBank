using FCBankBasicHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserEqualizerWorkerService.Data;
using UserEqualizerWorkerService.Models;

namespace UserEqualizerWorkerService.Services.v1
{
    public class UserEqualizerService
    {
        private readonly ILogger<UserEqualizerService> logger;
        private readonly PlaceHolderClient client;
        private readonly FcbankBasicContext context;
        public UserEqualizerService(ILogger<UserEqualizerService> logger, PlaceHolderClient client, FcbankBasicContext context)
        {
            this.logger = logger;
            this.client = client;
            this.context = context;
        }

        public virtual async Task<bool> ExecuteService()
        {
            logger.LogInformation("Starting process");

            var placeHolderUsers = await client.GetPlaceHolderUsers();

            var result = await EqualizeUsers(placeHolderUsers);

            logger.LogInformation("Ending process");

            return result;
        }
        public virtual async Task<bool> EqualizeUsers(List<PlaceHolderUser> phUsers)
        {
            var users = context.Users.ToList();

            var newUsers = phUsers.Where(x => !users.Any(x1 => x1.Username != x.Username && x1.Email != x.Email)).ToList();

            if (!newUsers.Any())
            {
                logger.LogInformation("No new users to add");
                return true;
            }

            logger.LogInformation($"Found {newUsers.Count} new users");

            return await SaveNewUsers(newUsers);
        }
        public virtual async Task<bool> SaveNewUsers(List<PlaceHolderUser> newUsers)
        {
            try
            {
                logger.LogInformation("Saving new users");

                var newUsersEntity = newUsers.Select(x => new User
                {
                    Id = x.Id,
                    Username = x.Username,
                    Email = x.Email,
                    Password = x.Password,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Birthday = x.Birthday,
                    PassportNumber = x.PassportNumber,
                    IsActive = x.IsActive,
                    Address = x.Address,
                }).ToList();

                await context.Users.AddRangeAsync(newUsersEntity);
                await context.SaveChangesAsync();

                logger.LogInformation("New users successfully saved");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error saving new users - {ex.Message}");
                return false;
            }
        }
    }
}
