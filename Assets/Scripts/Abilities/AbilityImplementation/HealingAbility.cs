using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingAbility : AbilityComponent<HealData>
{
    public HealingAbility(HealData data, Ability parentAbility)
        : base(data, parentAbility)
    { }

    public override System.Type GetComponentType()
    {
        return typeof(HealingAbility);
    }

    public override IAbilityComponent Duplicate()
    {
        HealingAbility copy = new HealingAbility(Data, ParentAbility);
        return copy;
    }

    public override void OnResolve()
    {
        Unit target = ParentAbility.GetAbilityComponent<TargetingAbility>().Target;
        target.Health += Data.HealAmount;
        if (target.Health > target.MaxHealth)
        {
            target.Health = target.MaxHealth;
        }
    }
}
