using Hexagonal.Arch.Domain.Dtos;
using Hexagonal.Arch.Domain.Ports;

namespace Hexagonal.Arch.Api.Endpoints;

public static class CustomerEndpoints
{
    public static void RegisterCustomersEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("customers", async (CreateCustomerDto customer, ICreateCustomerService createCustomerService) => {
            await createCustomerService.CreateAsync(customer);
            return TypedResults.Created();
        })
        .WithName("CreateCustomer")
        .WithOpenApi();

        endpoints.MapGet("customers/{id}", async (int id, IGetCustomerService getCustomerService) => {

            return TypedResults.Ok(await getCustomerService.GetAsync(id));
        })
        .WithName("GetCustomer")
        .WithOpenApi();
    }
}