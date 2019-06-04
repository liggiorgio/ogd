using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
    This class contains information about tiles that are about to be moved
    after a collapse or a new tile creation event.
*/

public class AlteredTileInfo
{
    private List<GameObject> newTile { get; set; }
    public int MaxDistance { get; set; }

    public IEnumerable<GameObject> AlteredTile
    {
        get
        {
            return newTile.Distinct();
        }
    }

    public void AddTile(GameObject go)
    {
        if ( !newTile.Contains(go) )
            newTile.Add(go);
    }

    public AlteredTileInfo()
    {
        newTile = new List<GameObject>();
    }
}
