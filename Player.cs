using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Jason
{

    //Player stats
    public class Player
    {
        static Random random = new Random();

        //Player stuff
        public int health = 100;
        public static int damage = 20;
        public int potions = 3;
        public int potionStrength = 10;
        public int gold = 0;

        //Armor
        public int highArmor;
        public int mediumArmor;
        public int lowArmor;

        //Crit
        public int critChance;
        public int crit = random.Next(1, 4);

        //Warrior
        public string warriorLightAttack = "Light Attack";
        public string warriorHeavyAttack = "Heavy Attack";



        //Classes
        public void Warrior() //More armor, medium damage, parry
        {
            int warriorArmor = mediumArmor;
            int warriorDamage = damage;
        }

        public static int WarriorLightAttack(int h, string n)
        {
            int attack = random.Next(damage - 15, damage - 4);
            h -= attack;
            if (h < 0)
            {
                h = 0;
            }
            Console.WriteLine($"You attack the {n} for {attack} damage leaving the {n} with {h} health.");

            return h;
         }

        public static int WarriorHeavyAttack(int h, string n)
        {
            int attack = random.Next(0, damage + 10);
            h -= attack;
            if (h < 0)
            {
                h = 0;
            }
            Console.WriteLine($"You attack the {n} for {attack} damage leaving the {n} with {h} health.");

            return h;
        }



        public void Mage() //Low armor, high damage, spells
        {
            int mageArmor = lowArmor;
            int mageDamage = damage + 10;
        }



        public void Rogue() //Low armor (evade), low damage (crit chance)
        {
            if (critChance == 1)
            {
                crit = 10;
            }

            int rogueArmor = lowArmor;
            int rogueDamage = damage + crit;
        }
    }
}
