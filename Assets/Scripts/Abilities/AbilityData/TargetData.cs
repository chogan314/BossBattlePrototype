using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetFaction
{
    Ally,
    AllyNotSelf,
    Self,
    Enemy,
    Any
}

public enum TargetingMethod
{
    Random,
    Derived
}

[CreateAssetMenu(menuName = "AbilityComponents/Targeting")]
public class TargetData : AbilityComponentData
{
    [SerializeField]
    private TargetFaction targetFaction;
    public TargetFaction TargetFaction { get { return targetFaction; } }
    [SerializeField]
    private TargetingMethod targetingMethod;
    public TargetingMethod TargetingMethod { get { return targetingMethod; } }

    public override IAbilityComponent GenerateAbilityComponent(Ability parentAbility)
    {
        return new TargetingAbility(this, parentAbility);
    }
}
