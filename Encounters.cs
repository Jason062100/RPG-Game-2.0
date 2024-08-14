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

                while (!validChoice && player.stun == false) //While you make a valid choice and you aren't stunned, attack
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
                                Console.WriteLine("");
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

                //Player stun
                if (player.stun == true) //If the player is stunned
                {
                    Console.WriteLine("You are stunned and can't attack!");
                    player.stun = false;
                }

                
                //Enemy bleed
                if (Player.enemy.enemyBleed == true && Player.enemy.enemyBleedTurn < 2 && h > 0) //If the enemy has bleed and it's less than 2 turns
                {
                    h -= 5;
                    if (h < 0) h = 0;
                    Console.WriteLine($"The {n} bleeds 5 health. Enemy health: {h}");
                    Player.enemy.enemyBleedTurn++;
                }
                else if (Player.enemy.enemyBleed == true && Player.enemy.enemyBleedTurn >= 2) //If the enemy has bleed and it has been 2 turns
                {
                    Player.enemy.enemyBleed = false;
                    Player.enemy.enemyBleedTurn = 0;
                    Console.WriteLine("The enemy stopped bleeding.");
                }

                //Enemy poison
                if (Player.enemy.poison == true && h > 0) //If the enemy is poisoned and alive
                {
                    h -= 3;
                    if (h < 0) h = 0;
                    Console.WriteLine($"The {n} loses 3 health due to poison. Enemy health: {h}");
                }

                //Enemy attack
                Player.enemy.EnemyAttack(player, n, p, h);

                //Player bleed
                if (player.bleed == true && player.bleedTurn < 2 && player.Health > 0) //If you are bleeding and it's less than 2 turns
                {
                    player.TakeBleedDamage(5);
                    player.bleedTurn++;
                }

                else if (player.bleed == true && player.bleedTurn >= 2) //If you are bleeding and it has been 2 turns
                {
                    player.bleed = false;
                    player.bleedTurn = 0;
                    Console.WriteLine("You stopped bleeding.");
                }

                //Healing Staff
                if (player.Weapon == "Healing Staff" && player.Health > 0 && player.Class == "Mage") player.Heal(weapons.HealingStaff(player));

                //Poison Dagger
                if (player.Weapon == "Poison Dagger" && h > 0 && player.Health > 0 && player.Class == "Rogue" && player.Invis == false) weapons.PoisonDagger();

                //Flame Axe
                if (player.Weapon == "Flame Axe" && h > 0 && player.Health > 0 && player.Class == "Warrior") weapons.FlameAxe(player);
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
                Console.WriteLine($"You used 1 potion to heal {potionStrength} health. Player health: {player.Health} Player potions: {player.potions}.");
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
