using Microsoft.AspNetCore.Mvc;
using UnicontaAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

//app.UseAuthorization();

app.MapPost("/invoice/create", async (
    [AsParameters] UnicontaCredentialsDto credentials,
    [FromBody] CreateInvoiceDto request)
    =>
{
    var uniconta = new UnicontaAPIClient(credentials.APIKey);

    var result = await uniconta.LoginAsync(credentials.LoginId, credentials.Password);

    if (!result.IsSucess)
        return Results.UnprocessableEntity(result);

    result = await uniconta.CreateInvoiceAsync(request);

    if (!result.IsSucess)
        return Results.UnprocessableEntity(result);

    return Results.Ok(result);
});

app.MapControllers();

app.Run();