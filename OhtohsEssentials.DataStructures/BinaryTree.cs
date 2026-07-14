namespace OhtohsEssentials.DataStructures;

// https://www.geeksforgeeks.org/dsa/binary-tree-data-structure/
public class BinaryTree<T> : Graph<T>
{
    public BinaryTreeNode Root { get; private set; }

    public BinaryTree(BinaryTreeNode root)
    {
        Root = root;
    }

    public BinaryTree(T rootValue)
    {
        Root = new BinaryTreeNode(rootValue);
    }

    public override IEnumerable<Node> Nodes
    {
        get
        {
            Stack<BinaryTreeNode> stack = new();
            stack.Push(Root);

            while (stack.Count > 0)
            {
                BinaryTreeNode node = stack.Pop();
                yield return node;

                if (node.Right is not null) stack.Push(node.Right);
                if (node.Left is not null) stack.Push(node.Left);
            }
        }
    }

    public class BinaryTreeNode : Node
    {
        public BinaryTreeNode? Left { get; set; }
        public BinaryTreeNode? Right { get; set; }

        public override IEnumerable<Node> Neighbors
        {
            get
            {
                if (Left is not null) yield return Left;
                if (Right is not null) yield return Right;
            }
        }

        public BinaryTreeNode(T data) : base(data)
        {
        }
    }
}
