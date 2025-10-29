using OldPhonePadApi.Models.ResponseDto;
using System.Text;

namespace OldPhonePadApi.Helpers.DecodeInput
{
    public class Decoder : IDecoder
    {
        #region key mapping
        private static readonly Dictionary<char, string> Keypad = new()
        {
            ['1'] = "&'(",
            ['2'] = "ABC",
            ['3'] = "DEF",
            ['4'] = "GHI",
            ['5'] = "JKL",
            ['6'] = "MNO",
            ['7'] = "PQRS",
            ['8'] = "TUV",
            ['9'] = "WXYZ",
            ['0'] = " ",
        };
        #endregion

        public Response DecodeInput(string input)
        {

            #region Trim # Char and Split Group
            var result = new StringBuilder();
            var groups = input.TrimEnd('#').Split(' ', StringSplitOptions.RemoveEmptyEntries);
            #endregion

            foreach (var group in groups)
            {
                var processed = HandleBackspaces(group);
                result.Append(MapNumerGroupToLetters(processed));
            }

            return new Response { Message = result.ToString() };
        }

        private static string HandleBackspaces(string group)
        {
            var sb = new StringBuilder(group);

            while (true)
            {
                int index = sb.ToString().IndexOf('*');
                if (index == -1) break;

                if (index > 0)
                {
                    char lastChar = sb[index - 1];
                    int start = index - 1;

                    // Move backwards to remove repeating characters
                    while (start - 1 >= 0 && sb[start - 1] == lastChar)
                        start--;

                    // Remove the sequence + '*'
                    sb.Remove(start, index - start + 1);
                }
                else
                {
                    // '*' at beginning — just remove it
                    sb.Remove(index, 1);
                }
            }

            return sb.ToString();
        }

        private static string MapNumerGroupToLetters(string group)
        {
            var sb = new StringBuilder();
            int startOfIndex = 0;
            while (startOfIndex < group.Length)
            {
                char key = group[startOfIndex];
                int sequenceLength = startOfIndex;

                while (sequenceLength < group.Length && group[sequenceLength] == group[startOfIndex])
                    sequenceLength++;

                int count = sequenceLength - startOfIndex;

                if (Keypad.ContainsKey(key))
                {
                    string letters = Keypad[key];
                    int index = (count - 1) % letters.Length;
                    sb.Append(letters[index]);
                }

                startOfIndex = sequenceLength;
            }

            return sb.ToString();
        }
    }
}
