using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TYAPKurs
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
/*			RegularGrammarOutput.Text = "a\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\na";
*/		}

		private void ReadFromFileButtonClick(object sender, RoutedEventArgs e)
		{
			string filePath = "";
			
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory = "c:\\";
			openFileDialog.Filter = "txt files (*.txt)|*.txt";
			openFileDialog.RestoreDirectory = true;
			openFileDialog.ShowDialog();
			filePath = openFileDialog.FileName;
			if (filePath == "")
			{
				return;
			}
			Console.WriteLine(filePath);
			List<string> config = FileProcessor.ReadConfig(filePath);
			foreach (string configItem in config)
			{
				Console.WriteLine(configItem);
			}
			StartChainInput.Text = config[0];
			EndChainInput.Text = config[1];
			AlphabetInput.Text = config[2];
			MultipleInput.Text = config[3];
			MinChainLengthInput.Text = config[4];
			MaxChainLengthInput.Text = config[5];
			if (config[6] == "0")
			{
				LeftLinear.IsChecked = true;
			} else if (config[6] == "1")
			{
				RightLinear.IsChecked = true;
			}

		}

		private void WriteToFileInputDataButtonClick(object sender, RoutedEventArgs e)
		{
			List<string> settings = GetAllSettings();


			SaveFileDialog saveFileDialog = new SaveFileDialog();
			
			saveFileDialog.InitialDirectory = "c:\\";
			saveFileDialog.Filter = "txt files (*.txt)|*.txt";
			saveFileDialog.RestoreDirectory = true;

			
			
			if (settings.Count != 1)
			{
				saveFileDialog.ShowDialog();
				string filePath = saveFileDialog.FileName;
				if (filePath == "")
				{
					return;
				}
				FileProcessor.WriteToFile(filePath, settings);
				Console.WriteLine(filePath);

			}



		}

		private void WriteToFileOutputDataButtonClick(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			saveFileDialog.InitialDirectory = "c:\\";
			saveFileDialog.Filter = "txt files (*.txt)|*.txt";
			saveFileDialog.RestoreDirectory = true;
			saveFileDialog.ShowDialog();

			string filePath = saveFileDialog.FileName;
			
			if (filePath == "")
			{
				return;
			}
			Console.WriteLine(filePath);

		}

		private void OpenWindowAuthorButtonClick(object sender, RoutedEventArgs e)
		{
			Window aboutAuthorWindow = new AboutAuthor();
			aboutAuthorWindow.Show();
		}

		private void OpenWindowThemeButtonClick(object sender, RoutedEventArgs e)
		{
			Window themeWindow = new CourseWorkTheme();
			themeWindow.Show();
		}

		private void CalculateAllButtonClick(object sender, RoutedEventArgs e)
		{

		}

		private void OutputAllDataChainsButtonClick(object sender, RoutedEventArgs e)
		{

		}

		private List<string> GetAllSettings()
		{
			Console.WriteLine("СЧИТЫВАНИЕ НАСТРОЕК!!!!");
			List<string> settings = new List<string>
			{
				StartChainInput.Text,
				EndChainInput.Text,
				AlphabetInput.Text,
				MultipleInput.Text,
				MinChainLengthInput.Text,
				MaxChainLengthInput.Text,
				GetRadioButtonValue().ToString()
			};

			foreach (string configItem in settings)
			{
				Console.WriteLine(configItem);
			}

			if (CheckAllSettings(settings) == "ok")
			{
				return settings;
			}
			else
			{
				return new List<string>{"Ошибка"};
			}

		}

		public int GetRadioButtonValue()
		{
			if (LeftLinear.IsChecked == true)
			{
				return 0;
			}
			else if (RightLinear.IsChecked == true)
			{
				return 1;
			}
			else
			{
				// Если ни одна радиокнопка не выбрана, можно вернуть -1 или другое значение по умолчанию
				return -1;
			}
		}
		private string CheckAllSettings(List<string> settings)
		{
			if (settings[0] != settings[0].Replace(" ", ""))
			{
				ErrorTextBlock.Text = "Присутствуют пробелы в начальной подцепочке";
				return "Присутствуют пробелы в начальной подцепочке";
			}
			if (settings[1] != settings[1].Replace(" ", ""))
			{
				ErrorTextBlock.Text = "Присутствуют пробелы в конечной подцепочке";
				return "Присутствуют пробелы в конечной подцепочке";
			}
			if (settings[2].Length == 0)
			{
				ErrorTextBlock.Text = "Алфавит пуст";
				return "Алфавит пуст";
			}

			//проверка алфавита на то, что все нетерминальные символы имеют длину в 1 символ и уникальные

			if (settings[2].Replace(" ", "").Length != settings[2].Split(' ').Length)
			{
				ErrorTextBlock.Text = "Какой-то из нетерминальных символов является строкой";
				return "Какой-то из нетерминальных символов является строкой";
			}
			List<string> TempListAlphabet = settings[2].Split(' ').ToList();
			for(int i = 0; i < TempListAlphabet.Count-1; i++)
			{
				for(int q = i+1; q<TempListAlphabet.Count; q++)
				{
					if (TempListAlphabet[i] == TempListAlphabet[q])
					{
						ErrorTextBlock.Text = "Символы алфавита номер " + i + " и номер " + q + " одинаковые";
						return "Символы алфавита номер " + i + " и номер " + q + " одинаковые";
					}
				}
			}
			// проверка начальной и конечной строки на соответствие алфавиту
			for (int q = 0; q < settings[0].Length; q++)
			{
				bool tempCheckFlag = true;
				for (int i = 0; i < TempListAlphabet.Count; i++)
				{
					
					if (TempListAlphabet[i] == settings[0][q].ToString())
					{
						tempCheckFlag = false;

					}
					
				}
				if (tempCheckFlag)
				{
					ErrorTextBlock.Text = "В начальной подстроке символ номер " + q + " (" + settings[0][q] + ") отсутствует в алфавите";
					return "В начальной подстроке символ номер " + q + " (" + settings[0][q] + ") отсутствует в алфавите";
				}
			}
			for (int q = 0; q < settings[1].Length; q++)
			{
				bool tempCheckFlag = true;
				for (int i = 0; i < TempListAlphabet.Count; i++)
				{

					if (TempListAlphabet[i] == settings[1][q].ToString())
					{
						tempCheckFlag = false;
					}

				}
				if (tempCheckFlag)
				{
					ErrorTextBlock.Text = "В конечной подстроке символ номер " + q + " (" + settings[1][q] + ") отсутствует в алфавите";
					return "В конечной подстроке символ номер " + q + " (" + settings[1][q] + ") отсутствует в алфавите";
				}
			}


			if (int.TryParse(settings[3], out int result))
			{

			}
			else
			{
				ErrorTextBlock.Text = "значение инпута кратности цепочки не переводится в число";
				return "значение инпута кратности цепочки не переводится в число";
			}

			if (int.TryParse(settings[4], out int result2))
			{

			}
			else
			{
				ErrorTextBlock.Text = "значение инпута минимальной длины цепочки не переводится в число";
				return "значение инпута минимальной длины цепочки не переводится в число";
			}
			
			if (int.TryParse(settings[5], out int result3))
			{

			}
			else
			{
				ErrorTextBlock.Text = "значение инпута максимальной длины цепочки не переводится в число";
				return "значение инпута максимальной длины цепочки не переводится в число";
			}

			if (Int32.Parse(settings[3]) > Int32.Parse(settings[5]))
			{
				ErrorTextBlock.Text = "Кратность длины цепочки не может быть больше длины цепочки";
				return "Кратность длины цепочки не может быть больше длины цепочки";
			}
			if (Int32.Parse(settings[3]) == 0)
			{
				ErrorTextBlock.Text = "Кратность длины цепочки не может быть равна нулю";
				return "Кратность длины цепочки не может быть равна нулю";
			}

			if (result2 > result3)
			{
				ErrorTextBlock.Text = "Минимальная длина цепочки больше максимальной";
				return "Минимальная длина цепочки больше максимальной";
			}



			return "ok";
		}

/*		public static void SetError(string message)
		{
			if(message == "")
			{
				ErrorTextBlock.Text = "";
				ErrorTextBlock.Height = 0;
			} else
			{
				ErrorTextBlock.Text = message;
				ErrorTextBlock.Height = 30;
			}
		}*/
	}
}
