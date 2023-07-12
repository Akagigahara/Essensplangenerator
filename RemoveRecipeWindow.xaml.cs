using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Windows.Controls;

namespace Essensplangenerator
{

	public partial class RemoveRecipeWindow : Window
	{
		/// <summary>
		/// The list of saved recipes.
		/// </summary>
		readonly List<Recipe> Recipes = App.recipes;
		/// <summary>
		/// An instance of the FileHandler class.
		/// </summary>
		readonly RecipeFileHandler FileHandler = new();

		/// <summary>
		/// A constructor for <see cref="RemoveRecipeWindow"/>
		/// </summary>
		public RemoveRecipeWindow() 
		{
			InitializeComponent();
			foreach (Recipe Recipe in Recipes)
			{
				CheckBox checkBox = new()
				{
					Content = Recipe.RecipeName + " - " + Recipe.Allergens,
				};

				RecipeList.Children.Insert(0, checkBox);
			}
		}

		/// <summary>
		/// The function that gets called when one presses the <see cref="RemoveRecipeButton"/>
		/// </summary>
		/// <param name="sender">Unused</param>
		/// <param name="e">Unused</param>
		private void RemoveRecipe(object sender, RoutedEventArgs e)
		{
			foreach (CheckBox checkBox in (from CheckBox tickedCheckBox
										   in RecipeList.Children
										   where (tickedCheckBox.IsChecked ?? false)
										   select tickedCheckBox))
			{
				Recipe[] recipesToRemove = (from Recipe recipe
											in Recipes
											where checkBox.Content.ToString()!.Contains(recipe.RecipeName)
											select recipe).ToArray();

				foreach(Recipe recipe in recipesToRemove)
				{
					if(checkBox.Content.ToString()!.Contains(recipe.RecipeName))
					{
						FileHandler.RemoveRecipe(recipe);
						App.recipes.Remove(recipe);
					}
				}
			}
			Close();
		}
		
		/// <summary>
		/// Closes the window to return to the main part of the application
		/// </summary>
		/// <param name="sender">Unused</param>
		/// <param name="e">Unused</param>
		private void Return(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}