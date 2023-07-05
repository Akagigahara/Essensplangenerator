using System;

namespace Essensplangenerator
{
	public class Recipe
	{
		/// <summary>
		/// The name of the Recipe
		/// </summary>
		public string RecipeName { get; set; } = "";

		/// <summary>
		/// The allergens in the Recipe
		/// </summary>
		public AllergenList Allergens
		{ get; set; }

		/// <summary>
		/// The enum for the allergens contained within the recipe
		/// </summary>
		[Flags]
		public enum AllergenList
		{
			None = 0,
			Gluten = 1,
			Shellfish = 1 << 1,
			Peanuts = 1 << 2,
			Lactose = 1 << 3,
			Celery = 1 << 4,
			TreeNuts = 1 << 5,
			Sesame = 1 << 6,
			Soy = 1 << 7,
		}
	}
}
