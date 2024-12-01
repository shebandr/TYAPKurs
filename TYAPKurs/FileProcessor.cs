using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace TYAPKurs
{
	internal class FileProcessor
	{
		public static List<string> ReadConfig(string filePath)
		{
			var config = new List<string>();

			string[] lines;
			try
			{
				lines = File.ReadAllLines(filePath);
				foreach (string line in lines)
				{
					Console.WriteLine(line);
						config.Add(line);




				}
			}
			catch (IOException e)
			{
				Console.WriteLine("Ошибка при чтении файла:");
				Console.WriteLine(e.Message);
				return new List<string> { "Ошибка при чтении файла " + e.Message };
			}
			if(config.Count != 7) {
				return new List<string> { "Глобальная ошибка файла" };

			}
			
			// как выглядит файл:
			// 1 строка - начальная подцепочка; 2 - конечная подцепочка; 3 - алфавит; 4 - кратность; 5 - минимальная длина; 6 - максимальная длина; 7 - выбранная сторона

			return config;
		}
		public static string WriteToFile(string filePath, List<string> data)
		{
			try
			{
				File.WriteAllLines(filePath, data);
				return "Запись успешна";
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
				return $"Ошибка при записи в файл: {ex.Message}";
			}
		}
	}
}
