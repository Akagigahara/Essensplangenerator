using System.Collections.Generic;
using System.Windows;

namespace Essensplangenerator
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		static public List<Recipe> recipes = RecipeFileHandler.LoadSavedRecipes();
	}
}
