using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingAbility : AbilityComponent<TargetData>
{
    private Unit target;
    public Unit Target { get { return target; } }

    public TargetingAbility(TargetData targetData, Ability parentAbility)
        : base(targetData, parentAbility)
    { }

    public override System.Type GetComponentType()
    {
        return typeof(TargetingAbility);
    }

    public override IAbilityComponent Duplicate()
    {
        TargetingAbility copy = new TargetingAbility(Data, ParentAbility);
        copy.target = target;
        return copy;
    }

    // calculates target on cast and saves value for later
    public override void OnCast()
    {
        target = GetTarget();
    }

    private List<Unit> GetValidTargets()
    {
        Unit owner = ParentAbility.Owner;
        List<Unit> allUnits = owner.Game.GetAllUnits();
        List<Unit> validTargets = new List<Unit>();

        foreach (Unit unit in allUnits)
        {
            switch (Data.TargetFaction)
            {
                case TargetFaction.Ally:
                    if (unit.IsAllyTo(owner))
                    {
                        validTargets.Add(unit);
                    }
                    break;
                case TargetFaction.AllyNotSelf:
                    if (unit.IsAllyTo(owner) && !unit.IsSameAs(owner))
                    {
                        validTargets.Add(unit);
                    }
                    break;
                case TargetFaction.Self:
                    if (unit.IsSameAs(owner))
                    {
                        validTargets.Add(unit);
                    }
                    break;
                case TargetFaction.Enemy:
                    if (!unit.IsAllyTo(owner))
                    {
                        validTargets.Add(unit);
                    }
                    break;
                case TargetFaction.Any:
                    validTargets.Add(unit);
                    break;
            }
        }

        return validTargets;
    }

    // todo: handle targeting that's not purely random
    private Unit GetTarget()
    {
        List<Unit> validTargets = GetValidTargets();

        if (validTargets.Count == 0)
        {
            throw new UnityException("Ability has no valid targets");
        }

        if (Data.TargetingMethod == TargetingMethod.Random)
        {
            return validTargets[Random.Range(0, validTargets.Count)];
        }

        if (Data.TargetingMethod == TargetingMethod.Derived)
        {
            return ParentAbility.GetAbilityComponent<UnitStatComparingAbility>().GetUnit(validTargets);
        }

        return null;
    }
}
