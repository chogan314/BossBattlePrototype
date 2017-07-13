using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeringAbility : AbilityComponent<TriggerData>
{
    public TriggeringAbility(TriggerData triggerData, Ability parentAbility)
        : base(triggerData, parentAbility)
    { }

    public override System.Type GetComponentType()
    {
        return typeof(TriggeringAbility);
    }

    public override IAbilityComponent Duplicate()
    {
        TriggeringAbility copy = new TriggeringAbility(Data, ParentAbility);
        return copy;
    }

    // Tries to cast its parent spell if it's off cooldown
    // Doesn't look at all like what it will
    // GameEvents have an associated Unit and an associated Ability
    // This component will need to interact with something that can differentiate units and something that can differentiate abilities
    public override void HandleEvent(GameEvent gameEvent)
    {
        if (gameEvent.EventType == EventType.UnitTurnMainPhase
            && ParentAbility.Owner.IsSameAs(gameEvent.Unit))
        {
            if (ParentAbility.HasAbilityComponent<CooldownAbility>()
                && ParentAbility.GetAbilityComponent<CooldownAbility>().CooldownRemaining == 0)
            {
                ParentAbility.Owner.Game.RequestCast(ParentAbility);
                ParentAbility.GetAbilityComponent<CooldownAbility>().SetCooldown();
            }            
        }
    }
}
