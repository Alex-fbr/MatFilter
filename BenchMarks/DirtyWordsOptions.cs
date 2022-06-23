namespace BenchMarks
{
    public class DirtyWordsOptions
    {
        public List<string> DirtyWords { get; set; }
        public Dictionary<string, char> Alphabet { get; set; }
        public Dictionary<char, char> ReplaceSymbols =>
            new(Alphabet?.Select(x => new KeyValuePair<char, char>(x.Key[0], x.Value)) ?? Array.Empty<KeyValuePair<char, char>>());
    }
}