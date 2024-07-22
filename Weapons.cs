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
        Player player = new Player();

        public int HealingStaff(int userH)
        {
            int health = userH;

            int healChance = random.Next(0, 5); // 1/5 chance to heal
            if (healChance == 0) 
            {
                health += 5;
                Console.WriteLine($"Your Healing Staff healed you for 5 health. Your health is now: {health}.");
            }

            return health;
        }


    }
}
