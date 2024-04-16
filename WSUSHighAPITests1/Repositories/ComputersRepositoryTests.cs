using WSUSHighAPI.Models;

namespace WSUSHighAPI.Repositories.Tests
{
	[TestClass]
	public class ComputersRepositoryTests
	{
		private readonly ComputersRepository _computersRepository;

		public ComputersRepositoryTests()
		{
			// Initialize the ComputersRepository for testing
			_computersRepository = new ComputersRepository();
		}

		[TestMethod]
		public void GetAllComputers_ReturnsNotEmptyList()
		{
			// Act
			var computers = _computersRepository.GetAllComputers();

			// Assert
			Assert.IsNotNull(computers);
			Assert.IsTrue(computers.Any()); // Check if the list contains any elements
		}

		[TestMethod]
		public void GetComputerById_ExistingId_ReturnsComputer()
		{
			// Arrange
			int existingId = 3;

			// Act
			var computer = _computersRepository.GetComputerById(existingId);

			// Assert
			Assert.IsNotNull(computer);
			Assert.AreEqual(existingId, computer.ComputerID);
		}

		[TestMethod]
		public void GetComputerById_NonExistingId_ReturnsNull()
		{
			// Arrange
			int nonExistingId = -1;

			// Act
			var computer = _computersRepository.GetComputerById(nonExistingId);

			// Assert
			Assert.IsNull(computer);
		}

		[TestMethod]
		public void AddComputer_ValidComputer_AddsSuccessfully()
		{
			// Arrange
			var computer = new Computer
			{
				ComputerName = "TestComputer",
				IPAddress = "192.168.1.1",
				OSVersion = "Windows 10",
				LastConnection = DateTime.Now
			};

			// Act
			var addedComputer = _computersRepository.AddComputer(computer);

			// Assert
			Assert.IsNotNull(addedComputer);
			Assert.AreNotEqual(0, addedComputer.ComputerID); // Ensure the ID is not 0
			Assert.AreEqual(computer.ComputerName, addedComputer.ComputerName);
			Assert.AreEqual(computer.IPAddress, addedComputer.IPAddress);
			Assert.AreEqual(computer.OSVersion, addedComputer.OSVersion);
			Assert.AreEqual(computer.LastConnection.ToString("yyyy-MM-dd HH:mm:ss"), addedComputer.LastConnection.ToString("yyyy-MM-dd HH:mm:ss"));
		}

		[TestMethod]
		public void UpdateComputer_ValidComputer_UpdatesSuccessfully()
		{
			// Arrange
			var computer = new Computer
			{
				ComputerID = 3,
				ComputerName = "UpdatedTestComputer",
				IPAddress = "192.168.1.2",
				OSVersion = "Windows 11",
				LastConnection = DateTime.Now
			};

			// Act
			_computersRepository.UpdateComputer(computer);
			var updatedComputer = _computersRepository.GetComputerById(computer.ComputerID);

			// Assert
			Assert.IsNotNull(updatedComputer);
			Assert.AreEqual(computer.ComputerName, updatedComputer.ComputerName);
			Assert.AreEqual(computer.IPAddress, updatedComputer.IPAddress);
			Assert.AreEqual(computer.OSVersion, updatedComputer.OSVersion);
			Assert.AreEqual(computer.LastConnection.ToString("yyyy-MM-dd HH:mm:ss"), updatedComputer.LastConnection.ToString("yyyy-MM-dd HH:mm:ss")); // Compare DateTime strings to avoid millisecond differences
		}

		[TestMethod]
		public void DeleteComputer_ExistingId_DeletesSuccessfully()
		{
			// Arrange
			var computer = new Computer
			{
				ComputerName = "TestComputerToDelete",
				IPAddress = "192.168.1.3",
				OSVersion = "Windows 10",
				LastConnection = DateTime.Now
			};
			_computersRepository.AddComputer(computer); // Add a computer to delete it later

			// Act
			_computersRepository.DeleteComputer(computer.ComputerID);
			var deletedComputer = _computersRepository.GetComputerById(computer.ComputerID);

			// Assert
			Assert.IsNull(deletedComputer);
		}
	}
}