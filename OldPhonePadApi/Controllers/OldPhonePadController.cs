using Microsoft.AspNetCore.Mvc;
using OldPhonePad.Services;
using OldPhonePadApi.Models.ApiModels;

namespace OldPhonePad.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OldPhonePadController : ControllerBase
    {
        private readonly ILogger<OldPhonePadController> _logger;
        private IOldPhonePadService _oldPhonePadService;

        public OldPhonePadController(ILogger<OldPhonePadController> logger, IOldPhonePadService oldPhonePadService)
        {
            _logger = logger;
            _oldPhonePadService = oldPhonePadService;
        }

        [HttpPost(Name = "OldPhonePad")]
        public ApiResponse OldPhonePad(ApiRequest req)
        {
            return new ApiResponse
            {
                Output = _oldPhonePadService.OldPhonePad(req.Input)
            };
        }
    }
}
