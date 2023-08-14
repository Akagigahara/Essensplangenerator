using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Essensplangenerator
{
	/// <summary>
	/// Interaction logic for GeneratedPlanWindow.xaml
	/// </summary>
	public partial class GeneratedPlanWindow : Window
	{
		//readonly List<List<List<TextBlock>>> Days;

		/// <summary>
		/// Function to initialize the GeneratedPlan Window
		/// </summary>
		/// <param name="GenerationOptions">The Dictionary in which the Options to generate the plan are stored</param>
		public GeneratedPlanWindow(GenerationOptions GenerationOptions)
		{
			InitializeComponent();

			//This region is for setting up and sorting the TextBlocks in an easily accessible format
			#region Days sorting
			/*List<TextBlock> MondayLunch = new(){ LunchMondayRecipe, LunchMondayAllergens};
			List<TextBlock> MondayDinner = new() { DinnerMondayRecipe, DinnerMondayAllergens };
			List<List<TextBlock>> Monday = new() { MondayLunch, MondayDinner };

			List<TextBlock> TuesdayLunch = new() { LunchTuesdayRecipe, LunchTuesdayAllergens };
			List<TextBlock> TuesdayDinner = new() { DinnerTuesdayRecipe, DinnerTuesdayAllergens };
			List<List<TextBlock>> Tuesday = new() { TuesdayLunch, TuesdayDinner };

			List<TextBlock> WednesdayLunch = new() { LunchWednesdayRecipe, LunchWednesdayAllergens };
			List<TextBlock> WednesdayDinner = new() { DinnerWednesdayRecipe,  DinnerWednesdayAllergens };
			List<List<TextBlock>> Wednesday = new() { WednesdayLunch, WednesdayDinner };

			List<TextBlock> ThursdayLunch = new() { LunchThursdayRecipe, LunchThursdayAllergens };
			List<TextBlock> ThursdayDinner = new() { DinnerThursdayRecipe, DinnerThursdayAllergens };
			List<List<TextBlock>> Thursday = new() { ThursdayLunch, ThursdayDinner};

			List<TextBlock> FridayLunch = new() { LunchFridayRecipe, LunchFridayAllergens };
			List<TextBlock> FridayDinner = new() { DinnerFridayRecipe, DinnerFridayAllergens };
			List<List<TextBlock>> Friday = new() { FridayLunch, FridayDinner };

			List<TextBlock> SaturdayLunch = new() { LunchSaturdayRecipe, LunchSaturdayAllergens };
			List<TextBlock> SaturdayDinner = new() { DinnerSaturdayRecipe, DinnerSaturdayAllergens };
			List<List<TextBlock>> Saturday = new() {  SaturdayLunch, SaturdayDinner };

			List<TextBlock> SundayLunch = new() { LunchSundayRecipe, LunchSundayAllergens };
			List<TextBlock> SundayDinner = new() { DinnerSundayRecipe, DinnerSundayAllergens };
			List<List<TextBlock>> Sunday = new() { SundayLunch, SundayDinner };

			Days = new() { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday};*/
			#endregion
			Random rand = new(); // New Random object to obtain random Recipe from Recipes
			List<Recipe> Recipes = GenerationOptions.RecipesToUse!;

			foreach(int Day in Enumerable.Range(0, MealGrid.ColumnDefinitions.Count))
			{ 
				foreach(int Meal in Enumerable.Range(1, GenerationOptions.MealsInADay))
				{
					Recipe recipe = Recipes[rand.Next(Recipes.Count)];
					TextBlock toBeAdded = new( new Run(recipe.RecipeName));
					toBeAdded.Inlines.Add(new LineBreak());
					toBeAdded.Inlines.Add(new Run(recipe.Allergens.ToString()));
					Grid.SetColumn(toBeAdded, Day);
					Grid.SetRow(toBeAdded, Meal);
					MealGrid.RowDefinitions.Add(new RowDefinition());
					MealGrid.Children.Add(toBeAdded);
				}
			}
		/*	foreach (var day in Days)
			{
				foreach(var course in day)
				{
					Recipe recipe = Recipes[rand.Next(Recipes.Count)];
					if ((bool)GenerationOptions["AllowRepeatingRecipes"])
					{
						Recipes.Remove(recipe);
					}
					course[0].Text = recipe.RecipeName;
					course[1].Text = recipe.Allergens.ToString();
				}
			}
		*/
		}

		/// <summary>
		/// Function for the <see cref="SavePlan_Button"/> 
		/// </summary>
		/// <param name="sender">Unused</param>
		/// <param name="e">Unused</param>
		public void SavePlan(object sender,  RoutedEventArgs e)
		{
			SaveFileDialog saveDialog = new()
			{
				FileName = "MealPlan",
				Filter = "",
				Title = "Save Meal Plan...",
			};

			saveDialog.ShowDialog();
			if(saveDialog.FileName is not "")
			{
				FileStream saveFileStream  = (FileStream)saveDialog.OpenFile();
				saveFileStream.Write(Encoding.UTF8.GetBytes(GetContentAsString()));
				saveFileStream.Close();
			}
		}

		/// <summary>
		/// Formats the Plan as as a string to be saved.
		/// </summary>
		/// <returns>The stringified version of the plan</returns>
		private string GetContentAsString()
		{
			var content = new StringBuilder();
			/*foreach(var day in Days)
			{
				foreach(var course in day)
				{
					content.Append(course[0].Text + course[1].Text + Environment.NewLine);
				}
			}*/
			return content.ToString();
		}

		/// <summary>
		/// Function to close the plan window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void ClosePlan(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
