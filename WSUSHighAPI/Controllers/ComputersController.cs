using WSUSHighAPI.Models;
using WSUSHighAPI.Repositories;
using WSUSHighAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace WSUSHighAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ComputersController : ControllerBase
	{
		private readonly ComputersRepository _computersRepository;

		public ComputersController(ComputersRepository computersRepository)
		{
			_computersRepository = computersRepository;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public ActionResult<IEnumerable<Computer>> Get()
		{
			List<Computer> computers = _computersRepository.GetAllComputers();
			
			if (computers.Any())
			{
				return Ok(computers);
			}
			else
			{
				return NoContent();
			}
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<Computer> GetComputerById(int id)
		{
			Computer? computer = _computersRepository.GetComputerById(id);

			if (computer != null)
			{
				return Ok(computer);
			}
			else
			{
				return NotFound();
			}
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<Computer> AddComputer([FromBody] Computer computer)
		{
			try
			{
				_computersRepository.AddComputer(computer);
				return CreatedAtAction(nameof(GetComputerById), new { id = computer.ComputerID }, computer);
			}
			catch (ArgumentNullException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<Computer> UpdateComputer(int id, [FromBody] Computer updatedComputer)
		{
			Computer? existingComputer = _computersRepository.GetComputerById(id);
			if (existingComputer != null)
			{
				try
				{
					_computersRepository.UpdateComputer(id, updatedComputer);
					return Ok(updatedComputer);
				}
				catch (ArgumentNullException ex)
				{
					return BadRequest(ex.Message);
				}
				catch (ArgumentOutOfRangeException ex)
				{
					return BadRequest(ex.Message);
				}
			}
			else
			{
				return NotFound();
			}
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult DeleteComputer(int id)
		{
			Computer? computer = _computersRepository.GetComputerById(id);
			if (computer != null)
			{
				_computersRepository.DeleteComputer(id);
				return NoContent();
			}
			else
			{
				return NotFound();
			}
		}


		[HttpGet("{id}/version")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<string>> GetVersion(int id)
		{
			Computer? computer = _computersRepository.GetComputerById(id);
			if (computer != null)
			{
				string ipAddress = computer.IPAddress;

				try
				{
					TcpClientService tcpClient = new(5000); // Assuming the port remains constant
					string version = await tcpClient.GetVersionAsync(ipAddress);
					return Ok(version);
				}
				catch (Exception ex)
				{
					return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
				}
			}
			else 
			{
				return NotFound("Computer not found");
			}
		}

		[HttpPost("{id}/update")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult> Update(int id)
		{
			Computer? computer = _computersRepository.GetComputerById(id);
			if (computer != null)
			{
				string ipAddress = computer.IPAddress;

				try
				{
					TcpClientService tcpClient = new(5000);
					bool updateSuccess = await tcpClient.UpdateAsync(ipAddress);
					if (updateSuccess)
					{
						return Ok("Update successful");
					}
					else
					{
						return StatusCode(StatusCodes.Status500InternalServerError, "Update failed");
					}
				}
				catch (Exception ex)
				{
					return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
				}
			}
			else 
			{
				return NotFound("Computer not found");
			}
		}
	}
}