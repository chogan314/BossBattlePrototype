using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingAbility : AbilityComponent<DamageData>
{
    public DamagingAbility(DamageData damageData, Ability parentAbility)
        : base(damageData, parentAbility)
    { }

    public override System.Type GetComponentType()
    {
        return typeof(DamagingAbility);
    }

    public override IAbilityComponent Duplicate()
    {
        DamagingAbility copy = new DamagingAbility(Data, ParentAbility);
        return copy;
    }

    // Damages target selected by TargetingAbility component by value provided by DamageData
    // Interacting directly with the TargetingAbility component is not something that I really want, so this will probably be changed later
    public override void OnResolve()
    {
        Unit target = ParentAbility.GetAbilityComponent<TargetingAbility>().Target;
        target.Health -= Data.Damage;
        if(target.Health < 0)
        {
            target.Health = 0;
        }
    }
}
