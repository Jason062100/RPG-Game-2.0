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

        public static string playAgain = "y";

        public static void Main(string[] args)
        {
            while (playAgain == "y")
            {
                //Starts game and resets player stats
                Start();
                Encounters.ResetPlayer();


                //Encounters

                if (Encounters.userH > 0) Encounters.Combat("Zombie", 10, 30); //(Name, Power, Health)

                if (Encounters.userH > 0) Encounters.Combat("Goblin", 15, 40);

                if (Encounters.userH > 0) Encounters.Combat("Skeleton Knight", 20, 50);



                //Win
                if (Encounters.userH > 0)
                {
                    Console.WriteLine("You win. Congrats!");
                }

                //Lose
                if (Encounters.userH <= 0)
                {
                    Console.WriteLine($"You have been slain. Your adventure is over. GG.");
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
            Console.WriteLine("1. Warrior  2. Mage  3. Rogue");

            int input = Convert.ToInt32(Console.ReadLine());
            bool x = true;

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

            Console.WriteLine("You are trying to find your way out of this forest for a while now...");
        }
    }
}
