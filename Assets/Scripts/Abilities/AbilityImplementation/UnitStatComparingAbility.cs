using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatComparingAbility : AbilityComponent<UnitStatComparisonData>
{
    public UnitStatComparingAbility(UnitStatComparisonData data, Ability parentAbility)
        : base(data, parentAbility)
    { }

    public override System.Type GetComponentType()
    {
        return typeof(UnitStatComparingAbility);
    }

    public override IAbilityComponent Duplicate()
    {
        UnitStatComparingAbility copy = new UnitStatComparingAbility(Data, ParentAbility);
        return copy;
    }

    private delegate bool Comparer(Unit lhs, Unit rhs);

    public Unit GetUnit(List<Unit> units)
    {
        Unit unit = null;

        switch (Data.ComparisonType)
        {
            case ComparisonType.Highest:
                unit = GetUnit(units, (x, y) => x.Health < y.Health);
                break;
            case ComparisonType.Lowest:
                unit = GetUnit(units, (x, y) => x.Health > y.Health);
                break;
            default:
                break;
        }

        return unit;
    }

    private Unit GetUnit(List<Unit> units, Comparer func)
    {
        Unit most = null;
        foreach (Unit unit in units)
        {
            if (most == null || func(most, unit))
            {
                most = unit;
            }
        }
        return most;
    }
}
