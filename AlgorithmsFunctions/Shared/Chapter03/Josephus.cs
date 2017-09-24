namespace AlgorithmsFunctions.Shared.Chapter03
{
    public class Josephus
    {
        public int NumberOfParticipants { get; private set; }
        public int OrderToRemoveParticipants { get; private set; }

        private Josephus()
        {

        }

        public Josephus(int numberOfParticipants, int orderToRemoveParticipants) : this()
        {
            NumberOfParticipants = numberOfParticipants;
            OrderToRemoveParticipants = orderToRemoveParticipants;
        }

        public Node<int> ExecuteJosephusSimulation()
        {
            var head = new Node<int>(1);
            var nextNode = head;
            
            for (int i = 2; i < NumberOfParticipants; i++)
            {
                nextNode = nextNode.Next = new Node<int>(i);
            }

            nextNode.Next = head;

            while (nextNode != nextNode.Next)
            {
                for (int i = 1; i < OrderToRemoveParticipants; i++)
                {
                    nextNode.Next = nextNode.Next.Next;
                }
            }

            return nextNode;
        }
    }
}
