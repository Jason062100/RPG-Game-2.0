using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Jason;

namespace Jason
{
    public class Shop
    {
        public void UseShop(Player player)
        {
            //Shop list
            Dictionary<string, int> store = new Dictionary<string, int>();
            store.Add("POTION", 10);
            store.Add("EXAMPLE", 0);

            Console.Clear();
            Console.WriteLine("You see a wandering trader on a donkey. Would you like to look at his wares? (y/n): ");
            string input = Console.ReadLine().ToLower();

            if (input == "y")
            {
                Console.WriteLine("");
                Console.WriteLine("              SHOP");
                Console.WriteLine("|==============================|");

                foreach (KeyValuePair<string, int> kvp in store)
                {
                    Console.WriteLine($"Item: {kvp.Key} Cost: {kvp.Value} Gold");
                }
                Console.WriteLine("|==============================|");
                Console.WriteLine("");

                Console.WriteLine($"Player gold: {player.gold}");
                Console.Write("Would you like to purchase something? (y/n): ");
                string input2 = Console.ReadLine().ToLower();

                while (input2 == "y")
                {
                    Console.Write("Which item would you like to buy? Type e if you want to exit. ");
                    string input3 = Console.ReadLine().ToUpper();
                    if (store.TryGetValue(input3, out int value)) { }
                    else if (input3 == "E") input2 = "n";
                    else Console.WriteLine("No such item.");

                    if (store.ContainsKey(input3) && player.gold >= value)
                    {
                        switch (input3)
                        {
                            case "POTION":
                                player.potions += 1;
                                player.gold -= value;
                                Console.WriteLine($"You spent {value} gold to buy 1 potion. You now have {player.potions} potions and {player.gold} gold.");
                                break;

                            case "EXAMPLE":
                                Console.WriteLine("EXAMPLE Bought!");
                                break;

                            default:
                                break;
                        }

                        Console.Write("Would you like to buy something else? ");
                        input2 = Console.ReadLine().ToLower();
                    }

                    else if (store.ContainsKey(input3) && player.gold < value) Console.WriteLine("Not enough gold.");
                }

                Console.WriteLine("\"Come again soon\", the trader says.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
