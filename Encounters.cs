using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jason
{
    public class Encounters
    {
        static Random random = new Random();
        public static int userH = Program.currentPlayer.health;

        //Combat Encounter
        public static void Combat(string name, int power, int health)
        {
            string n = name;
            int p = power;
            int h = health;

            if (userH > 0)
            {
                Console.WriteLine($"All of the sudden a {n} appears with {h} health. You have no choice but to fight.");

                while (h > 0 && userH > 0)
                {
                    Console.WriteLine("Would you like to 1. Attack or 2. Potion");
                    string input = Console.ReadLine();
                    try
                    {
                        if (Convert.ToInt32(input) == 1)
                        {
                            Console.WriteLine("Would you like to use 1. Light Attack (5-15 dmg) or 2. Heavy Attack (0-20 dmg)");
                            string attackChoice = Console.ReadLine();
                            if (Convert.ToInt32(attackChoice) == 1)
                            {
                                h = useLightAttack(h, n);
                            }
                            else if (Convert.ToInt32(attackChoice) == 2)
                            {
                                h = useHeavyAttack(h, n);
                            }
                            else
                            {
                                Console.WriteLine("That is not a valid attack.");
                            }

                            //Enemy Attack
                            if (h > 0)
                            {
                                int enemyAttack = random.Next(0, p + 1);
                                userH -= enemyAttack;
                                if (userH < 0)
                                {
                                    userH = 0;
                                }
                                Console.WriteLine($"The {n} attacks you back for {enemyAttack} damage leaving you with {userH} health.");
                            }
                        }
                        //Use Potion
                        else if (Convert.ToInt32(input) == 2)
                        {
                            UsePotion();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid input.");
                    }
                }

                //if you kill the mob
                if (h <= 0 && userH > 0)
                {
                    Console.WriteLine($"You killed the {n}!");

                    //Gold drop
                    int goldDropped = Loot.goldDrop();
                    Program.currentPlayer.gold += goldDropped; //Updates the players gold with the amount gained

                    //Potion drop
                    int potionDropped = Loot.potionDrop();
                    Program.currentPlayer.potions += potionDropped; //Updates the players gold with the amount gained
                    Console.WriteLine($"The {n} dropped {goldDropped} gold and {potionDropped} potions. You have a total of {Program.currentPlayer.gold} gold and {Program.currentPlayer.potions} potions.");

                    Console.WriteLine("Press any key to continue your adventure.");
                    Console.ReadKey();
                    Console.Clear();
                }
                //if the mob kills you
                else if (h > 0 && userH <= 0)
                {
                    Console.WriteLine($"The {n} killed you. Your adventure is over.");
                }
            }
        }

        //Resets the players stats at the start of a new game
        public static void ResetPlayer()
        {
            userH = Program.currentPlayer.health;
            Program.currentPlayer.gold = 0;
        }


        //Uses a potion
        public static void UsePotion()
        {
            Console.WriteLine("Do you want to use a potion? (y/n):");
            string usePot = Console.ReadLine().ToLower();
            while (usePot == "y")
            {
                if (Program.currentPlayer.potions > 0)
                {
                    //heal
                    int potionStrength = random.Next(1, Program.currentPlayer.potionStrength);
                    userH += potionStrength;
                    Program.currentPlayer.potions--;
                    Console.WriteLine($"You used 1 potion to heal {potionStrength} health. You now have {userH} health and {Program.currentPlayer.potions} potions left.");
                    usePot = "n";
                }
                else
                {
                    Console.WriteLine("You are out of potions.");
                    usePot = "n";
                }
            }
        }


        //Uses a light attack
        public static int useLightAttack(int h, string n) //5-15 dmg
        {
            int attack = random.Next(Program.currentPlayer.damage - 15, Program.currentPlayer.damage - 4);
            h -= attack;
            if (h < 0)
            {
                h = 0;
            }
            Console.WriteLine($"You attack the {n} for {attack} damage leaving the {n} with {h} health.");

            return h;
        }


        //Uses a heavy attack
        public static int useHeavyAttack(int h, string n) //0-20 dmg
        {
            int attack = random.Next(0, Program.currentPlayer.damage + 1);
            h -= attack;
            if (h < 0)
            {
                h = 0;
            }
            Console.WriteLine($"You attack the {n} for {attack} damage leaving the {n} with {h} health.");

            return h;
        }
    }
}
