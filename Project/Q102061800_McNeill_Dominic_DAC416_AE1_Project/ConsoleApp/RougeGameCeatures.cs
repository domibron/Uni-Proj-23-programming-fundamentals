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


        public const float normalRechargeRate = 1f;
        // float to allow percent muliplication 0.5f = 50%
        public float energyRechargeMult = 1;

        public const float normalHitChance = 1f; // might not need these
        // how much to reduce the percent chance to hit. rnd <= 80 + hitChanceAddition.
        public float hitChanceAddition = 0;


        // Attack
        public const int attackDamageMin = 1;
        public const int attackDamageMax = 10;
        public const int attackHitChance = 80;
        public const int attackCost = 5;

        // Special Attack
        public const int specialAttackDamageMin = 5;
        public const int specialAttackDamageMax = 20;
        public const int specialAttackHitChance = 50;
        public const int specialAttackCost = 20;

        // Re-Charge
        public const int rechargeHitChance = 10;
        public const float rechargeRate = 4f;

        // Dodge
        public const int dodgeHitChance = -30;
        public const float dodgeEnergyReChargeRate = 0.5f;

        // Heal
        public const int healCost = 10;
        // \/ Not used yet.
        public const float energyConvertion = 0.5f;

        public void NewCreature()
        {
            ResetStats();
            //health = maxHealth;
            //energy = maxEnergy;

            ResetMultipliers();
            //energyRechargeMult = 1;
            //hitChanceAddition = 0;
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

        public void TakeDamage(int damage)
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
            energy += (int)(rechargeRate * energyRechargeMult);

            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }
        }
    }
}
