namespace BenchMarks
{
    public class Tree : ITree
    {
        private readonly TreeNode _rootTrieNode;

        public Tree()
        {
            _rootTrieNode = new TreeNode(' ');
        }

        public void AddWord(string word) =>
            AddWord(_rootTrieNode, word.ToCharArray());

        public bool HasPrefix(string prefix) =>
            GetTreeNode(prefix) != null;

        public bool HasWord(string word) =>
            GetTreeNode(word)?.HasWord ?? false;

        private TreeNode? GetTreeNode(string prefix) =>
            _rootTrieNode.GetTreeNode(prefix);

        private static void AddWord(TreeNode trieNode, IEnumerable<char> word)
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
