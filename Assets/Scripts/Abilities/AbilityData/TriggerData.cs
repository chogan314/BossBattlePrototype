using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilityComponents/Trigger")]
public class TriggerData : AbilityComponentData
{
    [SerializeField]
    private EventType triggerEvent;
    public EventType TriggerEvent { get { return triggerEvent; } }

    public override IAbilityComponent GenerateAbilityComponent(Ability parentAbility)
    {
        return new TriggeringAbility(this, parentAbility);
    }
}
