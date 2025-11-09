namespace PROG7312_POE.Models.DataStructures
{
    public class TreeNode<T> where T : IComparable<T>
    {
        public T Value;
        public TreeNode<T>? Left;
        public TreeNode<T>? Right;

        public TreeNode(T value)
        {
            Value = value;
        }
    }

    public class BinarySearchTree<T> where T : IComparable<T>
    {
        private TreeNode<T>? _root;

        public void Insert(T value) => _root = Insert(_root, value);

        private TreeNode<T> Insert(TreeNode<T>? node, T value)
        {
            if (node == null) return new TreeNode<T>(value);
            if (value.CompareTo(node.Value) < 0) node.Left = Insert(node.Left, value);
            else node.Right = Insert(node.Right, value);
            return node;
        }

        public IEnumerable<T> InOrder() => InOrder(_root);

        private IEnumerable<T> InOrder(TreeNode<T>? node)
        {
            if (node == null) yield break;
            foreach (var v in InOrder(node.Left)) yield return v;
            yield return node.Value;
            foreach (var v in InOrder(node.Right)) yield return v;
        }
    }
}
