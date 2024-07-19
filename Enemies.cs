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
            int enemyAttack = random.Next(0, p + 1);

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
