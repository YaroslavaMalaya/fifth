Console.WriteLine("Enter your text:");
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

