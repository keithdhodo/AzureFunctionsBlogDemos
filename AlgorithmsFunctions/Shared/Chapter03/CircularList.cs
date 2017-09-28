using System.Collections.Generic;

namespace AlgorithmsFunctions.Shared.Chapter03
{
    public class CircularList<T>
    {        
        /// <summary>
        /// Inserts a node after a given node
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Node<T> Insert(Node<T> currentNode, T value)
        {
            var nodeToInsert = new Node<T>(value);
            if (EqualityComparer<T>.Default.Equals(currentNode.Item, default(T)))
            {
                nodeToInsert.Next = nodeToInsert;
            }
            else if (currentNode.Next != null)
            {
                nodeToInsert.Next = currentNode.Next;
                currentNode.Next = nodeToInsert;
            }
            else
            {
                nodeToInsert.Next = currentNode.Next;
                currentNode = nodeToInsert;
            }
            return nodeToInsert;
        }

        /// <summary>
        /// Removed the next node
        /// </summary>
        /// <param name="currentNode"></param>
        public void RemoveNextNode(Node<T> currentNode)
        {
            currentNode.Next = currentNode.Next.Next;
        }
    }
}
