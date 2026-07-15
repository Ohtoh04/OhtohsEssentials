namespace OhtohsEssentials.DataStructures;

public class BinarySearchTree<T> : BinaryTree<T> where T : IComparable<T>
{
    public BinarySearchTree(BinaryTreeNode root) : base(root)
    {
    }

    public BinarySearchTree(T rootValue) : base(rootValue)
    {
    }

    public override BinarySearchTree<T> Insert(BinaryTreeNode node)
    {
        var currentNode = Root;

        while (true)
        {
            if (node.Data.CompareTo(currentNode.Data) < 0)
            {
                if (currentNode.Left is null)
                {
                    currentNode.Left = node;
                    return this;
                }

                currentNode = currentNode.Left;
            }
            else
            {
                if (currentNode.Right is null)
                {
                    currentNode.Right = node;
                    return this;
                }

                currentNode = currentNode.Right;
            }
        }
    }

    public override BinarySearchTree<T> Insert(T nodeValue)
    {
        Insert(new BinaryTreeNode(nodeValue));

        return this;
    }

    public override BinaryTreeNode? Search(T nodeValue)
    {
        var currentNode = Root;

        while (currentNode is not null)
        {
            int comparison = nodeValue.CompareTo(currentNode.Data);

            if (comparison == 0)
            {
                return currentNode;
            }

            currentNode = comparison < 0 ? currentNode.Left : currentNode.Right;
        }

        return null;
    }

    public override bool Delete(T nodeValue)
    {
        BinaryTreeNode? parent = null;
        var currentNode = Root;

        while (currentNode is not null && currentNode.Data.CompareTo(nodeValue) != 0)
        {
            parent = currentNode;
            currentNode = nodeValue.CompareTo(currentNode.Data) < 0 ? currentNode.Left : currentNode.Right;
        }

        if (currentNode is null)
        {
            return false;
        }

        if (currentNode.Left is not null && currentNode.Right is not null)
        {
            BinaryTreeNode successorParent = currentNode;
            BinaryTreeNode successor = currentNode.Right;

            while (successor.Left is not null)
            {
                successorParent = successor;
                successor = successor.Left;
            }

            currentNode.Data = successor.Data;
            parent = successorParent;
            currentNode = successor;
        }

        BinaryTreeNode? replacementNode = currentNode.Left ?? currentNode.Right;

        if (parent is null)
        {
            if (replacementNode is null)
            {
                return false;
            }

            Root = replacementNode;
        }
        else if (ReferenceEquals(parent.Left, currentNode))
        {
            parent.Left = replacementNode;
        }
        else
        {
            parent.Right = replacementNode;
        }

        return true;
    }
}
