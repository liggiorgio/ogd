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
}
