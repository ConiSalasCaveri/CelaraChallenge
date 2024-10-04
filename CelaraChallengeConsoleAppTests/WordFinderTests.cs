using CelaraChallengeConsoleApp;

namespace CelaraChallengeConsoleAppTests;

[TestClass]
public class WordFinderTests
{
    [TestMethod]
    public void Find_NoMatchWords_ReturnsNoMatches()
    {
        var wordFinder = new WordFinder(["abcdce", "fgwior", "chille", "pqnsdy", "uvdxyx", "pcoldl"]);
        var result = wordFinder.Find(["rush"]);
        Assert.AreEqual(0, result.Count());
    }
    
    [TestMethod]
    public void Find_MatchWord_ReturnsOneMatch()
    {
        var wordFinder = new WordFinder(["abcdce", "fgwior", "chille", "pqnsdy", "uvdxyx", "pcoldl"]);
        var result = wordFinder.Find(["wind"]);
        Assert.AreEqual(1, result.Count());
        Assert.AreEqual("wind", result.First());
    }
    
    [TestMethod]
    public void Find_MatchWords_ReturnsAllMatches()
    {
        var wordFinder = new WordFinder(["abcdce", "fgwior", "chille", "pqnsdy", "uvdxyx", "pcoldl"]);
        var result = wordFinder.Find(["wind", "cold", "chill", "rush"]);
        Assert.AreEqual(3, result.Count());
        Assert.IsTrue(result.Contains("wind"));
        Assert.IsTrue(result.Contains("cold"));
        Assert.IsTrue(result.Contains("chill"));
    }
}