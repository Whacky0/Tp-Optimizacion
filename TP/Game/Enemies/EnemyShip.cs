using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Engine;
using Engine.Utils;

namespace Game
{
    public class EnemyShip : GameObject
    {
        private static Random rnd = new Random();

        private int shipIndex;
        private EnemyBehavior behavior;
        
        public EnemyShip(int shipIndex, EnemyBehavior behavior)
        {
            this.shipIndex = shipIndex;
            this.behavior = behavior;
            this.MiImagen = LoadImage();
            
            Visible = false;
        }

        public PlayerShip Player
        {
            get
            {
                return AllObjects
                    .Select(obj => obj as PlayerShip)
                    .FirstOrDefault(obj => obj != null);
            }
        }

        public EnemyBehavior Behavior
        {
            get { return behavior; }
        }

        public override void Update(float deltaTime)
        {
            if (Position.X <= 0)//Eliminar los enemigos cuando salen de la pantalla
            {

                this.Delete();
                return;
            }

            behavior.Update(this, deltaTime);
            Visible = true;
        }

        public void Explode()
        {
            if (rnd.NextDouble() > 0.95)
            {
                PowerUp pup = new PowerUp();
                pup.Center = Center;
                Root.AddChild(pup);
            }
            Explosion.Burst(Parent, Center);
            Delete();
        }

        public override void DrawOn(Graphics graphics)
        {
            graphics.DrawImage(MiImagen, Bounds);
        }
        Image MiImagen;

        private Image LoadImage()
        {
            Image ThisImage = Spritesheet.Ships[shipIndex];
            Extent = new SizeF(ThisImage.Size.Width / 2, ThisImage.Size.Height / 2);
            return ThisImage;
        }
    }
}
