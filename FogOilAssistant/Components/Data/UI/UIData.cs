using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogOilAssistant.Components.Data.UI
{
    public static class UIData
    {
        public enum MessageColor {
            WARMING,
            SUCCESS,
            ERROR,
            DEFAULT
        }

        public static string GetColor(MessageColor ID)
        {
            switch((int)ID)
            {

                case 0:
                    return "#FCD003";
                case 1:
                    return "#45FC03";
                case 2:
                    return "#FF0000";
                case 3:
                    return "#FFFFFF";
                default:
                    return "#FFFFFF";
            }    
        }

    }
}
