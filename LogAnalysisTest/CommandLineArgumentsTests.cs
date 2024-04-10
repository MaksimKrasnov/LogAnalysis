using Xunit;
using LogAnalysis;
using System;

namespace LogAnalysis.Tests
{
	public class CommandLineArgumentsTests
	{
		/// <summary>
		///  Проверяет что конструктор по умолчанию устанавливает значения по умолчанию для аргументов командной строки
		/// </summary>
		[Fact]
		public void DefaultConstructor_ShouldSetDefaultValues()
		{
			var commandLineArgs = new CommandLineArguments();

			Assert.Equal("0.0.0.0", commandLineArgs.AddressStart);
			Assert.Equal(0, commandLineArgs.AddressMask);
			Assert.Null(commandLineArgs.FileLog);
			Assert.Null(commandLineArgs.FileOutput);
			Assert.Null(commandLineArgs.TimeStart);
			Assert.Null(commandLineArgs.TimeEnd);
		}
		/// <summary>
		///  Проверяет что конструктор с передачей валидных аргументов устанавливает свойства корректно
		/// </summary>
		[Fact]
		public void Constructor_WithValidArgs_ShouldSetProperties()
		{
			string[] args = {
				"--file-log", "Log.txt",
				"--file-output", "Res.txt",
				"--address-start", "127.0.0.1",
				"--address-mask", "24",
				"--time-start", "2024-04-09 09:00:00",
				"--time-end", "2024-04-09 11:00:00"
			};

			var commandLineArgs = new CommandLineArguments(args);

			Assert.Equal("Log.txt", commandLineArgs.FileLog);
			Assert.Equal("Res.txt", commandLineArgs.FileOutput);
			Assert.Equal("127.0.0.1", commandLineArgs.AddressStart);
			Assert.Equal(24, commandLineArgs.AddressMask);
			Assert.Equal("2024-04-09 09:00:00", commandLineArgs.TimeStart);
			Assert.Equal("2024-04-09 11:00:00", commandLineArgs.TimeEnd);
		}
		/// <summary>
		/// Проверяет что конструктор с невалидным значением для маски адреса выбрасывает исключение ArgumentException
		/// </summary>
		[Fact]
		public void Constructor_WithInvalidMask_ShouldThrowArgumentException()
		{
			string[] args = {
				"--address-mask", "invalid_mask"
			};

			Assert.Throws<ArgumentException>(() => new CommandLineArguments(args));
		}
		/// <summary>
		/// Проверяет что конструктор с неизвестным аргументом выбрасывает исключение ArgumentException
		/// </summary>
		[Fact]
		public void Constructor_WithUnknownArgument_ShouldThrowArgumentException()
		{
			// Arrange
			string[] args = {
				"--unknown-arg", "value"
			};

			// Act & Assert
			Assert.Throws<ArgumentException>(() => new CommandLineArguments(args));
		}
	}
}
