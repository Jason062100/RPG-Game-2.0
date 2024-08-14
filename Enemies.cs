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
        Loot loot = new Loot();

        //Status effects
        //Property to manage bleed status
        public bool enemyBleed { get; set; }
        public int enemyBleedTurn = 0;
        public bool enemyStun { get; set; }
        public bool poison { get; set; }

        //Method to inflict enemy bleed
        public void InflictBleed()
        {
            enemyBleed = true;
        }

        public void InflictStun()
        {
            enemyStun = true;
        }

        public void InflictPoison()
        {
            poison = true;
        }

        //Enemy attack rewrite
        public void EnemyAttack(Player player, string n, int p, int h)
        {
            //If the enemy is alive, isn't stunned, and the player isn't invis
            if (h > 0 && enemyStun == false && player.Invis == false && player.ShadowStep == false)
            {
                int enemyAttack = random.Next(0, p + 1);
                int armor;

                //Checks class, uses armor for that class to negate damage
                switch (player.Class)
                {
                    case "Warrior":
                        armor = random.Next(5, player.warriorArmor + 1); //5-10
                        enemyAttack -= armor;
                        Console.WriteLine($"{armor} damage from the enemy is negated by your armor.");
                        break;

                    case "Mage":
                        armor = random.Next(0, player.mageArmor + 1); //0-5
                        enemyAttack -= armor;
                        Console.WriteLine($"{armor} damage from the enemy is negated by your armor.");
                        break;

                    case "Rogue":
                        armor = random.Next(0, player.rogueArmor + 1); //0-5
                        enemyAttack -= armor;
                        Console.WriteLine($"{armor} damage from the enemy is negated by your armor.");
                        break;
                }
                if (enemyAttack < 0) enemyAttack = 0;
                player.TakeDamage(enemyAttack);
            }

            if (h > 0 && enemyStun == false) //If the enemy is alive and isn't stunned, try to inflict status
            {
                player.InflictStatus();
            }

            //If the enemy is alive and stunned
            else if (h > 0 && enemyStun == true)
            {
                Console.WriteLine("The enemy is stunned and cannot attack until next turn.");
                enemyStun = false;
            }

            //If the player is invis, skip attack
            else if (h > 0 && player.Invis == true)
            {
                Console.WriteLine("The enemy doesn't know where you are.");
            }

            //If the player shadow stepped, skip attack
            if (h > 0 && player.ShadowStep == true)
            {
                Console.WriteLine("You shadow stepped and avoided the enemies attack!");
                player.ShadowStep = false;
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

                //Item Drop
                loot.ItemDrop(player);

                //Xp Drop
                loot.XpDrop(player, n);

                //Reset status effects
                enemyBleed = false;
                enemyBleedTurn = 0;
                enemyStun = false;
                poison = false;
                player.shadowsUsage = 0;
                player.ShadowStep = false;
                player.Invis = false;
                player.shadowsTurn = 0;

                if (player.increaseDamage == true) player.IncreaseDamage(true);
            }
        }
    }
}
