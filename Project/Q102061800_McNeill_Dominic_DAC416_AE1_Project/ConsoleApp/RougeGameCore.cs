using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeGame
{
    public class RougeGameCore
    {
        public static int PlayerInput()
        {
            RougeGameUtil.DisplayText("[1] Attack\n[2] Special Attack\n[3] Recharge\n[4] Dodge\n[5] Heal\n\nAction:");
            string? input = Console.ReadLine();

            int value;

            RougeGameUtil.ValidateInput(input, out value);

            return -1;
        }
    }

    

    
}
