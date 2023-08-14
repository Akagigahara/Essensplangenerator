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
		public bool AllowRepeatingRecipes;
		public int DaysToGenerate;
		public int MealsInADay;
		public List<Recipe> RecipesToUse = new();
	}
}
