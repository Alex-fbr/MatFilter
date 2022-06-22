using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ObsceneWordsController : ControllerBase
    {
        private readonly ILogger<ObsceneWordsController> _logger;
        private readonly ObsceneWordsOption _obsceneWordsOption;
        private readonly DirtyWordsOption dirtyWordsOption;

        public ObsceneWordsController(
            ILogger<ObsceneWordsController> logger,
            IOptionsMonitor<ObsceneWordsOption> optionsMonitor,
             IOptionsMonitor<DirtyWordsOption> dirtyWordsOption)
        {
            _logger = logger;
            _obsceneWordsOption = optionsMonitor.CurrentValue;
            this.dirtyWordsOption = dirtyWordsOption.CurrentValue;
        }

        [HttpGet]
        [Route("isContainObsceneWords")]
        public IActionResult Get([StringLength(500, MinimumLength = 20)] string originalText = "Мои друзья все такие лоhИ, как гавайская пицца")
        {
            _logger.LogInformation($"originalText = '{originalText}'");

            var timer = new Stopwatch();
            timer.Start();
            var (result, fragment, word) = Check(originalText);
            timer.Stop();

            _logger.LogError($"Фрагмент '{fragment}' похож на '{word}' в тексте: '{originalText}'");
            _logger.LogInformation($"TotalMilliseconds = {timer.Elapsed.TotalMilliseconds} ms");

            return Ok($"Result = {result}, TotalMilliseconds = {timer.Elapsed.TotalMilliseconds} ms");
        }

        private (bool result, string? fragment, string? word) Check(string originalText)
        {
            var inputStr = new StringBuilder();
            var phrase = PreparePhrase(originalText);

            // Проходимся по всем словам.
            foreach (var word in _obsceneWordsOption?.ObsceneWords)
            {
                // Разбиваем слово на части, и проходимся по ним.
                for (int part = 0; part < phrase.Length; part++)
                {
                    // Вот сам наш фрагмент
                    inputStr.Clear();
                    inputStr.Append(phrase.AsSpan(part, phrase.Length - part > word.Length ? word.Length + 1 : phrase.Length - part));
                    var fragment = inputStr.ToString();

                    // Если отличие этого фрагмента меньше или равно 25% этого слова, то считаем, что они равны.
                    var distance = GetDistance(fragment, word);
                    var normilize = word.Length <= 4 ? 1.0 : word.Length * 0.20;

                    if (distance <= normilize)
                    {
                        if (fragment.Trim().Equals(word))
                        {
                            var exceptions = _obsceneWordsOption?.ExceptionWords.FirstOrDefault(x => x.Key == word)?.Value;

                            if (exceptions == null)
                            {
                                return (true, fragment, word);
                            }

                            if (exceptions.All(x => !phrase.Contains(x)))
                            {
                                return (true, fragment, word);
                            }
                        }
                    }
                }
            }

            return (false, null, null);
        }

        private string PreparePhrase(string originalText)
        {
            var inputStringBuilder = new StringBuilder(originalText.ToLower().Replace(" ", ""));

            foreach (var item in _obsceneWordsOption?.Alphabet)
            {
                // Проходимся по каждой букве в значении словаря. То есть по вот этим спискам ['а', 'a', '@'].
                foreach (var letter in item.Value)
                {
                    // Проходимся по каждой букве в нашей фразе.
                    foreach (var phr in inputStringBuilder.ToString().ToCharArray())
                    {
                        // Если буква совпадает с буквой в нашем списке.
                        if (letter == phr.ToString())
                        {
                            // Заменяем эту букву на ключ словаря.
                            inputStringBuilder.Replace(phr, item.Key);
                        }
                    }
                }
            }

            return inputStringBuilder.ToString();
        }

        static double GetDistance(string originText, string template)
        {
            // Calculates the Levenshtein distance between a and b.
            int n = originText.Length, m = template.Length;

            if (n > m)
            {
                // Make sure n <= m, to use O(min(n, m)) space
                (originText, template) = (template, originText);
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

                    if (originText[j - 1] != template[i - 1])
                    {
                        change++;
                    }

                    current_row[j] = new List<int> { add, delete, change }.Min();
                }
            }

            return current_row[n];
        }
    }
}