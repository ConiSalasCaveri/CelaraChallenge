namespace CelaraChallengeConsoleApp;

internal sealed class TrieNode
{
    public Dictionary<char, TrieNode> Children { get; } = new();
    public bool IsWord { get; set; }
}