namespace OhtohsEssentials.DataStructures;

// https://www.geeksforgeeks.org/dsa/binary-tree-data-structure/
public class BinaryTree<T> : Graph<T> where T : IComparable<T>
{
    public BinaryTreeNode Root { get; protected set; }

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

    public virtual BinaryTree<T> Insert(BinaryTreeNode node)
    {
        Stack<BinaryTreeNode> stack = new();
        stack.Push(Root);

        while (stack.Count > 0)
        {
            BinaryTreeNode currentNode = stack.Pop();

            if (currentNode.Left is null)
            {
                currentNode.Left = node;
                return this;
            }

            if (currentNode.Right is null)
            {
                currentNode.Right = node;
                return this;
            }

            stack.Push(currentNode.Right);
            stack.Push(currentNode.Left);
        }

        return this;
    }

    public virtual BinaryTree<T> Insert(T nodeValue)
    {
        var node = new BinaryTreeNode(nodeValue);

        Insert(node);

        return this;
    }

    public virtual BinaryTreeNode? Search(T nodeValue)
    {
        foreach (Node node in Nodes)
        {
            if (node.Data.CompareTo(nodeValue) == 0)
            {
                return (BinaryTreeNode)node;
            }
        }

        return null;
    }

    public virtual bool Delete(T nodeValue)
    {
        BinaryTreeNode? nodeToDelete = null;
        BinaryTreeNode? deepestNode = null;
        BinaryTreeNode? parentOfDeepestNode = null;

        Stack<(BinaryTreeNode Node, BinaryTreeNode? Parent)> stack = new();
        stack.Push((Root, null));

        while (stack.Count > 0)
        {
            (BinaryTreeNode node, BinaryTreeNode? parent) = stack.Pop();

            if (node.Data.CompareTo(nodeValue) == 0)
            {
                nodeToDelete ??= node;
            }

            deepestNode = node;
            parentOfDeepestNode = parent;

            if (node.Right is not null) stack.Push((node.Right, node));
            if (node.Left is not null) stack.Push((node.Left, node));
        }

        if (nodeToDelete is null || deepestNode is null)
        {
            return false;
        }

        if (ReferenceEquals(nodeToDelete, deepestNode))
        {
            if (parentOfDeepestNode is null)
            {
                return false;
            }

            if (ReferenceEquals(parentOfDeepestNode.Left, deepestNode))
            {
                parentOfDeepestNode.Left = null;
            }
            else
            {
                parentOfDeepestNode.Right = null;
            }

            return true;
        }

        nodeToDelete.Data = deepestNode.Data;

        if (parentOfDeepestNode is not null)
        {
            if (ReferenceEquals(parentOfDeepestNode.Left, deepestNode))
            {
                parentOfDeepestNode.Left = null;
            }
            else
            {
                parentOfDeepestNode.Right = null;
            }
        }

        return true;
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
