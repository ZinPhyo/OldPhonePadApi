using OldPhonePadApi.Models.ResponseDto;
using System.Text.RegularExpressions;

namespace OldPhonePadApi.Helpers.ValidationInput
{
    public class Validator : IValidator
    {
        public Response InputValidate(string input)
        {
            #region Check input is empty
            if (string.IsNullOrEmpty(input))
            {
                return new Response
                {
                    RespCode = "001",
                    RespDescription = "Fail",
                    Message = "Input is Empty"
                };
            }
            #endregion

            #region Check input allow character
            Regex pattern = new Regex(@"^[0-9*# ]+$");
            if (!pattern.IsMatch(input))
            {
                return new Response
                {
                    RespCode = "001",
                    RespDescription = "Fail",
                    Message = "Input only allow 0 to 9 numbers, space, * and #"
                };
            }
            #endregion

            #region Check input contain send character
            if (input.LastIndexOf("#") == -1)
            {
                return new Response
                {
                    RespCode = "001",
                    RespDescription = "Fail",
                    Message = "Input must be end with '#'"
                };
            }
            #endregion

            return new Response();
        }
    }
}
