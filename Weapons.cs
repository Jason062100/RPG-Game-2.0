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
                health = random.Next(2, 9); //Heal 2-8 health
                Console.Write($"Your Healing Staff healed you for {health} health! ");
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

            if (flameChance == 0 && player.increasedDamage == false && player.Stun == false) player.IncreaseDamage(false);

            else if (player.increasedDamage == true) player.IncreaseDamage(true);
        }

        public void UpgradeWeapon(Player player)
        {
            Console.WriteLine("You come across a blacksmith.");

            if (player.Weapon != null) Console.WriteLine($"\"Ah, I see you have a {player.Weapon}.\"");

            Console.Write("\"How about I upgrade your weapon? It'll cost 5 gold and that dragon scale you have.\" (1) Yes (2) No: ");
            int input2 = Convert.ToInt32(Console.ReadLine());

            if (input2 == 2) Console.WriteLine("Come again soon.");
            else if (input2 == 1 && player.gold < 5) Console.WriteLine("Sorry, you don't have enough gold. Come again soon.");
            else if (input2 == 1 && player.gold >= 5)
            {
                player.gold -= 5;
                Console.WriteLine("\"Alright, one dragon infused weapon coming up.\"");
                Console.WriteLine("\"All done, here you go. It looks like your weapon gained a new attack.\"");
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
