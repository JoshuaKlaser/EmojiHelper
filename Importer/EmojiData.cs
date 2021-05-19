using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Importer
{
    public class EmojiData
    {
        public string Emoji { private set; get; }
        public string Name { private set; get; }

        public EmojiData(string name, string emoji)
        {
            Emoji = emoji;
            Name = name;
        }
    }
}
