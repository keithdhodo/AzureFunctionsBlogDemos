using AlgorithmsFunctions.Shared.Chapter03;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Chapter03.Tests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void Node_ReverseSimpleLinkedList()
        {
            var head = new Node<int>(1);
            var nextNode = head;

            for (int i = 2; i < 10; i++)
            {
                nextNode = nextNode.Next = new Node<int>(i);
            }

            var reversedLinkedList = head.Reverse();
            Assert.AreEqual(9, reversedLinkedList.Item);
        }

        [TestMethod]
        public void Node_ReverseSimpleLinkedListStartAtSecondNode()
        {
            var head = new Node<int>(1);
            var nextNode = head;

            for (int i = 2; i < 10; i++)
            {
                nextNode = nextNode.Next = new Node<int>(i);
            }

            var reversedLinkedList = head.Next.Reverse();

            var lastNode = new Node<int>(0);
            var traversal = reversedLinkedList;

            while (traversal.Next != null)
            {
                lastNode = traversal.Next;
                traversal = traversal.Next;
            }

            Assert.AreEqual(2, lastNode.Item);
            Assert.AreEqual(9, reversedLinkedList.Item);
        }

        [TestMethod]
        public void Node_SortSimpleLinkedList()
        {
            var head = new Node<int>(0);
            var nextNode = head;

            for (int i = 1; i < 10; i++)
            {
                nextNode = nextNode.Next = new Node<int>(100 / i);
            }

            var sortedLinkedList = head.Sort();

            var lastNode = new Node<int>(0);
            var traversal = sortedLinkedList;

            while (traversal.Next != null)
            {
                lastNode = traversal.Next;
                traversal = traversal.Next;
            }

            Assert.AreEqual(11, sortedLinkedList.Item);
            Assert.AreEqual(100, lastNode.Item);
        }

        [TestMethod]
        public void Node_SorSmallLinkedList()
        {
            var head = new Node<int>(0);
            var nextNode = head;

            for (int i = 1; i < 1000; i++)
            {
                nextNode = nextNode.Next = new Node<int>(1000 % i);
            }

            var sortedLinkedList = head.Sort();

            var lastNode = new Node<int>(0);
            var traversal = sortedLinkedList;

            while (traversal.Next != null)
            {
                lastNode = traversal.Next;
                traversal = traversal.Next;
            }

            Assert.AreEqual(0, sortedLinkedList.Item);
            Assert.AreEqual(499, lastNode.Item);
        }

        [TestMethod]
        public void Node_SortMediumLinkedList()
        {
            var head = new Node<int>(0);
            var nextNode = head;
            var items = (int)Math.Pow(10, 4);

            for (int i = 1; i < items; i++)
            {
                nextNode = nextNode.Next = new Node<int>(items % i);
            }

            var sortedLinkedList = head.Sort();

            var lastNode = new Node<int>(0);
            var traversal = sortedLinkedList;

            while (traversal.Next != null)
            {
                lastNode = traversal.Next;
                traversal = traversal.Next;
            }

            Assert.AreEqual(0, sortedLinkedList.Item);
            Assert.AreEqual(4999, lastNode.Item);
        }

        /// <summary>
        /// This test will take quite some time to run.
        /// </summary>
        [Ignore]
        [TestMethod]
        public void Node_SortLargeLinkedList()
        {
            var head = new Node<int>(0);
            var nextNode = head;
            var items = (int)Math.Pow(10, 6);

            for (int i = 1; i < items; i++)
            {
                nextNode = nextNode.Next = new Node<int>(items % i);
            }

            var sortedLinkedList = head.Sort();

            var lastNode = new Node<int>(0);
            var traversal = sortedLinkedList;

            while (traversal.Next != null)
            {
                lastNode = traversal.Next;
                traversal = traversal.Next;
            }

            Assert.AreEqual(0, sortedLinkedList.Item);
            Assert.AreEqual(4999, lastNode.Item);
        }

        [TestMethod]
        public void Node_MoveLargestItemToEnd_SingleNode()
        {
            var head = new Node<int>(50);

            Assert.AreEqual(head, head.MoveLargestItemToEnd());
        }
    }
}
