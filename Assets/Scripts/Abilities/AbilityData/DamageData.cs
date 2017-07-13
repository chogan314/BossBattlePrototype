using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Physical,
    Holy,
    Dark,
    Frost,
    Fire,
    Lightning
}

public static class DamageTypeMethods
{
    public static bool IsPhysical(this DamageType damageType)
    {
        return damageType == DamageType.Physical;
    }

    public static bool IsMagical(this DamageType damageType)
    {
        return damageType == DamageType.Holy ||
            damageType == DamageType.Dark ||
            damageType == DamageType.Frost ||
            damageType == DamageType.Fire ||
            damageType == DamageType.Lightning;
    }

    public static bool IsElemental(this DamageType damageType)
    {
        return damageType == DamageType.Frost ||
            damageType == DamageType.Fire ||
            damageType == DamageType.Lightning;
    }
}

[CreateAssetMenu(menuName = "AbilityComponents/Damage")]
public class DamageData : AbilityComponentData
{
    [SerializeField]
    private int damage;
    public int Damage { get { return damage; } }

    [SerializeField]
    private DamageType damageType;
    public DamageType DamageType { get { return damageType; } }

    public override IAbilityComponent GenerateAbilityComponent(Ability parentAbility)
    {
        return new DamagingAbility(this, parentAbility);
    }
}
