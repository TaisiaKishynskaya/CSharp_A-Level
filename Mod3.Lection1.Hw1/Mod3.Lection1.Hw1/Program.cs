using Mod3.Lection1.Hw1;

var tree = new BinaryTree<int>();
tree.Add(5);
tree.Add(3);
tree.Add(7);
tree.Add(2);
tree.Add(4);
tree.Add(6);
tree.Add(8);

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
