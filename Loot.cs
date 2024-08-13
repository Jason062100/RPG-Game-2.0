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

                player.gold += gold * player.goldMultiplier;

                if (player.item == "Golden Ring") Console.WriteLine($"The {n} dropped {gold} gold. Your Golden Ring doubled it to {gold * player.goldMultiplier} gold! Player gold: {player.gold}");
                else Console.WriteLine($"The {n} dropped {gold} gold. Player gold: {player.gold}");
            }
        }

        //Potion drop chance
        public void PotionDrop(Player player, string n)
        {
            int potions = 0;
            int chance = random.Next(0, 8); //1 in 8 chance to get a potion
            if (chance == 0)
            {
                potions++;
            }

            if (potions != 0)
            {
                player.potions += potions;
                Console.WriteLine($"The {n} dropped {potions} potion. Player potions: {player.potions}");
            }
        }

        public void WeaponDrop(Player player)
        {
            int weaponDropChance;

            //Flame Axe
            if (player.Class == "Warrior" && player.Weapon != "Flame Axe")
            {
                weaponDropChance = random.Next(0, 5); //1 in 5 chance to get flame axe
                if (weaponDropChance == 0)
                {
                    player.Weapon = "Flame Axe";
                    Console.WriteLine("You found a Flame Axe! Stats: 1 in 3 chance to increase attack by 25%.");
                }
            }

            //Healing Staff
            if (player.Class == "Mage" && player.Weapon != "Healing Staff")
            {
                weaponDropChance = random.Next(0, 5); // 1 in 5 chance to get healing staff
                if (weaponDropChance == 0)
                {
                    player.Weapon = "Healing Staff";
                    Console.WriteLine("You found a Healing Staff! Stats: 1 in 5 chance to heal 5 health after each turn.");
                }
            }

            //Poison Dagger
            if (player.Class == "Rogue" && player.Weapon != "Poison Dagger")
            {
                weaponDropChance = random.Next(0, 5); //1 in 5 chance to get poison dagger
                if (weaponDropChance == 0)
                {
                    player.Weapon = "Poison Dagger";
                    Console.WriteLine("You found a Poison Dagger! Stats: 1 in 5 chance to poison the enemy. 3 damage every turn and it doesn't go away until   the enemy is dead.");
                }
            }
        }

        public void ItemDrop(Player player)
        {

            //Golden Ring Drop
            int goldenRingDropChance;
            if (player.item != "Golden Ring")
            {
                goldenRingDropChance = random.Next(0, 5); //1 in 5 drop chance

                if (goldenRingDropChance == 0)
                {
                    player.item = "Golden Ring";
                    player.goldMultiplier = 2;
                    Console.WriteLine($"You found a Golden Ring! Your gold multiplier is now {player.goldMultiplier}");
                }
            }
        }

        public void XpDrop(Player player, string n)
        {
            int xp = random.Next(5, 11); //5 to 10 xp

            player.Experience += xp;

            Console.WriteLine($"The {n} dropped {xp} experience. Player EXP: {player.Experience}");

            if (player.Experience >= 20) player.LevelUp();

        }
    }
}
