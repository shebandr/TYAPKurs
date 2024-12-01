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

			} else
			{
				ErrorTextBlock.Text = settings[0];
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

			List<string> settings = GetAllSettings();
			List<List<string>> rules = new List<List<string>>();
			List<List<List<string>>> chains = new List<List<List<string>>>();
			string outputRules = "";
			string outputChains = "";


			if (settings.Count != 1)
			{
				rules = RegularGrammar.CalculateRegularGrammar(settings[0], settings[1], settings[2], Int32.Parse(settings[3]), Int32.Parse(settings[4]), Int32.Parse(settings[5]), Int32.Parse(settings[6]));
				if (rules.Count != 1)
				{
					for (int i = 0; i < rules.Count; i++)
					{
						outputRules += rules[i][0] + "->" + rules[i][1] + rules[i][2] + "\n";
					}
					RegularGrammar regularGrammar = new RegularGrammar();
					RegularGrammarOutput.Text = outputRules;
					chains = regularGrammar.CalculateStrings(rules, Int32.Parse(settings[4]), Int32.Parse(settings[5]), Int32.Parse(settings[6]));

					for (int i = 0; i < chains.Count; i++)
					{
						outputChains += i + ") ";
						outputChains += chains[i][chains[i].Count - 1][0] + chains[i][chains[i].Count - 1][1] + " \n";
					}
					Console.WriteLine("Цепочек сгенерировано: " + chains.Count);
					AllChainsOutput.Text = outputChains;
					ErrorTextBlock.Text = "";
				}
			}
			else
			{
				ErrorTextBlock.Text = settings[0];
			}





		}

		private void OutputAllDataChainsButtonClick(object sender, RoutedEventArgs e)
		{
			List<string> settings = GetAllSettings();
			List<List<string>> rules = new List<List<string>>();
			List<List<List<string>>> chains = new List<List<List<string>>>();
			string outputRules = "";
			string outputChains = "";


			if (settings.Count != 1)
			{
				rules = RegularGrammar.CalculateRegularGrammar(settings[0], settings[1], settings[2], Int32.Parse(settings[3]), Int32.Parse(settings[4]), Int32.Parse(settings[5]), Int32.Parse(settings[6]));
				if (rules.Count != 1)
				{
					for (int i = 0; i < rules.Count; i++)
					{
						outputRules += rules[i][0] + "->" + rules[i][1] + rules[i][2] + "\n";
					}
					RegularGrammar regularGrammar = new RegularGrammar();
					RegularGrammarOutput.Text = outputRules;
					chains = regularGrammar.CalculateStrings(rules, Int32.Parse(settings[4]), Int32.Parse(settings[5]), Int32.Parse(settings[6]));

					for (int i = 0; i < chains.Count; i++)
					{
						outputChains += i + ") ";
						for(int q = 0; q< chains[i].Count-1; q++)
						{
							outputChains += chains[i][q][0] + chains[i][q][1] + "->";
						}
						outputChains += chains[i][chains[i].Count - 1][0] + chains[i][chains[i].Count - 1][1] + " \n";
					}
					Console.WriteLine("Цепочек сгенерировано: " + chains.Count);
					AllChainsOutput.Text = outputChains;
					ErrorTextBlock.Text = "";
				}
			}
			else
			{
				ErrorTextBlock.Text = settings[0];
			}

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
			string resultDataCheck = RegularGrammar.CheckAllSettings(settings);
			if (resultDataCheck == "ok")
			{
				return settings;
			}
			else
			{
				return new List<string>{ resultDataCheck };
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
				
				return -1;
			}
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
