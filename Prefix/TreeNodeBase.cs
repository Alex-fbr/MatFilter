namespace Prefix
{
#pragma warning disable S4035
    //Seal class 'CacheKey' or implement 'IEqualityComparer<T>' instead.
    public abstract class TreeNodeBase : IEquatable<TreeNodeBase>
    {
        public char Character { get; private set; }

        private readonly IDictionary<char, TreeNodeBase> children;

        protected TreeNodeBase(char character)
        {
            Character = character;
            children = new Dictionary<char, TreeNodeBase>();
        }

        internal void RemoveChild(char character) =>
            children.Remove(character);

        internal virtual void Clear() =>
            children.Clear();

        internal IEnumerable<TreeNodeBase> GetChildrenInner() =>
            children.Values;

        internal TreeNodeBase? GetChildInner(char character)
        {
            children.TryGetValue(character, out var trieNode);

            return trieNode;
        }

        internal TreeNodeBase? GetTrieNodeInner(string prefix)
        {
            TreeNodeBase? trieNode = this;

            foreach (var prefixChar in prefix)
            {
                trieNode = trieNode.GetChildInner(prefixChar);

                if (trieNode == null)
                {
                    break;
                }
            }

            return trieNode;
        }

        internal void SetChild(TreeNodeBase child)
        {
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            children[child.Character] = child;
        }

        public bool Equals(TreeNodeBase? other) =>
            other != null && Character == other.Character;
    }
}
