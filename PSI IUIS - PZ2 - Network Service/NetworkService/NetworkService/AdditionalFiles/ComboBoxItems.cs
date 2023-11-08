using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.AdditionalFiles
{
    public class ComboBoxItems
    {
        public static Dictionary<string, string> comboBoxTypes = new Dictionary<string, string>()
        {
            { "iA", "pack://application:,,,/NetworkService;component/Icons/roadTypeA.png" },
            { "iB", "pack://application:,,,/NetworkService;component/Icons/roadTypeB.png" }
        };
    }
}
