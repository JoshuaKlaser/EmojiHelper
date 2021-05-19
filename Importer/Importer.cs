using System;
using System.Collections.Generic;
using System.IO;

namespace Importer
{
    public class Importer
    {
        public IReadOnlyList<EmojiData> ImportEmojisFromFile(string fileName)
        {
            var fileData = ImportFile(fileName);

            ProcessFileData(fileData);
        }

        public void Import()
        {
            var fileData = ImportFile();

            ProcessFileData(fileData);

            SaveEmojisToFile(_emojis);
        }

        public string[] ImportFile(string fileName)
        {
            return File.ReadAllLines(fileName);
        }

        private void ProcessFileData(string[] fileData)
        {
            var lineNumCount = 0;
            var emojiCount = 0;
            var previousStartingHex = string.Empty;

            try
            {
                foreach (var line in fileData)
                {
                    lineNumCount++;

                    if (line.StartsWith("#"))
                        continue;

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    ParseLine(line, ref emojiCount, ref previousStartingHex);
                }
            }
            catch (Exception e)
            {
                var q = 0;
            }

            var w = 0;
        }

        private void ParseLine(string line, ref int emojiCount, ref string previousStartingHexValue)
        {
            var sections = line.Split(';', '#');

            var hexValue = sections[0].Trim();
            var hexValues = hexValue.Split(' ');
            var startingHexValue = hexValues.First();

            var status = sections[1].Trim();
            var emojiAndName = sections[2].Trim();
            var emoji = ParseHexSectionIntoEmoji(hexValues);

            if (startingHexValue == previousStartingHexValue)
            {
                _emojis.Add(emojiCount + " " + (hexValues.Length) + " " + emoji);
            }
            else
            {
                emojiCount++;

                _emojis.Add(emojiCount + " " + emoji);
            }

            var emojiTest = emojiAndName.Replace(emoji, string.Empty);
            var emojiSuccess = emojiTest != emojiAndName;

            _previousEmoji = emoji;
            previousStartingHexValue = startingHexValue;
        }

        private string ParseHexSectionIntoEmoji(string[] hexVals)
        {
            var emoji = string.Empty;

            foreach (var hexVal in hexVals)
            {
                var hexNumber = int.Parse(hexVal, System.Globalization.NumberStyles.HexNumber);
                emoji += char.ConvertFromUtf32(hexNumber);
            }

            return emoji;
        }

        private void SaveEmojisToFile(IEnumerable<string> emojis)
        {
            File.WriteAllText("parsed-emojis.txt", string.Join(Environment.NewLine, emojis));
        }

        private void CreateEmojiClass(IEnumerable<string> emojis)
        {

        }
    }
}
