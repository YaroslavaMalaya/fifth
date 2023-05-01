while (true)
{
    Console.Write("\n> Enter your text: ");
    var input = Console.ReadLine().Split(" ");
    var punctuation = new char[] {',', '.', '?', '!', ';', ':', '"'};
    var words = new List<string>();
    
    foreach (var word in input)
    {
        words.Add(new string(word.Where(c => !punctuation.Contains(c)).ToArray()));
        // .ToString() одразу не працює, тому спочатку в Array, а потім формуємо string()
        //Console.WriteLine(word.Where(c => !punctuation.Contains(c)).ToArray());
    }
    
    List<string> spellChecker = new List<string>();
    foreach (var line in File.ReadAllLines("words_list.txt"))
    {
        spellChecker.Add(line.ToLower());
    }
    List<string> wrongWords = new List<string>();
    foreach (var word in words)
    {
        if (!spellChecker.Contains(word.ToLower())) 
        {
            wrongWords.Add(word);
        }
    }
    
    if (wrongWords.Count != 0)
    {
        Console.Write("< Looks like you don't know how to type: ");
        Console.WriteLine(string.Join(", ", wrongWords));
        
        var distances = new Dictionary<string, int>();
        
        foreach (var wrong in wrongWords)
        {
            foreach (var word in File.ReadAllLines("words_list.txt"))
            {
                distances[word] = DamerauLevenshteinDistance(wrong, word);
            }
    
            var sortedKeyValuePairs = distances.OrderBy(x => x.Value).Take(5).ToList();
            var suggestions = new List<string>();
    
            foreach (var pair in sortedKeyValuePairs) suggestions.Add(pair.Key);
    
            Console.Write($"< Possible suggestions for {wrong}: ");
            Console.WriteLine(string.Join(", ", suggestions));
        }
    }
    else
    {
        Console.WriteLine("< At least you have a dictionary at home. Congrats.");
    }
}

int DamerauLevenshteinDistance(string word1, string word2)
{
    var w1 = word1.Length;
    var w2 = word2.Length;

    var matrix = new int[w1 + 1, w2 + 1]; 
    
    for (int j = 0; j <= w2; j++)
        matrix[0, j] = j; // прописує рядок з індексами
    for (int i = 0; i <= w1; i++)
        matrix[i, 0] = i; // прописує стовбець з індексами

    for (var j = 1; j <= w2; j++)
    {
        for (var i = 1; i <= w1; i++)
        {
            var cost = word1[i - 1] == word2[j - 1] ? 0 : 1;
            
            /* the same as
             if (word1[j - 1] == word2[i - 1])
                cost = 0;
            else cost = 1;
            */

            matrix[i, j] = Math.Min(
                Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                matrix[i - 1, j - 1] + cost);
            
            if (i > 1 && j > 1 && word1[i - 1] == word2[j - 2] && word1[i - 2] == word2[j - 1])
            {
                matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + cost);
            }
        }
    }
    return matrix[w1 , w2];
}

/* additional 1
string s = "abcdef";
string t = "a";
if (LevenshteinDistance(t, s) <= s.Length - t.Length)
{
    Console.WriteLine($"{t} is a substring of {s}");
}
else
{
    Console.WriteLine($"{t} is not a substring of {s}");
}
*/