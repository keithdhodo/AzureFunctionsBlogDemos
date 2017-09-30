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
            var list = new CircularList<int>();
            var nodeToInsert = new Node<int>(default(int));

            for (int i = 1; i <= NumberOfParticipants; i++)
            {
                nodeToInsert = list.Insert(nodeToInsert, i);
            }

            while (nodeToInsert != nodeToInsert.Next)
            {
                for (int i = 1; i < OrderToRemoveParticipants; i++)
                {
                    nodeToInsert = nodeToInsert.Next;
                    
                }
                list.RemoveNextNode(nodeToInsert);
            }

            return nodeToInsert;
        }
    }
}
