namespace Day21
{
    internal class Allergen
    {
        public string Name { get; init; }

        public Allergen(string name)
        {
            Name = name;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Allergen alg)
                return false;
            return this.Name == alg.Name;
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
