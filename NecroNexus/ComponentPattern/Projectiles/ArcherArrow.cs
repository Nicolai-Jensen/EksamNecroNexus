using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class ArcherArrow : Component, IGameListener
    {
        private float speed;
        private Vector2 position;
        private Vector2 velocity;

        private float Speed { get; set; }

        private Damage damage;
        private int tier;

        public override bool ToRemove { get; set; }

        public ArcherArrow(int tier,Vector2 position, Vector2 velocity)
        {
            this.tier = tier;
            this.position = position;
            this.velocity = velocity;

            switch (this.tier)
            {
                case (0):
                    ApplyTierZero();
                    break;
                case (1):
                    ApplyTier1();
                    break;
                case (2):
                    ApplyTier2();
                    break;
                case (3):
                    ApplyTier3();
                    break;

            }

        }

        public override void Start()
        {
            GameObject.Tag = "ArcherArrow";
            GameObject.Transform.Translate(position);
        }

        public override void Update()
        {
            Move();

        }


        public void Move()
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            velocity *= speed;
            GameObject.Transform.Translate(velocity * GameWorld.DeltaTime);
        }



       

        /// <summary>
        /// Method for applying upgrades. When called the variables change. 
        /// </summary>
        public void ApplyTierZero()
        {
            speed = 400f;

            damage = new Damage(DamageType.Physical, 0.5f);
        }

        private void ApplyTier1()
        {
            speed = 450f;

            damage = new Damage(DamageType.Physical, 1f);

        }

        private void ApplyTier2()
        {
            speed = 500f;

            damage = new Damage(DamageType.Physical, 2f);

        }

        private void ApplyTier3()
        {
            speed = 550f;

            damage = new Damage(DamageType.Physical, 3f);

        }


        public void Notify(GameEvent gameEvent)
        {

            if (gameEvent is CollisionEvent)
            {
                GameObject other = (gameEvent as CollisionEvent).Other;

                if (other.Tag == "Enemy")
                {
                    if (other.HasComponent<Grunt>())
                    {
                        Grunt enemy = (Grunt)other.GetComponent<Grunt>();
                        if (enemy.IsInDamagedList(this.GameObject) == false)
                        {
                            enemy.TakeDamage(damage);
                            enemy.AddToList(this.GameObject);
                        }
                    }
                    
                    else if (other.HasComponent<ArmoredGrunt>())
                    {
                        ArmoredGrunt enemy = (ArmoredGrunt)other.GetComponent<ArmoredGrunt>();
                        if (enemy.IsInDamagedList(this.GameObject) == false)
                        {
                            enemy.TakeDamage(damage);
                            enemy.AddToList(this.GameObject);
                        }
                    }
                    else if (other.HasComponent<Knight>())
                    {
                        Knight enemy = (Knight)other.GetComponent<Knight>();
                        if (enemy.IsInDamagedList(this.GameObject) == false)
                        {
                            enemy.TakeDamage(damage);
                            enemy.AddToList(this.GameObject);
                        }
                    }
                    else if (other.HasComponent<HorseRider>())
                    {
                        HorseRider enemy = (HorseRider)other.GetComponent<HorseRider>();
                        if (enemy.IsInDamagedList(this.GameObject) == false)
                        {
                            enemy.TakeDamage(damage);
                            enemy.AddToList(this.GameObject);
                        }
                    }
                    else if (other.HasComponent<Cleric>())
                    {
                        Cleric enemy = (Cleric)other.GetComponent<Cleric>();
                        if (enemy.IsInDamagedList(this.GameObject) == false)
                        {
                            enemy.TakeDamage(damage);
                            enemy.AddToList(this.GameObject);
                        }
                    }
                    else if (other.HasComponent<Paladin>())
                    {
                        Paladin enemy = (Paladin)other.GetComponent<Paladin>();
                        if (enemy.IsInDamagedList(this.GameObject) == false)
                        {
                            enemy.TakeDamage(damage);
                            enemy.AddToList(this.GameObject);
                        }
                    }
                    else if (other.HasComponent<Valkyrie>())
                    {
                        Valkyrie enemy = (Valkyrie)other.GetComponent<Valkyrie>();
                        if (enemy.IsInDamagedList(this.GameObject) == false)
                        {
                            enemy.TakeDamage(damage);
                            enemy.AddToList(this.GameObject);
                        }
                    }
                }
            }
        }
    }
}
