using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //***********//KASPER KNUDSEN//***********//
    public class ArcherArrow : Component, IGameListener
    {
        private float speed;
        private Vector2 position;
        private Vector2 velocity;
        private Damage damage;
        private int hits;

        public override bool ToRemove { get; set; }

       
        //Used for determening which applytier() method to call in the switch case below.
        private int tier;

        public ArcherArrow(int tier,Vector2 position, Vector2 velocity)
        {
            this.tier = tier;
            this.position = position;
            this.velocity = velocity;
            hits = 0;

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
            GameObject.Tag = "ArcherArrow";
            GameObject.Transform.Translate(position);
        }

        public override void Update()
        {
            Move();
            if (hits >=3)
            {
                ToRemove = true;
            }
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
            
            hits = 1;
            speed = 400f;
            damage = new Damage(DamageType.Physical, 1f);
        }

        private void ApplyTier1()
        {
            hits = 0;
            speed = 450f;
            damage = new Damage(DamageType.Physical, 2f);

        }

        private void ApplyTier2()
        {
            hits = -1;
            speed = 500f;
            damage = new Damage(DamageType.Physical, 3f);

        }

        private void ApplyTier3()
        {
            hits = -2;
            speed = 550f;
            damage = new Damage(DamageType.Physical, 4f);

        }

        /// <summary>
        /// The Notify() method is used for collision.
        ///if the opposing collider has the tag "Enemy", run a bunch of if's that check what enemy it is.
        ///if the grunt is not in the "IsInDamagedList", then take damage, and add the grunt to the list.
        ///This is the only projectile that adds enemies to the list, because the arrow is piercing, and therefor not removed upon collision
        /// </summary>
        /// <param name="gameEvent"></param>
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
                            hits++;
                        }
                    }
                    
                    else if (other.HasComponent<ArmoredGrunt>())
                    {
                        ArmoredGrunt enemy = (ArmoredGrunt)other.GetComponent<ArmoredGrunt>();
                        if (enemy.IsInDamagedList(this.GameObject) == false)
                        {
                            enemy.TakeDamage(damage);
                            enemy.AddToList(this.GameObject);
                            hits++;

                        }
                    }
                    else if (other.HasComponent<Knight>())
                    {
                        Knight enemy = (Knight)other.GetComponent<Knight>();
                        if (enemy.IsInDamagedList(this.GameObject) == false)
                        {
                            enemy.TakeDamage(damage);
                            enemy.AddToList(this.GameObject);
                            hits++;

                        }
                    }
                    else if (other.HasComponent<HorseRider>())
                    {
                        HorseRider enemy = (HorseRider)other.GetComponent<HorseRider>();
                        if (enemy.IsInDamagedList(this.GameObject) == false)
                        {
                            enemy.TakeDamage(damage);
                            enemy.AddToList(this.GameObject);
                            hits++;

                        }
                    }
                    else if (other.HasComponent<Cleric>())
                    {
                        Cleric enemy = (Cleric)other.GetComponent<Cleric>();
                        if (enemy.IsInDamagedList(this.GameObject) == false)
                        {
                            enemy.TakeDamage(damage);
                            enemy.AddToList(this.GameObject);
                            hits++;

                        }
                    }
                    else if (other.HasComponent<Paladin>())
                    {
                        Paladin enemy = (Paladin)other.GetComponent<Paladin>();
                        if (enemy.IsInDamagedList(this.GameObject) == false)
                        {
                            enemy.TakeDamage(damage);
                            enemy.AddToList(this.GameObject);
                            hits++;

                        }
                    }
                    else if (other.HasComponent<Valkyrie>())
                    {
                        Valkyrie enemy = (Valkyrie)other.GetComponent<Valkyrie>();
                        if (enemy.IsInDamagedList(this.GameObject) == false)
                        {
                            enemy.TakeDamage(damage);
                            enemy.AddToList(this.GameObject);
                            hits++;

                        }
                    }
                }
            }
        }
    }
}
