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
        static Weapons weapons = new Weapons();
        public static int userH = player.health;

        //Combat Encounter
        public static void Combat(string name, int power, int health)
        {
            //Enemy stats
            string n = name;
            int p = power;
            int h = health;
            //Player attacks
            List<string> attacks;
            List<string> attackDetails;

            int potionUse = 0;

            Console.Write($"All of the sudden a ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{n}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($" appears with {h} health. You have no choice but to fight.");

            while (h > 0 && userH > 0) //While the enemy and user are alive
            {
                Console.WriteLine("");
                Console.WriteLine("|===================================|");
                Console.WriteLine("       1. Attack  2. Potion ");
                Console.WriteLine("|===================================|");

                int input = Convert.ToInt32(Console.ReadLine());


                if (input == 2 && potionUse == 0) //Use Potion
                {
                    UsePotion();
                    potionUse++;
                    continue;
                }
                else Console.WriteLine("You can only use one potion per turn!");

                switch (Program.currentPlayer.Class) //Checks player class and adds attacks to list
                {
                    case "Warrior":
                        attacks = Program.currentPlayer.WarriorAttackList();
                        attackDetails = Program.currentPlayer.WarriorAttackDetailsList();
                        break;

                    case "Mage":
                        attacks = Program.currentPlayer.MageAttackList();
                        attackDetails = Program.currentPlayer.MageAttackDetailsList();
                        break;

                    case "Rogue":
                        attacks = Program.currentPlayer.RogueAttackList();
                        attackDetails = Program.currentPlayer.RogueAttackDetailsList();
                        break;

                    default:
                        Console.WriteLine("Invalid player class.");
                        return;
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
                            h = ExecuteAttack(Program.currentPlayer.Class, 1, h, n);
                            validChoice = true;
                            break;

                        case 2:
                            h = ExecuteAttack(Program.currentPlayer.Class, 2, h, n);
                            validChoice = true;
                            break;

                        case 3:
                            h = ExecuteAttack(Program.currentPlayer.Class, 3, h, n);
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

                //Reset potion use counter
                potionUse = 0;

                //Bleed
                if (Program.currentPlayer.bleed == true && Program.currentPlayer.bleedTurn < 2 && h > 0) //If the enemy has bleed and has had it for 2 turns
                {
                    h -= 5;
                    if (h < 0) h = 0;
                    Console.WriteLine($"The {n} bleeds 5 health and now has {h} health.");
                    Program.currentPlayer.bleedTurn++;
                }

                else if (Program.currentPlayer.bleed == true && Program.currentPlayer.bleedTurn >= 2)
                {
                    Program.currentPlayer.bleed = false;
                    Program.currentPlayer.bleedTurn = 0;
                }


                //Enemy attack
                if (h > 0 && player.stun == false && player.invis == false) //If the enemy is alive and isn't stunned, attack
                {
                    userH = enemy.EnemyAttack(userH, p, n);
                }

                else if (h > 0 && player.stun == true) //If the enemy is stunned, skip attack
                {
                    Console.WriteLine("The enemy is stunned and cannot attack until next turn.");
                    player.stun = false;
                }

                else if (h > 0 && player.invis == true) //If the player is invis skip attack
                {
                    Console.WriteLine("The enemy doesn't know where you are.");
                }

                else //If the enemy is dead
                {
                    Console.WriteLine($"You killed the {n}!");

                    //Gold Drop
                    loot.GoldDrop(n);


                    //Potion Drop
                    loot.PotionDrop(n);

                    //Weapon Drop
                    int weaponDropChance = 0;
                    if (weaponDropChance == 0) loot.WeaponDrop();
                }

                //If you have a healing staff, heal
                if (Program.currentPlayer.weapon == "Healing Staff")
                {
                    userH = weapons.HealingStaff(userH);
                }

                //Reset
                attacks.Clear();
                attackDetails.Clear();
            }
        }

        //Execute Attack Method
        static string playerClass = Program.currentPlayer.Class;
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
                    if (attackChoice == 3) player.Shadows();
                    break;
            }

            return enemyHealth;
        }


        //Resets the players stats at the start of a new game
        public static void ResetPlayer()
        {
            userH = player.health;
            Program.currentPlayer.gold = 0;
            Program.currentPlayer.potions = 3;
            Program.currentPlayer.Class = "";
            Program.currentPlayer.weapon = "";
        }

        //Reset status effects
        public static void ResetStatus()
        {
            Program.currentPlayer.bleed = false;
            Program.currentPlayer.bleedTurn = 0;
            Program.currentPlayer.stun = false;
            Program.currentPlayer.stunTurn = 0;
            Program.currentPlayer.invis = false;
        }



        //Uses a potion
        public static void UsePotion()
        {
            Console.WriteLine("Do you want to use a potion? (y/n):");
            string usePot = Console.ReadLine().ToLower();
            if (usePot == "y" && Program.currentPlayer.potions > 0)
            {
                //heal
                int potionStrength = random.Next(5, Program.currentPlayer.potionStrength);
                userH += potionStrength;
                Program.currentPlayer.potions--;
                Console.WriteLine($"You used 1 potion to heal {potionStrength} health. You now have {userH} health and {Program.currentPlayer.potions} potions left.");
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
