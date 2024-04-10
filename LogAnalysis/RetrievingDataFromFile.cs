using NetTools;
using System.Net;

namespace LogAnalysis
{
	public class RetrievingDataFromFile
	{
		/// <summary>
		/// Метод фильтрует данные из лог файла. Если ip адресс и маска передна то фильтрует в переданном диапазоне, если нет то переходим к фильтрации по времени
		/// </summary>
		
		public static List<(string, DateTime)> FilterLogEntriesByAddress(List<(string, DateTime)> logEntries, CommandLineArguments commandLineArguments)  
		{
			IEnumerable<(string, DateTime)> filteredEntries = logEntries;

			if (commandLineArguments.AddressMask > 0)
			{
				var range = IPAddressRange.Parse(commandLineArguments.AddressStart + "/" + commandLineArguments.AddressMask);
				filteredEntries = filteredEntries.Where(entry => range.Contains(IPAddress.Parse(entry.Item1)));
			}
			filteredEntries = FilterLogEntriesByTime(filteredEntries, commandLineArguments);
			return filteredEntries.ToList();
		}
		/// <summary>
		/// Метод фильтрует данные из лог файла по заданному временному диапазону. Если диапазон не задан то выбирается записи за текущий день
		/// </summary>
		
		public static List<(string, DateTime)> FilterLogEntriesByTime(IEnumerable<(string, DateTime)> logEntries, CommandLineArguments commandLineArguments)
		{
			IEnumerable<(string, DateTime)> filteredEntries = logEntries;

			DateTime startTime = DateTime.Today;
			DateTime endTime = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59);

			if (commandLineArguments.TimeStart != null || commandLineArguments.TimeEnd != null)//Если не указана верхняя или нижняя граница временного диапозона результат будет выводится за текущий день
			{
				startTime = DateTime.ParseExact(commandLineArguments.TimeStart, "yyyy-MM-dd", null);
				endTime = DateTime.ParseExact(commandLineArguments.TimeEnd, "yyyy-MM-dd HH:mm:ss", null);
			}


			filteredEntries = filteredEntries.Where(entry => entry.Item2 >= startTime && entry.Item2 <= endTime);

			return filteredEntries.ToList();
		}
		//Метод считает количество обращений с каждого адреса
		public static Dictionary<string, int> CountOccurrences(List<(string, DateTime)> entries)
		{
			var counts = new Dictionary<string, int>();
			foreach (var entry in entries)
			{
				if (counts.ContainsKey(entry.Item1))
				{
					counts[entry.Item1]++;
				}
				else
				{
					counts[entry.Item1] = 1;
				}
			}
			return counts;
		}

	}
}
