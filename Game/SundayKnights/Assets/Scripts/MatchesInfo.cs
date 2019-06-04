using System.Collections;
﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
    This class manages information about tiles that were matched, taking
    into account BonusType for the whole match as well.
*/

public class MatchesInfo
{
    private List<GameObject> matchedTiles;
    public BonusType BonusesContained { get; set; }

    public IEnumerable<GameObject> MatchedTile
    {
        get
        {
            return matchedTiles.Distinct();
        }
    }

    public void AddObject(GameObject go)
    {
        if ( !matchedTiles.Contains(go) )
            matchedTiles.Add(go);
    }

    public void AddObjectRange(IEnumerable<GameObject> gos)
    {
        foreach (var item in gos)
            AddObject(item);
    }

    public MatchesInfo()
    {
        matchedTiles = new List<GameObject>();
        BonusesContained = BonusType.None;
    }
}
