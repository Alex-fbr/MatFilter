using System.Text;
using Microsoft.Extensions.ObjectPool;
using BenchmarkDotNet.Attributes;

namespace BenchMarks
{
    [MemoryDiagnoser]
    //[InliningDiagnoser]
    //[TailCallDiagnoser]
    //[EtwProfiler]
    //[ConcurrencyVisualizerProfiler]
    //[NativeMemoryProfiler]
    [ThreadingDiagnoser]
    public class DirtyWordsInspector
    {
        private const int MaxTextLength = 500;
        private readonly DirtyWordsOptions _options;
        private readonly ITree _dirtyWordTrie;
        private readonly HashSet<char> _marks;
        private readonly ObjectPool<StringBuilder> _stringBuilderPool;

        public DirtyWordsInspector()
        {
            _options = new DirtyWordsOptions
            {
                #region

                Alphabet = new Dictionary<string, char>
                {
                    {"a", '?'},
                    {"@", '?'},
                    {"6", '?'},
                    {"b", '?'},
                    {"v", '?'},
                    {"g", '?'},
                    {"r", '?'},
                    {"d", '?'},
                    {"e", '?'},
                    {"?", '?'},
                    {"3", '?'},
                    {"z", '?'},
                    {"i", '?'},
                    {"?", '?'},
                    {"k", '?'},
                    {"l", '?'},
                    {"m", '?'},
                    {"n", '?'},
                    {"o", '?'},
                    {"0", '?'},
                    {"p", '?'},
                    {"c", '?'},
                    {"s", '?'},
                    {"t", '?'},
                    {"y", '?'},
                    {"u", '?'},
                    {"f", '?'},
                    {"x", '?'},
                    {"h", '?'}
                },
                DirtyWords = new List<string>
                {
                    "6??",

					#region

					"6????",
                    "6????",
                    "b3?e?",
                    "cock",
                    "cunt",
                    "e6a??",
                    "ebal",
                    "eblan",
                    "e?a?",
                    "e?a??",
                    "e?y?",
                    "e????",
                    "e???",
                    "e???????",
                    "fuck",
                    "fucker",
                    "fucking",
                    "xy??",
                    "xy?",
                    "xy?",
                    "x??",
                    "x??",
                    "x??",
                    "zaeb",
                    "zaebal",
                    "zaebali",
                    "zaebat",
                    "???????????",
                    "?????",
                    "??????",
                    "???????",
                    "??????",
                    "?????",
                    "??????",
                    "?????",
                    "??????",
                    "?????",
                    "???????",
                    "?????",
                    "???????",
                    "??????",
                    "???????",
                    "???????",
                    "???",
                    "?????",
                    "???????",
                    "????",
                    "?????",
                    "???????",
                    "???????",
                    "??????",
                    "?????????",
                    "????????",
                    "??????",
                    "???????",
                    "????????",
                    "?????",
                    "???????",
                    "?????",
                    "???????",
                    "???????",
                    "??????????",
                    "????",
                    "????????",
                    "???????",
                    "???????",
                    "?????????",
                    "????????",
                    "????????",
                    "????",
                    "???????",
                    "??????",
                    "?????????",
                    "??????",
                    "???????????",
                    "?????????",
                    "?????????",
                    "?????????",
                    "??????",
                    "?????",
                    "??????",
                    "????????",
                    "?????",
                    "??????",
                    "????",
                    "?????",
                    "?????",
                    "???????",
                    "????????",
                    "???????",
                    "????????",
                    "???????",
                    "???????",
                    "?????",
                    "???????",
                    "?????????",
                    "?????????",
                    "??????",
                    "???????",
                    "?????????",
                    "??????",
                    "????????",
                    "???????",
                    "??????",
                    "???????????",
                    "???????",
                    "???????",
                    "?????????",
                    "??????",
                    "?????",
                    "??????????",
                    "????????",
                    "???????",
                    "????????",
                    "????????",
                    "???????",
                    "???????",
                    "????????",
                    "?????????",
                    "???????",
                    "??????",
                    "??????",
                    "?6??",
                    "?6??",
                    "?? ???? ????",
                    "?? ???? ????",
                    "??a?",
                    "??a??",
                    "??y?",
                    "????",
                    "?????",
                    "????????",
                    "????",
                    "?????????",
                    "??????",
                    "??????",
                    "??????",
                    "???????????",
                    "???????",
                    "???????????",
                    "??????",
                    "???????",
                    "?????????",
                    "??????",
                    "??????",
                    "???????",
                    "?????",
                    "????",
                    "????",
                    "????????",
                    "?????",
                    "?????-??????",
                    "???????",
                    "???????",
                    "?????",
                    "????",
                    "????",
                    "????",
                    "????",
                    "????",
                    "?????",
                    "?????????",
                    "????",
                    "????",
                    "?????",
                    "???????",
                    "??????",
                    "????",
                    "??????",
                    "????",
                    "??????",
                    "???",
                    "??????",
                    "????????",
                    "????",
                    "???????",
                    "??????",
                    "??????",
                    "??????????",
                    "????",
                    "????",
                    "????",
                    "?????",
                    "?????",
                    "??????",
                    "??????",
                    "??????",
                    "????",
                    "?????",
                    "????",
                    "?????",
                    "????????",
                    "????",
                    "?????????",
                    "???????????",
                    "??????????",
                    "???????",
                    "???6",
                    "???6",
                    "????",
                    "????",
                    "?????",
                    "??????",
                    "????????",
                    "?????????",
                    "?????????",
                    "???????",
                    "?????????",
                    "?????????",
                    "?????????",
                    "?????????",
                    "?????????",
                    "?????????",
                    "?????????",
                    "?????????",
                    "???????",
                    "?????????",
                    "???????????",
                    "?????",
                    "??????",
                    "??????????",
                    "????????",
                    "??????????",
                    "????????????",
                    "???????????",
                    "????????",
                    "???????",
                    "??????",
                    "????????",
                    "??????",
                    "?????????",
                    "?????????",
                    "?????",
                    "?????????",
                    "?????????",
                    "?????????",
                    "????????",
                    "??????",
                    "?????????",
                    "???????????",
                    "???????????",
                    "?????",
                    "???????",
                    "??????",
                    "????????????????????",
                    "?????",
                    "?????",
                    "?????????",
                    "???",
                    "?????a",
                    "??????",
                    "??????",
                    "?????",
                    "?????",
                    "?????",
                    "??????????",
                    "??????????",
                    "??????????",
                    "??????",
                    "???????",
                    "???????",
                    "???????",
                    "??????",
                    "?????",
                    "??????",
                    "?????",
                    "????????",
                    "?????????",
                    "?????",
                    "??????????",
                    "??????????",
                    "?????",
                    "???ak",
                    "???a?",
                    "?????",
                    "?????",
                    "????",
                    "??????",
                    "??????",
                    "????",
                    "?????",
                    "??????",
                    "????????",
                    "?????",
                    "??????",
                    "????????",
                    "????????",
                    "?? ???",
                    "?? ???",
                    "???????",
                    "????????",
                    "?????????",
                    "??????????",
                    "?????????",
                    "???????",
                    "??????",
                    "????????",
                    "??????????",
                    "?????????",
                    "????????",
                    "?????????",
                    "?????????",
                    "?????????",
                    "???????",
                    "????????????",
                    "?????",
                    "??????",
                    "?????",
                    "????????",
                    "?? ????",
                    "?? ????",
                    "????????????",
                    "??????????",
                    "??????",
                    "??????",
                    "?????",
                    "????????????",
                    "???????",
                    "????????",
                    "????????",
                    "??????",
                    "?????",
                    "??????",
                    "?????",
                    "????????????",
                    "?????????",
                    "????????",
                    "????????",
                    "??????????",
                    "????????",
                    "???????",
                    "???????? ???????",
                    "??????????????",
                    "???????",
                    "??????",
                    "?????????????",
                    "???????????",
                    "???????????",
                    "??????????",
                    "?????????",
                    "???????????",
                    "????????",
                    "????????",
                    "??????????????",
                    "????????",
                    "??????????",
                    "?????",
                    "???????",
                    "?????????",
                    "??????",
                    "??????????",
                    "???????????",
                    "?????????",
                    "??????????",
                    "????????",
                    "?????",
                    "?????",
                    "???????",
                    "???????",
                    "???????",
                    "???????",
                    "?????",
                    "??????",
                    "???????",
                    "????????",
                    "???????",
                    "???????",
                    "???????",
                    "??????",
                    "???????",
                    "??????",
                    "?????????",
                    "??????",
                    "????????",
                    "???????",
                    "??????????",
                    "????????",
                    "????????",
                    "??????",
                    "????????",
                    "?????????",
                    "????????",
                    "???????",
                    "?????",
                    "????????",
                    "???????",
                    "???????",
                    "??3?",
                    "??3??",
                    "??3??",
                    "??z???",
                    "?????",
                    "?????a?",
                    "???????",
                    "????????",
                    "??????",
                    "?????",
                    "????????",
                    "???????",
                    "???????",
                    "??????",
                    "??????",
                    "?????",
                    "?????????",
                    "???????????",
                    "?????????????",
                    "???????",
                    "????????",
                    "????????",
                    "????????",
                    "????????",
                    "????????",
                    "???????",
                    "??????",
                    "??????",
                    "???????",
                    "?????????",
                    "???????",
                    "???????",
                    "???????",
                    "????????",
                    "?????????",
                    "???????????",
                    "??????????",
                    "??????????",
                    "????????",
                    "??????????",
                    "???????????",
                    "???????????",
                    "???????????????",
                    "?????",
                    "??????",
                    "??????",
                    "????????",
                    "?????",
                    "???????",
                    "??????",
                    "?????????",
                    "???????",
                    "??????",
                    "?????????",
                    "???????",
                    "??????",
                    "????????????????",
                    "?????",
                    "???????",
                    "?? ???",
                    "?? ???",
                    "??????????",
                    "???????",
                    "???????",
                    "??????????",
                    "????????????",
                    "???????",
                    "???????",
                    "??????????",
                    "???????",
                    "???????",
                    "?????????",
                    "??????????",
                    "?????",
                    "???????",
                    "????????",
                    "????????",
                    "??????",
                    "??????",
                    "???????",
                    "?????",
                    "???????",
                    "?????????",
                    "?????",
                    "????????",
                    "??????????",
                    "??????????",
                    "????????????",
                    "????????????",
                    "??????????",
                    "????????",
                    "?????",
                    "?????????",
                    "????????",
                    "??????????",
                    "?????????",
                    "???????????",
                    "??????????",
                    "????????????",
                    "?????????",
                    "??????????",
                    "??????",
                    "???????",
                    "????????",
                    "?????????",
                    "?????????",
                    "????????????",
                    "?????????",
                    "?????????????",
                    "?????????",
                    "???????",
                    "???????",
                    "????????",
                    "??????",
                    "?????",
                    "??????",
                    "????????",
                    "??????",
                    "????",
                    "??????",
                    "????????",
                    "????",
                    "???????",
                    "????????",
                    "???????",
                    "????????",
                    "????????",
                    "???????",
                    "????????",
                    "?????",
                    "?????",
                    "??????",
                    "??????",
                    "?????",
                    "????",
                    "?????",
                    "?????",
                    "??????",
                    "?????????????",
                    "????",
                    "????",
                    "??????????",
                    "??????",
                    "?????",
                    "???????",
                    "?????",
                    "??????",
                    "?????",
                    "?????",
                    "?????????",
                    "?????",
                    "??????6",
                    "???????",
                    "???????",
                    "?????????",
                    "???????",
                    "??????",
                    "??????",
                    "??????",
                    "??????",
                    "????????",
                    "????????",
                    "????",
                    "?????",
                    "?????",
                    "?????",
                    "?????",
                    "????",
                    "????????",
                    "???????",
                    "?_?_?_?_?",
                    "?y?",
                    "?y?",
                    "?y???",
                    "?????",
                    "???",
                    "?????",
                    "????????",
                    "????????",
                    "???????",
                    "??????????????",
                    "??????????",
                    "??e?",
                    "???",
                    "???",
                    "???????",
                    "??????????",
                    "???????",
                    "?????",
                    "??????",
                    "??????",
                    "????",
                    "????",
                    "????",
                    "????",
                    "?????",
                    "??????",
                    "????????",
                    "???????",
                    "???????",
                    "???????????????",
                    "??????",
                    "???????",
                    "??????",
                    "????????",
                    "?????",
                    "???????",
                    "????",
                    "???",
                    "???",
                    "??????",
                    "?????",
                    "?????",
                    "??????",
                    "?????",
                    "????",
                    "???",
                    "????",
                    "???",
                    "????",
                    "???????",
                    "????????",
                    "?????",
                    "?????",
                    "???????",
                    "?????",
                    "???",
                    "???????",
                    "?????",
                    "??????",
                    "???????",
                    "???????????",
                    "?????",
                    "??????",
                    "??????"

					#endregion
				}

                #endregion
            };

            _dirtyWordTrie = new Tree();
            _marks = new HashSet<char> { '.', '!', '?', '.', ',', ':', ':', '-', '"', ' ' };
            _options.DirtyWords.ForEach(dirtyWord => _dirtyWordTrie.AddWord(dirtyWord));
            _stringBuilderPool = new DefaultObjectPoolProvider().CreateStringBuilderPool(MaxTextLength / 2, MaxTextLength);
        }

