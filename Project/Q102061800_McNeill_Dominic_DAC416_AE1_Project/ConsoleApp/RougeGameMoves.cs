using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougeGame;

namespace RougeGame
{
    public enum Moves
    {
        Attack=1,
        SpecialAttack=2,
        Recharge=3,
        Dodge=4,
        Heal=5
    }

    public struct GameAction
    {
        public Moves action;
        public bool heal;

        public GameAction(Moves action = Moves.Attack, bool healed = false)
        {
            this.action = action;
            this.heal = healed;
        }

        public GameAction()
        {
            this.action = Moves.Attack;
            this.heal = false;
        }
    }

    public class RougeGameMoves
    {
        public static void EnergyRechargeForRound(ref Creature creature, int energyRechargeAmmount = 4)
        {
            creature.energy += (int)(energyRechargeAmmount * creature.energyRechargeMult);

            if (creature.energy > creature.maxEnergy)
            {
                creature.energy = creature.maxEnergy;
            }
        }

        public static void Heal(ref Creature creature, int costToHeal = 10)
        {
            creature.energy -= costToHeal;

            creature.health += creature.energy / 2;

            creature.energy /= 2;

            if (creature.health > creature.maxHealth)
            {
                creature.health = creature.maxHealth;
            }
        }

        public static void Recharge(ref Creature CreatureRecharging, ref Creature EnemyCreature, float additionalHitChance = 10, float energyRechargeMultiplyer = 4)
        {
            EnemyCreature.hitChanceAddition = additionalHitChance;

            CreatureRecharging.energyRechargeMult = energyRechargeMultiplyer;
        }

        public static void Dodge(ref Creature CreatureDodging, ref Creature EnemyCreature, float additionalHitChance = -30, float energyRechargeMultiplyer = 0.5f)
        {
            EnemyCreature.hitChanceAddition = additionalHitChance;

            CreatureDodging.energyRechargeMult = energyRechargeMultiplyer;
        }

        // can be used for both special and normal, as there is variables for all 3 values.
        public static bool Attack(ref Creature CreatureAttacking, ref Creature EnemyCreature, int hitChance = 80, int dmgMin = 1, int dmgMax = 10, int energyCost = 5)
        {
            // cost energy.
            CreatureAttacking.energy -= energyCost;

            // random percent.
            int rnd = RougeGameUtil.RandomInt(0, 100);

            // check to see if the value is less than because 80 chance to hit means <= or it will be 20 if >=.
            if (rnd <= hitChance + CreatureAttacking.hitChanceAddition)
            {
                EnemyCreature.health -= RougeGameUtil.RandomInt(dmgMin, dmgMax);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void ResetMults(ref Creature CreatureToReset)
        {
            // want to add more creatures or have more fights in one game then reset at the start of a new game.
            CreatureToReset.energyRechargeMult = 1;

            CreatureToReset.hitChanceAddition = 0;
        }
    }
}
