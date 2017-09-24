namespace AlgorithmsFunctions.Shared.Chapter03
{
    public class Node<T>
    {
        public T Item { get; private set; }
        public Node<T> Next { get; set; }

        public Node(T item)
        {
            Item = item;
            Next = null;
        }
    }
}