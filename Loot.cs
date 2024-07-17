using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jason;

namespace Jason
{
    public class Loot
    {
        static Random random = new Random();

        //Gold drop chance
        public static int goldDrop()
        {
            int gold = 0;
            int chance = random.Next(0, 2); //1 in 2 chance to get a potion
            if (chance == 0)
            {
                gold += random.Next(0, 10);
            }

            return gold;
        }

        //Potion drop chance
        public static int potionDrop()
        {
            int potions = 0;
            int chance = random.Next(0, 6); //1 in 5 chance to get a potion
            if (chance == 0)
            {
                potions++;
            }

            return potions;
        }
    }
}
