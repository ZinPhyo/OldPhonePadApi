using Microsoft.Extensions.Logging;
using Moq;
using OldPhonePad.Controllers;
using OldPhonePad.Models;
using OldPhonePad.Services;

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

            var request = new ApiRequest { input = input };

            // Act
            var response = controller.OldPhonePad(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedOutput, response.output);
        }
    }
}