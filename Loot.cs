using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Jason;

namespace Jason
{
    public class Loot
    {
        static Random random = new Random();
        static Player player = new Player();

        //Gold drop chance
        public void GoldDrop(string n)
        {
            int gold = 0;
            int chance = random.Next(0, 1); //1 in 2 chance to get gold
            if (chance == 0)
            {
                gold += random.Next(1, 10);
            }

            if (gold != 0)
            {
                Program.currentPlayer.gold += gold;
                Console.WriteLine($"The {n} dropped {gold} gold. You now have {Program.currentPlayer.gold} gold.");
            }
        }

        //Potion drop chance
        public void PotionDrop(string n)
        {
            int potions = 0;
            int chance = random.Next(0, 4); //1 in 4 chance to get a potion
            if (chance == 0)
            {
                potions++;
            }

            if (potions != 0)
            {
                Program.currentPlayer.potions += potions;
                Console.WriteLine($"The {n} dropped {potions} potion. You now have {Program.currentPlayer.potions} potions.");
            }
        }
    }
}
