namespace Prefix
{
    public interface ITree
    {
        void AddWord(string word);
        bool HasWord(string word);
        bool HasPrefix(string prefix);
    }
}
