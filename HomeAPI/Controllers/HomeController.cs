using AutoMapper;
using HomeAPI.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HomeAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    private IOptions<HomeOptions> _options;
    private IMapper _mapper;

    public HomeController(IOptions<HomeOptions> options, IMapper mapper)
    {
        _options = options;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("info")]
    public IActionResult Info()
    {
        var infoResponse = _mapper.Map<HomeOptions,HomeAPI.Contracts.Models.Home.InfoResponse>(_options.Value);

        return StatusCode(200, infoResponse);
    }
}