namespace Day21
{
    internal class Ingredient
    {
        public string Name { get; init; }

        public Ingredient(string name)
        {
            Name = name;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Ingredient ing)
                return false;
            return this.Name == ing.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
