using AlgorithmsFunctions.Shared.Chapter01.Enums;

namespace AlgorithmsFunctions.Shared.Chapter01
{
    public interface IMergingArray
    {
        int[] NumberToUnionFrom { get; set; }
        int[] NumberToUnionTo { get; set; }
        int[] Output { get; set; }

        void Merge(MergingArray input, MergeAlgorithms algorithmName);
    }
}
