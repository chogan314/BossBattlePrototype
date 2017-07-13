using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    AbilityCast,
    AbilityPreResolve,
    AbilityPostResolve,
    UnitTurnStart,
    UnitTurnMainPhase,
    UnitTurnEnd,
    RoundStart,
    RoundEnd,
}

public class GameEvent
{
    private EventType eventType;
    private Unit unit;
    private Ability ability;

    public EventType EventType { get { return eventType; } }
    public Unit Unit { get { return unit; } }
    public Ability Ability { get { return ability; } }

    public GameEvent(EventType eventType, Unit unit = null, Ability ability = null)
    {
        this.eventType = eventType;
        this.unit = unit;
        this.ability = ability;
    }
}