        [Params(
            "???? ?????, ?????? ?????? ????????? ???? ???? ???????. ???????? ???? 48 ???. ???????? ?????? ???? ???????, ???????? ???. ????????????? ????? ??????? ??? ???????, ??? ??????? ???? ? ?????? ?????? ?????? ? ???? ?? 23 ????. ??? ?????? ??????!!!"
            )]
        public string OriginalText { get; set; }

        [Benchmark(Description = "FindDirtyWord")]
        public (bool isContain, string? dirtyWord) FindDirtyWord()
        {
            var replaceSymbols = ReplaceSymbols(OriginalText.ToLower());
            var preparedText = RemoveDoubleCharAndPunctuationMarks(replaceSymbols);

            foreach (var word in preparedText.Split(' ').Where(x => x.Length > 2))
            {
                var isTreeContainFullWord = _dirtyWordTrie.HasWord(word);

                if (isTreeContainFullWord)
                {
                    return (true, word);
                }

                var isTreeContainPartWord = _dirtyWordTrie.HasWord(word[..^1]);

                if (isTreeContainPartWord)
                {
                    return (true, word[..^1]);
                }
            }

            return (false, null);
        }

        [Benchmark(Description = "FindDirtyWord2")]
        public (bool isContain, string? dirtyWord) FindDirtyWord2()
        {
            var text = OriginalText.ToLower();
            var sb = new StringBuilder(OriginalText.Length);
            var words = new List<string>();

            if (text[0] != ' ')
            {
                var insertChar = _options.ReplaceSymbols.TryGetValue(text[0], out var changedSymbol) ? changedSymbol : text[0];

                if (!_marks.Contains(insertChar))
                {
                    sb.Append(insertChar);
                }
            }

            for (var i = 1; i < text.Length; i++)
            {
                var currentChar = text[i];
                var beforeChar = text[i - 1];

                if (currentChar == beforeChar)
                {
                    continue;
                }

                var insertChar = _options.ReplaceSymbols.TryGetValue(currentChar, out var changedSymbol) ? changedSymbol : currentChar;

                if (insertChar == ' ' && _marks.Contains(beforeChar))
                {
                    continue;
                }

                if (!_marks.Contains(currentChar))
                {
                    sb.Append(insertChar);
                }
                else if (sb.Length > 0)
                {
                    words.Add(sb.ToString());
                    sb.Clear();
                }
            }

            foreach (var word in words.Where(x => x.Length > 2))
            {
                var isTreeContainFullWord = _dirtyWordTrie.HasWord(word);

                if (isTreeContainFullWord)
                {
                    return (true, word);
                }

                var isTreeContainPartWord = _dirtyWordTrie.HasWord(word[..^1]);

                if (isTreeContainPartWord)
                {
                    return (true, word[..^1]);
                }
            }

            return (false, null);
        }

        private string ReplaceSymbols(string preparedPhrase)
        {
            var sb = _stringBuilderPool.Get();

            foreach (var originalSymbol in preparedPhrase)
            {
                sb.Append(_options.ReplaceSymbols.TryGetValue(originalSymbol, out var changedSymbol) ? changedSymbol : originalSymbol);
            }

            var result = sb.ToString();
            _stringBuilderPool.Return(sb);
            return result;
        }

        private string RemoveDoubleCharAndPunctuationMarks(string text)
        {
            var sb = _stringBuilderPool.Get();

            if (!_marks.Contains(text[0]) && text[0] != ' ')
            {
                sb.Append(text[0]);
            }

            var repeatCount = 0;

            for (var i = 1; i < text.Length; i++)
            {
                var currentChar = text[i];
                var beforeChar = text[i - 1];

                repeatCount = currentChar == beforeChar ? repeatCount + 1 : 0;

                if (repeatCount < 1)
                {
                    sb.Append(_marks.Contains(currentChar) ? ' ' : currentChar);
                }
            }

            var result = sb.ToString();
            _stringBuilderPool.Return(sb);
            return result;
        }
    }
}
