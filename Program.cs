using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jason;

namespace Jason
{
    public class Program
    {
        public static Player currentPlayer = new Player();
        public static Shop shop = new Shop();

        public static string playAgain = "y";

        public static void Main(string[] args)
        {
            while (playAgain == "y")
            {
                shop.UseShop();

                //Starts game and resets player stats
                Console.Title = "RPG Game";
                Encounters.ResetPlayer();
                Start();


                //Encounters

                //Encounter 1
                if (Encounters.userH > 0) Encounters.Combat("Zombie", 15, 30); //(Name, Power, Health)
                Encounters.ResetStatus();

                if (Encounters.userH > 0)
                {
                    Console.WriteLine("You stumble upon a hatch. Maybe this could be the way out...");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                }

                //Encounter 2
                if (Encounters.userH > 0) Encounters.Combat("Goblin", 20, 40);
                Encounters.ResetStatus();

                if (Encounters.userH > 0)
                {
                    Console.WriteLine("There is a lever on the wall. You decide to pull it and see a huge door that you thought was just a wall open.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                }

                //Encounter 3
                if (Encounters.userH > 0) Encounters.Combat("Skeleton Knight", 25, 60);
                Encounters.ResetStatus();



                //Win
                if (Encounters.userH > 0) Console.WriteLine("You win. Congrats!");


                //Lose
                if (Encounters.userH <= 0) Console.WriteLine($"You have been slain. Your adventure is over. GG.");

                //Play again
                Console.WriteLine("Would you like to play again? (y/n):");
                playAgain = Console.ReadLine().ToLower();
            }
        }

        static void Start()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Welcome to RPG Game. Which class would you like to play as? ");
            Console.WriteLine("1. Warrior  2. Mage  3. Rogue");

            int input = Convert.ToInt32(Console.ReadLine());
            bool x = true;

            //Choose Class
            while (x)
            {
                switch (input)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("You have chosen a Warrior.");
                        currentPlayer.Class = "Warrior";
                        x = false;
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("You have chosen a Mage.");
                        currentPlayer.Class = "Mage";
                        x = false;
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("You have chosen a Rogue.");
                        currentPlayer.Class = "Rogue";
                        x = false;
                        break;

                    default:
                        Console.WriteLine("Invalid input. Try again:");
                        break;
                }
            }

            Console.WriteLine("You have been trying to find your way out of this dungeon for a while now...");
        }
    }
}
