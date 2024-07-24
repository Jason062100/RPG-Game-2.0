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

        //Gold drop chance
        public void GoldDrop(Player player, string n)
        {
            int gold = 0;
            int chance = random.Next(0, 1); //1 in 2 chance to get gold
            if (chance == 0)
            {
                gold += random.Next(1, 10);
            }

            if (gold != 0)
            {
                player.gold += gold;
                Console.WriteLine($"The {n} dropped {gold} gold. You now have {player.gold} gold.");
            }
        }

        //Potion drop chance
        public void PotionDrop(Player player, string n)
        {
            int potions = 0;
            int chance = random.Next(0, 4); //1 in 4 chance to get a potion
            if (chance == 0)
            {
                potions++;
            }

            if (potions != 0)
            {
                player.potions += potions;
                Console.WriteLine($"The {n} dropped {potions} potion. You now have {player.potions} potions.");
            }
        }

        public void WeaponDrop(Player player)
        {
            if (player.Class == "Mage" && player.Weapon != "Healing Staff")
            {
                int weaponDropChance = random.Next(0, 5); // 1 in 5 chance to get healing staff
                if (weaponDropChance == 0)
                {
                    player.Weapon = "Healing Staff";
                    Console.WriteLine("You found a Healing Staff! Stats: 1 in 5 chance to heal 5 health after each turn.");
                }
            }
        }
    }
}
