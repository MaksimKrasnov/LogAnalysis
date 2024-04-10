using LogAnalysis;

namespace LogAnalyzer
{
	class Program
	{
		private static CommandLineArguments? commandLineArgs;

		static void Main(string[] args)
		{
			try
			{
				Console.WriteLine('1');

				commandLineArgs = new CommandLineArguments(args);
			}
			catch (ArgumentException ex)
			{

				Console.WriteLine(ex.Message);
				return;
			}
			try
			{

				Console.WriteLine($"file-log: {commandLineArgs.FileLog}");
				Console.WriteLine($"file-output: {commandLineArgs.FileOutput}");
				Console.WriteLine($"address-start: {commandLineArgs.AddressStart}");
				Console.WriteLine($"address-mask: {commandLineArgs.AddressMask}");
				Console.WriteLine($"time-start: {commandLineArgs.TimeStart}");
				Console.WriteLine($"time-end: {commandLineArgs.TimeEnd}");
			}
			catch (ArgumentException ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
			// Чтение файла журнала
			var counts = new Dictionary<string, int>();
			try
			{
				var logEntries = WorkingWithFile.ReadLogFile(commandLineArgs.FileLog);
				var filteredEntries = RetrievingDataFromFile.FilterLogEntriesByAddress(logEntries, commandLineArgs);
				counts = RetrievingDataFromFile.CountOccurrences(filteredEntries);


			}
			catch (FileNotFoundException ex)
			{
				Console.WriteLine($"Не найден файл по указанному пути: {ex.Message}");
				return;

			}
			catch (ArgumentException ex)
			{
				Console.WriteLine(ex.Message);
				return;

			}
			catch (Exception ex)
			{
				Console.WriteLine($" {ex.Message}");
				return;

			}
			// Фильтрация записей журнала по заданным параметрам

			// Подсчет количества обращений с каждого адреса

			// Запись результатов в файл
			try
			{
				WorkingWithFile.WriteResults(commandLineArgs.FileOutput, counts);

				Console.WriteLine("Анализ завершен. Результаты записаны в файл: " + commandLineArgs.FileOutput);
			}
			catch (DirectoryNotFoundException ex)
			{
				Console.WriteLine($"Данная директория не существует {ex.Message}");
			}
			catch (ArgumentNullException ex)
			{
				Console.WriteLine($" {ex.Message}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($" {ex.Message}");
			}
		}
	}
}




