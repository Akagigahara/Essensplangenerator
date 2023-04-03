using System.Windows;

namespace Essensplangenerator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			AddRecipeWindow RecipeWindow = new();
			RecipeWindow.Show();
		}
	}
}
