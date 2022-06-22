namespace Prefix
{
    public class TreeNode : TreeNodeBase
    {
        public bool IsWord =>
            WordCount > 0;

        public int WordCount { get; internal set; }

        internal TreeNode(char character) : base(character)
        {
            WordCount = 0;
        }

        internal override void Clear()
        {
            base.Clear();
            WordCount = 0;
        }

        public TreeNode? GetChild(char character) =>
            GetChildInner(character) as TreeNode;

        public bool HasChild(char character) =>
            GetChild(character) != null;

        public TreeNode? GetTrieNode(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            return GetTrieNodeInner(prefix) as TreeNode;
        }

        public IEnumerable<TreeNode> GetChildren() =>
            GetChildrenInner()
                .Cast<TreeNode>();
    }
}
