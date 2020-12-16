using System.Collections.Generic;

namespace Day16
{
    internal class Rule
    {
        private readonly List<int> allowedColumns;

        public string Name { get; private set; }

        public int Min1 { get; private set; }

        public int Max1 { get; private set; }

        public int Min2 { get; private set; }

        public int Max2 { get; private set; }

        public List<int> AllowedColumns => allowedColumns;

        public bool IsDeparture => Name.StartsWith("departure");

        public Rule(string name, int min1, int max1, int min2, int max2)
        {
            Name = name;
            Min1 = min1;
            Max1 = max1;
            Min2 = min2;
            Max2 = max2;

            allowedColumns = new();
            for (int i = 0; i < 20; i++)
            {
                allowedColumns.Add(i);
            }
        }

        public bool IsValid(int val)
        {
            return val >= Min1 && val <= Max1 || val >= Min2 && val <= Max2;
        }
    }
}
