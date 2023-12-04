using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RougeGame
{
    public struct Creature
    {
        public int maxHealth;
        public int health;

        public int maxEnergy;
        public int energy;

        // float to allow percent muliplication 0.5f = 50%
        public float energyRechargeMult;
        // how much to reduce the percent chance to hit. rnd <= 80 + hitChanceAddition.
        public float hitChanceAddition;

        public Creature(int totalHealth, int totalEnergy)
        {
            this.maxHealth = totalHealth;
            this.maxEnergy = totalEnergy;

            this.health = this.maxHealth;
            this.energy = this.maxEnergy;

            // want to add more creatures or have more fights in one game then reset at the start of a new game.
            this.energyRechargeMult = 1;
            this.hitChanceAddition = 0;
        }
        public Creature()
        {
            this.maxHealth = 100;
            this.maxEnergy = 50;

            this.health = this.maxHealth;
            this.energy = this.maxEnergy;

            // want to add more creatures or have more fights in one game then reset at the start of a new game.
            this.energyRechargeMult = 1;
            this.hitChanceAddition = 0;
        }
    }

    public class RougeGameCeatures
    {
       
    }


    // planning features.

    public abstract class CreatureBase
    {
        ////base stuff
        //public const int maxHealth = 100;
        //public const int maxEnergy = 100;

        public CreatureBase()
        {
            health = maxHealth;
            energy = maxEnergy;
        }


        public virtual int maxHealth { get; set; } = 100;
        public int health;


        public virtual int maxEnergy { get; private set; } = 50;
        public int energy;


        public virtual float normalRechargeRate { get; private set; } = 1f;
        // float to allow percent muliplication 0.5f = 50%
        public float energyRechargeMult = 1;

        public virtual float normalHitChance { get; private set; } = 1f; // might not need these
        // how much to reduce the percent chance to hit. rnd <= 80 + hitChanceAddition.
        public float hitChanceAddition = 0;


        // Attack
        public virtual int attackDamageMin { get; private set; } = 1;
        public virtual int attackDamageMax { get; private set; } = 10;
        public virtual int attackHitChance { get; private set; } = 80;
        public virtual int attackCost { get; private set; } = 5;

        // Special Attack
        public virtual int specialAttackDamageMin { get; private set; } = 5;
        public virtual int specialAttackDamageMax { get; private set; } = 20;
        public virtual int specialAttackHitChance { get; private set; } = 50;
        public virtual int specialAttackCost { get; private set; } = 20;

        // Re-Charge
        public virtual int rechargeHitChance { get; private set; } = 10;
        public virtual float rechargeRate { get; private set; } = 4f;

        // Dodge
        public virtual int dodgeHitChance { get; private set; } = -30;
        public virtual float dodgeEnergyReChargeRate { get; private set; } = 0.5f;

        // Heal
        public virtual int healCost { get; private set; } = 10;
        // \/ Not used yet.
        public virtual float energyConvertion { get; private set; } = 0.5f;

        public virtual void NewCreature()
        {
            ResetStats();
            //health = maxHealth;
            //energy = maxEnergy;

            ResetMultipliers();
            //energyRechargeMult = 1;
            //hitChanceAddition = 0;
        }

        public virtual void ResetMultipliers()
        {
            energyRechargeMult = 1;
            hitChanceAddition = 0;
        }

        public virtual void ResetStats()
        {
            health = maxHealth;
            energy = maxEnergy;
        }

        public virtual void TakeDamage(int damage)
        {
            health -= damage;
        }

        public virtual void Heal()
        {
            energy -= healCost;

            health += energy / 2;

            if (health > maxHealth)
            {
                health = maxHealth;
            }

            energy /= 2;
        }

        public virtual void RechargeEnergy()
        {
            energy += (int)(rechargeRate * energyRechargeMult);

            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }
        }
    }

    public class StandardCreature : CreatureBase
    {
        // nothing to be added yet
    }

    public class BossCreature : CreatureBase
    {
        public override int maxHealth { get; set; } = 200;
    }
}
