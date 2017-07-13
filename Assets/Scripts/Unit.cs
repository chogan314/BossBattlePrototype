using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    Player,
    Enemy
}

public class Unit : MonoBehaviour
{
    [SerializeField]
    private Game game;
    public Game Game { get { return game; } }

    [SerializeField]
    private Faction faction;

    [SerializeField]
    private string unitName;
    public string Name { get { return unitName; } }

    [SerializeField]
    private int maxHealth;
    public int MaxHealth { get { return maxHealth; } }
    public int Health { get; set; }

    [SerializeField]
    private List<AbilityData> abilityData = new List<AbilityData>();
    private List<Ability> abilities = new List<Ability>();

    private System.Guid id;
    public System.Guid ID { get { return id; } }

    void Start()
    {
        Health = maxHealth;
        id = System.Guid.NewGuid();
        foreach (AbilityData data in abilityData)
        {
            abilities.Add(data.GenerateAbility(this));
        }
    }

    public void HandleEvent(GameEvent gameEvent)
    {
        foreach (Ability ability in abilities)
        {
            ability.HandleEvent(gameEvent);
        }
    }

    public bool IsSameAs(Unit other)
    {
        return id.Equals(other.id);
    }

    public bool IsAllyTo(Unit other)
    {
        return faction == other.faction;
    }

    public bool IsDead()
    {
        return Health <= 0;
    }
}
