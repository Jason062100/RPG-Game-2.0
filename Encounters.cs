using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Jason
{
    public class Encounters
    {
        static Random random = new Random();
        static Player player = new Player();
        static Enemies enemy = new Enemies();
        static Loot loot = new Loot();
        public static int userH = player.health;
        static string playerClass = Program.currentPlayer.Class;

        //Combat Encounter
        public static void Combat(string name, int power, int health)
        {
            string n = name;
            int p = power;
            int h = health;
            List<string> attacks;

            Console.WriteLine($"All of the sudden a {n} appears with {h} health. You have no choice but to fight.");


            while (h > 0 && userH > 0) //While the enemy and user are alive
            {
                Console.WriteLine("|===================================|");
                Console.WriteLine("       1. Attack  2. Potion ");
                Console.WriteLine("|===================================|");

                int input = Convert.ToInt32(Console.ReadLine());


                if (input == 2) //Use Potion
                {
                    UsePotion();
                    continue;
                }

                switch (playerClass) //Checks player class and adds attacks to list
                {
                    case "Warrior":
                        attacks = player.WarriorAttackList();
                        break;

                    case "Mage":
                        attacks = player.MageAttackList();
                        break;

                    case "Rogue":
                        attacks = player.RogueAttackList();
                        break;

                    default:
                        Console.WriteLine("Invalid player class.");
                        return;
                }

                Console.WriteLine("Which attack would you like to use?");
                Console.WriteLine("|===================================|");
                Console.WriteLine($"     1. {attacks[0]} 2. {attacks[1]}");
                Console.WriteLine("|===================================|");

                int attackChoice = Convert.ToInt32(Console.ReadLine());
                bool validChoice = false;

                while (!validChoice)
                {
                    switch (attackChoice)
                    {
                        case 1:
                            h = ExecuteAttack(playerClass, 1, h, n);
                            validChoice = true;
                            break;

                        case 2:
                            h = ExecuteAttack(playerClass, 2, h, n);
                            validChoice = true;
                            break;

                        default:
                            Console.WriteLine("Invalid input. Please type a valid attack:");
                            attackChoice = Convert.ToInt32(Console.ReadLine());
                            break;
                    }
                }

                if (h > 0) //If the enemy is alive, attack
                {
                    userH = enemy.EnemyAttack(userH, p, n);
                }
                else //If the enemy is dead
                {
                    //Gold Drop
                    loot.GoldDrop(n);


                    //Potion Drop
                    loot.PotionDrop(n);

                    Console.WriteLine($"You killed the {n}!");

                }
            }
        }

        private static int ExecuteAttack(string playerClass, int attackChoice, int enemyHealth, string enemyName)
        {
            switch (playerClass)
            {
                case "Warrior":
                    if (attackChoice == 1) return player.Stab(enemyHealth, enemyName);
                    if (attackChoice == 2) return player.OverheadSwing(enemyHealth, enemyName);
                    break;

                case "Mage":
                    if (attackChoice == 1) return player.Fireball(enemyHealth, enemyName);
                    if (attackChoice == 2) return player.MagicMissle(enemyHealth, enemyName);
                    break;

                case "Rogue":
                    if (attackChoice == 1) return player.FuryStrike(enemyHealth, enemyName);
                    if (attackChoice == 2) return player.Backstab(enemyHealth, enemyName);
                    break;
            }

            return enemyHealth;
        }


        //Resets the players stats at the start of a new game
        public static void ResetPlayer()
        {
            userH = player.health;
            Player.gold = 0;
            Player.potions = 3;
        }



        //Uses a potion
        public static void UsePotion()
        {
            Console.WriteLine("Do you want to use a potion? (y/n):");
            string usePot = Console.ReadLine().ToLower();
            if (usePot == "y" && Player.potions > 0)
            {
                //heal
                int potionStrength = random.Next(5, player.potionStrength);
                userH += potionStrength;
                Player.potions--;
                Console.WriteLine($"You used 1 potion to heal {potionStrength} health. You now have {userH} health and {Player.potions} potions left.");
            }
            else
            {
                Console.WriteLine("You are out of potions.");
            }
        }
    }
}

