namespace UnicontaAPI
{
    public static class EndpointsExtenson
    {
        public static void UseEndpoints(this IEndpointRouteBuilder app)
        {
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
        }
    }
}