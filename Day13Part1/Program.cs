﻿Console.WriteLine(new List<string[]>(){File.ReadAllLines("input.txt")}.Select(j => j.Aggregate(new List<List<string>>(){new()}, (previous, current) => 
    string.IsNullOrEmpty(current) ? [..previous, new List<string>()] : [..previous.SkipLast(1), [..previous.Last(), current]]))
    .Select(j => j.Select(mirror => mirror.Aggregate(new List<List<bool>>(), (previous, current) => 
        [..previous, current.Select(t => t == '#').ToList()])).Where(t => t.Any(r => r.Count != 0)).ToList())
    .First().Select(mirror => new Tuple<int,int>(
        Enumerable.Range(0, mirror.Count-1).FirstOrDefault(row => 
            Enumerable.Range(0, mirror[0].Count).All(t => 
                Enumerable.Range(0, mirror[0].Count).All(i => row - i < 0 || row + 1 + i >= mirror.Count || mirror[row-i][t] == mirror[row+1+i][t])), -1),
        Enumerable.Range(0, mirror[0].Count-1).FirstOrDefault(col => 
            Enumerable.Range(0, mirror.Count).All(t => 
                Enumerable.Range(0, mirror.Count).All(i => col - i < 0 || col + i + 1 >= mirror[t].Count || mirror[t][col - i] == mirror[t][col + i +1])), -1)))
    .Select(t => t.Item1 > t.Item2?100*(t.Item1+1) : t.Item2+1).Sum());