using System;

namespace Essensplangenerator
{
	/// <summary>
	/// Class describing a recipe object
	/// </summary>
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
			/// <summary>
			/// The flag value for no allergens
			/// </summary>
			None = 0,

			/// <summary>
			/// The flag value for gluten
			/// </summary>
			Gluten = 1,

			/// <summary>
			/// The flag value for shellfish
			/// </summary>
			Shellfish = 1 << 1,

			/// <summary>
			/// The flag value for peanuts
			/// </summary>
			Peanuts = 1 << 2,

			/// <summary>
			/// The flag value for lactose
			/// </summary>
			Lactose = 1 << 3,

			/// <summary>
			/// The flag value for celery
			/// </summary>
			Celery = 1 << 4,

			/// <summary>
			/// The flag value for tree nuts
			/// </summary>
			TreeNuts = 1 << 5,

			/// <summary>
			/// The flag value for tree nuts
			/// </summary>
			Sesame = 1 << 6,

			/// <summary>
			/// The flag value for soy
			/// </summary>
			Soy = 1 << 7,
		}
	}
}
