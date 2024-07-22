using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jason;

namespace Jason
{
    public class Enemies
    {
        static Random random = new Random();

        //Enemy Attack
        public int EnemyAttack(int playerHealth, int p, string n)
        {
            int armor;
            int enemyAttack = random.Next(0, p + 1);

            switch (Program.currentPlayer.Class)
            {
                case "Warrior":
                    armor = random.Next(0, Program.currentPlayer.highArmor + 1);
                    enemyAttack -= armor;
                    Console.WriteLine($"{armor} damage was negated by your armor.");
                    break;
                
                case "Mage":
                    armor = random.Next(0, Program.currentPlayer.lowArmor + 1);
                    enemyAttack -= armor;
                    Console.WriteLine($"{armor} damage was negated by your armor.");
                    break;

                case "Rogue":
                    armor = random.Next(0, Program.currentPlayer.lowArmor + 1);
                    enemyAttack -= armor;
                    Console.WriteLine($"{armor} damage was negated by your armor.");
                    break;
            }

            if (enemyAttack < 0)
            {
                enemyAttack = 0;
            }
            
            int newPlayerHealth = playerHealth - enemyAttack;

            if (newPlayerHealth < 0)
            {
                newPlayerHealth = 0;
            }

            Console.WriteLine($"The {n} hit you back for {enemyAttack} damage leaving you on {newPlayerHealth} health.");

            return newPlayerHealth;
        }
    }
}
