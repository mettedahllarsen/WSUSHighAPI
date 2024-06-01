namespace WSUSHighAPI.Models
{	public class Computer
	{
		public int ComputerID { get; set; }
		public string ComputerName { get; set; }
		public string IPAddress { get; set; }
		public string OSVersion { get; set; }
		public DateTime LastConnection { get; set; }

		// Override of the ToString() method to return a string representation of the computer object
		public override string ToString()
		{
			return $"ComputerID: {ComputerID}, Name: {ComputerName}, IP: {IPAddress}, OS: {OSVersion}, Last Connection: {LastConnection}";
		}

		// Validation method to check if ComputerName is valid
		public bool ValidateComputerName()
		{
			// Check if ComputerName is empty or null
			if (string.IsNullOrEmpty(ComputerName))
			{
				Console.WriteLine("ComputerName cannot be empty.");
				return false;
			}
			// Check if the length of ComputerName exceeds the limit of 100 characters
			else if (ComputerName.Length > 100)
			{
				Console.WriteLine("ComputerName cannot exceed 100 characters.");
				return false;
			}
			return true;
		}

		// Validation method to check if IPAddress is valid
		public bool ValidateIPAddress()
		{
			// Check if IPAddress is empty or null
			if (string.IsNullOrEmpty(IPAddress))
			{
				Console.WriteLine("IPAddress cannot be empty.");
				return false;
			}
			// Check if the length of IPAddress exceeds the limit of 50 characters
			else if (IPAddress.Length > 50)
			{
				Console.WriteLine("IPAddress cannot exceed 50 characters.");
				return false;
			}
			// Check if IPAddress is a valid IPv4 address
			else if (!System.Net.IPAddress.TryParse(IPAddress, out _))
			{
				Console.WriteLine("IPAddress is not a valid IPv4 address.");
				return false;
			}
			return true;
		}
	}
}
