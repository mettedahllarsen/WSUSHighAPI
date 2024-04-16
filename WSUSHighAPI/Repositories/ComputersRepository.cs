using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WSUSHighAPI.Models;

namespace WSUSHighAPI.Repositories
{

	public class ComputersRepository
	{
		private readonly string _connectionString = "server=localhost;database=WSUSHighDB;" + "user id=sa;password=WRITEPASSWORDHERE;TrustServerCertificate=True";
		
		public IEnumerable<Computer> GetAllComputers()
		{
			List<Computer> computers = new List<Computer>();

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand("SELECT * FROM Computers", connection);
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					Computer computer = new Computer
					{
						ComputerID = Convert.ToInt32(reader["ComputerID"]),
						ComputerName = reader["ComputerName"].ToString(),
						IPAddress = reader["IPAddress"].ToString(),
						OSVersion = reader["OSVersion"].ToString(),
						LastConnection = Convert.ToDateTime(reader["LastConnection"])
					};
					computers.Add(computer);
				}
			}

			return computers;
		}

		public Computer GetComputerById(int id)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand("SELECT * FROM Computers WHERE ComputerID = @ComputerID", connection);
				command.Parameters.AddWithValue("@ComputerID", id);

				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					return new Computer
					{
						ComputerID = Convert.ToInt32(reader["ComputerID"]),
						ComputerName = reader["ComputerName"].ToString(),
						IPAddress = reader["IPAddress"].ToString(),
						OSVersion = reader["OSVersion"].ToString(),
						LastConnection = Convert.ToDateTime(reader["LastConnection"])
					};
				}
				else
				{
					return null; // Return null if no computer with the given ID is found
				}
			}
		}

		public Computer AddComputer(Computer computer)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand("INSERT INTO Computers (ComputerName, IPAddress, OSVersion, LastConnection) VALUES (@ComputerName, @IPAddress, @OSVersion, @LastConnection); SELECT SCOPE_IDENTITY();", connection);
				command.Parameters.AddWithValue("@ComputerName", computer.ComputerName);
				command.Parameters.AddWithValue("@IPAddress", computer.IPAddress);
				command.Parameters.AddWithValue("@OSVersion", computer.OSVersion);
				command.Parameters.AddWithValue("@LastConnection", computer.LastConnection);

				// Execute the command and get the newly generated ID
				int newComputerId = Convert.ToInt32(command.ExecuteScalar());

				// Set the ID of the computer object to the newly generated ID
				computer.ComputerID = newComputerId;

				// Return the computer object
				return computer;
			}
		}



		public void UpdateComputer(Computer computer)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand("UPDATE Computers SET ComputerName = @ComputerName, IPAddress = @IPAddress, OSVersion = @OSVersion, LastConnection = @LastConnection WHERE ComputerID = @ComputerID", connection);
				command.Parameters.AddWithValue("@ComputerID", computer.ComputerID);
				command.Parameters.AddWithValue("@ComputerName", computer.ComputerName);
				command.Parameters.AddWithValue("@IPAddress", computer.IPAddress);
				command.Parameters.AddWithValue("@OSVersion", computer.OSVersion);
				command.Parameters.AddWithValue("@LastConnection", computer.LastConnection);
				command.ExecuteNonQuery();
			}
		}

		public void DeleteComputer(int id)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand("DELETE FROM Computers WHERE ComputerID = @ComputerID", connection);
				command.Parameters.AddWithValue("@ComputerID", id);
				command.ExecuteNonQuery();
			}
		}
	}
}