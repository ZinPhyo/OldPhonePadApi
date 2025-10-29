using OldPhonePadApi.Models.ResponseDto;

namespace OldPhonePadApi.Helpers.ValidationInput
{
    public interface IValidator
    {
        public Response InputValidate(string input);
    }
}
