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
            store.Add("POTION", 5);
            store.Add("FLAME AXE", 10);
            store.Add("HEALING STAFF", 10);
            store.Add("POISON DAGGER", 10);
            store.Add("GOLDEN RING", 10);

            List<string> itemDescriptions = new List<string>();
            itemDescriptions.Add("Potion: A potion flask that heals between 5 and 10 health.");
            itemDescriptions.Add("Flame Axe: A great fiery axe that has a 1 in 3 chance to create an aura that increases your damage by 25%.");
            itemDescriptions.Add("Healing Staff: A luminous staff for mages that has a 1 in 3 chance to heal you for 5 health.");
            itemDescriptions.Add("Poison Dagger: A dagger coated in poison for rogues that has a 1 in 3 chance to poison an enemy. Poison stays until     enemy is killed.");
            itemDescriptions.Add("Golden Ring: A ring shining in gold that attracts 2x as much gold to the player.");

            Console.Clear();
            Console.WriteLine("You see a wandering trader on a donkey. Would you like to look at his wares? (y/n): ");
            string input = Console.ReadLine().ToLower();

            if (input == "y")
            {
                Console.WriteLine("");
                Console.WriteLine("              SHOP");
                Console.WriteLine("|================================|");

                foreach (KeyValuePair<string, int> kvp in store)
                {
                    Console.WriteLine($"Item: {kvp.Key} Cost: {kvp.Value} Gold");
                }
                Console.WriteLine("|================================|");
                Console.WriteLine("");

                Console.WriteLine($"Player gold: {player.gold}");
                Console.WriteLine("");
                Console.Write("Would you like to (1) purchase something or (2) see item descriptions? ");
                int input2 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("");

                if (input2 == 2)
                {
                    for (int i = 0; i < itemDescriptions.Count; i++)
                    {
                        Console.WriteLine(itemDescriptions[i]);
                        Console.WriteLine("");
                    }
                    input2 = 1;
                }

                while (input2 == 1)
                {
                    Console.Write("Which item would you like to buy? Type e if you want to exit. ");
                    string input3 = Console.ReadLine().ToUpper();
                    if (store.TryGetValue(input3, out int value)) { }
                    else if (input3 == "E") input2 = 3;
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

                            case "FLAME AXE":
                                if (player.Class != "Warrior")
                                {
                                    Console.WriteLine("You have to be a Warrior to buy this!");
                                }

                                else if (player.Weapon == "Flame Axe")
                                {
                                    Console.WriteLine("You already have a Flame Axe!");
                                }

                                else
                                {
                                    player.Weapon = "Flame Axe";
                                    player.gold -= value;
                                    Console.WriteLine($"You spent {value} gold to buy a Flame Axe. You now have {player.gold} gold.");
                                }
                                break;

                            case "HEALING STAFF":
                                if (player.Class != "Mage") Console.WriteLine("You have to be a Mage to buy this!");

                                else if (player.Weapon == "Healing Staff") Console.WriteLine("You already have a Healing Staff!");

                                else
                                {
                                    player.Weapon = "Healing Staff";
                                    player.gold -= value;
                                    Console.WriteLine($"You spent {value} gold to buy a Healing Staff. You now have {player.gold} gold.");
                                }
                                break;

                            case "POISON DAGGER":
                                if (player.Class != "Rogue")
                                {
                                    Console.WriteLine("You have to be a Rogue to buy this!");
                                }

                                else if (player.Weapon == "Poison Dagger")
                                {
                                    Console.WriteLine("You already have a Poison Dagger!");
                                }

                                else
                                {
                                    player.Weapon = "Poison Dagger";
                                    player.gold -= value;
                                    Console.WriteLine($"You spent {value} gold to buy a Poison Dagger. You now have {player.gold} gold.");
                                }
                                break;

                            case "GOLDEN RING":
                                if (player.Item != "Golden Ring")
                                {
                                    player.Item = "Golden Ring";
                                    player.goldMultiplier = 2;
                                    player.gold -= value;
                                    Console.WriteLine($"You spent {value} gold to buy a Golden Ring. You now have {player.gold} gold.");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("You already have a Golden Ring!");
                                    break;
                                }

                            default:
                                Console.WriteLine("Invalid input.");
                                break;
                        }

                        Console.Write("Would you like to buy something else? (1) Yes (2) No: ");
                        input2 = Convert.ToInt32(Console.ReadLine());
                    }

                    else if (store.ContainsKey(input3) && player.gold < value) Console.WriteLine("Seems like your gold pouch is empty.");
                }

                Console.WriteLine("\"Come again soon\", the trader says.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
