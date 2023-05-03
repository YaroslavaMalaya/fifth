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
        Console.WriteLine($"< Looks like you don't know how to type: {string.Join(", ", wrongWords)}");
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
    
            Console.WriteLine($"< Possible suggestions for {wrong}: {string.Join(", ", suggestions)}");
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
    string t = "acf";
    if (DamerauLevenshteinDistance(t, s) <= s.Length - t.Length)
    {
        Console.WriteLine($"{t} is a substring of {s}");
    }
    else
    {
        Console.WriteLine($"{t} is not a substring of {s}");
    }
*/

/*
//additional 2
Console.Write("\n> Enter the number of pairs brackets: ");
int n = int.Parse(Console.ReadLine());
List<string> brackets = new List<string>();
BracketGenerationRecursive(brackets, n, "", 0, 0);
Console.WriteLine($"< {String.Join(", ", brackets)}");

void BracketGenerationRecursive(List<string> brackets, int n, string group, int open, int close) 
    // open = number of open brackets 
    // close = number of close brackets 
{
    if (open < n)
    {
        BracketGenerationRecursive(brackets, n, group + "(", open + 1, close);
    }

    if (close < open) 
    {
        BracketGenerationRecursive(brackets, n, group + ")", open, close + 1);
    }
    
    if (group.Length == n * 2) // n * 2 = maximum number of brackets in one group
    {
        brackets.Add(group);
    }
}
*/

/* additional 3
int[] prices = { 1, 7, 5, 3, 6, 4 };
int[] prices2 = { 1, 1, 1, 1, 1 };
void MaxProfit(int[] pr) 
{
    var minPrice = int.MaxValue; // 2147483647 - the largest possible value of int32
    var maxProfit = 0;
    var sell = 0;
    var buy = 0;
    
    for (var i = 0; i < pr.Length; i++) 
    {
        if (pr[i] < minPrice) 
        {
            minPrice = pr[i];
            buy = i;
            // if price on the ith day is less then the minimum, it becomes the minimum
            // and we mark the day we bought the share
        } 
        else if (pr[i] - minPrice > maxProfit) 
        {
            maxProfit = pr[i] - minPrice;
            sell = i;
            // otherwise if price on the ith day is greater, then if this price - minimum is greater
            // than the maximum, it becomes the maximum
            // + we mark the day we sold the share
        }
    }

    if (maxProfit != 0)
    {
        Console.WriteLine(maxProfit);
        Console.WriteLine($"You should buy on the {buy + 1}th day and sell on the {sell + 1}th day.\n");
    }
    else Console.WriteLine("You didn't gain any profit.\n");
}

MaxProfit(prices);
MaxProfit(prices2);
*/