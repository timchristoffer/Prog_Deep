namespace PasswordTester
{
    [TestFixture]
    public class PasswordValidatorTests
    {
        [Test]
        public void Password_ShouldBeAtLeast8CharactersLong()
        {
            // Arrange
            string password = "abc123"; // Ett l�senord som �r kortare �n 8 tecken

            // Act
            bool isValid = PasswordValidator.ValidatePassword(password);

            // Assert
            Assert.IsTrue(isValid, "L�senordet ska vara minst 8 tecken");
        }

        [Test]
        public void Password_ShouldInclude_UpperCase()
        {
            string password = "abc1231111114";

            bool isValid = PasswordValidator.ValidatePassword(password);

            Assert.IsTrue(isValid, "L�senordet m�ste inneh�lla stor bokstav");
        }

        [Test]
        public void Password_ShouldInclucde_LowerCase()
        {
            string password = "AKASDO12ads313!";

            bool isValid = PasswordValidator.ValidatePassword(password);

            Assert.IsTrue(isValid, "L�senordet m�ste inneh�lla sm� bokst�ver");
        }

        [Test]
        public void Password_Is_Correct()
        {
            string password = "A#4213kna0K.";

            bool isValid = PasswordValidator.ValidatePassword(password);

            Assert.IsTrue(isValid, "L�senordet �r korrekt.");
        }

    }
}