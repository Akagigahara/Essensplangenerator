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
			foreach(CheckBox Box in AllergenCheckList.Children) //This foreach loop cycles through the
			{													//allergen checkboxes and applies the applicable
				if(Box.IsChecked is true)						//bit-flag
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

			Recipe NewRecipe = new()
			{
				RecipeName = RecipeName.Text,
				Allergens = FlagsSet,
			};

			List<Recipe> Recipes = App.recipes;
			Recipes.Add(NewRecipe);
			FileHandler.SaveRecipes(NewRecipe);
			App.recipes = Recipes;

			foreach(Window Window in App.Current.Windows)
			{
				if (Window is MainWindow)
				{
#pragma warning disable CS8602 // Dereference of a possibly null reference. Cannot otherwise be removed, it is impossible for Window to evaluate as null.
					(Window as MainWindow).AddRecipeToRecipeList(NewRecipe);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
				}
			}
			this.Close();
		}
	}
}
