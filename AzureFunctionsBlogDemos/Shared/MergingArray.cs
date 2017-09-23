using System.Linq;
using static AzureFunctionsBlogDemos.Shared.Enums;

namespace AzureFunctionsBlogDemos.Merging
{
    /// <summary>
    /// QuickFindInput takes an array of integers and then uses the two other arrays to union
    /// </summary>
    public class MergingArray : IMergingArray
    {
        public int[] NumberToUnionFrom { get; set; }
        public int[] NumberToUnionTo { get; set; }
        public int[] Output { get; set; }

        public void Merge(MergingArray input, MergeAlgorithms algorithmName)
        {
            int max = 0;
            max = input.NumberToUnionFrom.Max() > input.NumberToUnionTo.Max() ? input.NumberToUnionFrom.Max() : input.NumberToUnionTo.Max();

            // setup the Output array
            input.Output = new int[max + 1];
            for (int i = 0; i < input.Output.Length; i++)
            {
                input.Output[i] = i;
            }

            switch(algorithmName)
            {
                case MergeAlgorithms.QuickFind:
                    QuickFind(input);
                    break;
                case MergeAlgorithms.QuickUnion:
                    QuickUnion(input);
                    break;
                case MergeAlgorithms.WeightedQuickUnion:
                    WeightedQuickUnion(input, max);
                    break;
                case MergeAlgorithms.WeightedQuickUnionWithPathCompression:
                    WeightedQuickUnionWithPathCompression(input, max);
                    break;
            }
        }

        private static void QuickFind(MergingArray quickFind)
        {
            for (int i = 0; i < quickFind.NumberToUnionTo.Length; i++)
            {
                int fromIndex = quickFind.NumberToUnionFrom[i];
                int toIndex = quickFind.NumberToUnionTo[i];
                int hold = quickFind.Output[fromIndex];

                for (int j = 0; j < quickFind.Output.Length; j++)
                {
                    if (quickFind.Output[j] == hold)
                    {
                        quickFind.Output[j] = quickFind.Output[toIndex];
                    }
                }
            }
        }

        private static void QuickUnion(MergingArray quickUnion)
        {
            for (int i = 0; i < quickUnion.NumberToUnionTo.Length; i++)
            {
                int NumberToUnionFrom = quickUnion.NumberToUnionFrom[i];
                int NumberToUnionTo = quickUnion.NumberToUnionTo[i];

                int j, k;

                for (j = NumberToUnionFrom; j != quickUnion.Output[j]; j = quickUnion.Output[j]) ;
                for (k = NumberToUnionTo; k != quickUnion.Output[k]; k = quickUnion.Output[k]) ;

                quickUnion.Output[j] = k;
            }
        }

        private static void WeightedQuickUnion(MergingArray weightedQuickUnion, int max)
        {
            // setup the depth array
            int[] depth = new int[max + 1];

            for (int i = 0; i < weightedQuickUnion.Output.Length; i++)
            {
                depth[i] = 1;
            }

            for (int i = 0; i < weightedQuickUnion.NumberToUnionTo.Length; i++)
            {
                int j, k;

                int NumberToUnionFrom = weightedQuickUnion.NumberToUnionFrom[i];
                int NumberToUnionTo = weightedQuickUnion.NumberToUnionTo[i];

                for (j = NumberToUnionFrom; j != weightedQuickUnion.Output[j]; j = weightedQuickUnion.Output[j]) ;
                for (k = NumberToUnionTo; k != weightedQuickUnion.Output[k]; k = weightedQuickUnion.Output[k]) ;

                if (depth[j] < depth[k])
                {
                    weightedQuickUnion.Output[j] = k;
                    depth[k] += depth[j];
                }
                else
                {
                    weightedQuickUnion.Output[k] = j;
                    depth[j] += depth[k];
                }
            }
        }

        private static void WeightedQuickUnionWithPathCompression(MergingArray quickFind, int max)
        {
            // setup the depth array
            int[] depth = new int[max + 1];

            for (int i = 0; i < quickFind.Output.Length; i++)
            {
                depth[i] = 1;
            }

            for (int i = 0; i < quickFind.NumberToUnionTo.Length; i++)
            {
                int j, k;

                int NumberToUnionFrom = quickFind.NumberToUnionTo[i];
                int NumberToUnionTo = quickFind.NumberToUnionTo[i];

                for (j = NumberToUnionFrom; j != quickFind.Output[j]; j = quickFind.Output[j])
                {
                    quickFind.Output[j] = quickFind.Output[quickFind.Output[j]];
                }
                for (k = NumberToUnionTo; k != quickFind.Output[k]; k = quickFind.Output[k])
                {
                    quickFind.Output[k] = quickFind.Output[quickFind.Output[k]];
                }

                if (depth[j] < depth[k])
                {
                    quickFind.Output[j] = k;
                    depth[k] += depth[j];
                }
                else
                {
                    quickFind.Output[k] = j;
                    depth[j] += depth[k];
                }
            }
        }
    }
}