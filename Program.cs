using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Jason;

namespace Jason
{
    public class Program
    {
        public static Player currentPlayer = new Player();

        public static string playAgain = "y";

        public static void Main(string[] args)
        {
            while (playAgain == "y")
            {
                Weapons.sayShoud();
                //Starts game and resets player stats
                Start();
                Encounters.ResetPlayer();


                //Encounters
                Encounters.Combat("Zombie", 10, 30);

                Encounters.Combat("Goblin", 10, 30);

                Encounters.Combat("Skeleton Knight", 20, 50);

                //Win
                if (currentPlayer.health > 0)
                {
                    Console.WriteLine("You win. Congrats!");
                }

                //Play again
                Console.WriteLine("Would you like to play again? (y/n):");
                playAgain = Console.ReadLine().ToLower();
            }
        }

        static void Start()
        {
            Console.Clear();
            Console.WriteLine("Welcome to RPG Game. Which class would you like to play as? ");
            Console.WriteLine("1. Knight");

            int x = 1;
            int input = 0;
            while (x == 1 && input == 0)
            {
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid input.");
                }
            }

            Console.Clear();
            Console.WriteLine("You are trying to find your way out of this forest for a while now...");
        }
    }
}
