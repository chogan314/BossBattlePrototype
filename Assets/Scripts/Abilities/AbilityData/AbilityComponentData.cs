using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityComponentData : ScriptableObject
{
    public abstract IAbilityComponent GenerateAbilityComponent(Ability parentAbility);
}