using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Console Tester");

            ActualLoadFile();
        }

        private static void ActualLoadFile()
        {
            var importer = new MainFileImporter();

            importer.Import();
        }

        private static void LoadTestFile2()
        {
            Console.WriteLine("Loading Test File");

            // var manEmoji = 0x1F468;
            var manEmoji = 0x263A;
            var extra = 0xFE0F;
            var emojiManString = char.ConvertFromUtf32(manEmoji) + char.ConvertFromUtf32(extra);

            var someText = $"This is the emoji {emojiManString} man";

            someText = someText.Replace(emojiManString, string.Empty);

            Console.WriteLine("File Loaded");
            Console.ReadLine();
        }

        private static void LoadTestFile()
        {
            Console.WriteLine("Loading Test File");

            var fileText = File.ReadAllLines("../../../emoji-sequences.txt");
            var charArray = new List<char>();

            var intEmoji = 0x231A;
            var intEmoji2 = 0x231B;

            var intEmojiSequence = 0x0001F468;
            var intEmojiSequence2 = 0x200D;
            var intEmojiSequence3 = 0x2695;
            var intEmojiSequence4 = 0xFE0F;

            var intEmojiMan = 0x0001F468;
            string charEmojiMan = "\u0001F468";
            byte[] byteArrayEmojiMan = { 0x01, 0xF4, 0x68 };

            foreach (var chars in charEmojiMan)
            {
                var isSurr = char.IsSurrogate(chars);
            }

            var isPair = char.IsSurrogatePair(charEmojiMan[0], charEmojiMan[1]);

            var emoji = char.ConvertFromUtf32(intEmoji);
            var emoji2 = char.ConvertFromUtf32(intEmoji2);

            var emoji3 = char.ConvertFromUtf32(intEmojiSequence) +
                         char.ConvertFromUtf32(intEmojiSequence2) +
                         char.ConvertFromUtf32(intEmojiSequence3) +
                         char.ConvertFromUtf32(intEmojiSequence4);

            var emoji4 = char.ConvertFromUtf32(intEmojiMan);

            var thingo = char.ConvertToUtf32(emoji, 0);

            var ba = Encoding.BigEndianUnicode.GetBytes(emoji);
            var hex = BitConverter.ToString(ba);

            var ba2 = Encoding.BigEndianUnicode.GetBytes(emoji3);
            var hex2 = BitConverter.ToString(ba2);

            // Man converts back to hex as 0xD83DDC68.
            var ba3 = Encoding.BigEndianUnicode.GetBytes(emoji4);
            var hex3 = BitConverter.ToString(ba3);

            var thing = BitConverter.ToString(byteArrayEmojiMan).Replace("-", string.Empty);

            var result = Int32.TryParse(emoji, out int theResult);

            foreach (var character in fileText[40])
            {
                charArray.Add(character);
                Console.Write(character);
            }

            Console.WriteLine();
            Console.WriteLine("File Loaded");
            Console.ReadLine();
        }
    }
}
 