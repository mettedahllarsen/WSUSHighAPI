using System.Data.SqlClient;
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

		public void UpdateComputer(Computer computer)
		{
			computer.ValidateComputerName();
			computer.ValidateIPAddress();
			_context.Computers.Update(computer);
			_context.SaveChanges();
		}

		public void DeleteComputer(int id)
		{
			var computer = _context.Computers.Find(id);
			if (computer != null)
			{
				_context.Computers.Remove(computer);
				_context.SaveChanges();
			}
		}
	}
}