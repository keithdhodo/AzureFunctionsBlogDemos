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

        /// <summary>
        /// Move the largest item to the end of the list
        /// </summary>
        /// <returns></returns>
        public Node<T> MoveLargestItemToEnd()
        {
            var emtpyNode = new Node<T>(default(T));
            emtpyNode.Next = this;
            var currentNode = this;
            var lastNode = emtpyNode;

            if (currentNode.Next == null)
            {
                return currentNode;
            }

            while (currentNode.Next != null)
            {
                if (Comparer<T>.Default.Compare(currentNode.Item, currentNode.Next.Item) > 0) // swap
                {
                    var swapNode = currentNode.Next;
                    lastNode.Next = swapNode;
                    currentNode.Next = swapNode.Next;
                    swapNode.Next = currentNode;
                    lastNode = swapNode;
                }
                else
                {
                    lastNode = currentNode;
                    currentNode = currentNode.Next;
                }

            }

            return emtpyNode.Next;
        }

        /// <summary>
        /// Move the largest item to the end of the list
        /// </summary>
        /// <returns></returns>
        public Node<T> MoveSmallestItemToFront()
        {
            var emtpyNode = new Node<T>(default(T));
            emtpyNode.Next = this;
            var currentNode = this;
            var lastNode = emtpyNode;
            var swapNode = this;

            if (currentNode.Next == null)
            {
                return currentNode;
            }

            var smellestNode = new Node<T>(default(T));

            // move the smallest node to the end
            while (currentNode.Next != null)
            {
                if (Comparer<T>.Default.Compare(currentNode.Item, currentNode.Next.Item) < 0) // swap
                {
                    swapNode = currentNode.Next;
                    lastNode.Next = swapNode;
                    currentNode.Next = swapNode.Next;
                    swapNode.Next = currentNode;
                    lastNode = swapNode;
                    smellestNode = currentNode;
                }
                else if (currentNode.Next.Next == null && (Comparer<T>.Default.Compare(currentNode.Item, currentNode.Next.Item) > 0))
                {
                    smellestNode = currentNode.Next;
                    currentNode.Next = null;
                }
                else
                {
                    lastNode = currentNode;
                    currentNode = currentNode.Next;
                }
            }

            swapNode = emtpyNode.Next;
            emtpyNode.Next = smellestNode;
            smellestNode.Next = swapNode;

            return emtpyNode.Next;
        }

        public Node<T> GetLastNode()
        {
            Node<T> lastNode = this;

            while (lastNode.Next != null)
            {
                lastNode = lastNode.Next;
            }

            return lastNode;
        }
    }
}