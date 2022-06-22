namespace Prefix
{
    public class Tree : ITree
    {
        private readonly TreeNode rootTrieNode;

        public Tree()
        {
            rootTrieNode = new TreeNode(' ');
        }

        public void AddWord(string word)
        {
            if (word == null)
            {
                throw new ArgumentNullException(nameof(word));
            }

            AddWord(rootTrieNode, word.ToCharArray());
        }

        public bool HasWord(string word)
        {
            if (word == null)
            {
                throw new ArgumentNullException(nameof(word));
            }

            var trieNode = GetTrieNode(word);

            return trieNode?.IsWord ?? false;
        }

        public bool HasPrefix(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            return GetTrieNode(prefix) != null;
        }

        public TreeNode? GetTrieNode(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            return rootTrieNode.GetTrieNode(prefix);
        }

        private void AddWord(TreeNode trieNode, char[] word)
        {
            foreach (var c in word)
            {
                var child = trieNode.GetChild(c);

                if (child == null)
                {
                    child = new TreeNode(c);
                    trieNode.SetChild(child);
                }

                trieNode = child;
            }

            trieNode.WordCount++;
        }
    }
}
