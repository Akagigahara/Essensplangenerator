using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Essensplangenerator
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class AddRecipeWindow : Window
	{
		readonly RecipeFileHandler FileHandler = new();

		/// <summary>
		/// Handles the setup of the AddRecipe Window
		/// </summary>
		public AddRecipeWindow() => InitializeComponent();

		/// <summary>
		/// Event function that handles the event sent by <see cref="SaveRecipe"/>
		/// </summary>
		/// <param name="sender">Unused</param>
		/// <param name="e">Unused</param>
		public void SaveButton(object sender, RoutedEventArgs e)
		{
			Recipe.AllergenList FlagsSet = 0;

			//This foreach loop cycles through the
			//allergen checkboxes and applies the applicable
			//bit-flag.
			foreach (CheckBox Box in AllergenCheckList.Children)
			{												
				if(Box.IsChecked is true)					
				{
					switch(Box.Content) 
					{
						case "Gluten": 
							FlagsSet |= Recipe.AllergenList.Gluten;
							break;
						case "Shellfish": 
							FlagsSet |= Recipe.AllergenList.Shellfish;
							break;
						case "Peanuts":
							FlagsSet |= Recipe.AllergenList.Peanuts;
							break;
						case "Lactose":
							FlagsSet |= Recipe.AllergenList.Lactose;
							break;
						case "Celery":
							FlagsSet |= Recipe.AllergenList.Celery;
							break;
						case "Tree Nuts":
							FlagsSet |= Recipe.AllergenList.TreeNuts;
							break;
						case "Sesame":
							FlagsSet |= Recipe.AllergenList.Sesame;
							break;
						case "Soy":
							FlagsSet |= Recipe.AllergenList.Soy;
							break;
					}
				}
			}

			//Creates the newly registered user-defined recipe as a Recipe object.
			Recipe NewRecipe = new()
			{
				RecipeName = RecipeName.Text,
				Allergens = FlagsSet,
			};

			List<Recipe> Recipes = App.recipes; // Loads the recipe list from the app's memory.
			Recipes.Add(NewRecipe); //User-defined recipe is added to the recipe list.
			FileHandler.SaveRecipes(NewRecipe); //Recipe is saved in SavedRecipes.json.
			App.recipes = Recipes; //Saves the recipe to the app's memory.

			this.Close(); //Closes the window.
		}
	}
}
