using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
    Enumerations used to track tiles' special effects (plus utility routines),
    and the game state when a game is running.
*/

[Flags]
public enum BonusType
{
    None,
    DestroyRowCol
}

public static class BonusTypeUtils
{
    // Return if bonus is the same
    public static bool ContainsDestroyRowCol(BonusType bt)
    {
        return (bt & BonusType.DestroyRowCol) == BonusType.DestroyRowCol;
    }
}

public enum GameState
{
    None,
    Selecting,
    Animating,
    Comboing,
    Bombing,
    Jello,
    Timestopped
}
