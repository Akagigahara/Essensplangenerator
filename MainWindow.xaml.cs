using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Diagnostics.Eventing.Reader;

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
		readonly List<Recipe> Recipes = App.recipes;

		/// <summary>
		/// The Main Window of the Application.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			Recipes = RecipeFileHandler.LoadSavedRecipes();
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
			RemoveRecipeWindow removeWindow = new();
			removeWindow.ShowDialog();
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
					IsChecked = true,
				};

				SavedRecipeList.Children.Insert(0, checkBox);
			}
		}

		/// <summary>
		/// Function to open the window for the generated plan.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GeneratePlan(object sender, RoutedEventArgs e)
		{
			GenerationOptions GenerationOptions = new()
			{
				AllowRepeatingRecipes = AllowRepeatableRecipes.IsChecked ?? false,
				MealsInADay = int.Parse(MealsInDay.Text),
			};

			foreach(CheckBox checkBox in (from CheckBox recipe in SavedRecipeList.Children where (recipe.IsChecked ?? false) select recipe))
			{
				GenerationOptions.RecipesToUse.Add((from Recipe recipe in Recipes where checkBox.Content.ToString()!.Contains(recipe.RecipeName) select recipe).First());
			}


			//Readiness checks
			//This check is to prevent users from accidentally crashing or inducing unexpected behavior by not supplying enough recipes to not use AllowRepeatingRecipes
			if(GenerationOptions.MealsInADay * 7 >= GenerationOptions.RecipesToUse.Count && !GenerationOptions.AllowRepeatingRecipes)
			{
				MessageBox.Show($"The required number of {GenerationOptions.MealsInADay * 7} Recipes is not met. Add more recipes or allow repeating recipes.");
			}
			else
			{
				new GeneratedPlanWindow(GenerationOptions).ShowDialog();
			}
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

		/// <summary>
		/// The function used to refresh the Window, called by <see cref="Refresh_Button"/>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RefreshWindow(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new();
			mainWindow.Show();
			Close();
		}

		private void ValidateInput(object sender, TextCompositionEventArgs e)
		{
			Regex Valid = new("[^0-9.-]+");
			e.Handled = Valid.IsMatch(e.Text);
		}
    }
}
