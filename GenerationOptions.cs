using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensplangenerator
{
	/// <summary>
	/// A class dedicated
	/// </summary>
	public class GenerationOptions
	{
		/// <summary>
		/// Whether the user allows repeating recipes or not.
		/// </summary>
		public bool AllowRepeatingRecipes;

		/// <summary>
		/// How many weeks the user wants to generate.
		/// </summary>
		public int WeeksToGenerate;

		/// <summary>
		/// How many meals in a day the user wants to have.
		/// </summary>
		public int MealsInADay;

		/// <summary>
		/// The recipes the user chose.
		/// </summary>
		public List<Recipe> RecipesToUse = new();
	}
}
