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

        public void PoisonDagger()
        {
            int poisonChance = random.Next(0, 3); // 1 in 3 chance to poison
            
            if (poisonChance == 0 && Player.enemy.poison == false) //If the enemy isn't already poisoned
            {
                Console.WriteLine("You inflicted poison on the enemy!");
                Player.enemy.InflictPoison();
            }
        }

        public void FlameAxe(Player player)
        {
            int flameChance = random.Next(0, 3); // 1 in 3 chance

            if (flameChance == 0 && player.increaseDamage == false && player.stun == false) player.IncreaseDamage(false);

            else if (player.increaseDamage == true) player.IncreaseDamage(true);
        }
    }
}
