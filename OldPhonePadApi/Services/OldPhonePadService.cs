using OldPhonePadApi.Helpers.DecodeInput;
using OldPhonePadApi.Helpers.ValidationInput;

namespace OldPhonePad.Services
{
    public class OldPhonePadService : IOldPhonePadService
    {
        private readonly IValidator _validator;
        private readonly IDecoder _decoder;

        public OldPhonePadService(IValidator validator, IDecoder decoder)
        {
            _validator = validator;
            _decoder = decoder;
        }
        public string OldPhonePad(string input)
        {
            #region Validation
            var validationResult = _validator.InputValidate(input);
            if(validationResult != null && validationResult.RespCode != "000") { return validationResult.Message; }
            #endregion

            #region Decode Input
            var decodeResult = _decoder.DecodeInput(input);
            #endregion

            if(decodeResult != null && decodeResult.RespCode == "000") { return decodeResult.Message; }

            return "Decode Fail";
        }
    }
}
