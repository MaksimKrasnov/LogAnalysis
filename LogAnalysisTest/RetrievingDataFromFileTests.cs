using System;
using System.Collections.Generic;
using LogAnalysis;
using Xunit;

namespace LogAnalysis.Tests
{
	public class RetrievingDataFromFileTests
	{
		/// <summary>
		/// Проверка что метод правильно фильтрует записи журнала по по указанному аргументу командной строки и возвращает отфильтрованый списко
		/// </summary>
		[Fact]
		public void FilterLogEntriesByAddress_ShouldReturnFilteredEntries()
		{
			
			var logEntries = new List<(string, DateTime)>
			{
				("192.168.1.1", DateTime.Now),
				("192.168.1.2", DateTime.Now),
				("192.168.1.3", DateTime.Now),
				("192.168.2.1", DateTime.Now),
			};

			var commandLineArguments = new CommandLineArguments
			{
				AddressStart = "192.168.1.1",
				AddressMask = 24,
				TimeStart = null,
				TimeEnd = null
			};


			
			var result = RetrievingDataFromFile.FilterLogEntriesByAddress(logEntries, commandLineArguments);

			
			Assert.Equal(3, result.Count);
		}

		/// <summary>
		/// Проверка что метод правильно фильтрует записи по временному диапазону
		/// </summary>
		[Fact]
		public void FilterLogEntriesByTime_ShouldReturnFilteredEntries()
		{
			
			var logEntries = new List<(string, DateTime)>
			{
				("192.168.1.1", DateTime.Now),
				("192.168.1.2", DateTime.Now.AddHours(-1)),
				("192.168.1.3", DateTime.Now.AddHours(-2)),
			};
			var commandLineArguments = new CommandLineArguments
			{
				AddressStart = null,
				AddressMask = 0,
				TimeStart = DateTime.Now.AddHours(-2).ToString("yyyy-MM-dd HH:mm:ss"),
				TimeEnd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
			};

			
			var result = RetrievingDataFromFile.FilterLogEntriesByTime(logEntries, commandLineArguments);

			
			Assert.Equal(2, result.Count);
		}
		/// <summary>
		/// Проверка что метод правильно подсчитывает количество вхождений уникального IP-адреса
		/// </summary>
		[Fact]
		public void CountOccurrences_ShouldReturnCorrectCounts()
		{
			
			var logEntries = new List<(string, DateTime)>
			{
				("192.168.1.1", DateTime.Now),
				("192.168.1.2", DateTime.Now),
				("192.168.1.1", DateTime.Now),
				("192.168.1.3", DateTime.Now),
			};

			
			var result = RetrievingDataFromFile.CountOccurrences(logEntries);

			
			Assert.Equal(2, result["192.168.1.1"]);
			Assert.Equal(1, result["192.168.1.2"]);
			Assert.Equal(1, result["192.168.1.3"]);
		}
		/// <summary>
		/// Проверка что метод возвращает пустой список если не одна запись не попала в диапазон адресов
		/// </summary>
		[Fact]
		public void FilterLogEntriesByAddress_ShouldReturnEmptyListWhenNoMatches()
		{
			
			var logEntries = new List<(string, DateTime)>
			{
				("192.168.1.1", DateTime.Now),
				("192.168.1.2", DateTime.Now),
				("192.168.1.3", DateTime.Now),
				("192.168.2.1", DateTime.Now),
			};

			var commandLineArguments = new CommandLineArguments
			{
				AddressStart = "10.0.0.1",
				AddressMask = 24,
				TimeStart = null,
				TimeEnd = null
			};


			
			var result = RetrievingDataFromFile.FilterLogEntriesByAddress(logEntries, commandLineArguments);

		
			Assert.Empty(result);
		}
		/// <summary>
		/// Проверка что метод возвращает пустой список если не одна запись не попала во временной диапазон
		/// </summary>
		[Fact]
		public void FilterLogEntriesByTime_ShouldReturnEmptyListWhenNoMatches()
		{
		
			var logEntries = new List<(string, DateTime)>
			{
				("192.168.1.1", DateTime.Now),
				("192.168.1.2", DateTime.Now.AddHours(-1)),
				("192.168.1.3", DateTime.Now.AddHours(-2)),
			};
#pragma warning disable CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
			var commandLineArguments = new CommandLineArguments
			{
				AddressStart = null,
				AddressMask = 0,
				TimeStart = DateTime.Now.AddHours(-4).ToString("yyyy-MM-dd HH:mm:ss"),
				TimeEnd = DateTime.Now.AddHours(-3).ToString("yyyy-MM-dd HH:mm:ss")
			};
#pragma warning restore CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.

			
			var result = RetrievingDataFromFile.FilterLogEntriesByTime(logEntries, commandLineArguments);

			
			Assert.Empty(result);
		}
		/// <summary>
		/// Проверка что метод возвращает пустой словарь если в отфильтрованом списке нет записей
		/// </summary>
		[Fact]
		public void CountOccurrences_ShouldReturnEmptyDictionaryWhenNoEntries()
		{
			
			var logEntries = new List<(string, DateTime)>();

			
			var result = RetrievingDataFromFile.CountOccurrences(logEntries);

			
			Assert.Empty(result);
		}
	}
}
