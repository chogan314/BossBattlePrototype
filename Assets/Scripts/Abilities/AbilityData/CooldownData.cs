using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilityComponents/Cooldown")]
public class CooldownData : AbilityComponentData
{
    [SerializeField]
    public int cooldown;
    public int Cooldown { get { return cooldown; } }

    [SerializeField]
    public int initialCooldown;
    public int InitialCooldown { get { return initialCooldown; } }

    public override IAbilityComponent GenerateAbilityComponent(Ability parentAbility)
    {
        return new CooldownAbility(this, parentAbility);
    }
}
