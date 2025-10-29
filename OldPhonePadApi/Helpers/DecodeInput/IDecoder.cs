using OldPhonePadApi.Models.ResponseDto;

namespace OldPhonePadApi.Helpers.DecodeInput
{
    public interface IDecoder
    {
        public Response DecodeInput(string input);
    }
}
