using CSharpAdvanced.Oop.Infrastructure;
using CSharpAdvanced.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAdvanced.Api.Controllers
{
	[ApiController]
	[Route("api/customers")]
	public class CustomersController : ControllerBase
	{
		[HttpGet]
		public IActionResult DoAll([FromServices] ICustomerRepository repository)
		{
            var customer = new Customer("LuisDev");
            var id = repository.Add(customer);

            var existingCustomer = repository.GetCustomerById(id);

            return Ok(existingCustomer);
		}
	}
}

