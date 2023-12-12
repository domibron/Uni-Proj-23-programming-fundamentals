using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougeGame.Util;
using RougeGame.GameCreatures;

namespace RougeGame.GameMoves
{

    public enum Moves
    {
        Attack = 1,
        SpecialAttack = 2,
        Recharge = 3,
        Dodge = 4,
        Heal = 5
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
        // not really needed.
        public static void EnergyRechargeForRound(ref CreatureBase creature, ref CreatureBase otherCreature)
        {
            creature.RechargeEnergy();
            otherCreature.RechargeEnergy();
        }

        public static void Heal(ref CreatureBase creature)
        {
            //creature.energy -= CreatureBase.healCost;

            creature.Heal();
        }

        public static void Recharge(ref CreatureBase CreatureRecharging, ref CreatureBase EnemyCreature)
        {
            EnemyCreature.hitChanceAddition = CreatureRecharging.rechargeHitChance;

            CreatureRecharging.energyRechargeMult = CreatureRecharging.rechargeRate;
        }

        public static void Dodge(ref CreatureBase CreatureDodging, ref CreatureBase EnemyCreature)
        {
            EnemyCreature.hitChanceAddition = CreatureDodging.dodgeHitChance;

            CreatureDodging.energyRechargeMult = CreatureDodging.dodgeEnergyReChargeRate;
        }

        // look into making it work for both attack and special attack. or maybe not.
        public static bool Attack(ref CreatureBase CreatureAttacking, ref CreatureBase EnemyCreature)
        {
            // cost energy.
            CreatureAttacking.energy -= CreatureAttacking.attackCost;

            // random percent.
            int rnd = RougeGameUtil.RandomInt(0, 100);

            // check to see if the value is less than because 80 chance to hit means <= or it will be 20 if >=.
            if (rnd <= CreatureAttacking.attackHitChance + CreatureAttacking.hitChanceAddition)
            {
                EnemyCreature.TakeDamage(RougeGameUtil.RandomInt(CreatureAttacking.attackDamageMin, CreatureAttacking.attackDamageMax));
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool SpecialAttack(CreatureBase CreatureAttacking, CreatureBase EnemyCreature)
        {
            // cost energy.
            CreatureAttacking.energy -= CreatureAttacking.specialAttackCost;

            // random percent.
            int rnd = RougeGameUtil.RandomInt(0, 100);

            // check to see if the value is less than because 80 chance to hit means <= or it will be 20 if >=.
            if (rnd <= CreatureAttacking.specialAttackHitChance + CreatureAttacking.hitChanceAddition)
            {
                EnemyCreature.TakeDamage(RougeGameUtil.RandomInt(CreatureAttacking.specialAttackDamageMin, CreatureAttacking.specialAttackDamageMax));
                return true;
            }
            else
            {
                return false;
            }
        }

        [Obsolete("ResetMults is deprecated, Please use CreatureBase.ResetMultipliers instead.")]
        public static void ResetMults(ref CreatureBase player, ref CreatureBase other)
        {
            // want to add more creatures or have more fights in one game then reset at the start of a new game.
            player.ResetMultipliers();
            other.ResetMultipliers();

            // use the built in one that is attached to the class rather than this.
        }
    }
}
