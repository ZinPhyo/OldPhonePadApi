using Microsoft.Extensions.Logging;
using Moq;
using OldPhonePad.Controllers;
using OldPhonePad.Services;
using OldPhonePadApi.Models.ApiModels;

namespace OldPhonePadTest
{
    public class OldPhonePadTest
    {
        [Theory]
        [InlineData("33#", "E")]
        [InlineData("227*#", "B")]
        [InlineData("4433555 555666#", "HELLO")]
        [InlineData("8 88777444666*664#", "TURING")]
        public void OldPhonePadReturnsCorrectResponse(string input, string expectedOutput)
        {
            // Arrange
            var mockService = new Mock<IOldPhonePadService>();
            mockService.Setup(s => s.OldPhonePad(input)).Returns(expectedOutput);

            var controller = new OldPhonePadController(
                Mock.Of<ILogger<OldPhonePadController>>(),
                mockService.Object
            );

            var request = new ApiRequest { Input = input };

            // Act
            var response = controller.OldPhonePad(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedOutput, response.Output);
        }

        [Theory]
        [InlineData("", "Input is Empty")]
        [InlineData("123ABC#", "Input only allow 0 to 9 numbers, space, * and #")]
        [InlineData("123", "Input must be end with '#'")]
        [InlineData("*****#", "")]
        [InlineData("     #", "")]
        [InlineData("0#", " ")]
        public void OldPhonePadReturnsErrorOrEmptyResponse(string input, string expectedOutput)
        {
            // Arrange
            var mockService = new Mock<IOldPhonePadService>();
            mockService.Setup(s => s.OldPhonePad(input)).Returns(expectedOutput);

            var controller = new OldPhonePadController(
                Mock.Of<ILogger<OldPhonePadController>>(),
                mockService.Object
            );

            var request = new ApiRequest { Input = input };

            // Act
            var response = controller.OldPhonePad(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedOutput, response.Output);
        }
    }
}