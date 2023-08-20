using System.Collections.Generic;
using System.Windows;

namespace Essensplangenerator
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		/// <summary>
		/// Storage for the recipe list to make it accessible in every window.
		/// </summary>
		static public List<Recipe> recipes = RecipeFileHandler.LoadSavedRecipes();
	}
}
