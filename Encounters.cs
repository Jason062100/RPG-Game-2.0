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
        static Enemies enemy = new Enemies();
        static Loot loot = new Loot();
        static Weapons weapons = new Weapons();
        static List<string> attacks = new List<string>();
        static List<string> attackDetails = new List<string>();

        //Combat Encounter
        public static void Combat(Player player, string name, int power, int health)
        {
            //Enemy stats
            string n = name;
            int p = power;
            int h = health;

            Console.Write($"All of the sudden a ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{n}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($" appears with {h} health. You have no choice but to fight.");

            //Checks player class and adds attacks to list
            switch (player.Class)
            {
                case "Warrior":
                    attacks.Clear();
                    attackDetails.Clear();
                    attacks = player.WarriorAttackList();
                    attackDetails = player.WarriorAttackDetailsList();
                    break;

                case "Mage":
                    attacks.Clear();
                    attackDetails.Clear();
                    attacks = player.MageAttackList();
                    attackDetails = player.MageAttackDetailsList();
                    break;

                case "Rogue":
                    attacks.Clear();
                    attackDetails.Clear();
                    attacks = player.RogueAttackList();
                    attackDetails = player.RogueAttackDetailsList();
                    break;

                default:
                    Console.WriteLine("Invalid player class.");
                    return;
            }

            //While the enemy and user are alive
            while (h > 0 && player.Health > 0)
            {
                Console.WriteLine("");
                Console.WriteLine("|===================================|");
                Console.WriteLine("       1. Attack  2. Potion ");
                Console.WriteLine("|===================================|");

                int input = Convert.ToInt32(Console.ReadLine());


                //Use Potion
                if (input == 2)
                {
                    UsePotion(player);
                }

                //Attack
                Console.WriteLine("");
                Console.WriteLine("Which attack would you like to use? Press 0 if you would like to see attack details.");
                Console.WriteLine("|=======================================|");

                for (int i = 1; i < attacks.Count + 1; i++)
                {
                    Console.WriteLine($"           {i}. {attacks[i - 1]}");
                }
                Console.WriteLine("|=======================================|");

                int attackChoice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("");
                bool validChoice = false;

                while (!validChoice)
                {
                    switch (attackChoice)
                    {
                        case 1:
                            h = ExecuteAttack(player, player.Class, 1, h, n);
                            validChoice = true;
                            break;

                        case 2:
                            h = ExecuteAttack(player, player.Class, 2, h, n);
                            validChoice = true;
                            break;

                        case 3:
                            h = ExecuteAttack(player, player.Class, 3, h, n);
                            validChoice = true;
                            break;

                        case 0: //List all attack details
                            for (int i = 0; i < attackDetails.Count; i++)
                            {
                                Console.WriteLine($"{attackDetails[i]}");
                            }
                            Console.WriteLine("Which attack would you like to use?");
                            attackChoice = Convert.ToInt32(Console.ReadLine());
                            break;

                        default:
                            Console.WriteLine("Invalid input. Please type a valid attack:");
                            attackChoice = Convert.ToInt32(Console.ReadLine());
                            break;
                    }
                }

                //Bleed
                if (player.Bleed == true && player.bleedTurn < 2 && h > 0) //If the enemy has bleed and it's less than 2 turns
                {
                    h -= 5;
                    if (h < 0) h = 0;
                    Console.WriteLine($"The {n} bleeds 5 health and now has {h} health.");
                    player.bleedTurn++;
                }

                else if (player.Bleed == true && player.bleedTurn >= 2) //If the enemy has bleed and it has been 2 turns
                {
                    player.Bleed = false;
                    player.bleedTurn = 0;
                }

                //Enemy attack rewrite
                enemy.EnemyAttack(player, n, p, h);


                //If you have a healing staff, heal
                if (player.Weapon == "Healing Staff") player.Heal(weapons.HealingStaff(player));
            }
        }

        //Execute Attack Method
        private static int ExecuteAttack(Player player, string playerClass, int attackChoice, int enemyHealth, string enemyName)
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
                    if (attackChoice == 3) player.Shadows();
                    break;
            }

            return enemyHealth;
        }

        //Uses a potion
        public static void UsePotion(Player player)
        {
            Console.WriteLine("Do you want to use a potion? (y/n):");
            string usePot = Console.ReadLine().ToLower();
            if (usePot == "y" && player.potions > 0)
            {
                //heal
                int potionStrength = random.Next(5, player.potionStrength);
                player.Heal(potionStrength);
                player.potions--;
                Console.WriteLine($"You used 1 potion to heal {potionStrength} health. You now have {player.Health} health and {player.potions} potions left.");
            }
            else if (usePot == "n")
            {
                Console.WriteLine("Didn't use a potion.");
            }
            else
            {
                Console.WriteLine("You are out of potions.");
            }
        }
    }
}
