using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo: set up stat system with units
public enum UnitStat
{
    Health
}

public enum ComparisonType
{
    Lowest,
    Highest
}

[CreateAssetMenu(menuName = "AbilityComponents/UnitComparison")]
public class UnitStatComparisonData : AbilityComponentData
{
    [SerializeField]
    private UnitStat unitStat;
    public UnitStat UnitStat { get { return unitStat; } }

    [SerializeField]
    private ComparisonType comparisonType;
    public ComparisonType ComparisonType { get { return comparisonType; } }

    public override IAbilityComponent GenerateAbilityComponent(Ability parentAbility)
    {
        return new UnitStatComparingAbility(this, parentAbility);
    }
}
