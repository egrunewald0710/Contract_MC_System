using System.Linq;
using Contract_MC_System;
using Xunit;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace Contract_MC_System.Tests
{
    public class UserTests
    {
        [Fact]
        public void CanCreateUserAccount()
        {
            // Arrange
            var db = new TestDbContext("CreateUserTest");

            var newUser = new User
            {
                Username = "ethan",
                Password = "1234",
                Role = "Lecturer"
            };

            // Act
            db.Users.Add(newUser);
            db.SaveChanges();

            // Assert
            var user = db.Users.FirstOrDefault(u => u.Username == "ethan");
            Assert.NotNull(user);
            Assert.Equal("Lecturer", user.Role);
        }

        [Fact]
        public void CannotLoginWithInvalidPassword()
        {
            // Arrange
            var db = new TestDbContext("InvalidLoginTest");

            db.Users.Add(new User { Username = "admin", Password = "1234", Role = "Manager" });
            db.SaveChanges();

            // Act
            var user = db.Users.FirstOrDefault(u => u.Username == "admin" && u.Password == "wrongpass");

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public void CanLoginWithCorrectCredentials()
        {
            // Arrange
            var db = new TestDbContext("ValidLoginTest");

            db.Users.Add(new User { Username = "manager", Password = "abcd", Role = "Manager" });
            db.SaveChanges();

            // Act
            var user = db.Users.FirstOrDefault(u => u.Username == "manager" && u.Password == "abcd");

            // Assert
            Assert.NotNull(user);
            Assert.Equal("Manager", user.Role);
        }
    }
}
