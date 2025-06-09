using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GirisimciBulmaPlatform.Shared
{
public class MenuItem
    {
        public string Title { get; set; }
        public string Link { get; set; }

        public string IconClass { get; set; }
        public List<MenuItem> SubItems { get; set; } = new List<MenuItem>();

        public bool HasSubItems => SubItems.Any();
    }
}
