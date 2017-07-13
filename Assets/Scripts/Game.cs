using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * TODO: (possibly in the far future)
 *          make this class readable and easier to interact with
 *          game flow should work regardless
 */ 

public class Game : MonoBehaviour
{
    public List<Unit> playerUnits = new List<Unit>();
    public List<Unit> enemyUnits = new List<Unit>();
    private List<Unit> allUnits = new List<Unit>();
    private int activeUnit = -1;
    private Stack<Ability> abilityStack = new Stack<Ability>();
    private int turn = 0;

    void Start()
    {
        foreach (Unit unit in playerUnits)
        {
            allUnits.Add(unit);
        }
        foreach (Unit unit in enemyUnits)
        {
            allUnits.Add(unit);
        }
    }

    void Update()
    {
        EventLoop();
    }

    public void RequestCast(Ability ability)
    {
        ability.OnCast();
        Ability instance = ability.Duplicate();
        abilityStack.Push(instance);

        Debug.Log(instance.Owner.Name + " casts " + instance.Name + "\n");

        SendEvent(new GameEvent(EventType.AbilityCast, instance.Owner, instance));
        SendEvent(new GameEvent(EventType.AbilityPreResolve, instance.Owner, instance));
        Ability toResolve = abilityStack.Pop();
        if (instance.ID != toResolve.ID)
        {
            throw new System.Exception("Attempting to resolve an ability that does not have the same ID as the on originally put on the stack.");
        }
        toResolve.OnResolve();

        Debug.Log(toResolve.Owner.Name + "'s " + toResolve.Name + " resolves.\n");
        Debug.Log(GetUnitHealthTotals());

        SendEvent(new GameEvent(EventType.AbilityPostResolve, toResolve.Owner, toResolve));
    }

    private void EventLoop()
    {
        if (activeUnit < 0)
        {
            Debug.Log("Turn: " + turn + "\n");
            turn++;
            activeUnit = 0;
            SendEvent(new GameEvent(EventType.RoundStart));
        }

        Unit unit = allUnits[activeUnit];
        SendEvent(new GameEvent(EventType.UnitTurnStart, unit));
        SendEvent(new GameEvent(EventType.UnitTurnMainPhase, unit));
        SendEvent(new GameEvent(EventType.UnitTurnEnd, unit));
        if (activeUnit == allUnits.Count - 1)
        {
            activeUnit = -1;
            SendEvent(new GameEvent(EventType.RoundEnd));
        }
        else
        {
            activeUnit++;
        }
    }

    private void SendEvent(GameEvent gameEvent)
    {
        foreach (Unit unit in allUnits)
        {
            unit.HandleEvent(gameEvent);
        }
    }

    public List<Unit> GetAllUnits()
    {
        return allUnits;
    }

    private bool AllUnitsDead(List<Unit> units)
    {
        bool unitAlive = false;

        foreach (Unit unit in units)
        {
            unitAlive = !unit.IsDead() || unitAlive;
        }

        return !unitAlive;
    }

    private bool IsGameOver()
    {
        return AllUnitsDead(playerUnits) || AllUnitsDead(enemyUnits);
    }

    private string GetUnitHealthTotals()
    {
        string toPrint = "";
        foreach (Unit unit in allUnits)
        {
            toPrint += (unit.Name + ": " + unit.Health + " / " + unit.MaxHealth + "\n");
        }
        return toPrint;
    }
}
