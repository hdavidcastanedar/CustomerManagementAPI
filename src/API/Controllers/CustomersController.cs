using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: /Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }

        // GET: /Customers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> Get(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST: /Customers
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create([FromBody] CustomerDto customerDto)
        {
            try
            {
                var createdCustomer = await _customerService.CreateAsync(customerDto);
                return CreatedAtAction(nameof(Get), new { id = createdCustomer.Id }, createdCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: /Customers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CustomerDto customerDto)
        {
            if (id != customerDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                await _customerService.UpdateAsync(customerDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: /Customers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _customerService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
