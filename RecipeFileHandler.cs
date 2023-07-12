using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace Essensplangenerator
{

	class RecipeFileHandler
	{	
		readonly JsonSerializerOptions options = new()
		{
			WriteIndented = true,
			AllowTrailingCommas = true,
		};

		/// <summary>
		/// This function loads saved <see cref="Recipe"/>s from the SavedRecipes.json file.
		/// </summary>
		/// <returns></returns>
		public static List<Recipe> LoadSavedRecipes()
		{
			List<Recipe> Recipes = new();
			try
			{
				Recipes = JsonSerializer.Deserialize<List<Recipe>>(File.ReadAllText("SavedRecipes.json"))!;
			}
			catch (JsonException error)
			{
				MessageBox.Show("Could not load SavedRecipes.json \n Error:" + 
					error.Message,
					"File loading error");
			}
			catch (FileNotFoundException error)
			{
				MessageBox.Show("Could not load SavedRecipes.json \n Error:" +
					error.Message +
					"\n\n If you previously had a file, please check its name and location.",
					"File loading Error");
			}
			return Recipes;
		}

		/// <summary>
		/// This function saves <see cref="Recipe"/>s to the SavedRecipes.json file
		/// </summary>
		/// <param name="Recipe">Appends the SavedRecipes.json with the <see cref="Recipe"/> in this parameter</param>
		public void SaveRecipes(Recipe Recipe)
		{

			if (!File.Exists("SavedRecipes.json")) //The branch that creates a new SavedRecipes.json if it doesn't exist
			{
				FileStream RecipeFile = File.Create("SavedRecipes.json");
				RecipeFile.Write(Encoding.UTF8.GetBytes("["+ JsonSerializer.Serialize(Recipe, options) + "]"));
				RecipeFile.Close();
			}

			else //The branch that appends the Recipe to the SavedRecipes.json
			{
				FileStream RecipeFile = new("SavedRecipes.json", new FileStreamOptions() { Access = FileAccess.ReadWrite });
				RecipeFile.Seek(-1, SeekOrigin.End);
				RecipeFile.Write(Encoding.UTF8.GetBytes("," + JsonSerializer.Serialize(Recipe, options) + "]"));
				RecipeFile.Close();
			}
		}

		/// <summary>
		/// Removes a Recipe from the SavedRecipes.json file. It does this by removing the recipe from the
		/// RecipeList and then serializing the new list into SavedRecipes.json.
		/// </summary>
		/// <param name="RecipeToRemove">The recipes to be removed</param>
		public void RemoveRecipe(Recipe RecipeToRemove)
		{
			List<Recipe> Recipes = App.recipes;
			Recipes.Remove(RecipeToRemove);
			File.Delete("SavedRecipes.json");
			FileStream RecipeFile = File.Create("SavedRecipes.json");
			RecipeFile.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(Recipes, options)));
			RecipeFile.Close();
		}
	}
}
