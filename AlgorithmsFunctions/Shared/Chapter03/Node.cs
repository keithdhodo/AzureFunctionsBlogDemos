using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Sort a LinkedList -- first Node is expected to be an empty head - need to optimize
        /// </summary>
        /// <returns></returns>
        public Node<T> Sort()
        {
            Node<T> head, itemAfterNext, secondLinkedListHead, nextItemInSecondLinkedList = new Node<T>(default(T));
            
            while (this.Next != null)
            {
                head = this.Next;
                itemAfterNext = head.Next;
                this.Next = itemAfterNext;

                for (secondLinkedListHead = nextItemInSecondLinkedList; secondLinkedListHead.Next != null; secondLinkedListHead = secondLinkedListHead.Next)
                {
                    if (Comparer<T>.Default.Compare(secondLinkedListHead.Next.Item, head.Item) > 0)
                    {
                        break;
                    }
                }
                head.Next = secondLinkedListHead.Next;
                secondLinkedListHead.Next = head;
            }

            return nextItemInSecondLinkedList.Next;
        }
    }
}