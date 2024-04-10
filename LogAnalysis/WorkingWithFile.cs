namespace LogAnalysis
{
	public class WorkingWithFile
	{
		public static List<(string, DateTime)> ReadLogFile(string filePath)         //Метод считывает с файла записи. Возвращает список значений ip адрес и дату обращения.
		{
			var logEntries = new List<(string, DateTime)>();
			try
			{
				foreach (var line in File.ReadLines(filePath))
				{
					var parts = line.Split(new char[] { ':' }, 2);
					if (parts.Length == 2 && DateTime.TryParse(parts[1], out var time))
					{
						logEntries.Add((parts[0], time));
						Console.WriteLine($"IP Address: {parts[0]}, Time: {time}");
					}
				}
			}
			catch (FileNotFoundException)  // Если файл не найден 
			{
				throw new FileNotFoundException(filePath);




			}
			catch (ArgumentException)   //если файл не указан 
			{
				throw new ArgumentException("Путь к файлу не может быть пустым");



			}
			catch (Exception ex) //другие исключения
			{
				Console.WriteLine($"An error occurred while reading the file: {ex.Message}");

			}

			return logEntries;
		}

		public static void WriteResults(string filePath, Dictionary<string, int> counts) //Метод записывает в файл отфильтрованые записи. Записывает ip адресс и количество обращаний.
		{
			try
			{
				using (var writer = new StreamWriter(filePath))
				{
					foreach (var kvp in counts)
					{
						writer.WriteLine($"{kvp.Key}: {kvp.Value}");
					}
				}
			}
			catch (DirectoryNotFoundException)
			{
				throw new DirectoryNotFoundException(filePath);

			}
			catch (ArgumentNullException)
			{
				throw new ArgumentException("Путь к директории куда нужно сохранить файл не можут быть пустым");

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);

			}
		}
	}
}
