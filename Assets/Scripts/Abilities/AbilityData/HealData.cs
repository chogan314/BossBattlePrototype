using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilityComponents/Heal")]
public class HealData : AbilityComponentData
{
    [SerializeField]
    private int healAmount;
    public int HealAmount { get { return healAmount; } }

    public override IAbilityComponent GenerateAbilityComponent(Ability parentAbility)
    {
        return new HealingAbility(this, parentAbility);
    }
}
