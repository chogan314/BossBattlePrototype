using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Base class for every ability. Has some number of components that determine ability behavior.
 */
public class Ability
{
    private AbilityData abilityData;
    private Dictionary<System.Type, IAbilityComponent> abilityComponents = new Dictionary<System.Type, IAbilityComponent>();

    private Unit owner;
    public Unit Owner { get { return owner; } }

    // Unique id (useful when a copy of the ability gets cast and put on the stack)
    private System.Guid id;
    public System.Guid ID { get { return id; } }

    public string Name { get { return abilityData.name; } }

    public Ability(AbilityData abilityData, Unit owner)
    {
        this.abilityData = abilityData;
        this.owner = owner;
        id = System.Guid.NewGuid();
    }

    // Called as the ability is put on the stack
    public void OnCast()
    {
        foreach (IAbilityComponent abilityComponent in abilityComponents.Values)
        {
            abilityComponent.OnCast();
        }
    }

    // Called as the ability is taken off of the stack
    public void OnResolve()
    {
        foreach (IAbilityComponent abilityComponent in abilityComponents.Values)
        {
            abilityComponent.OnResolve();
        }
    }

    // Called whenever a GameEvent occurs (see Game, GameEvent)
    public void HandleEvent(GameEvent gameEvent)
    {
        foreach (IAbilityComponent abilityComponent in abilityComponents.Values)
        {
            abilityComponent.HandleEvent(gameEvent);
        }
    }

    public bool HasAbilityComponent<T>() where T : IAbilityComponent
    {
        return abilityComponents.ContainsKey(typeof(T));
    }

    public T GetAbilityComponent<T>() where T : IAbilityComponent
    {
        if (abilityComponents.ContainsKey(typeof(T)))
        {
            return (T) abilityComponents[typeof(T)];
        }
        else
        {
            throw new MissingComponentException("Ability does not have component " + typeof(T).ToString());
        }
    }

    public void AddAbilityComponent<T>() where T : IAbilityComponent { }

    public void AddAbilityComponent(IAbilityComponent abilityComponent)
    {
        abilityComponents.Remove(abilityComponent.GetComponentType());
        abilityComponents.Add(abilityComponent.GetComponentType(), abilityComponent);
    }

    // Duplicates the ability, usually to preserve state on the stack
    public Ability Duplicate()
    {
        Ability copy = new Ability(abilityData, owner);
        foreach (System.Type key in abilityComponents.Keys)
        {
            copy.abilityComponents.Add(key, abilityComponents[key].Duplicate());
        }
        return copy;
    }
}
