Console.WriteLine(new List<string[]>(){File.ReadAllLines("input.txt")}.Select(j => j.Aggregate(new List<List<string>>(){new()}, (previous, current) => 
    string.IsNullOrEmpty(current) ? [..previous, new List<string>()] : [..previous.SkipLast(1), [..previous.Last(), current]]))
    .Select(j => j.Select(mirror => mirror.Aggregate(new List<List<bool>>(), (previous, current) => 
        [..previous, current.Select(t => t == '#').ToList()])).Where(t => t.Any(r => r.Count != 0)).ToList())
    .First().Select(mirror => new Tuple<int,int>(
        Enumerable.Range(0, mirror.Count-1).FirstOrDefault(row => 
            Enumerable.Range(0, mirror[0].Count).Sum(t => 
                Enumerable.Range(0, mirror[0].Count).Where(i => row - i >= 0 && row + 1 + i < mirror.Count).Aggregate(0, (previous, i) => mirror[row-i][t] == mirror[row+1+i][t]?previous:previous+1)) == 1, -1),
        Enumerable.Range(0, mirror[0].Count-1).FirstOrDefault(col => 
            Enumerable.Range(0, mirror.Count).Sum(t => 
                Enumerable.Range(0, mirror.Count).Where(i => col - i >= 0 && col + i + 1 < mirror[t].Count).Aggregate(0, (previous, i) => mirror[t][col - i] == mirror[t][col + i +1] ? previous : previous + 1)) == 1, -1)))
    .Select(t => t.Item1 > t.Item2?100*(t.Item1+1) : t.Item2+1).Sum());