using Microsoft.Win32;
using System;
using System.CodeDom;
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
		private int Week = 0;
		private readonly List<Grid> GridList = new();

		/// <summary>
		/// Function to initialize the GeneratedPlan Window
		/// </summary>
		/// <param name="GenerationOptions">The Dictionary in which the Options to generate the plan are stored</param>
		public GeneratedPlanWindow(GenerationOptions GenerationOptions)
		{
			InitializeComponent();

			GeneratePlan(GenerationOptions);
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
		/// The main logic behind creating the plan.
		/// </summary>
		/// <param name="options"> The <see cref="GenerationOptions"/> used for generation.</param>
		private void GeneratePlan(GenerationOptions options)
		{
			Random rand = new(); //In
			foreach(int Week in Enumerable.Range(0, options.WeeksToGenerate))
			{
				Grid grid = new();
				foreach(int Day in Enumerable.Range(0, 7))
				{
					grid.ColumnDefinitions.Add(new());
					foreach(int Meal in Enumerable.Range(0, options.MealsInADay))
					{
						Recipe recipe = options.RecipesToUse[rand.Next(options.RecipesToUse.Count)];
						if(!options.AllowRepeatingRecipes)
						{
							options.RecipesToUse.Remove(recipe);
						}
						TextBlock toBeAdded = new(new Run(recipe.RecipeName));
						toBeAdded.Inlines.Add(new LineBreak());
						toBeAdded.Inlines.Add(new Run(recipe.Allergens.ToString()));
						Grid.SetColumn(toBeAdded, Day);
						Grid.SetRow(toBeAdded, Meal);
						grid.RowDefinitions.Add(new());
						grid.Children.Add(toBeAdded);
					}
				}
				GridList.Add(grid);
			}
			
			MealGrid.Children.Add(GridList[0]);
			Previous.IsEnabled = false;
			if(Week + 1 == GridList.Count)
			{
				Next.IsEnabled = false;
			}
		}

		/// <summary>	An event function that changes the current week displayed. </summary>
		///
		/// <param name="sender">	The button that sends the event. </param>
		/// <param name="e">	 	Unused. </param>

		private void ChangeWeek(object sender, RoutedEventArgs e)
		{
			Button trigger = (sender as Button)!; //narrows down what type of object sender will be.
			switch(trigger.Name.ToLower())
			{
				case "previous":
					MealGrid.Children.Remove(GridList[Week]);
					MealGrid.Children.Add(GridList[--Week]);
					break;
				case "next":
					MealGrid.Children.Remove(GridList[Week]);
					MealGrid.Children.Add(GridList[++Week]);
					break;
			}

			//Disables the Previous button when the current displayed week is 0.
			if (Week == 0)
			{
				Previous.IsEnabled = false;
			}
			else
			{
				Previous.IsEnabled = true;
			}

			//Disables the Next button when the current displayed week is the last generated week.
			if(Week == GridList.Count-1)
			{
				Next.IsEnabled = false;
			}
			else
			{
				Next.IsEnabled = true;
			}

		}

		/// <summary>
		/// Formats the Plan as as a string to be saved.
		/// </summary>
		/// <returns>The stringified version of the plan</returns>
		private string GetContentAsString()
		{
			var content = new StringBuilder();
			foreach(int week in Enumerable.Range(0,GridList.Count))
			{
				foreach(int day in Enumerable.Range(0,GridList[week].Children.Count))
				{
					content.Append($"Week {week}, Day {(day/3)+1}, Meal {(day%3)+1}: ");
					foreach(Inline line in (GridList[week].Children[day] as TextBlock)!.Inlines)
					{
						if(line is Run)
						{
							content.Append($" {(line as Run)!.Text}");
						}
					}
					content.Append('\n');
				}
			}
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
