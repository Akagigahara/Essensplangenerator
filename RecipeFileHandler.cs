using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Essensplangenerator
{
	class RecipeFileHandler
	{
		/// <summary>
		/// This function loads saved <see cref="Recipe"/>s from the SavedRecipes.json file.
		/// </summary>
		/// <returns></returns>
		public static List<Recipe> LoadSavedRecipes()
		{
			List<Recipe> Recipes = new();
			if(File.Exists("SavedRecipes.json"))
			{
				Recipes = JsonSerializer.Deserialize<List<Recipe>>(File.ReadAllText("SavedRecipes.json")) ?? new();
			}
			return Recipes;
		}

		/// <summary>
		/// This function saves <see cref="Recipe"/>s to the SavedRecipes.json file
		/// </summary>
		/// <param name="Recipe">Appends the SavedRecipes.json with the <see cref="Recipe"/> in this parameter</param>
		public static void SaveRecipes(Recipe Recipe)
		{

			if (!File.Exists("SavedRecipes.json")) //The branch that creates a new SavedRecipes.json if it doesn't exist
			{
				FileStream RecipeFile = File.Create("SavedRecipes.json");
				RecipeFile.Write(Encoding.UTF8.GetBytes("["+ JsonSerializer.Serialize(Recipe) + "]"));
				RecipeFile.Close();
			}

			else //The branch that appends the Recipe to the SavedRecipes.json
			{
				FileStream RecipeFile = new("SavedRecipes.json", new FileStreamOptions() { Access = FileAccess.ReadWrite });
				RecipeFile.Seek(-2, SeekOrigin.End);
				RecipeFile.Write(Encoding.UTF8.GetBytes("," + JsonSerializer.Serialize(Recipe) + "]"));
				RecipeFile.Close();
			}
		}

		/// <summary>
		/// Removes a Recipe from the SavedRecipes.json file. It does this by removing the recipe from the
		/// RecipeList and then serializing the new list into SavedRecipes.json.
		/// </summary>
		/// <param name="Recipe">The recipe to be removed</param>
		public static void RemoveRecipe(Recipe Recipe)
		{
			List<Recipe> Recipes = (List<Recipe>)(App.Current.Properties["Recipes"] ?? new());
			Recipes.Remove(Recipe);
			File.Delete("SavedRecipes.json");
			FileStream RecipeFile = File.Create("SavedRecipes.json");
			RecipeFile.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(Recipes)));
			RecipeFile.Close();
		}
	}
}
