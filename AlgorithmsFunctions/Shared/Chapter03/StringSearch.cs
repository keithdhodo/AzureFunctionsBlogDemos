namespace AlgorithmsFunctions.Shared.Chapter03
{
    public class StringSearch
    {
        public int CountMatches(string pattern, string searchString)
        {
            int count = 0;
            
            if (pattern.Length > searchString.Length)
            {
                return 0;
            }

            for (int i = 0; i < searchString.Length; i++)
            {
                for(int j = 0; j < pattern.Length; j++)
                {
                    if (searchString[i + j] != pattern[j])
                    {
                        break;
                    }

                    if (j == pattern.Length - 1)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
