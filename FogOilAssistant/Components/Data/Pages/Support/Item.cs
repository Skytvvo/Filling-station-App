using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogOilAssistant.Components.Data.Pages.Support
{
    public class SupportItem
    {
        string text;
        string name;
        string color;
        string link;

        public string Text { get => text; set => text = value; }
        public string Name { get => name; set => name = value; }
        public string Color { get => color; set => color = value; }
        public string Link { get => link; set => link = value; }
    }
}
