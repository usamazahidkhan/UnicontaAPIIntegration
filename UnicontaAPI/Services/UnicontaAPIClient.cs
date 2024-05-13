namespace UnicontaAPI
{
    public sealed class UnicontaAPIClient
    {
        public UnicontaConnection Connection { get; }
        public Session Session { get; }
        public Guid APIKey { get; }

        public UnicontaAPIClient(string apiKey)
        {
            Connection = new UnicontaConnection(APITarget.Live);
            Session = new Session(Connection);
            this.APIKey = new Guid(apiKey);
        }

        public async Task<Result> LoginAsync(string loginId, string password)
        {
            var status = await Session.LoginAsync(loginId, password, LoginType.API, APIKey);

            if (status != ErrorCodes.Succes)
            {
                return Result.Failed(new Error(nameof(ErrorCodes), status.ToString()));
            }

            return Result.Success();
        }

        public async Task<Result> CreateInvoiceAsync(CreateInvoiceDto request)
        {
            var company = await Session.GetCompany(request.CompanyId);

            if (company == null)
            {
                return Result.Failed(new Error(nameof(request.CompanyId),
                                     $"Company with CompanyId '{request.CompanyId}' not found"));
            }

            var filter = PropValuePair.GenereteWhereElements(
                nameof(DebtorOrderClient.OrderNumber),
                request.OrderNumber,
                CompareOperator.Equal
            );
            
            var crudAPI = new CrudAPI(Session, company);

            var orders = await crudAPI.Query<DebtorOrderClient>([filter]);
            var order = orders.FirstOrDefault();

            if (order == null)
            {
                return Result.Failed(new Error(nameof(request.OrderNumber),
                                     $"Order with OrderNumber '{request.OrderNumber}' not found"));
            }

            var orderLines = await crudAPI.Query<DebtorOrderLineClient>(order);

            var invoiceAPI = new InvoiceAPI(crudAPI);
            var invoiceResult = await invoiceAPI.PostInvoice(
                order,
                orderLines,
                request.Date ?? DateTime.Now,
                request.InvoiceNumber,
                false
            );

            if (invoiceResult == null || invoiceResult.Err != ErrorCodes.Succes)
            {
                List<Error> errors = [];
                
                if (invoiceResult != null)
                {
                    errors.Add(new(nameof(ErrorCodes), invoiceResult.Err.ToString()));
                }
                return Result.Failed(errors, invoiceResult, "Unable to Create an Invoice.");
            }

            return Result.Success(invoiceResult, "Invoice is sucessfully created.");
        }
    }
}