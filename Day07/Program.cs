using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Day07
{
    internal class Program
    {
        private static void Main()
        {
            string InputFileName = @"..\..\..\Input.txt";
            string inputText = File.ReadAllText(InputFileName);
            string[] inputLines = inputText.Split(new[] { "\r\n" }, StringSplitOptions.None);

            const string shinygold = "shinygold";

            List<Bag> rootBags = new();

            foreach (string line in inputLines)
            {
                string[] str = line.Split(' ', StringSplitOptions.None);
                string bagName = str[0] + str[1];

                if (rootBags.FirstOrDefault(x => x.Name == bagName) is not Bag curBag)
                {
                    curBag = new Bag(bagName);
                    rootBags.Add(curBag);
                }

                int offset = 4;

                if (str[4] == "no")
                    continue;

                do
                {
                    int count = Convert.ToInt32(str[offset], CultureInfo.InvariantCulture);
                    string subBagName = str[offset + 1] + str[offset + 2];

                    if (rootBags.FirstOrDefault(x => x.Name == subBagName) is not Bag subBag)
                    {
                        subBag = new Bag(subBagName);
                        rootBags.Add(subBag);
                    }

                    curBag.Add(subBag, count);

                    offset += 4;
                } while (offset < str.Length);
            }

            int sum = rootBags.Where(bag => bag.Contains(shinygold)).Count() - 1;

            Console.WriteLine($"Part1: total shiny gold bags: {sum}");

            Bag shinyBag = rootBags.First(x => x.Name == shinygold);

            Console.WriteLine($"Part2: inside shiny gold bag: {shinyBag.TotalBagsInside()}");

            Console.ReadKey();
        }
    }

    internal class Bag
    {
        private readonly Dictionary<Bag, int> inside = new();

        public string Name { get; private set; }

        public Bag(string name)
        {
            Name = name;
        }

        public void Add(Bag bag, int count)
        {
            inside.Add(bag, count);
        }

        public bool Contains(string name)
        {
            return Name == name || inside.Any(kvp => kvp.Key.Contains(name));
        }

        public int TotalBagsInside()
        {
            return inside.Sum(kvp => kvp.Value * (kvp.Key.TotalBagsInside() + 1));
        }

        public override string ToString() => Name;
    }
}
