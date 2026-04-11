using FluentValidation.TestHelper;
using TaskManager.Models.Auth;
using TaskManager.Validators;

namespace TaskManager.Tests.Validators
{
    public class LoginModelValidatorTests
    {
        private readonly LoginModelValidator _validator = new();

        public static IEnumerable<object?[]> EmailTestData()
        {
            yield return new object?[] { "", "Email is required" };
            yield return new object?[] { "       ", "Email is required" };
            yield return new object?[] { "user", "Invalid email format" };
            yield return new object?[] { "user@", "Invalid email format" };
            yield return new object?[] { "user@domain.com", null };
        }

        public static IEnumerable<object?[]> PasswordTestData()
        {
            yield return new object?[] { "", "Password is required" };
            yield return new object?[] { "      ", "Password is required" };
            yield return new object?[] { "qwe", "Password must be at least 6 characters" };
            yield return new object?[] { "123456789", null };
        }

        [Theory]
        [MemberData(nameof(EmailTestData))]
        public void Email_Validate(string email, string expectedError)
        {
            var model = new LoginModel
            {
                Email = email,
                Password = "validPassword"
            };

            var result = _validator.TestValidate(model);

            if (expectedError != null)
                result.ShouldHaveValidationErrorFor(x => x.Email)
                    .WithErrorMessage(expectedError);
            else
                result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [MemberData(nameof(PasswordTestData))]
        public void Password_Validate(string password, string expectedError)
        {
            var model = new LoginModel
            {
                Email = "user@test.com",
                Password = password
            };

            var result = _validator.TestValidate(model);

            if (expectedError != null)
                result.ShouldHaveValidationErrorFor(x => x.Password)
                    .WithErrorMessage(expectedError);
            else
                result.ShouldNotHaveValidationErrorFor(x => x.Password);
        }
    }
}
