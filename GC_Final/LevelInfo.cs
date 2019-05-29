using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC_Final
{
    class LevelInfo
    {
        // Spawn variables (used to control range of random spawn time)
        public int minSpawnTime { get; set; }   //minimum time to spawn next enemy
        public int maxSpawnTime { get; set; }   //maximum time to spawn next enemy

        // Enemy count variables
        public int numberEnemies { get; set; }  //Number of enemies spawned for this level
        public int minSpeed { get; set; }       //Minimum speed enemy travels at
        public int maxSpeed { get; set; }       //Maximum speed enemy travels at

        // Misses (When enemy gets past you on z-axis, that is a miss)
        public int missesAllowed { get; set; }      //Number of misses before Game Over


        //Constructor that receives and sets all necessary values
        public LevelInfo(int minSpawnTime, int maxSpawnTime,
            int numberEnemies, int minSpeed, int maxSpeed,
            int missesAllowed)
        {
            this.minSpawnTime = minSpawnTime;
            this.maxSpawnTime = maxSpawnTime;
            this.numberEnemies = numberEnemies;
            this.minSpeed = minSpeed;
            this.maxSpeed = maxSpeed;
            this.missesAllowed = missesAllowed;
        }
    }
}
