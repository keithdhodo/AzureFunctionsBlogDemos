using System;
using System.Linq;

namespace AzureFunctionsBlogDemos.Sorting
{
    /// <summary>
    /// QuickFindInput takes an array of integers and then uses the two other arrays to union
    /// </summary>
    public class Arrays
    {
        public int[] NumberToUnionFrom { get; set; }
        public int[] NumberToUnionTo { get; set; }
        public int[] OutputArray { get; set; }

        public TimeSpan Runtime { get; set; }

        public string AlgorithmName { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }

        public static void QuickFind(Arrays quickFind)
        {
            int max = 0;
            max = quickFind.NumberToUnionFrom.Max() > quickFind.NumberToUnionTo.Max() ? quickFind.NumberToUnionFrom.Max() : quickFind.NumberToUnionTo.Max();

            // setup the output array
            quickFind.OutputArray = new int[max + 1];
            for (int i = 0; i < quickFind.OutputArray.Length; i++)
            {
                quickFind.OutputArray[i] = i;
            }

            for (int i = 0; i < quickFind.NumberToUnionTo.Length; i++)
            {
                int fromIndex = quickFind.NumberToUnionFrom[i];
                int toIndex = quickFind.NumberToUnionTo[i];
                int hold = quickFind.OutputArray[fromIndex];

                for (int j = 0; j < quickFind.OutputArray.Length; j++)
                {
                    if (quickFind.OutputArray[j] == hold)
                    {
                        quickFind.OutputArray[j] = quickFind.OutputArray[toIndex];
                    }
                }
            }
        }

        public static void QuickUnion(Arrays quickFind)
        {
            int max = 0;
            max = quickFind.NumberToUnionFrom.Max() > quickFind.NumberToUnionTo.Max() ? quickFind.NumberToUnionFrom.Max() : quickFind.NumberToUnionTo.Max();

            // setup the output array
            quickFind.OutputArray = new int[max + 1];
            for (int i = 0; i < quickFind.OutputArray.Length; i++)
            {
                quickFind.OutputArray[i] = i;
            }

            for (int i = 0; i < quickFind.NumberToUnionTo.Length; i++)
            {
                int unionFrom = quickFind.NumberToUnionFrom[i];
                int unionTo = quickFind.NumberToUnionTo[i];

                int j, k;

                for (j = unionFrom; j != quickFind.OutputArray[j]; j = quickFind.OutputArray[j]) ;
                for (k = unionTo; k != quickFind.OutputArray[k]; k = quickFind.OutputArray[k]) ;

                quickFind.OutputArray[j] = k;
            }
        }

        public static void WeightedQuickUnion(Arrays quickFind)
        {
            int max = 0;
            max = quickFind.NumberToUnionFrom.Max() > quickFind.NumberToUnionTo.Max() ? quickFind.NumberToUnionFrom.Max() : quickFind.NumberToUnionTo.Max();

            // setup the output and depth arrays
            quickFind.OutputArray = new int[max + 1];
            int[] depth = new int[max + 1];

            for (int i = 0; i < quickFind.OutputArray.Length; i++)
            {
                quickFind.OutputArray[i] = i;
                depth[i] = 1;
            }

            for (int i = 0; i < quickFind.NumberToUnionTo.Length; i++)
            {
                int j, k;

                int unionFrom = quickFind.NumberToUnionFrom[i];
                int unionTo = quickFind.NumberToUnionTo[i];

                for (j = unionFrom; j != quickFind.OutputArray[j]; j = quickFind.OutputArray[j]) ;
                for (k = unionTo; k != quickFind.OutputArray[k]; k = quickFind.OutputArray[k]) ;

                if (depth[j] < depth[k])
                {
                    quickFind.OutputArray[j] = k;
                    depth[k] += depth[j];
                }
                else
                {
                    quickFind.OutputArray[k] = j;
                    depth[j] += depth[k];
                }
            }
        }

        public static void WeightedQuickUnionWithPathCompression(Arrays quickFind)
        {
            int max = 0;
            max = quickFind.NumberToUnionFrom.Max() > quickFind.NumberToUnionTo.Max() ? quickFind.NumberToUnionFrom.Max() : quickFind.NumberToUnionTo.Max();

            // setup the output and depth arrays
            quickFind.OutputArray = new int[max + 1];
            int[] depth = new int[max + 1];

            for (int i = 0; i < quickFind.OutputArray.Length; i++)
            {
                quickFind.OutputArray[i] = i;
                depth[i] = 1;
            }

            for (int i = 0; i < quickFind.NumberToUnionTo.Length; i++)
            {
                int j, k;

                int unionFrom = quickFind.NumberToUnionFrom[i];
                int unionTo = quickFind.NumberToUnionTo[i];

                for (j = unionFrom; j != quickFind.OutputArray[j]; j = quickFind.OutputArray[j])
                {
                    quickFind.OutputArray[j] = quickFind.OutputArray[quickFind.OutputArray[j]];
                }
                for (k = unionTo; k != quickFind.OutputArray[k]; k = quickFind.OutputArray[k])
                {
                    quickFind.OutputArray[k] = quickFind.OutputArray[quickFind.OutputArray[k]];
                }

                if (depth[j] < depth[k])
                {
                    quickFind.OutputArray[j] = k;
                    depth[k] += depth[j];
                }
                else
                {
                    quickFind.OutputArray[k] = j;
                    depth[j] += depth[k];
                }
            }
        }
    }
}