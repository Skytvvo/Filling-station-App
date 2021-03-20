using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FogOilAssistant.Components.Data.MenuButton;

namespace FogOilAssistant.Components.Models.ToggleMenu
{
    public class ViewModelToggleMenu
    {
        public List<ButtonMenu> MenuButtons
        {
            get
            {
                return new List<ButtonMenu>
                {
                    new ButtonMenu{ Path="/Components/Images/support.svg", Text="Support"},
                    new ButtonMenu{ Path="/Components/Images/sHOP.svg", Text="Shop"},
                    new ButtonMenu{ Path="/Components/Images/oil-bottle.svg", Text="Oil"},
                    new ButtonMenu{ Path="/Components/Images/location.svg", Text="Refueling map"},
                    new ButtonMenu{ Path="/Components/Images/login.svg", Text="Login"}
                };
            }
        }
    }
}
