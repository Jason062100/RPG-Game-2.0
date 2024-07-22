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
        //Armor
        public int armor;
        public string weapon = "";
        public int warriorArmor;
        public int mageArmor;
        public int rogueArmor;
        public int health = 100;
        public int damage = 20;
        public int potions = 3;
        public int potionStrength = 10;
        public int gold = 0;

        //Armor
        public int highArmor = 10;
        public int mediumArmor = 7;
        public int lowArmor = 4;

        //Status Effects
        //Bleed
        public bool bleed = false;
        public int bleedTurn = 0;
        //Stun
        public bool stun = false;
        public int stunTurn = 0;
        public bool skipEnemyAttack = false;
        //Invisible
        public bool invis = false;




        //Classes

        //Warrior

        //Warrior Attacks
        public List<string> WarriorAttacks = new List<string>();
        public List<string> WarriorAttackList()
        {
            WarriorAttacks.Add("Stab");
            WarriorAttacks.Add("Overhead Swing");
            return WarriorAttacks;
        }

        //Attack Details
        public List<string> WarriorAttackDetails = new List<string>();
        public List<string> WarriorAttackDetailsList()
        {
            WarriorAttackDetails.Add("Stab does 5-15 damage. Has a 1 in 5 chance of inflicting bleed.");
            WarriorAttackDetails.Add("Overhead Swing does 0-30 damage. Has a 1 in 5 chance of inflicting stun.");
            return WarriorAttackDetails;
        }

        //Warrior Stab
        public int Stab(int h, string n)
        {
            int attack = random.Next(damage - 15, damage - 4); //5-15 dmg

            int bleedChance = random.Next(0, 5); //1/5 chance of bleed
            if (bleedChance == 0 && bleedTurn == 0)
            {
                bleed = true;
                Console.WriteLine($"You inflicted bleed on the {n}.");
                bleedTurn++;
            }
            if (bleed == true)
            {
                attack += 5;
                Console.WriteLine($"The {n} bleeds 5 health.");
            }

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
            int stunChance = random.Next(0, 5); //1/5 chance to stun
            if (stunChance == 0)
            {
                stun = true;
                Console.WriteLine("You inflicted stun on the enemy.");
            }

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

        //Mage Attacks
        public List<string> MageAttacks = new List<string>();
        public List<string> MageAttackList()
        {
            MageAttacks.Add("Fireball");
            MageAttacks.Add("Magic Missle");
            return MageAttacks;
        }

        //Attack Details
        public List<string> MageAttackDetails = new List<string>();
        public List<string> MageAttackDetailsList()
        {
            MageAttackDetails.Add("Fireball does 15-30 damage. Has a 1 in 3 chance of missing.");
            MageAttackDetails.Add("Does 1-3 damage per hit. Can hit 2-8 times.");
            return MageAttackDetails;
        }


        //Fireball
        public int Fireball(int h, string n)
        {
            int attack = random.Next(damage, damage + 11); // 20-30

            int missChance = random.Next(0, 3); //1/3 chance of missing
            if (missChance == 0)
            {
                attack = 0;
                Console.WriteLine("Your attack missed!");
            }

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
            int hits = random.Next(2, 9); //Can hit 2-8 times
            int attack = random.Next(damage - 19, damage - 15); // 1-3 damage per hit (2-32)
            attack *= hits;

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

        //Rogue Attacks
        public List<string> RogueAttacks = new List<string>();
        public List<string> RogueAttackList()
        {
            RogueAttacks.Add("Fury Strike");
            RogueAttacks.Add("Back Stab");
            RogueAttacks.Add("Shadows");
            return RogueAttacks;
        }

        //Attack Details
        public List<string> RogueAttackDetails = new List<string>();
        public List<string> RogueAttackDetailsList()
        {
            RogueAttackDetails.Add("Fury Strike does 10-20 damage. Has a 1 in 5 chance of getting a critical hit for 10 extra damage.");
            RogueAttackDetails.Add("Back Stab does 25-35 damage. Can only be used while hidden in the shadows.");
            RogueAttackDetails.Add("Shadows has a 1 in 3 chance to hide in the shadows.");
            return RogueAttackDetails;
        }

        //Fury Strike
        public int FuryStrike(int h, string n)
        {
            critChance = random.Next(1, 6); //1 in 5 chance
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

            if (invis == true) Console.WriteLine("You left the shadows.");
            invis = false;

            return h;
        }

        //Backstab
        public int Backstab(int h, string n)
        {
            if (invis == false)
            {
                Console.WriteLine("Backstab can only be used while hidden in the shadows.");
            }

            else
            {
                int attack = random.Next(damage + 5, damage + 16); // 25-35
                h -= attack;
                if (h < 0)
                {
                    h = 0;
                }
                Console.WriteLine($"You used Back Stab on the {n} for {attack} damage leaving the {n} with {h} health.");
                
                Console.WriteLine("You left the shadows.");
                invis = false;
            }

            return h;
        }

        public void Shadows()
        {
            int hide = random.Next(0, 3); //1/3 chance

            if (hide == 0)
            {
                invis = true;
                Console.WriteLine("You hid in the shadows.");
            }

            else {
                Console.WriteLine("You failed to hide.");
            }
        }
    }
}
