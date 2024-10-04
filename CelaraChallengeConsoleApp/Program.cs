namespace CelaraChallengeConsoleApp;

static class Program
{
    static void Main(string[] args)
    {
        var wordFinder = new WordFinder(["abcdce", "fgwior", "chille", "pqnsdy", "uvdxyx", "pcoldl"]);
        var result = wordFinder.Find(new List<string> {"wind", "cold", "chill", "rush"});
        Console.WriteLine( result.Any() ? string.Join(", ", result) : "No matches found.");
        Console.ReadLine();
    }
}