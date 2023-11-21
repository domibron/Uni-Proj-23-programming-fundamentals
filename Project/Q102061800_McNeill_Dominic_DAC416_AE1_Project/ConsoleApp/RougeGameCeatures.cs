using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

    public class CreatureBase
    {
        ////base stuff
        //public const int maxHealth = 100;
        //public const int maxEnergy = 100;

        public const int maxHealth = 100;
        public int health = maxHealth;

        public const int maxEnergy = 50;
        public int energy = maxEnergy;

        // float to allow percent muliplication 0.5f = 50%
        public float energyRechargeMult = 1;
        // how much to reduce the percent chance to hit. rnd <= 80 + hitChanceAddition.
        public float hitChanceAddition = 0;


        // Attack
        public const int attackDmgMin = 1;
        public const int attackDmgMax = 10;
        public const int attackHitChance = 80;
        public const int attackCost = 5;

        // Special Attack
        public const int specialAttackDmgMin = 5;
        public const int specialAttackDmgMax = 20;
        public const int specialAttackHitChance = 50;
        public const int specialAttackCost = 20;

        // Re-Charge
        public const int reChargeHitChance = 10;
        public const float reChargeRate = 4f;

        // Dodge
        public const int dodgeHitChance = -30;
        public const float dodgeEnergyReChargeRate = 0.5f;

        // Heal
        public const int healCost = 10;
        // \/ Not used yet.
        public const float energyConvertion = 0.5f;

        public void NewCreature()
        {
            health = maxHealth;
            energy = maxEnergy;

            energyRechargeMult = 1;
            hitChanceAddition = 0;
        }

        public void ResetMultipliers()
        {
            energyRechargeMult = 1;
            hitChanceAddition = 0;
        }

        public void ResetStats()
        {
            health = maxHealth;
            energy = maxEnergy;
        }

        public void Damage(int damage)
        {
            health -= damage;
        }

        public void Heal()
        {
            energy -= healCost;

            health += energy / 2;

            if (health > maxHealth)
            {
                health = maxHealth;
            }

            energy /= 2;
        }

        public void RechargeEnergy()
        {
            energy += (int)(reChargeRate * energyRechargeMult);

            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }
        }
    }
}
