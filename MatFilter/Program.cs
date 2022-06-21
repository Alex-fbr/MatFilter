using System.Collections.Concurrent;
using System.Text;

using Serilog;

int start = 1000, stop = 5000;

ILogger _logger = new LoggerConfiguration()
                .WriteTo.File(@$"D:\logs\мат-{start}-{stop}-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

var worlds = (File.ReadAllLines(@"E:\Загрузки\Answers.csv")).ToList() ?? new List<string>();
_logger.Information($"В файле {worlds.Count} строк");

var queue = new ConcurrentQueue<(string, bool)>();

for (int i = start; i < stop; i += 1000)
{
    _logger.Information($"{i}-й Цикл");
    await Processing(i, ObsceneWords.Inspector.ObsceneWords, ObsceneWords.Inspector.Alphabet, _logger, worlds);
    await Task.Delay(100);
}

_logger.Information($"THE END");

using var streamWriter = new StreamWriter(@$"D:\logs\results-{start}-{stop}.csv", true, Encoding.UTF8);

while (queue.TryDequeue(out var localValue))
{
    streamWriter.WriteLine(string.Join(",", localValue.Item1, localValue.Item2 ? "1" : "0"));
}

Console.WriteLine("готово");

string PreparePhrase(IEnumerable<(char key, string[] value)> alphabet, string phrase)
{
    var inputStringBuilder = new StringBuilder(phrase);

    foreach (var (key, value) in alphabet)
    {
        // Проходимся по каждой букве в значении словаря. То есть по вот этим спискам ['а', 'a', '@'].
        foreach (var letter in value)
        {
            // Проходимся по каждой букве в нашей фразе.
            foreach (var phr in inputStringBuilder.ToString().ToCharArray())
            {
                // Если буква совпадает с буквой в нашем списке.
                if (letter == phr.ToString())
                {
                    // Заменяем эту букву на ключ словаря.
                    inputStringBuilder.Replace(phr, key);
                }
            }
        }
    }

    return inputStringBuilder.ToString();
}

bool Check(IEnumerable<string> obsceneWords, string phrase)
{
    // Проходимся по всем словам.
    foreach (var word in obsceneWords)
    {
        // Разбиваем слово на части, и проходимся по ним.
        for (int part = 0; part < phrase.Length; part++)
        {
            // Вот сам наш фрагмент
            var fragment = phrase.Substring(part, phrase.Length - part > word.Length ? word.Length + 1 : phrase.Length - part);
            var distance = GetDistance(fragment, word);
            // Если отличие этого фрагмента меньше или равно 25% этого слова, то считаем, что они равны.
            var normilize = word.Length <= 4 ? 1.0 : word.Length * 0.25;
            //  _logger.Information($"в fragment = '{fragment}' ищем '{word}'. GetDistance = {distance}. ");

            if (distance <= normilize)
            {
                if (fragment.Trim().Equals(word))
                {
                    var exceptions = ObsceneWords.Inspector.ExceptionWords?.FirstOrDefault(x => x.Item1 == word).Item2;

                    if (exceptions == null)
                    {
                        _logger.Error($"Слово '{fragment}' похоже на '{word}' в тексте: '{phrase}'");
                        return true;
                    }

                    if (exceptions.All(x => !phrase.Contains(x)))
                    {
                        _logger.Error($"Слово '{fragment}' похоже на '{word}' в тексте: '{phrase}'");
                        return true;
                    }
                }
            }
        }
    }

    return false;
}

double GetDistance(string a, string b)
{
    // Calculates the Levenshtein distance between a and b.
    int n = a.Length, m = b.Length;

    if (n > m)
    {
        // Make sure n <= m, to use O(min(n, m)) space
        (a, b) = (b, a);
        (m, n) = (n, m);
    }

    // Keep current and previous row, not entire matrix
    var current_row = Enumerable.Range(0, n + 1).ToList();

    foreach (var i in Enumerable.Range(1, m))
    {
        var previous_row = current_row;
        current_row = new List<int> { i };
        current_row.AddRange(Enumerable.Repeat(0, n));

        foreach (var j in Enumerable.Range(1, n))
        {
            int add = previous_row[j] + 1, delete = current_row[j - 1] + 1, change = previous_row[j - 1];

            if (a[j - 1] != b[i - 1])
            {
                change++;
            }

            current_row[j] = new List<int> { add, delete, change }.Min();
        }
    }

    return current_row[n];
}

async Task Processing(int startIndex, IEnumerable<string> obsceneWords, IEnumerable<(char, string[])> alphabet, ILogger _logger, List<string> worlds)
{
    Task[] taskArray = new Task[10];

    for (int i = 0; i < taskArray.Length; i++)
    {
        _logger.Information($"{i}-я таска, startIndex = {startIndex + 100 * i} take 100");
        var packet = worlds.Skip(startIndex + 100 * i).Take(100).ToList();

        taskArray[i] = Task.Factory.StartNew(() =>
        {
            foreach (var str in packet)
            {
                var phrase = PreparePhrase(alphabet, str);
                var t = Check(obsceneWords, phrase);
                _logger.Warning($"Мат {(t ? string.Empty : "НЕ")} найден при обработке строки: [{str}]");
                queue.Enqueue((str.Replace(",", "").Replace(";", "").Replace('"', ' ').Trim(), t));
            }
        });
    }

    await Task.WhenAll(taskArray);
}