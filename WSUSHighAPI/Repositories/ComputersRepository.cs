using WSUSHighAPI.Contexts;
using WSUSHighAPI.Models;

namespace WSUSHighAPI.Repositories
{
	public class ComputersRepository
	{
		private readonly WSUSHighDbContext _context;

		public ComputersRepository(WSUSHighDbContext context)
		{
			_context = context;
		}

		public List<Computer> GetAllComputers()
		{
			return _context.Computers.ToList();
		}

		public Computer? GetComputerById(int id)
		{
			return _context.Computers.FirstOrDefault(c => c.ComputerID == id);
		}

		public void AddComputer(Computer computer)
		{
			computer.ValidateComputerName();
			computer.ValidateIPAddress();
			_context.Computers.Add(computer);
			_context.SaveChanges();
		}
		public void UpdateComputer(int id, Computer updatedComputer)
		{
			Computer? existingComputer = _context.Computers.Find(id);
			if (existingComputer != null)
			{
				// Validér navn og IP-adresse hvis de ikke er null
				if (!string.IsNullOrEmpty(updatedComputer.ComputerName))
				{
					updatedComputer.ValidateComputerName();
					existingComputer.ComputerName = updatedComputer.ComputerName;
				}

				if (!string.IsNullOrEmpty(updatedComputer.IPAddress))
				{
					updatedComputer.ValidateIPAddress();
					existingComputer.IPAddress = updatedComputer.IPAddress;
				}

				if (!string.IsNullOrEmpty(updatedComputer.OSVersion))
				{
					existingComputer.OSVersion = updatedComputer.OSVersion;
				}

				if (updatedComputer.LastConnection != null)
				{
					existingComputer.LastConnection = updatedComputer.LastConnection;
				}

				_context.SaveChanges();
			}
			else
			{
				throw new InvalidOperationException("Computer not found");
			}
		}

		public void DeleteComputer(int id)
		{
			Computer? computer = _context.Computers.Find(id);
			if (computer == null)
			{
				throw new InvalidOperationException("Computer not found");
			}
			else 
			{
				_context.Computers.Remove(computer);
				_context.SaveChanges();
			}
		}
	}
}