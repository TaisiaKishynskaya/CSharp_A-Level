using System.Collections;

namespace Mod3.Lection1.Hw1;

internal class BinaryTree<T>: IEnumerable<T>
{
    private TreeNode<T>? root;

    public int Count { get; private set; }

    public void Add(T value)
    {
        if (root == null)
        {
            root = new TreeNode<T>(value);
        }
        else
        {
            AddTo(root, value);
        }

        Count++;
        Console.WriteLine($"Amount nodes in tree: {Count}");
    }

    private void AddTo(TreeNode<T> node, T value)
    {
        if (Comparer<T>.Default.Compare(value, node.Value) < 0)
        {
            if (node.Left == null)
            {
                node.Left = new TreeNode<T>(value);
            }
            else
            {
                AddTo(node.Left, value);
            }
        }
        else
        {
            if (node.Right == null)
            {
                node.Right = new TreeNode<T>(value);
            }
            else
            {
                AddTo(node.Right, value);
            }
        }
    }


    public IEnumerator<T> GetEnumerator()
    {
        return InOrderTraversal(root).GetEnumerator();
    }

    public IEnumerable<T> PreOrderTraversal()
    {
        return PreOrderTraversal(root);
    }

    public IEnumerable<T> PostOrderTraversal()
    {
        return PostOrderTraversal(root);
    }


    private IEnumerable<T> InOrderTraversal(TreeNode<T>? node)
    {
        if (node != null)
        {
            foreach (var left in InOrderTraversal(node.Left))
                yield return left;

            yield return node.Value;

            foreach (var right in InOrderTraversal(node.Right))
                yield return right;
        }
    }

    private IEnumerable<T> PreOrderTraversal(TreeNode<T>? node)
    {
        if (node != null)
        {
            yield return node.Value;

            foreach (var left in PreOrderTraversal(node.Left))
                yield return left;

            foreach (var right in PreOrderTraversal(node.Right))
                yield return right;
        }
    }

    private IEnumerable<T> PostOrderTraversal(TreeNode<T>? node)
    {
        if (node != null)
        {
            foreach (var left in PostOrderTraversal(node.Left))
                yield return left;

            foreach (var right in PostOrderTraversal(node.Right))
                yield return right;

            yield return node.Value;
        }
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
