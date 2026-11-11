using FMAPI.Context;
using FMAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FMAPI.Service;

public class BbqService
{
    private readonly ILogger<BbqService> _logger;
    private readonly BbqContext _context;

    public BbqService(ILogger<BbqService> logger, BbqContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<List<BbqModel>> GetListOfBbq(int page, int pageSize)
    {
        try
        {
            _logger.LogInformation(
                "Getting restaurants list. Page: {Page}, PageSize: {PageSize}",
                page, pageSize);

            return await _context.bbq
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting restaurants list.");
            throw;
        }
    }

    public async Task<List<BbqModel>> GetMapMarkers()
    {
        try
        {
            _logger.LogInformation("Getting restaurant map markers");

            return await _context.bbq
                .AsNoTracking()
                .Where(x => x.Latitude != null && x.Longitude != null)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting map markers");
            throw;
        }
    }

    public async Task<List<BbqModel>> SearchBbqByName(string query)
    {
        try
        {
            _logger.LogInformation("Searching restaurants by name");

            return await _context.bbq
                .AsNoTracking()
                .Where(x => x.Name != null && EF.Functions.ILike(x.Name, $"%{query}%"))
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error searching restaurants by name");
            throw;
        }
    }
}