using FMAPI.Models;
using FMAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace FMAPI.Controllers;

[ApiController]
[Route("api/bbqApi")]
public class ControllerBbq : ControllerBase
{
    private readonly ILogger<ControllerBbq> _logger;
    private readonly BbqService _bbqService;
    
    public ControllerBbq(ILogger<ControllerBbq> logger,  BbqService bbqService)
    {
        _logger = logger;
        _bbqService = bbqService;
    }

    [HttpGet]
    [Route("/listBbq")]
    public async Task<List<BbqModel>> GetListOfBbq([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        return await _bbqService.GetListOfBbq(page, pageSize);
    }

    [HttpGet]
    [Route("/getMarkers")]
    public async Task<List<BbqModel>> GetMapMarkers()
    {
        return await _bbqService.GetMapMarkers();
    }

    [HttpPost]
    [Route("/searchBbq")]
    public async Task<List<BbqModel>> searchBbqByName(string query)
    {
        return await _bbqService.SearchBbqByName(query);
    }
}