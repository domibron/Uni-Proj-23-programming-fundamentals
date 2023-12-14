using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RougeGame.GameCreatures
{
    // base creature.
    public abstract class CreatureBase
    {
        ////base stuff
        public const int MaxHealth = 100;
        public const int MaxEnergy = 50;


        public CreatureBase()
        {
            health = maxHealth;
            energy = maxEnergy;
            energyRechargeMult = normalRechargeRate;
            hitChanceAddition = normalHitChance;
        }


        // health.
        public virtual int maxHealth { get; } = MaxHealth;
        public int health;


        // energy.
        public virtual int maxEnergy { get; } = MaxEnergy;
        public int energy;


        // energy recharge.
        public virtual float normalRechargeRate { get; } = 1f;
        // float to allow percent muliplication 0.5f = 50%
        public float energyRechargeMult;


        // hit chanace.
        public virtual float normalHitChance { get; } = 0f;
        // how much to reduce the percent chance to hit. rnd <= 80 + hitChanceAddition.
        public float hitChanceAddition;


        // Attack values
        public virtual int attackDamageMin { get; } = 1;
        public virtual int attackDamageMax { get; } = 10;
        public virtual int attackHitChance { get; } = 80;
        public virtual int attackCost { get; } = 5;


        // Special Attack values
        public virtual int specialAttackDamageMin { get; } = 5;
        public virtual int specialAttackDamageMax { get; } = 20;
        public virtual int specialAttackHitChance { get; } = 50;
        public virtual int specialAttackCost { get; } = 20;


        // Re-Charge values
        public virtual int rechargeHitChance { get; } = 10;
        public virtual float rechargeRate { get; } = 4f;


        // Dodge values
        public virtual int dodgeHitChance { get; } = -30;
        public virtual float dodgeEnergyReChargeRate { get; } = 0.5f;


        // Heal values
        public virtual int healCost { get; } = 10;

        // how much energy should we convert.
        public virtual float energyConvertion { get; } = 0.5f;

        public virtual void NewCreature()
        {
            ResetStats();

            ResetMultipliers();
        }

        public virtual void ResetMultipliers()
        {
            // resets the multiplyier 
            energyRechargeMult = normalRechargeRate;
            hitChanceAddition = normalHitChance;
        }

        public virtual void ResetStats()
        {
            // resets the character's health and energy.
            health = maxHealth;
            energy = maxEnergy;
        }

        public virtual void TakeDamage(int damage)
        { 
            health -= damage;
        }

        public virtual void Heal()
        {
            // take the cost from the energy. Do this first or they will be able to heal with less than 10 energy.
            energy -= healCost;

            // add the energy convertion to health.
            health += (int)(energy * energyConvertion);

            // if health exceeds max health then set the health to max health.
            if (health > maxHealth)
            {
                health = maxHealth;
            }

            // we now set the remaining energy
            energy = (int)(energy * energyConvertion);
        }

        public virtual void RechargeEnergy()
        {
            // add the recharge rate multiplues by the recharge multiplyer. 4 * 1 normal, 4 * 0.5f for 
            energy += (int)(rechargeRate * energyRechargeMult);

            // if the energy exceeds the max energy then set energy to max energy.
            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }
        }
    }

    public class StandardCreature : CreatureBase
    {
        // nothing to be added.
        // this is so the game can still create creatures because the creature base is a abstact class and you cannot use it to create new creatures.
    }

    public class BossCreature : CreatureBase
    {
        public override int maxHealth { get; } = 200;

        public override int attackDamageMax => 40;
    }
}
