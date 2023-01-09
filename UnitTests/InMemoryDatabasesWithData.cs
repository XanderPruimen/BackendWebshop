using BackendWebshop.Context;
using BackendWebshop.DTO_s;

namespace UnitTests
{

    public class InMemoryDatabasesWithData
        {
            //
            //FakeDatabase
            //
            public static void InMemoryDatabaseWithData(ApplicationDBContext context)
            {
                var users = new List<UserDTO>()
            {
                new UserDTO { AccountID = 1, Username = "Johan", Email = "Johan@gmail.com", Password = "Kaarsje12"},
                new UserDTO { AccountID = 2, Username = "Antoonus", Email = "Antonus@gmail.com", Password = "A4562!"},
                new UserDTO { AccountID = 3, Username = "PJ", Email = "Pj@test.com", Password = "Testje12"},
            };
                if (!context.users.Any())
                {
                    context.users.AddRange(users);
                };
                context.SaveChanges();
            }
        }
    }
