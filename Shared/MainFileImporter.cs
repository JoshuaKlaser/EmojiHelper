using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Shared
{
    public class MainFileImporter
    {
        private List<string> _emojis = new List<string>();

        public void Import()
        {
            var fileData = ImportFile();

            var emojisAndNames = ProcessFileData(fileData);

            SaveEmojisToFile(_emojis);

            CreateEmojiClass(emojisAndNames);
        }

        public string[] ImportFile()
        {
            return File.ReadAllLines("../../../emoji-test.txt");
        }

        private List<EmojiAndName> ProcessFileData(string[] fileData)
        {
            var lineNumCount = 0;
            var emojiCount = 0;
            var previousStartingHex = string.Empty;
            var emojisAndNames = new List<EmojiAndName>();

            try
            {
                foreach (var line in fileData)
                {
                    lineNumCount++;

                    if (line.StartsWith("#"))
                        continue;

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    emojisAndNames.Add(ParseLine(line, ref emojiCount, ref previousStartingHex));
                }
            }
            catch (Exception e)
            {
                var q = 0;
            }

            var w = 0;

            return emojisAndNames;
        }

        private EmojiAndName ParseLine(string line, ref int emojiCount, ref string previousStartingHexValue)
        {
            if (emojiCount == 21)
            {
                var hello = 0;
            }

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

            previousStartingHexValue = startingHexValue;

            TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;
            var name = emojiAndName.Substring(emojiAndName.IndexOf('E') + 1);
            var formattedName = name.Substring(name.IndexOf(' ') + 1).Replace("-", " ").Replace(":", "");
            var finalName = txtInfo.ToTitleCase(formattedName).Replace(" ", "").Replace(".", "").Replace("’", "")
                .Replace("“", "").Replace("”","").Replace("!", "").Replace("&","").Replace("(", "").Replace(")", "")
                .Replace("1St", "First").Replace("2Nd", "Second").Replace("3Rd", "Third").Replace("*","");

            // .ToUpperInvariant().Replace(" ", "");

            return new EmojiAndName
            {
                Emoji = emoji,
                Name = finalName
            };
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

        private void CreateEmojiClass(IEnumerable<EmojiAndName> emojis)
        {
            var fileOpening = "public class Emojis" + Environment.NewLine + "{" + Environment.NewLine;
            var fileClosing = "}";

            string fileString = fileOpening + string.Join(Environment.NewLine, emojis.Select(e => GenerateEmojiClassLine(e))) + Environment.NewLine + fileClosing;

            File.WriteAllText("Emojis.cs", fileString);
        }

        private string GenerateEmojiClassLine(EmojiAndName emojiAndName)
        {
            return $"   public string {emojiAndName.Name} = \"{emojiAndName.Emoji}\";";
        }

        private void BuildEmojiRegex()
        {

        }
    }
}
