using AlgorithmsFunctions.Shared.Chapter03;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chapter03.Tests
{
    [TestClass]
    public class NodeTest
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
    }
}
