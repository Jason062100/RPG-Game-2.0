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
        public static Shop shop = new Shop();

        public static string playAgain = "y";

        public static void Main(string[] args)
        {
            while (playAgain == "y")
            {
                Player player = new Player(100, 1, 0, 100); //100 health, level 1, starting xp, max health
                

                //Starts game
                Console.Title = "RPG Game";
                Start(player);


                //Dungeon 1
                //Encounter 1
                Encounters.Combat(player, "Zombie", 15, 30); //(Name, Power, Health)

                if (player.Health > 0)
                {
                    Console.WriteLine("You stumble upon a hatch. Maybe this could be the way out...");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                }

                //Encounter 2
                if (player.Health > 0) Encounters.Combat(player, "Goblin", 20, 40);

                if (player.Health > 0)
                {
                    Console.WriteLine("There is a lever on the wall. You decide to pull it and see a huge door that you thought was just a wall open.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                }

                //Encounter 3
                if (player.Health > 0) Encounters.Combat(player, "Skeleton Knight", 30, 60);

                if (player.Health > 0)
                {
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("After defeating the boss, you find the way out of the dungeon!");
                    Console.WriteLine("Finally you see daylight again. Your eyes take a while to adjust.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                }

                //Exit dungeon
                player.BeatDungeon();
                shop.UseShop(player);
                //Encounters.ResetForDungeon();

                //Dungeon 2   Troll, Golem, Griffin
                Encounters.Combat(player, "Troll", 25, 60);          //TBD


                //Win
                if (player.Health > 0) Console.WriteLine("You win. Congrats!");


                //Lose
                if (player.Health <= 0) Console.WriteLine($"You have been slain. Your adventure is over. GG.");

                //Play again
                Console.WriteLine("Would you like to play again? (y/n):");
                playAgain = Console.ReadLine().ToLower();
            }
        }

        static void Start(Player player)
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
                        player.ChooseClass("Warrior");
                        x = false;
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("You have chosen a Mage.");
                        player.ChooseClass("Mage");
                        x = false;
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("You have chosen a Rogue.");
                        player.ChooseClass("Rogue");
                        x = false;
                        break;

                    default:
                        Console.WriteLine("Invalid input. Try again:");
                        input = Convert.ToInt32(Console.ReadLine());
                        break;
                }
            }

            Console.WriteLine("You have been trying to find your way out of this dungeon for a while now...");
        }
    }
}
