using Authentication.Data;
using Authentication.Models;
using Authentication.Data;

namespace UnitTests
{

    public class InMemoryDatabasesWithData
        {
            //
            //FakeDatabase
            //
            public static void InMemoryDatabaseWithData(DataContext context)
            {
                var users = new List<User>()
            {
                new User { userID = 1, username = "Johan", email = "Johan@gmail.com", password = "Kaarsje12"},
                new User { userID = 2, username = "Antoonus", email = "Antonus@gmail.com", password = "A4562!"},
                new User { userID = 3, username = "PJ", email = "Pj@test.com", password = "Testje12"},
            };
                if (!context.Users.Any())
                {
                    context.Users.AddRange(users);
                };
                context.SaveChanges();
            }
        }
    }
