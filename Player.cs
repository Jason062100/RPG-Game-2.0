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
        public string Class = "";
        public int health = 50;
        public static int damage = 20;
        public static int potions = 3;
        public int potionStrength = 10;
        public static int gold = 0;

        //Armor
        public int highArmor;
        public int mediumArmor;
        public int lowArmor;

        //Warrior
        



        //Classes
        public void Warrior() //More armor, medium damage, parry
        {
            int warriorArmor = mediumArmor;
            int warriorDamage = damage;
        }

        //Warrior Attacks
        public List<string> WarriorAttacks = new List<string>();
        public List<string> WarriorAttackList()
        {
            WarriorAttacks.Add("Stab");
            WarriorAttacks.Add("Overhead Swing");
            return WarriorAttacks;
        }

        //Warrior Stab
        public int Stab(int h, string n)
        {
            int attack = random.Next(damage - 15, damage - 4); //5-15 dmg
            h -= attack;
            if (h < 0)
            {
                h = 0;
            }
            Console.WriteLine($"You used Stab on the {n} for {attack} damage leaving the {n} with {h} health.");

            return h;
        }

        //Warrior Overhead Swing
        public int OverheadSwing(int h, string n)
        {
            int attack = random.Next(0, damage + 11); //0-30 dmg
            h -= attack;
            if (h < 0)
            {
                h = 0;
            }
            Console.WriteLine($"You used Overhead Swing on the {n} for {attack} damage leaving the {n} with {h} health.");

            return h;
        }




        //Mage
        public void Mage() //Low armor, high damage, spells
        {
            int mageArmor = lowArmor;
            int mageDamage = damage + 10;
        }

        //Mage Attacks
        public List<string> MageAttacks = new List<string>();
        public List<string> MageAttackList()
        {
            MageAttacks.Add("Fireball");
            MageAttacks.Add("Magic Missle");
            return MageAttacks;
        }

        //Fireball
        public int Fireball(int h, string n)
        {
            int attack = random.Next(damage - 5, damage + 11); // 15-30
            h -= attack;
            if (h < 0)
            {
                h = 0;
            }
            Console.WriteLine($"You used Fireball on the {n} for {attack} damage leaving the {n} with {h} health.");

            return h;
        }

        //Magic Missile
        public int MagicMissle(int h, string n)
        {
            int attack = random.Next(20, damage + 6); // 20-25
            h -= attack;
            if (h < 0)
            {
                h = 0;
            }
            Console.WriteLine($"You used Magic Missile on the {n} for {attack} damage leaving the {n} with {h} health.");

            return h;
        }




        //Crit
        static int critChance;
        static int crit = 0;

        //Rogue
        public void Rogue() //Low armor (evade), low damage (crit chance)
        {
            int rogueArmor = lowArmor;
            int rogueDamage = damage + crit;
        }

        //Rogue Attacks
        public List<string> RogueAttacks = new List<string>();
        public List<string> RogueAttackList()
        {
            RogueAttacks.Add("Fury Strike");
            RogueAttacks.Add("Back Stab");
            return RogueAttacks;
        }

        //Fury Strike
        public int FuryStrike(int h, string n)
        {
            critChance = random.Next(1, 2); //1 in 5 chance
            if (critChance == 1)
            {
                crit = 10;
            }

            int attack = random.Next(damage - 10, damage + 1) + crit; // 10-20
            h -= attack;
            if (h < 0)
            {
                h = 0;
            }
            
            Console.WriteLine($"You used Fury Strike on the {n} for {attack} damage leaving the {n} with {h} health.");
            if (crit != 0)
            {
                Console.WriteLine($"You landed a critical hit for {crit} extra damage!");
            }

            return h;
        }

        //Backstab
        public int Backstab(int h, string n)
        {
            int attack = random.Next(20, damage + 6); // 20-25
            h -= attack;
            if (h < 0)
            {
                h = 0;
            }
            Console.WriteLine($"You used Backstab on the {n} for {attack} damage leaving the {n} with {h} health.");

            return h;
        }
    }
}
