namespace BenchMarks
{
    public class TreeNode
    {
        private readonly IDictionary<char, TreeNode> _children;
        private readonly char _character;

        public bool HasWord =>
            WordCount > 0;

        public int WordCount { get; set; }

        public TreeNode(char character)
        {
            _character = character;
            _children = new Dictionary<char, TreeNode>();
            WordCount = 0;
        }

        public void SetChild(TreeNode child) =>
            _children[child._character] = child;

        public TreeNode? GetChild(char character) =>
            _children.TryGetValue(character, out var trieNode) ? trieNode : null;

        public TreeNode? GetTreeNode(string prefix)
        {
            var trieNode = this;

            foreach (var prefixChar in prefix)
            {
                trieNode = trieNode.GetChild(prefixChar);

                if (trieNode == null)
                {
                    break;
                }
            }

            return trieNode;
        }
    }
}
