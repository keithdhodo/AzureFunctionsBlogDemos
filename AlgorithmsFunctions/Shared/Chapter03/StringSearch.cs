using System.Collections.Generic;

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
                for (int j = 0; j < pattern.Length; j++)
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

        public string Squeeze(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var inputAsArray = input.ToCharArray();
            var elementCount = inputAsArray.Length;
            var currentElement = 1;

            for (int i = 1; i < elementCount; i++)
            {
                inputAsArray[currentElement] = inputAsArray[i];
                if (inputAsArray[currentElement] != ' ')
                {
                    currentElement++;
                }
                else if (inputAsArray[currentElement - 1] != ' ')
                {
                    currentElement++;
                }
            }

            return new string(inputAsArray, 0, currentElement);
        }

        public Dictionary<int, int> FindMatches(string pattern, string searchString)
        {
            var positions = new Dictionary<int, int>();

            if (pattern.Length > searchString.Length)
            {
                return positions;
            }

            for (int i = 0; i < searchString.Length; i++)
            {
                var startPosition = -1;

                for (int j = 0; j < pattern.Length; j++)
                {
                    if (searchString[i + j] == pattern[j] && startPosition == -1)
                    {
                        startPosition = i + j;
                    }

                    if (searchString[i + j] != pattern[j])
                    {
                        startPosition = -1;
                        break;
                    }

                    if (j == pattern.Length - 1)
                    {
                        positions.Add(startPosition, i + j);
                    }
                }
            }

            return positions;
        }
    }
}
