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
            _logger.LogInformation("Getting BBQ list. Page: {Page}, PageSize: {PageSize}", page, pageSize);
            return await _context.bbq
                .OrderBy(x => x.Id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting BBQ list.");
            throw;
        }
    }
    
    public async Task<List<BbqModel>> GetMapMarkers()
    {
        try
        {
            _logger.LogInformation("Getting map markers");
            return await _context.bbq
                .AsNoTracking()
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
            _logger.LogInformation("Searching bbq by name");
            var result = await _context.bbq.FromSqlInterpolated($@"
                SELECT *
                FROM public.restaurants
                WHERE tags->>'name' ILIKE '%' || {query} || '%'
            ").ToListAsync();
            
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw;
        }
    }
}