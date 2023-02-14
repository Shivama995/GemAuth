using Common.Configuration;
using Common.Data;
using Data.User.Models;
using MongoDB.Driver;

namespace Data.User.DataAccess.Implementations
{
    public class UserRepository : RepoBase, IUserRepository
    {
        public UserRepository(IConfigManager configManager) : base(configManager) { }
        public async Task<UserDetailsModel> GetUserDetails(string emailAddress)
        {
            var Collection = Database.GetCollection<UserDetailsModel>("user_base");

            return (await Collection.FindAsync(Builders<UserDetailsModel>
                .Filter
                .Eq("EmailAddress", emailAddress)))
                .FirstOrDefault();

        }
        public async Task<UserDetailsModel> AddUser(UserAggregateModel user)
        {

            var UserAuthModelTask    = AddNewUserAuthModel(user);
            var UserDetailsModelTask = AddNewUserDetailsModel(user);

            Task.WaitAll(UserAuthModelTask, UserDetailsModelTask);
            return UserDetailsModelTask.Result;
        }

        private async Task<UserAuthModel> AddNewUserAuthModel(UserAggregateModel user)
        {
            UserAuthModel User = new UserAuthModel
            {
                FirstName    = user.FirstName,
                LastName     = user.LastName,
                EmailAddress = user.EmailAddress,
                DBName       = user.DBName,
                Id           = user.Id,
                OrgCode      = user.OrgCode,
                Role         = user.Role
            };
            var Collection = Database.GetCollection<UserAuthModel>("user_base");
            await Collection.InsertOneAsync(User);
            return User;
        }

        private async Task<UserDetailsModel> AddNewUserDetailsModel(UserAggregateModel user)
        {
            UserDetailsModel User = new UserDetailsModel
            {
                FirstName    = user.FirstName,
                LastName     = user.LastName,
                EmailAddress = user.EmailAddress,
                Password     = user.Password,
                Id           = user.Id,
                OrgCode      = user.OrgCode,
                Role         = user.Role
            };
            LoadDatabase(user.DBName);
            var Collection = Database.GetCollection<UserDetailsModel>("user_base");
            await Collection.InsertOneAsync(User);
            return User;
        }
    }
}
