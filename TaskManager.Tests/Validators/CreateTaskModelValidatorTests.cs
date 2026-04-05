using FluentValidation.TestHelper;
using TaskManager.Models;
using TaskManager.Validators;

namespace TaskManager.Tests.Validators
{
    public class CreateTaskModelValidatorTests
    {
        private readonly CreateTaskModelValidator _validator = new();

        public static IEnumerable<object[]> NameTestData()
        {
            yield return new object[] { "", "Name is required" };
            yield return new object[] { "         ", "Name is required" };
            yield return new object[] { "Valid Name", null };
            yield return new object[] { new string('A', 101), "Name must not exceed 100 characters" };
        }

        public static IEnumerable<object[]> DescriptionTestData()
        {
            yield return new object[] { "", "Description is required" };
            yield return new object[] { "             ", "Description is required" };
            yield return new object[] { "Valid description", null };
        }

        public static IEnumerable<object[]> PriorityIdTestData()
        {
            yield return new object[] { 0, "Selected priority is invalid" };
            yield return new object[] { 1, null };
            yield return new object[] { -1, "Selected priority is invalid" };
            yield return new object[] { null, "Selected priority is invalid" };
        }

        public static IEnumerable<object[]> CategoryIdTestData()
        {
            yield return new object[] { 0, "Selected category is invalid" };
            yield return new object[] { 1, null };
            yield return new object[] { -1, "Selected category is invalid" };
            yield return new object[] { null, null };
        }

        [Theory]
        [MemberData(nameof(NameTestData))]
        public void Name_Validate(string name, string expectedError)
        {
            var model = new CreateTaskModel
            {
                Name = name,
                Description = "desc",
                PriorityId = 1
            };

            var result = _validator.TestValidate(model);

            if (expectedError != null)
                result.ShouldHaveValidationErrorFor(x => x.Name)
                    .WithErrorMessage(expectedError);
            else
                result.ShouldNotHaveValidationErrorFor(x => x.Name);

        }

        [Theory]
        [MemberData(nameof(DescriptionTestData))]
        public void Description_Validate(string description, string expectedError)
        {
            var model = new CreateTaskModel
            {
                Name = "name",
                Description = description,
                PriorityId = 1
            };

            var result = _validator.TestValidate(model);

            if (expectedError != null)
                result.ShouldHaveValidationErrorFor(x => x.Description)
                    .WithErrorMessage(expectedError);
            else
                result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Theory]
        [MemberData(nameof(PriorityIdTestData))]
        public void PriorityId_Validate(int priorityId, string expectedError)
        {
            var model = new CreateTaskModel
            {
                Name = "name",
                Description = "desc",
                PriorityId = priorityId
            };

            var result = _validator.TestValidate(model);

            if (expectedError != null)
                result.ShouldHaveValidationErrorFor(x => x.PriorityId)
                    .WithErrorMessage(expectedError);
            else 
                result.ShouldNotHaveValidationErrorFor(x => x.PriorityId);
        }

        [Theory]
        [MemberData(nameof(CategoryIdTestData))]
        public void CategoryId_Validate(int? categoryId, string expectedError)
        {
            var model = new CreateTaskModel
            {
                Name = "name",
                Description = "desc",
                PriorityId = 1,
                CategoryId = categoryId
            };

            var result = _validator.TestValidate(model);

            if (expectedError != null)
                result.ShouldHaveValidationErrorFor(x => x.CategoryId)
                    .WithErrorMessage(expectedError);
            else
                result.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
        }
    }
}
