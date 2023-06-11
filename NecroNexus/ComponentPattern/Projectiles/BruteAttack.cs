using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //***********//KASPER KNUDSEN//***********//
    public class BruteAttack : Component, IGameListener
    {
        private float speed;
        private Vector2 position;
        private Vector2 velocity;
        private Damage damage;
    

        private float Speed { get; set; }
        public override bool ToRemove { get; set; }

        //Used for determening which applytier() method to call in the switch case below.
        private int tier;



        public BruteAttack(int tier, Vector2 position, Vector2 velocity)
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
        /// <summary>
        /// Gives the arrow a tag, and spawnposition
        /// </summary>
        public override void Start()
        {
            GameObject.Transform.Translate(position);
        }

        public override void Update()
        {
            Move();

        }
        /// <summary>
        /// if velocity is not 0, normalize(), thereafter it multiplies velocity with speed, and then uses translate to move the object.
        /// </summary>
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
        /// Method for applying upgrades. When called, variables can be changed, which in this case is speed and damage.
        ///damage is controlled by a seperate damage class, but with the use of enums, we can select which damagetype, this object (arrow) does.
        /// </summary>
        public void ApplyTierZero()
        {
            speed = 400f;

            damage = new Damage(DamageType.Physical, 0f);
        }

        private void ApplyTier1()
        {
            speed = 450f;

            damage = new Damage(DamageType.Physical, 0f);

        }

        private void ApplyTier2()
        {
            speed = 500f;

            damage = new Damage(DamageType.Physical, 0f);

        }

        private void ApplyTier3()
        {
            speed = 550f;

            damage = new Damage(DamageType.Physical, 0f);

        }

        /// <summary>
        /// The Notify() method is used for collision.
        ///if the opposing collider has the tag "Enemy", run a bunch of if's that check what enemy it is.
        ///if the grunt is not in the "IsInDamagedList", then take damage, and remove this projectile.
        /// </summary>
        /// <param name="gameEvent"></param>
        public void Notify(GameEvent gameEvent)
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
                        ToRemove = true;
                    }
                }

                else if (other.HasComponent<ArmoredGrunt>())
                {
                    ArmoredGrunt enemy = (ArmoredGrunt)other.GetComponent<ArmoredGrunt>();
                    if (enemy.IsInDamagedList(this.GameObject) == false)
                    {
                        enemy.TakeDamage(damage);
                        ToRemove = true;

                    }
                }
                else if (other.HasComponent<Knight>())
                {
                    Knight enemy = (Knight)other.GetComponent<Knight>();
                    if (enemy.IsInDamagedList(this.GameObject) == false)
                    {
                        enemy.TakeDamage(damage);
                        ToRemove = true;

                    }
                }
                else if (other.HasComponent<HorseRider>())
                {
                    HorseRider enemy = (HorseRider)other.GetComponent<HorseRider>();
                    if (enemy.IsInDamagedList(this.GameObject) == false)
                    {
                        enemy.TakeDamage(damage);
                        ToRemove = true;

                    }
                }
                else if (other.HasComponent<Cleric>())
                {
                    Cleric enemy = (Cleric)other.GetComponent<Cleric>();
                    if (enemy.IsInDamagedList(this.GameObject) == false)
                    {
                        enemy.TakeDamage(damage);
                        ToRemove = true;
                    }
                }
                else if (other.HasComponent<Paladin>())
                {
                    Paladin enemy = (Paladin)other.GetComponent<Paladin>();
                    if (enemy.IsInDamagedList(this.GameObject) == false)
                    {
                        enemy.TakeDamage(damage);
                        ToRemove = true;

                    }
                }
                else if (other.HasComponent<Valkyrie>())
                {
                    Valkyrie enemy = (Valkyrie)other.GetComponent<Valkyrie>();
                    if (enemy.IsInDamagedList(this.GameObject) == false)
                    {
                        enemy.TakeDamage(damage);
                        ToRemove = true;

                    }
                }
            }
        }

    }
}
