using Expenser.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("ExpenserConnection")
    ?? throw new InvalidOperationException("Connection string"
                                           + "'ExpenserConnection' not found.");

builder.Services.AddDbContext<ExpenserDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Expenser API");
    });
    app.MapOpenApi();
}



app.UseHttpsRedirection();

app.MapGet("/projects", (ExpenserDbContext expenserDbContext) => expenserDbContext.Projects);
app.MapGet("/projects/{id:int}", (ExpenserDbContext expenserDbContext, int id) => expenserDbContext.Projects.FirstOrDefault(p => p.Id == id));

app.MapGet("/projects/{id:int}/categories", (ExpenserDbContext expenserDbContext, int id) => expenserDbContext.BudgetCategories.Where(c => c.ProjectId == id));
app.MapPost("/projects/{id:int}/categories", async (ExpenserDbContext expenserDbContext, int id, BudgetCategory category) =>
{
    var project = expenserDbContext.Projects.FirstOrDefault(p => p.Id == id);
    
    if (project == null)
        return Results.NotFound();
    
    project.BudgetCategories.Add(category);
    await expenserDbContext.SaveChangesAsync();
    
    return Results.Created($"/categories/{category.Id}", category);
});
app.MapGet("/projects/{id:int}/transactions", (ExpenserDbContext expenserDbContext, int id) =>
{
    var project = expenserDbContext.Projects
        .Include(x => x.TransactionInProjects)
        .ThenInclude(x => x.Transaction)
        .FirstOrDefault(p => p.Id == id);

    return project == null ? Results.NotFound() : Results.Ok(project.Transactions);
});

app.MapPut("/categories/{id:int}", async (ExpenserDbContext expenserDbContext, int id, BudgetCategory category) =>
{
    var budgetCategory = expenserDbContext.BudgetCategories.FirstOrDefault(c => c.Id == id);
    
    if (budgetCategory == null)
        return Results.NotFound();
    
    budgetCategory.Name = category.Name;
    budgetCategory.Description = category.Description;
    budgetCategory.BudgetAmount = category.BudgetAmount;

    await expenserDbContext.SaveChangesAsync();
    
    return Results.NoContent();
});
app.MapGet("/categories/{id:int}", (ExpenserDbContext expenserDbContext, int id) => expenserDbContext.BudgetCategories.FirstOrDefault(c => c.Id == id));

app.MapGet("/accounts", (ExpenserDbContext expenserDbContext) => expenserDbContext.Accounts);
app.MapGet("/accounts/{id:int}", (ExpenserDbContext expenserDbContext, int id) => expenserDbContext.Accounts.FirstOrDefault(c => c.Id == id));
app.MapGet("/accounts/{id:int}/transactions", (ExpenserDbContext expenserDbContext, int id) => expenserDbContext.Transactions.Where(t => t.AccountId == id));

app.MapPut("/transaction/{id:int}/projects", (ExpenserDbContext expenserDbContext, int id, TransactionInProject tip) =>
{
    var tr = expenserDbContext.Transactions.FirstOrDefault(t => t.Id == id);

    if (tr == null)
        return Results.NotFound();

    var trInProj = expenserDbContext.TransactionInProjects
        .FirstOrDefault(x => x.TransactionId == tip.TransactionId && x.ProjectId == tip.ProjectId);

    if (trInProj == null)
    {
        expenserDbContext.TransactionInProjects.Add(tip);
        expenserDbContext.SaveChanges();
        return Results.Created();
    }

    if (trInProj.BudgetCategoryId == tip.BudgetCategoryId) return Results.NoContent();
    
    trInProj.BudgetCategoryId = tip.BudgetCategoryId;
    expenserDbContext.SaveChanges();

    return Results.NoContent();
});

app.MapPost("/accounts/{id:int}/transactions", (ExpenserDbContext expenserDbContext, int id, Transaction transaction) =>
{
    var account = expenserDbContext.Accounts.FirstOrDefault(c => c.Id == id);
    
    if(account == null)
        return Results.NotFound();
    
    account.Transactions.Add(transaction);
    account.Balance += transaction.Amount * (transaction.TransactionTypeId == TransactionType.Values.Debit ? -1 : 1);
    expenserDbContext.SaveChanges();
    
    return Results.Created($"/transactions/{transaction.Id}", transaction);
});

app.Run();
