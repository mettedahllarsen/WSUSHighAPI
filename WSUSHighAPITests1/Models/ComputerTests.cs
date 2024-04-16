﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSUSHighAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSUSHighAPI.Models.Tests
{
	[TestClass]
	public class ComputerTests
	{
		[TestMethod]
		// Test if ValidateComputerName returns true for a valid computer name.
		public void ValidateComputerName_ValidName_ReturnsTrue()
		{
			// Arrange
			var computer = new Computer
			{
				ComputerName = "MyComputer"
			};

			// Act
			bool isValid = computer.ValidateComputerName();

			// Assert
			Assert.IsTrue(isValid);
		}

		[TestMethod]
		// Test if ValidateComputerName returns false for a null value for the computer name.
		public void ValidateComputerName_NullName_ReturnsFalse()
		{
			// Arrange
			var computer = new Computer
			{
				ComputerName = null
			};

			// Act
			bool isValid = computer.ValidateComputerName();

			// Assert
			Assert.IsFalse(isValid);
		}

		[TestMethod]
		// Test if ValidateComputerName returns true for a computer name within the maximum length.
		public void ValidateComputerName_MaxLengthName_ReturnsTrue()
		{
			// Arrange
			var computer = new Computer
			{
				ComputerName = new string('A', 100) // Create a string with 100 characters
			};

			// Act
			bool isValid = computer.ValidateComputerName();

			// Assert
			Assert.IsTrue(isValid);
		}

		[TestMethod]
		// Test if ValidateIPAddress returns true for a valid IP address.
		public void ValidateIPAddress_ValidIPAddress_ReturnsTrue()
		{
			// Arrange
			var computer = new Computer
			{
				IPAddress = "192.168.1.100"
			};

			// Act
			bool isValid = computer.ValidateIPAddress();

			// Assert
			Assert.IsTrue(isValid);
		}

		[TestMethod]
		// Test if ValidateIPAddress returns false for a null value for the IP address.
		public void ValidateIPAddress_NullIPAddress_ReturnsFalse()
		{
			// Arrange
			var computer = new Computer
			{
				IPAddress = null
			};

			// Act
			bool isValid = computer.ValidateIPAddress();

			// Assert
			Assert.IsFalse(isValid);
		}

		[TestMethod]
		// Test if ValidateIPAddress returns false for an invalid IP address.
		public void ValidateIPAddress_InvalidIPAddress_ReturnsFalse()
		{
			// Arrange
			var computer = new Computer
			{
				IPAddress = "InvalidIPAddress"
			};

			// Act
			bool isValid = computer.ValidateIPAddress();

			// Assert
			Assert.IsFalse(isValid);
		}

		[TestMethod]
		// Test if ValidateOSVersion returns true for a valid OS version.
		public void ValidateOSVersion_ValidOSVersion_ReturnsTrue()
		{
			// Arrange
			var computer = new Computer
			{
				OSVersion = "Windows 10"
			};

			// Act
			bool isValid = computer.ValidateOSVersion();

			// Assert
			Assert.IsTrue(isValid);
		}

		[TestMethod]
		// Test if ValidateOSVersion returns false for a null value for the OS version.
		public void ValidateOSVersion_NullOSVersion_ReturnsFalse()
		{
			// Arrange
			var computer = new Computer
			{
				OSVersion = null
			};

			// Act
			bool isValid = computer.ValidateOSVersion();

			// Assert
			Assert.IsFalse(isValid);
		}

		[TestMethod]
		// Test if ValidateOSVersion returns true for an OS version within the maximum length.
		public void ValidateOSVersion_MaxLengthOSVersion_ReturnsTrue()
		{
			// Arrange
			var computer = new Computer
			{
				OSVersion = new string('A', 100) // Create a string with 100 characters
			};

			// Act
			bool isValid = computer.ValidateOSVersion();

			// Assert
			Assert.IsTrue(isValid);
		}
	}
}