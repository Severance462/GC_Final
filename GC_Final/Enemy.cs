using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC_Final
{
    class Enemy : BasicModel
    {
        Matrix rotation = Matrix.Identity;

        //*************************************************************
        // Rotation and movement variables
        float yawAngle = 0;
        float pitchAngle = 0;
        float rollAngle = 0;
        Vector3 direction;
        //*************************************************************


        //*************************************************************
        // MODIFIED Constructor
        // Params: The Model, its positon in the world, direction it will move in, rotations
        // Cals the base constructor, plus adds what is in braces
        public Enemy(Model m, Vector3 Position, Vector3 Direction, float yaw, float pitch, float roll) : base(m)
        {
            world = Matrix.CreateTranslation(Position); //Move to spec. position in world
            yawAngle = yaw;         //Set Yaw angle
            pitchAngle = pitch;     //Set pitch angle
            rollAngle = roll;       //set roll angle
            direction = Direction;  //set direction
        }
        //*************************************************************




        //*************************************************************
        // MODIFIED Update
        public override void Update()
        {
            // Rotate model using all three rotation angles (cumulative)
            rotation *= Matrix.CreateFromYawPitchRoll(yawAngle, pitchAngle, rollAngle);

            // Move model based on direction (cumulative)
            world *= Matrix.CreateTranslation(direction);
        }
        //*************************************************************




        //*************************************************************
        // MODIFIED 
        public override Matrix GetWorld()
        {
            //World already included movement based on direction,
            // but now needs to include the rotation
            return rotation * world;
        }
        //*************************************************************


    }
}