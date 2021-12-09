using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Days.Puzzles
{
    public class Day21 : Day, IDay
    {
        public void RunPartOne()
        {
            //var input = ReadInput(nameof(Day21));

            //var dictionary = new Dictionary<string, List<List<string>>>();

            //foreach (var s in input)
            //{
            //    var split = s.Split("(contains");
            //    var allergens = split[1].TrimEnd(')').Split(',').Select(al => al.Trim());

            //    foreach (var allergen in allergens)
            //        if (dictionary.TryAdd(allergen, new List<List<string>>()))
            //            dictionary[allergen].Add(split[0].Split(' ').Select(ing=>ing.Trim()).Where(ing=>!string.IsNullOrEmpty(ing)).ToList());
            //        else
            //            dictionary[allergen].Add(split[0].Split(' ').Select(ing => ing.Trim()).Where(ing => !string.IsNullOrEmpty(ing)).ToList());
            //}

            //var potentialAllergens = new Dictionary<string, List<string>>();

            //foreach (var entry in dictionary)
            //{
            //    var prods = entry.Value;

            //    var appearsInAll = prods.Aggregate((x, y) => x.Intersect(y).ToList());
            //    potentialAllergens.Add(entry.Key, new List<string>());
            //    foreach (var al in appearsInAll)
            //    {
            //        potentialAllergens[entry.Key].Add(al);
            //    }
            //}

            //while (potentialAllergens.Any(p => p.Value.Count > 1))
            //{
            //    var known = potentialAllergens.Where(p => p.Value.Count == 1).ToList();

            //    foreach (var keyValuePair in potentialAllergens.Except(known))
            //    {
            //        var left = keyValuePair.Value.Except(known.SelectMany(kv => kv.Value));
            //        potentialAllergens[keyValuePair.Key] = left.ToList();
            //    }

            //}

            //foreach (var entry in dictionary.Keys)
            //{
            //    dictionary[entry] = dictionary[entry].Select(e => e.Except(confirmedAllergens).ToList()).ToList();
            //}

            //var nonAllergens = new HashSet<string>();

            //foreach (var ingredient in dictionary.Values.SelectMany(s=>s).SelectMany(s=>s))
            //{
            //    nonAllergens.Add(ingredient);
            //}


            //long count = 0;
            //foreach (var s in input)
            //{
            //    foreach (var nonAllergen in nonAllergens)
            //    {
            //        if (s.Contains(nonAllergen))
            //        {
            //            count++;
            //        }
            //    }
            //}

            //Console.WriteLine(count);
        }


        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day21));

            var dictionary = new Dictionary<string, List<List<string>>>();

            foreach (var s in input)
            {
                var split = s.Split("(contains");
                var allergens = split[1].TrimEnd(')').Split(',').Select(al => al.Trim());

                foreach (var allergen in allergens)
                    if (dictionary.TryAdd(allergen, new List<List<string>>()))
                        dictionary[allergen].Add(split[0].Split(' ').Select(ing => ing.Trim()).Where(ing => !string.IsNullOrEmpty(ing)).ToList());
                    else
                        dictionary[allergen].Add(split[0].Split(' ').Select(ing => ing.Trim()).Where(ing => !string.IsNullOrEmpty(ing)).ToList());
            }

            var potentialAllergens = new Dictionary<string, List<string>>();

            foreach (var entry in dictionary)
            {
                var prods = entry.Value;

                var appearsInAll = prods.Aggregate((x, y) => x.Intersect(y).ToList());
                potentialAllergens.Add(entry.Key, new List<string>());
                foreach (var al in appearsInAll)
                {
                    potentialAllergens[entry.Key].Add(al);
                }
            }

            while (potentialAllergens.Any(p => p.Value.Count > 1))
            {
                var known = potentialAllergens.Where(p => p.Value.Count == 1).ToList();

                foreach (var keyValuePair in potentialAllergens.Except(known))
                {
                    var left = keyValuePair.Value.Except(known.SelectMany(kv => kv.Value));
                    potentialAllergens[keyValuePair.Key] = left.ToList();
                }
            }

            var pot = potentialAllergens.OrderBy(kv => kv.Key);

            Console.WriteLine(string.Join(',', pot.Select(kv=>kv.Value.Single())));

        }
    }
}