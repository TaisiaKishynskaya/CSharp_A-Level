namespace Mod3.Lection1.Hw2;

// Внутренний класс для представления узла в двусвязном списке
internal class DoublyNode<TNode>
{
    public TNode Value { get; set; }
    public DoublyNode<TNode> Next { get; set; }
    public DoublyNode<TNode> Previous { get; set; }

    public DoublyNode(TNode value)
    {
        Value = value;
    }
}
