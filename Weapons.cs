using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jason
{
    public class Weapons
    {
        Random random = new Random();

        public int HealingStaff(Player player)
        {
            int health = 0;

            int healChance = random.Next(0, 3); // 1 in 3 chance to heal
            if (healChance == 0) 
            {
                health = 5;
                Console.WriteLine($"Your Healing Staff healed you for 5 health! Your health is now: {health}.");
            }

            return health;
        }
    }
}
