using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMapsTester
{
    public static void Run()
    {

// PROBLEM 1: Find Pairs with Sets.
        Console.WriteLine("\n=========== Finding Pairs TESTS ===========");
        DisplayPairs(new[] { "am", "at", "ma", "if", "fi" });
        // ma & am
        // fi & if
        
        Console.WriteLine("---------");
        DisplayPairs(new[] { "ab", "bc", "cd", "de", "ba" });
        // ba & ab
        
        Console.WriteLine("---------");
        DisplayPairs(new[] { "ab", "ba", "ac", "ad", "da", "ca" });
        // ba & ab
        // da & ad
        // ca & ac
        
        Console.WriteLine("---------");
        DisplayPairs(new[] { "ab", "ac" }); // No pairs displayed
        
        Console.WriteLine("---------");
        DisplayPairs(new[] { "ab", "aa", "ba" });
        // ba & ab
        
        Console.WriteLine("---------");
        DisplayPairs(new[] { "23", "84", "49", "13", "32", "46", "91", "99", "94", "31", "57", "14" });
        // 32 & 23
        // 94 & 49
        // 31 & 13

// PROBLEM 2: DEGREE SUMMARY
     
        Console.WriteLine("\n=========== Census TESTS ===========");
        var degreeSummary = SummarizeDegrees("D:/BYU-Idaho/FALL 2023/SECOND BLOCK/CSE 212 Programming with Data Struct/code repository/cse212-rab/week03/code/census.txt");
        foreach (var degree in degreeSummary)
        {
            Console.WriteLine($"[{degree.Key}, {degree.Value}]");
        }

// PROBLEM 3: ANAGRAMS 

        Console.WriteLine("\n=========== Anagram TESTS ===========");
        Console.WriteLine(IsAnagram("CAT", "ACT")); // true
        Console.WriteLine(IsAnagram("DOG", "GOOD")); // false
        Console.WriteLine(IsAnagram("AABBCCDD", "ABCD")); // false
        Console.WriteLine(IsAnagram("ABCCD", "ABBCD")); // false
        Console.WriteLine(IsAnagram("BC", "AD")); // false
        Console.WriteLine(IsAnagram("Ab", "Ba")); // true
        Console.WriteLine(IsAnagram("A Decimal Point", "Im a Dot in Place")); // true
        Console.WriteLine(IsAnagram("tom marvolo riddle", "i am lord voldemort")); // true
        Console.WriteLine(IsAnagram("Eleven plus Two", "Twelve Plus One")); // true
        Console.WriteLine(IsAnagram("Eleven plus One", "Twelve Plus One")); // false

// PROBLEM 4: MAZE

        Console.WriteLine("\n=========== Maze TESTS ===========");
        var mazeMap = SetupMazeMap();
        var maze = new Maze(mazeMap);
        maze.ShowStatus(); // Should be at (1,1)
        maze.MoveUp(); // Error
        maze.MoveLeft(); // Error
        maze.MoveRight();
        maze.MoveRight(); // Error
        maze.MoveDown();
        maze.MoveDown();
        maze.MoveDown();
        maze.MoveRight();
        maze.MoveRight();
        maze.MoveUp();
        maze.MoveRight();
        maze.MoveDown();
        maze.MoveLeft();
        maze.MoveDown(); // Error
        maze.MoveRight();
        maze.MoveDown();
        maze.MoveDown();
        maze.MoveRight();
        maze.ShowStatus(); // Should be at (6,6)

// PROBLEM 5: EARTHQUAKE TESTS

        Console.WriteLine("\n=========== Earthquake TESTS ===========");
        EarthquakeDailySummary();
    }

    // ===========**SOLUTIONS**=========== //

// Problem #1: Find Pairs with Sets
    private static void DisplayPairs(string[] words)
    {
        var wordSet = new HashSet<string>();
        var symmetricPairs = new HashSet<string>();

        foreach (var word in words)
        {
            var reversedWord = new string(word.Reverse().ToArray());

            if (wordSet.Contains(reversedWord))
            {
                symmetricPairs.Add($"{word} & {reversedWord}");
            }
            else
            {
                wordSet.Add(word);
            }
        }

        foreach (var pair in symmetricPairs)
        {
            Console.WriteLine(pair);
        }
    }

// Problem #2: Degree Summary
    private static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            var degree = fields[3].Trim(); // Assuming the degree is in the 4th column

            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

// Problem #3: Anagrams
    private static bool IsAnagram(string word1, string word2)
    {
        var dict1 = new Dictionary<char, int>();
        var dict2 = new Dictionary<char, int>();

        foreach (var letter in word1.ToLower().Replace(" ", ""))
        {
            if (dict1.ContainsKey(letter))
            {
                dict1[letter]++;
            }
            else
            {
                dict1[letter] = 1;
            }
        }

        foreach (var letter in word2.ToLower().Replace(" ", ""))
        {
            if (dict2.ContainsKey(letter))
            {
                dict2[letter]++;
            }
            else
            {
                dict2[letter] = 1;
            }
        }

        return dict1.Count == dict2.Count && dict1.All(kv => dict2.TryGetValue(kv.Key, out var count) && count == kv.Value);
    }

// Problem #4: Maze
    public class Maze
    {
        private readonly Dictionary<ValueTuple<int, int>, bool[]> _map;
        private ValueTuple<int, int> _currentPosition;

        public Maze(Dictionary<ValueTuple<int, int>, bool[]> map)
        {
            _map = map;
            _currentPosition = _map.Keys.First();
        }

        public void ShowStatus()
        {
            Console.WriteLine($"Current Position: {_currentPosition}");
        }

        public void MoveLeft()
        {
            var newPosition = (_currentPosition.Item1 - 1, _currentPosition.Item2);
            Move(newPosition, 0);
        }

        public void MoveRight()
        {
            var newPosition = (_currentPosition.Item1 + 1, _currentPosition.Item2);
            Move(newPosition, 1);
        }

        public void MoveUp()
        {
            var newPosition = (_currentPosition.Item1, _currentPosition.Item2 - 1);
            Move(newPosition, 2);
        }

        public void MoveDown()
        {
            var newPosition = (_currentPosition.Item1, _currentPosition.Item2 + 1);
            Move(newPosition, 3);
        }

        private void Move(ValueTuple<int, int> newPosition, int direction)
        {
            if (_map.TryGetValue(_currentPosition, out var validMoves) && validMoves[direction])
            {
                _currentPosition = newPosition;
                Console.WriteLine($"Moved to {newPosition}");
            }
            else
            {
                Console.WriteLine("Invalid move");
            }
        }
    }
    private static Dictionary<ValueTuple<int, int>, bool[]> SetupMazeMap()
    {
        Dictionary<ValueTuple<int, int>, bool[]> map = new()
        {
            { (1, 1), new[] { false, true, false, true } },
            { (1, 2), new[] { false, true, true, false } },
            { (1, 3), new[] { false, false, false, false } },
            { (1, 4), new[] { false, true, false, true } },
            { (1, 5), new[] { false, false, true, true } },
            { (1, 6), new[] { false, false, true, false } },
            { (2, 1), new[] { true, false, false, true } },
            { (2, 2), new[] { true, false, true, true } },
            { (2, 3), new[] { false, false, true, true } },
            { (2, 4), new[] { true, true, true, false } },
            { (2, 5), new[] { false, false, false, false } },
            { (2, 6), new[] { false, false, false, false } },
            { (3, 1), new[] { false, false, false, false } },
            { (3, 2), new[] { false, false, false, false } },
            { (3, 3), new[] { false, false, false, false } },
            { (3, 4), new[] { true, true, false, true } },
            { (3, 5), new[] { false, false, true, true } },
            { (3, 6), new[] { false, false, true, false } },
            { (4, 1), new[] { false, true, false, false } },
            { (4, 2), new[] { false, false, false, false } },
            { (4, 3), new[] { false, true, false, true } },
            { (4, 4), new[] { true, true, true, false } },
            { (4, 5), new[] { false, false, false, false } },
            { (4, 6), new[] { false, false, false, false } },
            { (5, 1), new[] { true, true, false, true } },
            { (5, 2), new[] { false, false, true, true } },
            { (5, 3), new[] { true, true, true, true } },
            { (5, 4), new[] { true, false, true, true } },
            { (5, 5), new[] { false, false, true, true } },
            { (5, 6), new[] { false, true, true, false } },
            { (6, 1), new[] { true, false, false, false } },
            { (6, 2), new[] { false, false, false, false } },
            { (6, 3), new[] { true, false, false, false } },
            { (6, 4), new[] { false, false, false, false } },
            { (6, 5), new[] { false, false, false, false } },
            { (6, 6), new[] { true, false, false, false } }
        };
        return map;
    }

// Problem #5: Earthquake
    public class Earthquake
    {
        public string Place { get; set; }
        public double Magnitude { get; set; }
    }

    public class Feature
    {
        public Earthquake Properties { get; set; }
    }

    public class FeatureCollection
    {
        public List<Feature> Features { get; set; }
    }

    private static void EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        foreach (var feature in featureCollection.Features)
        {
            Console.WriteLine($"{feature.Properties.Place} - Mag {feature.Properties.Magnitude}");
        }
    }
}
