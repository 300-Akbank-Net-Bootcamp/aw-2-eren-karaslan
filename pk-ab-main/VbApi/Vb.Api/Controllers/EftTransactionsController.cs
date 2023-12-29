
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Data.Entity;
using Vb.Data;

namespace VbApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EftTransactionsController : ControllerBase
{
    private readonly VbDbContext dbContext;

    public EftTransactionsController(VbDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<EftTransaction>> Get()
    {
        return await dbContext.Set<EftTransaction>()
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<EftTransaction> Get(int id)
    {
        var efttransaction = await dbContext.Set<EftTransaction>()
            .Where(x => x.AccountId == id).FirstOrDefaultAsync();

        return efttransaction;
    }

    [HttpPost]
    public async Task Post([FromBody] EftTransaction efttransaction)
    {
        await dbContext.Set<EftTransaction>().AddAsync(efttransaction);
        await dbContext.SaveChangesAsync();
    }

    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] EftTransaction efttransaction)
    {
        var fromdb = await dbContext.Set<EftTransaction>().Where(x => x.AccountId == id).FirstOrDefaultAsync();
        fromdb.ReferenceNumber = efttransaction.ReferenceNumber;
        fromdb.TransactionDate = efttransaction.TransactionDate;

        await dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        var fromdb = await dbContext.Set<EftTransaction>().Where(x => x.AccountId == id).FirstOrDefaultAsync();
        await dbContext.SaveChangesAsync();
    }
}
