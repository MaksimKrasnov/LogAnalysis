namespace LogAnalysis
{
	public class CommandLineArguments                                                                         //DTO класс для хранеиния аргументов командной строки 
	{
		public string? FileLog { get; set; } 
		public string? FileOutput { get; set; } 

		public string? TimeStart { get; set; } 
		public string? TimeEnd { get; set; } 

		public string AddressStart { get; set; } = "0.0.0.0";
		public int AddressMask { get; set; } = 0;
		

		public CommandLineArguments() { }
		public CommandLineArguments(string[] args)
		{

			for (int i = 0; i < args.Length; i++)
			{
				switch (args[i])
				{
					case "--file-log":
						FileLog = args[++i]; 
						break;
					case "--file-output":
						FileOutput = args[++i];
						break;
					case "--address-start":
						AddressStart = args[++i];
						break;
					case "--address-mask":
						if (!int.TryParse(args[++i], out int mask))
						{
							throw new ArgumentException("Invalid value for address mask.");
						}
						AddressMask = mask;
						break;
					case "--time-start":
						TimeStart = args[++i];
						break;
					case "--time-end":
						TimeEnd = args[++i]+" 23:59:59";
						break;
					default:
						throw new ArgumentException($"Недопустимый аргумент: {args[i]}");
				}
			}
		}
	}
}

