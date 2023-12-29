
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Data.Entity;
using Vb.Data;

namespace VbApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountTransactionsController : ControllerBase
{
    private readonly VbDbContext dbContext;

    public AccountTransactionsController(VbDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<AccountTransaction>> Get()
    {
        return await dbContext.Set<AccountTransaction>()
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<AccountTransaction> Get(int id)
    {
        var accounttransaction = await dbContext.Set<AccountTransaction>()
            .Where(x => x.AccountId == id).FirstOrDefaultAsync();

        return accounttransaction;
    }

    [HttpPost]
    public async Task Post([FromBody] AccountTransaction accounttransaction)
    {
        await dbContext.Set<AccountTransaction>().AddAsync(accounttransaction);
        await dbContext.SaveChangesAsync();
    }

    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] AccountTransaction accounttransaction)
    {
        var fromdb = await dbContext.Set<AccountTransaction>().Where(x => x.AccountId == id).FirstOrDefaultAsync();
        fromdb.ReferenceNumber = accounttransaction.ReferenceNumber;
        fromdb.TransactionDate = accounttransaction.TransactionDate;

        await dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        var fromdb = await dbContext.Set<AccountTransaction>().Where(x => x.AccountId == id).FirstOrDefaultAsync();
        await dbContext.SaveChangesAsync();
    }
}
