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

        public int Health { get; private set; }
        public int Level { get; private set; }
        public int Experience { get; private set; }
        
        public Player(int initialHealth, int initialLevel, int initialExperience)
        {
            Health = initialHealth;
            Level = initialLevel;
            Experience = initialExperience;
        }


        //Property to manage bleed status
        public bool Bleed { get; set; }
        //Method to inflict bleed
        public void InflictBleed()
        {
            Bleed = true;
        }
        //Bleed turn
        public int bleedTurn = 0;

        //Property to manage stun status
        public bool Stun { get; set; }
        //Method to inflict stun
        public void InflictStun()
        {
            Stun = true;
        }

        //Property to manage stun status
        public bool Invis { get; set; }
        //Method to inflict invis
        public void InflictInvis()
        {
            Invis = true;
        }


        //Property to manage Class
        public string Class { get; set; }
        //Method to choose class
        public void ChooseClass(string value)
        {
            Class = value;
        }
        

        //Player stats
        public string Weapon { get; set; }
        public int damage = 20;
        public int potions = 3;
        public int potionStrength = 10;
        public int gold = 0;
        public int xp = 0;

        //Armor
        public int warriorArmor = 10;
        public int mageArmor = 5;
        public int rogueArmor = 5;

        //Status Effects
        //Stun
        public int stunTurn = 0;
        

        //Level Up
        public void LevelUp()
        {
            Level++;
            Health += 10;
            Console.WriteLine($"Congratulations! You level up to level {Level}.");
            Console.WriteLine($"Your health is now {Health}.");
        }

        //Take Damage
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
            Console.WriteLine($"You took {damage} damage and you now have {Health} health.");
        }

        public void Heal(int heal)
        {
            Health += heal;
        }

        


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
            WarriorAttackDetails.Add("Stab does 5-15 damage. Has a 1 in 5 chance of inflicting bleed and lasts for 2 turns.");
            WarriorAttackDetails.Add("Overhead Swing does 0-30 damage. Has a 1 in 5 chance of inflicting stun.");
            return WarriorAttackDetails;
        }

        //Warrior Stab
        public int Stab(int h, string n)
        {
            int attack = random.Next(damage - 15, damage - 4); //5-15 dmg
            h -= attack;
            if (h < 0) h = 0;
            Console.WriteLine($"You used Stab on the {n} for {attack} damage leaving the {n} with {h} health.");

            //Bleed
            int bleedChance = random.Next(0, 5); //1/5 chance of bleed
            if (bleedChance == 0 && bleedTurn == 0) //If you get bleed and the enemy has had it for less less than 2 turns
            {
                InflictBleed();
                Console.WriteLine($"You inflicted bleed on the {n}.");
            }

            return h;
        }

        //Warrior Overhead Swing
        public int OverheadSwing(int h, string n)
        {
            int attack = random.Next(0, damage + 11); //0-30 dmg
            h -= attack;
            if (h < 0) h = 0;

            Console.WriteLine($"You used Overhead Swing on the {n} for {attack} damage leaving the {n} with {h} health.");

            //Stun
            int stunChance = random.Next(0, 5); //1/5 chance to stun
            if (stunChance == 0)
            {
                InflictStun();
                Console.WriteLine("You inflicted stun on the enemy.");
            }

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
            if (h < 0) h = 0;

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
            if (h < 0) h = 0;

            Console.WriteLine($"You used Magic Missile {hits} times on the {n} for {attack} damage leaving the {n} with {h} health.");

            return h;
        }




        //Rogue

        //Crit
        static int critChance;

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
            int crit = 0;
            critChance = random.Next(0, 5); //1 in 5 chance
            if (critChance == 0) crit = 10;

            int attack = random.Next(damage - 10, damage + 1) + crit; // 10-20 + 10 crit
            h -= attack;
            if (h < 0) h = 0;

            Console.WriteLine($"You used Fury Strike on the {n} for {attack} damage leaving the {n} with {h} health.");

            if (critChance == 0) Console.WriteLine($"You landed a critical hit for {crit} extra damage!");

            if (Invis == true) Console.WriteLine("You left the shadows.");
            Invis = false;
            crit = 0;

            return h;
        }

        //Backstab
        public int Backstab(int h, string n)
        {
            if (Invis == false)
            {
                Console.WriteLine("Backstab can only be used while hidden in the shadows.");
            }

            else
            {
                int attack = random.Next(damage + 5, damage + 16); // 25-35
                h -= attack;
                if (h < 0) h = 0;

                Console.WriteLine($"You used Back Stab on the {n} for {attack} damage leaving the {n} with {h} health.");

                Console.WriteLine("You left the shadows.");
                Invis = false;
            }

            return h;
        }

        public void Shadows()
        {
            int hide = random.Next(0, 3); //1/3 chance

            if (hide == 0)
            {
                InflictInvis();
                Console.WriteLine("You hid in the shadows.");
            }

            else Console.WriteLine("You failed to hide.");
        }
    }
}
