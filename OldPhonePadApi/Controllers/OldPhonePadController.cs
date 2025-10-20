using Microsoft.AspNetCore.Mvc;
using OldPhonePad.Models;
using OldPhonePad.Services;

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
                output = _oldPhonePadService.OldPhonePad(req.input)
            };
        }
    }
}
