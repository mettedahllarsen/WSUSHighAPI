using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace WSUSHighAPI.Services
{
	public class TcpClientService
	{
		private readonly int _port;

		public TcpClientService(int port)
		{
			_port = port;
		}

		public async Task<string> GetVersionAsync(string ipAddress)
		{
			using (var client = new System.Net.Sockets.TcpClient(ipAddress, _port))
			{
				NetworkStream stream = client.GetStream();

				string requestJson = JsonConvert.SerializeObject(new { method = "get_version_number", ip_address = ipAddress });
				byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
				await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

				// Read response asynchronously
				byte[] responseBytes = new byte[1024];
				int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);

				// Process response
				string responseJson = Encoding.UTF8.GetString(responseBytes, 0, bytesRead);
				dynamic response = JsonConvert.DeserializeObject(responseJson);

				if (response["version"] != null)
				{
					return response["version"].ToString();
				}
				else
				{
					throw new Exception("Error: Could not retrieve version number");
				}
			}
		}

		public async Task<bool> UpdateAsync(string ipAddress)
		{
			using (var client = new System.Net.Sockets.TcpClient(ipAddress, _port))
			{
				NetworkStream stream = client.GetStream();

				string requestJson = JsonConvert.SerializeObject(new { method = "update", ip_address = ipAddress });
				byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
				await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

				// Read response asynchronously
				byte[] responseBytes = new byte[1024];
				int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);

				// Process response
				string responseJson = Encoding.UTF8.GetString(responseBytes, 0, bytesRead);
				dynamic response = JsonConvert.DeserializeObject(responseJson);

				if (response["success"] != null && response["success"] == true)
				{
					return true; 
				}
				else
				{
					throw new Exception("Error: Update failed on server");
				}
			}
		}
	}
}