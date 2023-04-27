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
