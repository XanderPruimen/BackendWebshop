using BackendWebshop.Context;
using BackendWebshop.DTO_s;

namespace BackendWebshop.Container
{
    public class UserContainer
    {
        private readonly ApplicationDBContext _dbContext;

        public UserContainer(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserDTO ConnectToOpenAccount()
        {
            return new UserDTO() { AccountID = 1 };
        }
    }
}
