using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;
using Authentication;
using Microsoft.EntityFrameworkCore;
using Authentication.Controllers;
using Authentication.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using Authentication.Data;

namespace UnitTests
{
    public class AccountTests
    {
        private readonly DataContext _dbContext;
        public AccountTests([CallerMemberName] string callerName = "")
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "InMemoryProductDb_" + callerName).Options;
            var context = new DataContext(options);
            InMemoryDatabasesWithData.InMemoryDatabaseWithData(context);
            _dbContext = context;
        }

        [Fact]
        private async Task Login_ShouldValidateAccount()
        {
            //Initialize Controller
            var controller = new AccountController(_dbContext);
            var tc = new TokenController();

            //New Data
            var loginUser = new User()
            {
                email = "Johan@gmail.com",
                password = "Kaarsje12",
            };

            //Run and get result
            var result = controller.login("", loginUser);
            var actionResult = result as OkObjectResult;
            ResponseValue test = actionResult.Value as ResponseValue;
            var user = tc.TokenToUser(actionResult.Value.ToString());

            //Check
            Assert.IsType<OkObjectResult>(result);
            //Assert.Equal(1, account.AccountID);
            //Assert.Equal("Barry@gmail.com", account.Email);
        }

        [Fact]
        private async Task Login_ShouldReturnErrorAccount_Not_Found()
        {
            //Initialize Controller
            var controller = new AccountController(_dbContext);
            var tc = new TokenController();

            //New Data
            var loginUser = new User()
            {
                email = "test",
                password = "test",
            };

            //Run and get result
            var result = controller.login("", loginUser);
            var actionResult = result as BadRequestObjectResult;

            //Check
            Assert.IsType<BadRequestObjectResult>(result);
            //Assert.Equal(400, actionResult.StatusCode);
            //Assert.Equal("Account_Not_Found", actionResult.Value.ToString());
        }

        [Fact]
        private async Task Login_ShouldReturnErrorMissingEmail()
        {
            //Initialize Controller
            var controller = new AccountController(_dbContext);
            var tc = new TokenController();

            //New Data
            var loginUser = new User()
            {
                email = "",
                password = "test",
            };

            //Run and get result
            var result = controller.login("", loginUser);
            var actionResult = result as BadRequestObjectResult;

            //Check
            Assert.IsType<BadRequestObjectResult>(result);
            //Assert.Equal(400, actionResult.StatusCode);
            //Assert.Equal("Missing_Email", actionResult.Value.ToString());
        }

        [Fact]
        private async Task Login_ShouldReturnErrorMissingPassword()
        {
            //Initialize Controller
            var controller = new AccountController(_dbContext);
            var tc = new TokenController();

            //New Data
            var loginUser = new User()
            {
                email = "test",
                password = "",
            };

            //Run and get result
            var result = controller.login("", loginUser);
            var actionResult = result as BadRequestObjectResult;

            //Check
            Assert.IsType<BadRequestObjectResult>(result);
            //Assert.Equal(400, actionResult.StatusCode);
            //Assert.Equal("Missing_Password", actionResult.Value.ToString());
        }
    }
}
