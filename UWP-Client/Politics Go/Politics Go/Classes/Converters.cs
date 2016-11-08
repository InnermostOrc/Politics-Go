using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Politics_Go.Classes;

namespace Politics_Go.Converters
{
    public class Converters
    {
        public string CreatureIDToName(int ID)
        {
            switch (ID.ToString().ToLower())
            {
                case "001":
                    return "Donald Trump";
                case "002":
                    return "Minister Trump";
                case "003":
                    return "Lord Trump";
                default:
                    return "Unknown Creature";
            }
        }
    }
}
