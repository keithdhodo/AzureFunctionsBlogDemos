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

        public Node<T> Reverse()
        {
            Node<T> currentNode = this;
            Node<T> nextNode;
            Node<T> holdNode = null;
            
            while (currentNode != null)
            {
                nextNode = currentNode.Next;
                currentNode.Next = holdNode;
                holdNode = currentNode;
                currentNode = nextNode;
            }

            return holdNode;
        }
    }
}