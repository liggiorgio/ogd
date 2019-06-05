using System.Collections;
using System.Collections.Generic;
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
}
