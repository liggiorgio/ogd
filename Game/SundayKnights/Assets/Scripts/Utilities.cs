using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
    public static bool AreVerticalOrHorizontalNeighbors(Tile s1, Tile s2)
    {
        return ( (s1.Column == s2.Column) || (s1.Row == s2.Row) ) &&
            (Mathf.Abs(s1.Column - s2.Column) <= 1) &&
            (Mathf.Abs(s1.Row - s2.Row) <= 1);
    }
}
