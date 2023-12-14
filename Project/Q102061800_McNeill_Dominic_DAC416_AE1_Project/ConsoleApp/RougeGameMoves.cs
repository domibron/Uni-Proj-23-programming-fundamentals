using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougeGame.Util;
using RougeGame.GameCreatures;

namespace RougeGame.GameMoves
{

    // used for input to move convertion.
    public enum Moves
    {
        Attack = 1,
        SpecialAttack = 2,
        Recharge = 3,
        Dodge = 4,
        Heal = 5
    }

    // used to play game move.
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

    // all possible move all opponent can play.
    public class RougeGameMoves
    {
        public static void Heal(ref CreatureBase creature)
        {
            // calls the heal function.
            creature.Heal();
        }

        public static void Recharge(ref CreatureBase CreatureRecharging, ref CreatureBase EnemyCreature)
        {
            // increase the enemy's creature hit chance.
            EnemyCreature.hitChanceAddition = CreatureRecharging.rechargeHitChance;

            // increase the creature recharging multiplyer.
            CreatureRecharging.energyRechargeMult = CreatureRecharging.rechargeRate;
        }

        public static void Dodge(ref CreatureBase CreatureDodging, ref CreatureBase EnemyCreature)
        {
            // reduces the enemy's hit change by the dodge hit chance of the creature dodging.
            EnemyCreature.hitChanceAddition = CreatureDodging.dodgeHitChance;

            // reduce engergy for the creature that is dodging.
            CreatureDodging.energyRechargeMult = CreatureDodging.dodgeEnergyReChargeRate;
        }

        // TODO: look into making it work for both attack and special attack.
        public static bool Attack(ref CreatureBase CreatureAttacking, ref CreatureBase EnemyCreature)
        {
            // cost energy. deducts the attack cost from the current creature 's energy.
            CreatureAttacking.energy -= CreatureAttacking.attackCost;

            // random percent.
            int rnd = RougeGameUtil.RandomInt(0, 100);

            // check to see if the value is less than because 80 chance to hit means <= or it will be 20 if >=.
            if (rnd <= CreatureAttacking.attackHitChance + CreatureAttacking.hitChanceAddition)
            {
                // deal random damage within a range.
                EnemyCreature.TakeDamage(RougeGameUtil.RandomInt(CreatureAttacking.attackDamageMin, CreatureAttacking.attackDamageMax));
                // return true as it hit.
                return true;
            }
            else
            {
                // return false as it missed.
                return false;
            }
        }

        public static bool SpecialAttack(CreatureBase CreatureAttacking, CreatureBase EnemyCreature)
        {
            // cost energy. deducts the attack cost from the current creature 's energy.
            CreatureAttacking.energy -= CreatureAttacking.specialAttackCost;

            // random percent. 0% to 100%
            int rnd = RougeGameUtil.RandomInt(0, 100);

            // check to see if the value is less than because 80 chance to hit means <= or it will be 20 if >=.
            if (rnd <= CreatureAttacking.specialAttackHitChance + CreatureAttacking.hitChanceAddition)
            {
                // deal random damage within a range.
                EnemyCreature.TakeDamage(RougeGameUtil.RandomInt(CreatureAttacking.specialAttackDamageMin, CreatureAttacking.specialAttackDamageMax));
                // return true as it hit.
                return true;
            }
            else
            {
                // return false as it missed.
                return false;
            }
        }
    }
}
