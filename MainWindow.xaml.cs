using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Essensplangenerator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// List of Recipes registered in the App.
		/// </summary>
		readonly List<Recipe> Recipes;

		/// <summary>
		/// The Main Window of the Application.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			Recipes = RecipeFileHandler.LoadSavedRecipes();
			App.Current.Properties["Recipes"] = Recipes;
			AddRecipesToRecipeList();
		}

		/// <summary>
		/// Function to open the AddRecipe Window, used to register new recipes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddRecipe(object sender, RoutedEventArgs e)
		{
			new AddRecipeWindow().ShowDialog();
		}

		/// <summary>
		/// Function to open the Remove Recipe Window, used to remove registered Recipes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemoveRecipe(object sender, RoutedEventArgs e)
		{
			new RemoveRecipeWindow().ShowDialog();
		}

		/// <summary>
		/// Function to add all registered recipes into the <see cref="SavedRecipeList"/> container as <see cref="CheckBox"/>es
		/// </summary>
		private void AddRecipesToRecipeList()
		{
			foreach(Recipe Recipe in Recipes)
			{
				CheckBox checkBox = new()
				{
					Content = Recipe.RecipeName + " - " + Recipe.Allergens,
				}; 

				SavedRecipeList.Children.Insert(0, checkBox);
			}
		}

		/// <summary>
		/// Function to open the window for the generated plan.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void GeneratePlan(object sender, RoutedEventArgs e)
		{
			// Options for the generation
			Dictionary<String, Object> GenerationOptions = new()
			{
				{ "AllowRepeatingRecipes", AllowRepeatableRecipes }
			};

			new GeneratedPlanWindow(GenerationOptions).ShowDialog();
		}

		/// <summary>
		/// The function to add an individual <see cref="Recipe"/> to the <see cref="SavedRecipeList"/> as a <see cref="CheckBox"/>
		/// </summary>
		/// <param name="Recipe">The Recipe to be added to <see cref="SavedRecipeList"/></param>
		public void AddRecipeToRecipeList(Recipe Recipe)
		{
			CheckBox recipeEntry = new()
			{
				Content = Recipe.RecipeName + " - " + Recipe.Allergens,
			};

			SavedRecipeList.Children.Insert(0, recipeEntry);
		}

	}
}
