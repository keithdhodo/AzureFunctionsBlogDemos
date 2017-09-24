using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class CommaSeparatedValueParser
    {
        public static IEnumerable<int> CsvToListOfIntegers(string path)
        {
            var result = new List<int>();
            using (TextFieldParser parser = new TextFieldParser(path))
            {
                // parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        var parsedField = 0;
                        int.TryParse(field, out parsedField);

                        if (parsedField > 0)
                        {
                            result.Add(parsedField);
                        }
                    }
                }
            }
            return result;
        }
    }
}
