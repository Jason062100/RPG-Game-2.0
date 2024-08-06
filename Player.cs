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
        public int Health { get; private set; }
        public int Level { get; private set; }
        public int Experience { get; set; }
        public int MaxHealth { get; private set; }

        //Player stats
        public string Weapon { get; set; }
        public double damage { get; set; }
        public int potions = 3;
        public int potionStrength = 11;
        public int gold = 0;
        public int xp = 0;

        //Armor
        public int warriorArmor = 10;
        public int mageArmor = 5;
        public int rogueArmor = 5;

        //Properties for status effects
        public bool bleed { get; set; }
        public int bleedTurn = 0;
        public bool stun { get; set; }
        public bool Invis { get; set; }
        public bool ShadowStep { get; set; }

        public bool increaseDamage { get; set; }


        static Random random = new Random();
        public static Enemies enemy = new Enemies();

        public Player(int initialHealth, int initialLevel, int initialExperience, int initialMaxHealth, int initialDamage)
        {
            Health = initialHealth;
            Level = initialLevel;
            Experience = initialExperience;
            MaxHealth = initialMaxHealth;
            damage = initialDamage;

            bleed = false;
            bleedTurn = 0;
            stun = false;
            Invis = false;
            increaseDamage = false;
        }


        //Method to inflict bleed
        public void InflictBleed()
        {
            bleed = true;
            Console.WriteLine("The enemy has inflicted bleed on you!");
        }
        //Method to inflict stun
        public void InflictStun()
        {
            stun = true;
            Console.WriteLine("The enemy has inflicted stun on you!");
        }
        //Method to inflict invis
        public void InflictInvis()
        {
            Invis = true;
        }


        public void InflictStatus()
        {
            if (bleed == false && Invis == false) //If the player isn't bleeding already and the player isn't invis
            {
                int playerBleedChance = random.Next(0, 8); //1 in 8 change to bleed
                if (playerBleedChance == 0) InflictBleed();
            }

            if (stun == false && Invis == false) //If the player isn't stunned already and the player isn't invis
            {
                int playerStunChance = random.Next(0, 8); //1 in 8 chance to be stunned
                if (playerStunChance == 0) InflictStun();
            }
        }

        public void IncreaseDamage(bool incDmg)
        {
            if (incDmg == false) //If you don't have a damage buff already, increase damage
            {
                damage *= 1.25;
                Console.WriteLine("Your Flame Axe created a fiery aura around you that increased your attack.");
                Console.WriteLine($"Your damage has been increased by 25%. Damage: {damage}");
                increaseDamage = true;
            }
            else if (incDmg == true) //If you do have damage buff already, return damage back to normal
            {
                damage /= 1.25;
                Console.WriteLine("Your fiery aura wore off. Your damage returned to normal.");
                increaseDamage = false;
            }
        }


        //Property to manage Class
        public string Class { get; set; }
        //Method to choose class
        public void ChooseClass(string value)
        {
            Class = value;
        }

        //Level Up
        public void LevelUp()
        {
            Level++;
            Experience -= 20;
            Console.WriteLine($"Congratulations! You leveled up to level {Level}! Player EXP: {Experience}");
            Console.Write($"Your max health has been increased from {MaxHealth} to");
            MaxHealth += 10;
            Console.WriteLine($" {MaxHealth}!");
            //heal a little?
        }

        public void BeatDungeon()
        {
            Health = MaxHealth;
            bleed = false;
            bleedTurn = 0;
            stun = false;
            Invis = false;

            Console.WriteLine("You make a campfire in a small clearing that looks safe. After cooking and eating, you fall asleep under the stars.");
            Console.WriteLine("You healed back to max health.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        //Take Damage
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
            Console.WriteLine($"You took {damage} damage. Player health: {Health}");
        }

        public void TakeBleedDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
            Console.WriteLine($"You bleed and lose 5 health. Player health: {Health}");
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
            int minAttack = (int)Math.Floor(damage - 15);
            int maxAttack = (int)Math.Floor(damage - 4);

            int attack = random.Next(minAttack, maxAttack); //5-15 dmg
            h -= attack;
            if (h < 0) h = 0;

            Console.WriteLine($"You used Stab on the {n} for {attack} damage. Enemy health: {h}");

            //Bleed
            int bleedChance = random.Next(0, 5); //1/5 chance of bleed
            if (bleedChance == 0 && enemy.enemyBleedTurn == 0) //If you get bleed and the enemy has had it for less less than 2 turns
            {
                enemy.InflictBleed();
                Console.WriteLine($"You inflicted bleed on the {n}.");
            }

            return h;
        }

        //Warrior Overhead Swing
        public int OverheadSwing(int h, string n)
        {
            int maxAttack = (int)Math.Floor(damage + 11);

            int attack = random.Next(0, maxAttack); //0-30 dmg
            h -= attack;
            if (h < 0) h = 0;

            Console.WriteLine($"You used Overhead Swing on the {n} for {attack} damage. Enemy health: {h}");

            //Stun
            int stunChance = random.Next(0, 5); //1/5 chance to stun
            if (stunChance == 0)
            {
                enemy.InflictStun();
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
            MageAttackDetails.Add("Fireball does 15-30 damage. Has a 1 in 3 chance of missing. 20-30 damage.");
            MageAttackDetails.Add("Magic Missle does 1-4 damage per hit. Can hit 2-8 times. 4-36 damage.");
            return MageAttackDetails;
        }


        //Fireball
        public int Fireball(int h, string n)
        {
            int minAttack = (int)Math.Floor(damage);
            int maxAttack = (int)Math.Floor(damage + 11);

            int attack = random.Next(minAttack, maxAttack); //20-30 dmg

            int missChance = random.Next(0, 3); //1/3 chance of missing
            if (missChance == 0)
            {
                attack = 0;
                Console.WriteLine("Your attack missed!");
            }

            h -= attack;
            if (h < 0) h = 0;

            Console.WriteLine($"You used Fireball on the {n} for {attack} damage. Enemy health: {h}");

            return h;
        }

        //Magic Missile
        public int MagicMissle(int h, string n)
        {
            int minAttack = (int)Math.Floor(damage - 18);
            int maxAttack = (int)Math.Floor(damage - 16);

            int attack = random.Next(minAttack, maxAttack); //2-4 dmg per hit
            int hits = random.Next(2, 9); //Can hit 2-8 times
            attack *= hits; //4-36 damage

            h -= attack;
            if (h < 0) h = 0;

            Console.WriteLine($"You used Magic Missile {hits} times on the {n} for {attack} damage. Enemy health: {h}");

            return h;
        }




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
            RogueAttackDetails.Add("1. Fury Strike does 10-20 damage and has a 1 in 5 chance of a critical hit for 10 extra damage. If you are invisible, it does 15-20 damage and a 1 in 3 chance for a critical hit..");
            RogueAttackDetails.Add("2. Back Stab does 5-20 damage. Has a chance to shadow step and evade incoming damage.");
            RogueAttackDetails.Add("3. Shadows has a 1 in 2 chance to hide in the shadows. Each time you successfully hide, the chance goes up by 1. Resets after killing an enemy.");
            return RogueAttackDetails;
        }

        //Fury Strike
        public int FuryStrike(int h, string n)
        {
            int minAttack;
            int maxAttack;
            int crit = 0;
            int critChance = 0;
            int attack;

            switch (Invis)
            {
                case true: //If invis
                    minAttack = (int)Math.Floor(damage + 1);
                    maxAttack = (int)Math.Floor(damage + 11);

                    attack = random.Next(minAttack, maxAttack); //20-30 damage (with crit: 30-40)
                    h -= attack;
                    if (h < 0) h = 0;

                    Console.WriteLine($"You used Fury Strike on the {n} for {attack} damage. Enemy health: {h}");

                    critChance = random.Next(0, 3); //1 in 3 chance

                    if (critChance == 0)
                    {
                        crit = 10;
                        h -= crit;
                        if (h < 0) h = 0;

                        Console.WriteLine($"You landed a critical hit for {crit} extra damage! Enemy health: {h}");
                    }

                    Console.WriteLine("You left the shadows.");
                    Invis = false;

                    break;

                case false: //If not invis
                    minAttack = (int)Math.Floor(damage - 15);
                    maxAttack = (int)Math.Floor(damage - 4);

                    attack = random.Next(minAttack, maxAttack); //5-15 damage (with crit: 15-25 damage)
                    h -= attack;
                    if (h < 0) h = 0;

                    Console.WriteLine($"You used Fury Strike on the {n} for {attack} damage. Enemy health: {h}");

                    critChance = random.Next(0, 5); //1 in 5 chance

                    if (critChance == 0)
                    {
                        crit = 10;
                        h -= crit;
                        if (h < 0) h = 0;

                        Console.WriteLine($"You landed a critical hit for {crit} extra damage! Enemy health: {h}");
                    }

                    break;
            }

            return h;
        }

        //Backstab
        public int Backstab(int h, string n)
        {
            int minAttack = (int)Math.Floor(damage - 9);
            int maxAttack = (int)Math.Floor(damage + 1);

            int attack = random.Next(minAttack, maxAttack); //10-20 damage
            h -= attack;
            if (h < 0) h = 0;

            Console.WriteLine($"You used Back Stab on the {n} for {attack} damage. Enemy health: {h}");

            if (Invis == true)
            {
                Console.WriteLine("You left the shadows.");
                Invis = false;
            }

            int shadowStepChance = random.Next(0, 3); //1 in 3 chance to evade next attack
            if (shadowStepChance == 0) ShadowStep = true;

            return h;
        }

        public int shadowsUsage = 0;
        public void Shadows()
        {
            int hide = random.Next(0, 2 + shadowsUsage); //1 in 2 chance + 1 every time you use it

            if (hide == 0)
            {
                InflictInvis();

                shadowsUsage++;
                Console.WriteLine("You hid in the shadows. Your shadows chance is now 1 in " + (2 + shadowsUsage) + ".");
            }

            else Console.WriteLine("You failed to hide.");
        }
    }
}
