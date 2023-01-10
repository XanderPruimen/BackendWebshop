using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;
using BackendWebshop;
using Microsoft.EntityFrameworkCore;
using BackendWebshop.Context;
using BackendWebshop.Controllers;
using BackendWebshop.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;

namespace UnitTests
    {
        public class AccountTests
        {
            private readonly ApplicationDBContext _dbContext;
            public AccountTests([CallerMemberName] string callerName = "")
            {
                var options = new DbContextOptionsBuilder<ApplicationDBContext>().UseInMemoryDatabase(databaseName: "InMemoryProductDb_" + callerName).Options;
                var context = new ApplicationDBContext(options);
                InMemoryDatabasesWithData.InMemoryDatabaseWithData(context);
                _dbContext = context;
            }

 /*           [Fact]
            private async Task Login_ShouldValidateAccount()
            {
                //Initialize Controller
                var controller = new UserController(_dbContext);
                var tc = new TokenController();

                //New Data
                var loginUser = new User()
                {
                    Email = "Johan@gmail.com",
                    Password = "Kaarsje12",
                };

            //Run and get result
            var result = await controller.Login(loginUser);
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
                var controller = new UserController(_dbContext);
                var tc = new TokenController();

                //New Data
                var loginUser = new User()
                {
                    Email = "test",
                    Password = "test",
                };

                //Run and get result
                var result = await controller.Login(loginUser);
                var actionResult = result as BadRequestObjectResult;

                //Check
                Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal(400, actionResult.StatusCode);
                Assert.Equal("Account_Not_Found", actionResult.Value.ToString());
            }

            [Fact]
            private async Task Login_ShouldReturnErrorMissingEmail()
            {
                //Initialize Controller
                var controller = new UserController(_dbContext);
                var tc = new TokenController();

                //New Data
                var loginUser = new User()
                {
                    Email = "",
                    Password = "test",
                };

                //Run and get result
                var result = await controller.Login(loginUser);
                var actionResult = result as BadRequestObjectResult;

                //Check
                Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal(400, actionResult.StatusCode);
                Assert.Equal("Missing_Email", actionResult.Value.ToString());
            }

            [Fact]
            private async Task Login_ShouldReturnErrorMissingPassword()
            {
                //Initialize Controller
                var controller = new UserController(_dbContext);
                var tc = new TokenController();

                //New Data
                var loginUser = new User()
                {
                    Email = "test",
                    Password = "",
                };

                //Run and get result
                var result = await controller.Login(loginUser);
                var actionResult = result as BadRequestObjectResult;

                //Check
                Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal(400, actionResult.StatusCode);
                Assert.Equal("Missing_Password", actionResult.Value.ToString());
            }*/
        }
    }
