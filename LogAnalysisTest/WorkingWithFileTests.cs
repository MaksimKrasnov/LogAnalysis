using System;
using System.Collections.Generic;
using Xunit;
using LogAnalysis;

namespace LogAnalysis.Tests
{
	public class WorkingWithFileTests
	{
		/// <summary>
		///�������� ��� ��� �������� ��������� ���� ����� ��������� �� ������ ������
		/// </summary>
		[Fact]
		public void ReadLogFile_WithValidFilePath_ShouldReturnNonEmptyList()
		{
			string validFilePath = "C:\\C#\\LogAnalysis\\LogAnalysis\\bin\\Debug\\net8.0\\Log.txt";

			var logEntries = WorkingWithFile.ReadLogFile(validFilePath);

			Assert.NotNull(logEntries);
			Assert.NotEmpty(logEntries);
		}
		/// <summary>
		/// �������� ��� ��� �������� ����������� ���� ����� ���������
		/// </summary>
		[Fact]
		public void ReadLogFile_WithInvalidFilePath_ShouldThrowFileNotFoundException()
		{
			string invalidFilePath = "invalidLogFile.txt";

			Assert.Throws<FileNotFoundException>(() => WorkingWithFile.ReadLogFile(invalidFilePath));
		}
		/// <summary>
		/// �������� ���� �� �������� ���� � ����� ����� ����������
		/// </summary>
		[Fact]
		public void ReadLogFile_WithEmptyFilePath_ShouldThrowArgumentException()
		{
			string emptyFilePath = "";

			Assert.Throws<ArgumentException>(() => WorkingWithFile.ReadLogFile(emptyFilePath));
		}
		/// <summary>
		/// ��������� ������ � ����
		/// </summary>
		[Fact]

		public void WriteResults_WithValidArguments_ShouldWriteToFile()
		{
			string filePath = "output.txt";

			var expectedCounts = new Dictionary<string, int>
				{
					{ "192.168.0.1", 10 },
					{ "192.168.0.2", 5 }
				};

			WorkingWithFile.WriteResults(filePath, expectedCounts);

			Assert.True(File.Exists(filePath), "���� �� ��� ������");

			var lines = File.ReadAllLines(filePath);

			Assert.Equal(expectedCounts.Count, lines.Length);

			foreach (var line in lines)
			{
				var parts = line.Split(':');
				Assert.Equal(2, parts.Length);
				var ipAddress = parts[0];
				var countStr = parts[1];

				Assert.Contains(ipAddress, expectedCounts.Keys);

				int actualCount;
				Assert.True(int.TryParse(countStr, out actualCount), "�� ������� ���������� ����������");
				Assert.Equal(expectedCounts[ipAddress], actualCount);
			}
		}
		/// <summary>
		/// �������� ���� �� �������� ���� � ����� ����� ����������
		/// </summary>
		[Fact]
		public void WriteResults_WithEmptyFilePath_ShouldThrowArgumentException()
		{

			string emptyFilePath = "";
			var counts = new Dictionary<string, int>
	{
		{ "192.168.0.1", 10 },
		{ "192.168.0.2", 5 }
	};


			Assert.Throws<Exception>(() => WorkingWithFile.WriteResults(emptyFilePath, counts));
		}
	}
}
