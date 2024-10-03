using System.Text;

namespace CelaraChallengeConsoleApp;

internal sealed class WordFinder(IEnumerable<string> matrix)
{
    private readonly string[] _matrix = matrix.ToArray();
    private readonly TrieNode _root = new();

    public IEnumerable<string> Find(IEnumerable<string> wordStream)
    {
        var wordCounts = new Dictionary<string, int>();
        
        foreach (var word in wordStream)
        {
            InsertWord(word);
            if (!wordCounts.ContainsKey(word))
            {
                wordCounts[word] = 0;
            }
        }
        
        foreach (var row in _matrix)
        {
            SearchInRow(row, wordCounts);
        }
        for (var col = 0; col < _matrix[0].Length; col++)
        {
            var columnWord = GetColumnWord(col);
            SearchInRow(columnWord, wordCounts);
        }
        
        return wordCounts
            .Where(x => x.Value != 0)
            .OrderByDescending(x => x.Value)
            .Take(10)
            .Select(x => x.Key);
    }
    
    private void InsertWord(string word)
    {
        var node = _root;
        foreach (var c in word)
        {
            if (!node.Children.ContainsKey(c))
            {
                node.Children[c] = new TrieNode();
            }
            node = node.Children[c];
        }
        node.IsWord = true;
    }
    
    private string GetColumnWord(int col)
    {
        var columnWord = new StringBuilder();
        for (var row = 0; row < _matrix.Length; row++)
        {
            columnWord.Append(_matrix[row][col]);
        }
        return columnWord.ToString();
    }
    
    private void SearchInRow(string line, Dictionary<string, int> wordCounts)
    {
        for (var i = 0; i < line.Length; i++)
        {
            var node = _root;
            for (var j = i; j < line.Length && node is not null; j++)
            {
                var c = line[j];
                if (!node.Children.ContainsKey(c)) break;
                node = node.Children[c];

                if (node.IsWord)
                {
                    var foundWord = line.Substring(i, j - i + 1);
                    if (wordCounts.ContainsKey(foundWord))
                    {
                        wordCounts[foundWord]++;
                    }
                }
            }
        }
    }
}