using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Required because AbilityComponent is generic
 */
public interface IAbilityComponent
{
    System.Type GetComponentType();
    IAbilityComponent Duplicate();
    void OnCast();
    void OnResolve();
    void HandleEvent(GameEvent gameEvent);
}

/*
 * Base class for all AbilityComponents. Paramaterized by associated AbilityComponentData
 */
public abstract class AbilityComponent<T> : IAbilityComponent
    where T : AbilityComponentData
{
    private T data;
    public T Data { get { return data; } }
    private Ability parentAbility;
    protected Ability ParentAbility { get { return parentAbility; } }

    public AbilityComponent(T data, Ability parentAbility)
    {
        this.data = data;
        this.parentAbility = parentAbility;
    }

    public abstract System.Type GetComponentType();
    public abstract IAbilityComponent Duplicate();
    public virtual void OnCast() { }
    public virtual void OnResolve() { }
    public virtual void HandleEvent(GameEvent gameEvent) { }
}
