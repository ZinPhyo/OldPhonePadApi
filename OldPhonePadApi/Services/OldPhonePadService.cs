using System.Text.RegularExpressions;

namespace OldPhonePad.Services
{
    public class OldPhonePadService : IOldPhonePadService
    {
        public string OldPhonePad(string input)
        {
            #region Check input is empty
            if(string.IsNullOrEmpty(input)) { return string.Empty; }
            #endregion

            #region Check input allow character
            Regex pattern = new Regex(@"^[0-9*# ]+$");
            if(!pattern.IsMatch(input)) { return "Input only allow 0 to 9 numbers, space, * and #."; }
            #endregion

            #region Check input contain send character
            if (input.LastIndexOf("#") == -1) { return string.Empty; }
            #endregion

            #region Decode Input
            string decodeNumber = DecodeInput(input);
            #endregion

            return decodeNumber;
        }

        #region Private Method
        private string DecodeInput(string input)
        {
            #region key mapping
            Dictionary<char, string> keypad = new Dictionary<char, string>
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

            #region Trim # Char and Split Group
            List<char> result = new();
            string[] groups = input.TrimEnd('#').Split(' ', StringSplitOptions.RemoveEmptyEntries);
            #endregion

            foreach (var group in groups)
            {
                string temp = group;

                #region Remove * and character before it and if behind characters are repeating sequences remove sequences numbers.
                if (temp.Contains('*'))
                {
                    int index = temp.IndexOf('*');

                    if(index >= 1)
                    {
                        int end = index - 1;
                        char lastChar = temp[end];

                        int start = end;
                        while (start - 1 >= 0 && temp[start - 1] == lastChar)
                            start--;

                        // Remove the first repeating sequences number behind '*' and '*'
                        temp = temp.Remove(start, index - start + 1);
                    }
                    else
                    {
                        temp = temp.Replace("*", "");
                    }
                }
                #endregion

                #region Check repeating sequences number count and map related key letters and handles cycling  
                int startOfIndex = 0;
                while(startOfIndex < temp.Length)
                {
                    int sequenceLength = startOfIndex;

                    while (sequenceLength < temp.Length && temp[sequenceLength] == temp[startOfIndex])
                        sequenceLength++;

                    char key = temp[startOfIndex];
                    int count = sequenceLength - startOfIndex;

                    if (keypad.ContainsKey(key))
                    {
                        string letters = keypad[key];
                        int index = (count - 1) % letters.Length;
                        result.Add(letters[index]);
                    }

                    startOfIndex = sequenceLength;
                }
                #endregion
            }

            return new string(result.ToArray());
        }
        #endregion
    }
}
