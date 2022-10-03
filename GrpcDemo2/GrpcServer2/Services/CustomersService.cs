using Grpc.Core;
using GrpcServer2.Protos;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcServer2.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;
        public CustomersService( ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();
            if (request.UserId == 1)
            {
                output.FirstName = "Joshua";
                output.LastName = "Kimmich";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Marco";
                output.LastName = "Reus";
            }
            else
            {
                output.FirstName = "Jude";
                output.LastName = "Bellingham";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request,
                                            IServerStreamWriter<CustomerModel> responseStream, 
                                            ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Bishwash",
                    LastName = "Parajuli",
                    EmailAddress = "bishwash@parajuli.com",
                    Age = 24,
                    IsAlive = true,
                },
                new CustomerModel
                {
                    FirstName = "John",
                    LastName = "Wick",
                    EmailAddress = "john@wick.com",
                    Age = 45,
                    IsAlive = true,
                },
                new CustomerModel
                {
                    FirstName = "Tino",
                    LastName = "Nori",
                    EmailAddress = "tino@nori.com",
                    Age = 91,
                    IsAlive = false,
                }
            };
            foreach(var cust in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
