namespace Prefix
{
    public interface IDirtyWordsInspector
    {
        (bool isContain, string? dirtyWord) IsContainDirtyWord(string originalText);
    }
}