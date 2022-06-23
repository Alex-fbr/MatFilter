using System.Diagnostics;
using System.Text;

using Prefix;

using Serilog;

var _logger = new LoggerConfiguration()
    .WriteTo.File(@"D:\logs\prefixMat-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var alphabet1 = new Dictionary<char, char>
{
    {'a', 'а' },
    {'@', 'а'},
    {'6', 'б'},
    {'b', 'б'},
    {'v', 'в'},
    {'g', 'г'},
    {'r', 'г'},
    {'d', 'д'},
    {'e', 'е'},
    {'ё', 'е'},
    {'3', 'з'},
    {'z', 'з'},
    {'i', 'и'},
    {'й', 'и'},
    {'k', 'к'},
    {'l', 'л'},
    {'m', 'м'},
    {'n', 'н'},
    {'o', 'о'},
    {'0', 'о'},
    {'p', 'п'},
    {'c', 'с'},
    {'s', 'с'},
    {'t', 'т'},
    {'y', 'у'},
    {'u', 'у'},
    {'f', 'ф'},
    {'x', 'х'},
    {'h', 'х'}
};

var DirtyWords = new[] { "пидар"};


ITree dirtyWordTrie = new Tree();

foreach (var dirtyWord in DirtyWords)
{
    dirtyWordTrie.AddWord(dirtyWord);
}


var timer = new Stopwatch();
timer.Start();

var originalText = "пидар";
_logger.Information($"originalText = '{originalText}'");

var replaceSymbols = ReplaceSymbols(alphabet1, originalText);
_logger.Information($"replaceSymbols = '{replaceSymbols}'");

string removePunctuationMarks = RemoveDoubleCharAndPunctuationMarks(replaceSymbols);
_logger.Information($"removePunctuationMarks = '{removePunctuationMarks}'");

//var removeDoubleChar = RemoveDoubleChar(removePunctuationMarks);
//_logger.Information($"removeDoubleChar = '{removeDoubleChar}'");

var words = removePunctuationMarks.Split(' ');

foreach (var word in words.Where(x => x.Length > 2))
{
    var hasWord = dirtyWordTrie.HasWord(word);
    var hasPrefix = dirtyWordTrie.HasPrefix(word);
    _logger.Information($"word = '{word}', hasWord = '{hasWord}', hasPrefix = '{hasPrefix}'");

    if (hasWord || hasPrefix)
    {
        _logger.Information($"МАТ = '{word}'");
    }
    else
    {
        var word2 = word.Substring(0, word.Length - 1);
        var hasWord2 = dirtyWordTrie.HasWord(word2);
        var hasPrefix2 = dirtyWordTrie.HasPrefix(word2);
        _logger.Information($"word2 = '{word2}', hasWord2 = '{hasWord2}', hasPrefix2 = '{hasPrefix2}'");

        if (hasWord2)
        {
            _logger.Information($"МАТ = '{word}'");
        }
    }
}

timer.Stop();
_logger.Information($"Время = '{timer.Elapsed.TotalMilliseconds}' мс");
_logger.Information($"THE END");


static string ReplaceSymbols(Dictionary<char, char> alphabet, string preparedPhrase)
{
    var str = new StringBuilder();

    foreach (var originalSymbol in preparedPhrase)
    {
        if (alphabet.TryGetValue(originalSymbol, out var changedSymbol))
        {
            str.Append(changedSymbol);
        }
        else
        {
            str.Append(originalSymbol);
        }
    }

    return str.ToString().ToLower();
}

static string RemoveDoubleCharAndPunctuationMarks(string text)
{
    var marks = new char[] { '.', '!', '?', '.', ',', ':', ':', '-', '"' };
    var str = new StringBuilder();
    str.Append(marks.Contains(text[0]) ? ' ' : text[0]);
    int repeatCount = 0;

    for (int i = 1; i < text.Length; i++)
    {
        var currentChar = text[i];
        var beforeChar = text[i - 1];

        repeatCount = currentChar == beforeChar ? repeatCount + 1 : 0;

        if (repeatCount < 1)
        {
            str.Append(marks.Contains(currentChar) ? ' ' : currentChar);
        }
    }

    return str.ToString().Trim().ToLower();
}

//static string RemovePunctuationMarks(string replaceSymbols)
//{
//    var marks = new char[] { '.', '!', '?', '.', ',', ':', ':', '-', '"' };
//    var str = new StringBuilder();

//    foreach (var symbol in replaceSymbols)
//    {
//        str.Append(marks.Contains(symbol) ? ' ' : symbol);
//    }

//    return str.ToString().Trim();
//}


//static string RemoveDoubleChar(string originalText)
//{
//    var inputStringBuilder = new StringBuilder();
//    inputStringBuilder.Append(originalText[0]);
//    int repeatCount = 0;

//    for (int i = 1; i < originalText.Length; i++)
//    {
//        var currentChar = originalText[i];
//        var beforeChar = originalText[i - 1];

//        repeatCount = currentChar == beforeChar ? repeatCount + 1 : 0;

//        if (repeatCount < 1)
//        {
//            inputStringBuilder.Append(currentChar);
//        }
//    }

//    return inputStringBuilder.ToString();
//}