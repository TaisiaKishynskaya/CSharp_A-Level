using Mod3.Lection1.Hw1;

var tree = new BinaryTree<int>{ 5, 3, 7, 2, 4, 6, 8 };

Console.WriteLine("In-Order Traversal:");
foreach (var value in tree)
{
    Console.Write(value + " ");
}

Console.WriteLine("\nPre-Order Traversal:");
foreach (var value in tree.PreOrderTraversal())
{
    Console.Write(value + " ");
}

Console.WriteLine("\nPost-Order Traversal:");
foreach (var value in tree.PostOrderTraversal())
{
    Console.Write(value + " ");
}
