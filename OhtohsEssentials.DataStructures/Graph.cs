using System.Collections;

namespace OhtohsEssentials.DataStructures;

/// <summary>
/// A set of <see cref="Node"/> instances
/// that are all mutually reachable through their neighbors
/// </summary>
public abstract class Graph<T> : IEnumerable<T> where T : IComparable<T>
{
    /// <summary>
    /// The complete set of nodes belonging to this graph.
    /// </summary>
    public abstract IEnumerable<Node> Nodes { get; }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (Node node in Nodes)
            yield return node.Data;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public class Node
    {
        protected readonly ICollection<Node> _neighbors = new List<Node>();

        public T Data { get; set; }

        public virtual IEnumerable<Node> Neighbors => _neighbors;

        public Node(T data)
        {
            Data = data;
        }
    }
}
