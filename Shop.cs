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
        public void UseShop()
        {
            Dictionary<string, int> store = new Dictionary<string, int>();
            store.Add("POTION", 10);
            store.Add("EXAMPLE", 0);

            Console.Write("You see a wandering trader. Would you like to look at his wares? (y/n): ");
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

                Console.Write("Would you like to purchase something? (y/n): ");
                string input2 = Console.ReadLine().ToLower();

                while (input2 == "y")
                {
                    Console.Write("Which item would you like to buy? ");
                    string input3 = Console.ReadLine().ToUpper();
                    if (store.TryGetValue(input3, out int value)) {}
                    else Console.WriteLine("No such item.");

                    if (store.ContainsKey(input3) && Program.currentPlayer.gold >= value)
                    {
                        switch (input3)
                        {
                            case "POTION":
                                Program.currentPlayer.potions += 1;
                                Console.WriteLine($"You bought 1 potion. You now have {Program.currentPlayer.potions} potions.");
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

                    else Console.WriteLine("Not enough gold.");
                }

                Console.WriteLine("Exiting Shop.");

                Console.ReadKey();
            }
        }
    }
}
