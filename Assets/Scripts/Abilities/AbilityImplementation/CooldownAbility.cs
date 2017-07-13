using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownAbility : AbilityComponent<CooldownData>
{
    private int cooldownRemaining;
    public int CooldownRemaining { get { return cooldownRemaining; } }

    public CooldownAbility(CooldownData cooldownData, Ability parentAbility)
        : base(cooldownData, parentAbility)
    {
        cooldownRemaining = cooldownData.initialCooldown;
    }

    public override System.Type GetComponentType()
    {
        return typeof(CooldownAbility);
    }

    public override IAbilityComponent Duplicate()
    {
        CooldownAbility copy = new CooldownAbility(Data, ParentAbility);
        copy.cooldownRemaining = cooldownRemaining;
        return copy;
    }

    // tick down cooldown at start of owning unit's turn
    // maybe reactive abilities' cooldowns could tick down when they're triggered while on cooldown?
    public override void HandleEvent(GameEvent gameEvent)
    {
        if (gameEvent.EventType == EventType.UnitTurnStart
            && gameEvent.Unit.IsSameAs(ParentAbility.Owner))
        {
            cooldownRemaining--;
            if (cooldownRemaining < 0)
            {
                cooldownRemaining = 0;
            }
        }
    }

    public void SetCooldown()
    {
        cooldownRemaining = Data.cooldown;
    }
}
