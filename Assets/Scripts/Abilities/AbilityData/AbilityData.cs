using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Data class for ability
 * Immutable! so the shared references actually work fine
 */
[CreateAssetMenu(menuName = "Ability")]
public class AbilityData : ScriptableObject
{
    [SerializeField]
    private string abilityName;

    [SerializeField]
    private List<AbilityComponentData> abilityComponentData = new List<AbilityComponentData>();

    public string AbilityName { get { return abilityName; } }

    public Ability GenerateAbility(Unit owner)
    {
        Ability ability = new Ability(this, owner);

        foreach (AbilityComponentData datum in abilityComponentData)
        {
            IAbilityComponent component = datum.GenerateAbilityComponent(ability);
            ability.AddAbilityComponent(component);
        }

        return ability;
    }
}
