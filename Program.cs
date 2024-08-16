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
        public static Random random = new Random();
        public static Weapons weapons = new Weapons();

        public static string playAgain = "y";

        public static void Main(string[] args)
        {
            while (playAgain == "y")
            {
                Player player = new Player(100, 1, 0, 100, 20); //100 health, level 1, starting xp, max health, damage


                //Starts game
                Console.Title = "RPG Game";
                Start(player);

                DungeonOne(player);

                ExitDungeon(player);

                DungeonTwo(player);

                ExitDungeon(player);

                weapons.UpgradeWeapon(player);

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
            List<string> classDetails = new List<string>();
            classDetails.Add("Warrior: Armor: Medium | Damage: Medium | Abilities: Bleed, stun");
            classDetails.Add("Mage: Armor: Low | Damage: High | Abilities: Massive damage with miss chance, multiple hits possible");
            classDetails.Add("Rogue: Armor: Low | Damage: Medium | Abilities: Critical hit chance, evasion, invisibility");

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Welcome to RPG Game. Which class would you like to play as? Type 0 for class details.");
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

                    case 0:
                        for (int i = 0; i < classDetails.Count; i++)
                        {
                            Console.WriteLine("");
                            Console.WriteLine(classDetails[i]);
                        }
                        Console.WriteLine("");
                        Console.WriteLine("Which class would you like to play as?");
                        input = Convert.ToInt32(Console.ReadLine());
                        break;

                    default:
                        Console.WriteLine("Invalid input. Try again:");
                        input = Convert.ToInt32(Console.ReadLine());
                        break;
                }
            }

            Console.WriteLine("You have been trying to find your way out of this dungeon for a while now...");
        }

        public static void DungeonOne(Player player)
        {
            //Encounter 1
            Encounters.Combat(player, ChooseMonster(), 15, 30); //(Name, Power, Health)
            if (player.Health > 0)
            {
                Console.WriteLine("You stumble upon a hatch. Maybe this could be the way out...");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
            }

            //Encounter 2
            if (player.Health > 0) Encounters.Combat(player, ChooseMonster(), 20, 40);
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
        }

        public static void DungeonTwo(Player player)
        {
            if (player.Health > 0) Encounters.Combat(player, ChooseMonster(), 30, 60);
            if (player.Health > 0)
            {
                Console.WriteLine("You weren't watching where you were going and fell into a pit! It's really dark.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
            }

            if (player.Health > 0) Encounters.Combat(player, ChooseMonster(), 40, 70);
            if (player.Health > 0)
            {
                Console.WriteLine("You see a massive door and decide to enter. Up ahead you can barely make out a figure. It's too big to be a monster, it must be something else.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
            }

            if (player.Health > 0) Encounters.Combat(player, "Dragon", 50, 80);
            if (player.Health > 0)
            {
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("You have slain the boss of this dungeon!");
                Console.WriteLine("You carve off some pieces of the dragon's scales.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
            }
        }


        //Monsters
        public static Dictionary<string, int> monsters = new Dictionary<string, int>
            {
                { "Troll", 0 },
                { "Golem", 0 },
                { "Goblin", 0},
                { "Zombie", 0 }
            };

        public static string ChooseMonster()
        {
            string[] chooseMonster = new string[] { "Troll", "Golem", "Goblin", "Zombie" };
            string chosenMonster = chooseMonster[random.Next(0, 4)];
            string monster = "Bird";

            while (monsters[chosenMonster] == 1)
            {
                chosenMonster = chooseMonster[random.Next(0, 4)];
            }

            if (monsters.ContainsKey(chosenMonster))
            {
                if (monsters[chosenMonster] == 0) //If the monster hasn't been chosen before
                {
                    monster = chosenMonster;
                    monsters[chosenMonster] = 1;
                }
            }

            return monster;
        }

        public static void ExitDungeon(Player player)
        {
            //Exit dungeon
            if (player.Health > 0)
            {
                player.BeatDungeon();
                shop.UseShop(player);
            }
        }
    }
}
