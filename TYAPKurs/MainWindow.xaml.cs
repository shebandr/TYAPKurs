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

		private void ReadFromFileByttonClick(object sender, RoutedEventArgs e)
		{

		}

		private void WriteToFileInputDataByttonClick(object sender, RoutedEventArgs e)
		{

		}

		private void WriteToFileOutputDataButtonClick(object sender, RoutedEventArgs e)
		{

		}

		private void OpenWindowAuthorButtonClick(object sender, RoutedEventArgs e)
		{

		}

		private void OpenWindowThemeButtonClick(object sender, RoutedEventArgs e)
		{
			Window themeWindow = new CourseWorkTheme();
			themeWindow.Show();
		}

		private void CalculateAllByttonClick(object sender, RoutedEventArgs e)
		{

		}

		private void OutputAllDataChainsByttonClick(object sender, RoutedEventArgs e)
		{

		}
	}
}
