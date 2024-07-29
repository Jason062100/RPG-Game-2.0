using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jason;

namespace Jason
{
    public class Enemies
    {
        static Random random = new Random();
        static Loot loot = new Loot();

        //Status effects
        //Property to manage bleed status
        public bool EnemyBleed { get; set; }
        public int enemyBleedTurn = 0;

        public bool EnemyStun { get; set; }

        //Method to inflict enemy bleed
        public void InflictBleed()
        {
            EnemyBleed = true;
        }

        public void InflictStun()
        {
            EnemyStun = true;
        }

        //Enemy attack rewrite
        public void EnemyAttack(Player player, string n, int p, int h)
        {
            //If the enemy is alive and isn't stunned and the player isn't invis
            if (h > 0 && EnemyStun == false && player.Invis == false)
            {
                int enemyAttack = random.Next(0, p + 1);
                int armor;
                
                //Checks class, uses armor for that class to negate damage
                switch (player.Class) 
                {
                    case "Warrior":
                        armor = random.Next(5, player.warriorArmor + 1);
                        enemyAttack -= armor;
                        Console.WriteLine($"{armor} damage from the enemy is negated by your armor.");
                        break;

                    case "Mage":
                        armor = random.Next(5, player.mageArmor + 1);
                        enemyAttack -= armor;
                        Console.WriteLine($"{armor} damage from the enemy is negated by your armor.");
                        break;

                    case "Rogue":
                        armor = random.Next(5, player.rogueArmor + 1);
                        enemyAttack -= armor;
                        Console.WriteLine($"{armor} damage from the enemy is negated by your armor.");
                        break;
                }
                if (enemyAttack < 0) enemyAttack = 0;
                player.TakeDamage(enemyAttack);
            }

            //If the enemy is alive and stunned
            else if (h > 0 && EnemyStun == true)
            {
                Console.WriteLine("The enemy is stunned and cannot attack until next turn.");
                EnemyStun = false;
            }

            //If the player is invis, skip attack
            else if (h > 0 && player.Invis == true)
            {
                Console.WriteLine("The enemy doesn't know where you are.");
            }

            //If the enemy is dead
            else if (h <= 0)
            {
                Console.WriteLine($"You killed the {n}!");

                //Gold Drop
                loot.GoldDrop(player, n);

                //Potion Drop
                loot.PotionDrop(player, n);

                //Weapon Drop
                loot.WeaponDrop(player);

                //Xp Drop
                loot.XpDrop(player, n);

                //Reset status effects
                EnemyBleed = false;
                enemyBleedTurn = 0;
                EnemyStun = false;
                player.shadowsUsage = 0;
            }
        }
    }
}
