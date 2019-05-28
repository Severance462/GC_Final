using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC_Final
{
    class BasicModel
    {
        public Model model { get; protected set; }

        protected Matrix world = Matrix.Identity;

        public BasicModel(Model m)
        {
            model = m;
        }

        public virtual void Update()
        {

        }

        public void Draw(Camera camera)
        {
            float scale = 0.03f;
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect be in mesh.Effects)
                {
                    be.EnableDefaultLighting();         //enable basic lighting effects
                    be.Projection = camera.Projection;  //Use the camera projection
                    be.View = camera.View;              //use the camera view
                    //Set the Basic Effect's world using Current world times the current transform.
                    be.World = GetWorld(scale) * mesh.ParentBone.Transform;
                }
                mesh.Draw();
            }
        }

        public void Draw(Camera camera, Vector3 location)
        {
            float scale = 0.03f;
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect be in mesh.Effects)
                {
                    be.EnableDefaultLighting();         //enable basic lighting effects
                    be.Projection = camera.Projection;  //Use the camera projection
                    be.View = camera.View;              //use the camera view
                    //Set the Basic Effect's world using Current world times the current transform.
                    be.World = GetWorld(scale) * mesh.ParentBone.Transform;
                }
                mesh.Draw();
            }
        }
        public virtual Matrix GetWorld()
        {
            return world;
        }
        public virtual Matrix GetWorld(float scale)
        {
            //return Matrix.CreateScale(scale) * translation * world;
            return Matrix.CreateScale(scale) * world;
        }
        public bool CollidesWith(Model otherModel, Matrix otherWorld)
        {
            foreach (ModelMesh myModelMeshes in model.Meshes)
            {
                foreach (ModelMesh hisModelMeshes in otherModel.Meshes)
                {
                    if (myModelMeshes.BoundingSphere.Transform(
                        GetWorld()).Intersects(
                        hisModelMeshes.BoundingSphere.Transform(otherWorld)))
                        return true;
                }
            }
            return false;
        }
    }
}
