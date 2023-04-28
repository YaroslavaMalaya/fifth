Console.Write("> Enter your text: ");
var input = Console.ReadLine().Split(" ");
var punctuation = new char[] {',', '.', '?', '!', ';', ':', '"'};
var words = new List<string>();

foreach (var word in input)
{
    words.Add(new string(word.Where(c => !punctuation.Contains(c)).ToArray()));
    // .ToString() одразу не працює, тому спочатку в Array, а потім формуємо string()
    //Console.WriteLine(word.Where(c => !punctuation.Contains(c)).ToArray());
}

foreach (var word in words)
{
    Console.WriteLine(word); // це так, щоб ми побачили що воно працює
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

Console.Write("< Looks like you don't know how to type: ");
Console.WriteLine(string.Join(", ", wrongWords));
/*
foreach (var word in wrongWords)
{
    Console.Write($"'{word}', "); // another option that looks like 'word1', 'word2', 
}
*/

var toyvo = new Dictionary<string, int>(); // слова підсказки та відстань до нього від неправильного
var check = 0; // щоб вивести в косоль для прикладу n-ну кількість матриць
foreach (var wrong in wrongWords)
{
    var wrongLnght = wrong.Length;
    foreach (var word in File.ReadAllLines("words_list.txt"))
    {
        check++;
        var wordLnght = word.Length;
        var tseyvo = new int[wrongLnght + 1, wordLnght + 1]; // матриця
        for (int j = 0; j <= wordLnght; j++)
            tseyvo[0, j] = j; // прописує рядок з індексами
        for (int i = 0; i <= wrongLnght; i++)
            tseyvo[i, 0] = i; // прописує стовбець з індексами

        // частина знизу виводить у консоль перші два слова з words_list, щоб побачити що воно правильне 
        if (check <= 2)
        {
            Console.WriteLine(word);
            for (int i = 0; i <= wrongLnght; i++)
            {
                for (int j = 0; j <= wordLnght; j++)
                {
                    Console.Write(tseyvo[i, j] + " ");
                }
                Console.WriteLine("");
            }
        }
    }
}
