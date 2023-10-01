using System.Collections;

namespace Mod3.Lection1.Hw2;

internal class DoublyLinkedList<T> : IEnumerable<T>
{
    private DoublyNode<T>? head;
    private DoublyNode<T>? tail;
    public int Count { get; private set; }

    // Индексатор для доступа к элементам по индексу
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var current = head;
            for (var i = 0; i < index; i++)
            {
                current = current?.Next;
            }

            return current!.Value;
        }
        set
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var current = head;
            for (var i = 0; i < index; i++)
            {
                current = current?.Next;
            }

            current!.Value = value;
        }
    }

    // Добавление элемента в конец списка
    public void Add(T item)
    {
        var newNode = new DoublyNode<T>(item);

        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            newNode.Previous = tail;
            tail = newNode;
        }

        Count++;
    }

    // Удаление элемента по значению
    public bool Remove(T item)
    {
        var current = head;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, item))
            {
                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    head = current.Next;
                }

                if (current.Next != null)
                {
                    current.Next.Previous = current!.Previous;
                }
                else
                {
                    tail = current.Previous;
                }

                Count--;
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    // Реализация интерфейса IEnumerable<T>
    public IEnumerator<T> GetEnumerator()
    {
        var current = head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    // Реализация интерфейса IEnumerable
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
