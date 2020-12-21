using System.Collections.Generic;

namespace Day21
{
    internal class Food
    {
        public List<Ingredient> Ingredients { get; init; } = new();

        public List<Allergen> Allergens { get; init; } = new();

    }
}
