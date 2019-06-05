using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
    This class represents the game grid in the form of a 2D array, with
    routines to encapsulate operations on tiles while playing.
*/

public class TilesArray
{
    private GameObject[,] tiles = new GameObject[Const.Rows, Const.Columns];
    private GameObject backupG1;
    private GameObject backupG2;

    public GameObject this[int row, int col]
    {
        get
        {
            try
            {
                return tiles[row, col];
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        set
        {
            tiles[row, col] = value;
        }
    }

    // Attempt to swap two tiles on the game grid
    public void Swap(GameObject g1, GameObject g2)
    {
        // hold a reference to original tiles
        backupG1 = g1;
        backupG2 = g2;

        var g1Tile = g1.GetComponent<Tile>();
        var g2Tile = g2.GetComponent<Tile>();

        // get array indexes
        int g1Row = g1Tile.Row;
        int g1Column = g1Tile.Column;
        int g2Row = g2Tile.Row;
        int g2Column = g2Tile.Column;

        // swap them in the array
        var temp = tiles[g1Row, g1Column];
        tiles[g1Row, g1Column] = tiles[g2Row, g2Column];
        tiles[g2Row, g2Column] = temp;

        // swap their respective properties
        Tile.SwapColumnRow(g1Tile, g2Tile);
    }

    // Cancel an unsuccessful swap
    public void UndoSwap()
    {
        if ( (backupG1 == null) || (backupG2 == null) )
            throw new System.Exception("BackupIsNullException");

        Swap(backupG1, backupG2);
    }

    // Check for matches in the current grid
    private IEnumerable<GameObject> GetMatchesHorizontally(GameObject go)
    {
        List<GameObject> matches = new List<GameObject>();
        matches.Add(go);
        var tile = go.GetComponent<Tile>();

        // check left
        if ( tile.Column != 0 )
            for ( int column = tile.Column - 1; column >= 0; column-- )
            {
                if ( (tiles[tile.Row, column] != null) && (tiles[tile.Row, column].GetComponent<Tile>().IsSameType(tile)) )
                    matches.Add(tiles[tile.Row, column]);
                else
                    break;
            }

        // check right
        if ( tile.Column != Const.Columns - 1 )
            for ( int column = tile.Column + 1; column < Const.Columns; column++ )
            {
                if ( (tiles[tile.Row, column] != null) && (tiles[tile.Row, column].GetComponent<Tile>().IsSameType(tile)) )
                    matches.Add(tiles[tile.Row, column]);
                else
                    break;
            }

        // we want at least three matches
        if ( matches.Count() < Const.MinimumMatches )
            matches.Clear();

        return matches.Distinct();
    }

    // Check for matches in the current grid
    private IEnumerable<GameObject> GetMatchesVertically(GameObject go)
    {
        List<GameObject> matches = new List<GameObject>();
        matches.Add(go);
        var tile = go.GetComponent<Tile>();

        // check bottom
        if ( tile.Row != 0 )
            for ( int row = tile.Row - 1; row >=0; row-- )
            {
                if ( (tiles[row, tile.Column] != null) && (tiles[row, tile.Column].GetComponent<Tile>().IsSameType(tile)) )
                    matches.Add(tiles[row, tile.Column]);
                else
                    break;
            }

        // check top
        if ( tile.Row != Const.Rows )
            for ( int row = tile.Row + 1; row < Const.Rows; row++ )
            {
                if ( (tiles[row, tile.Column] != null) && (tiles[row, tile.Column].GetComponent<Tile>().IsSameType(tile)) )
                    matches.Add(tiles[row, tile.Column]);
                else
                    break;
            }

        // we want at least three matches
        if ( matches.Count() < Const.MinimumMatches )
            matches.Clear();

        return matches.Distinct();
    }
}
