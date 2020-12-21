using System;
using System.Collections.Generic;
using System.Linq;

namespace Day21
{
    internal class Program
    {
        private static void Main()
        {
            string InputFileName = @"..\..\..\Input.txt";
            string[] inputLines = System.IO.File.ReadAllLines(InputFileName);

            List<Food> foods = new();

            foreach (string line in inputLines)
            {
                Food food = new Food();
                string[] ings = line.Split(" (")[0].Split(' ');
                for (int i = 0; i < ings.Length; i++)
                {
                    food.Ingredients.Add(new Ingredient(ings[i]));
                }
                string[] algs = line[..^1].Split("(contains ")[1].Split(", ");
                for (int i = 0; i < algs.Length; i++)
                {

                    food.Allergens.Add(new Allergen(algs[i]));
                }
                foods.Add(food);
            }

            List<Ingredient> sharedIngs = new();
            List<(Allergen, Ingredient)> part2 = new();

            bool changesDone;
            do
            {
                changesDone = false;
                foreach (Food food1 in foods)
                {
                    if (food1.Allergens.Count != 1)
                        continue;

                    sharedIngs.Clear();
                    sharedIngs.AddRange(food1.Ingredients);
                    Allergen curAlllergen = food1.Allergens[0];

                    foreach (Food food2 in foods)
                    {
                        if (food1 == food2)
                            continue;
                        if (!food2.Allergens.Contains(curAlllergen))
                            continue;

                        sharedIngs = sharedIngs.Intersect(food2.Ingredients).ToList();
                    }

                    if (sharedIngs.Count == 1)
                    {
                        Console.WriteLine($"removing: {sharedIngs[0].Name}, allerg: {curAlllergen.Name}");
                        part2.Add(new(curAlllergen, sharedIngs[0]));
                        foreach (Food tmpFood in foods)
                        {
                            tmpFood.Ingredients.RemoveAll(x => x.Name == sharedIngs[0].Name);
                            tmpFood.Allergens.RemoveAll(x => x.Name == curAlllergen.Name);
                        }
                        changesDone = true;
                        break;
                    }
                }
            } while (changesDone);

            List<Ingredient> part1 = new();
            for (int i = 0; i < foods.Count; i++)
            {
                part1.AddRange(foods[i].Ingredients);
            }

            Console.WriteLine($"Part1: {part1.Count} (right answer: 2542)");

            part2.Sort((x, y) => { return x.Item1.Name.CompareTo(y.Item1.Name); });

            Console.Write($"Part2: ");
            foreach (var item in part2)
            {
                Console.Write($"{item.Item2.Name},");
            }
            Console.WriteLine();
            Console.WriteLine("(right answer: hkflr,ctmcqjf,bfrq,srxphcm,snmxl,zvx,bd,mqvk)");

            Console.ReadKey();
        }
    }
}
