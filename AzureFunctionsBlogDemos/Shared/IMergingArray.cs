using static AzureFunctionsBlogDemos.Shared.Enums;

namespace AzureFunctionsBlogDemos.Merging
{
    public interface IMergingArray
    {
        int[] NumberToUnionFrom { get; set; }
        int[] NumberToUnionTo { get; set; }
        int[] Output { get; set; }

        void Merge(MergingArray input, MergeAlgorithms algorithmName);
    }
}