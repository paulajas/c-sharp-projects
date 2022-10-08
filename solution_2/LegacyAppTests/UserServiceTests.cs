using LegacyApp;
using System;
using Xunit;

namespace LegacyAppTests
{
    public class UserServiceTests
    {
        [Fact]
        public void ShouldAddJohnDoeSuccessfully()
        {
            //Arrange
            var userService = new UserService();
            
            //Act
            var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);

            //Assert
            Assert.True(addResult);
        }

        [Fact]
        public void ShouldFailBecauseOfNoFirstName()
        {
            //Arrange
            var userService = new UserService();

            //Act
            var addResult = userService.AddUser("", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);

            //Assert
            Assert.False(addResult);
        }

        [Fact]
        public void ShouldFailBecauseOfNoLastName()
        {
            //Arrange
            var userService = new UserService();

            //Act
            var addResult = userService.AddUser("John", "", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);

            //Assert
            Assert.False(addResult);
        }

        [Fact]
        public void ShouldFailBecauseOfIncorrectEmail()
        {
            //Arrange
            var userService = new UserService();

            //Act
            var addResult = userService.AddUser("John", "Doe", "johndoegmailcom", DateTime.Parse("1982-03-21"), 1);

            //Assert
            Assert.False(addResult);
        }

        [Fact]
        public void ShouldFailBecauseOfUserIsTooYoung()
        {
            //Arrange
            var userService = new UserService();

            //Act
            var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Now.AddYears(-10), 1);

            //Assert
            Assert.False(addResult);
        }
    }
}
