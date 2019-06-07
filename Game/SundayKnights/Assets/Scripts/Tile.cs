﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Tiles are put inside the game grid, and are the objects player interacts
    with to score points. They differ by type, bonus, and other properties.
*/

public class Tile : MonoBehaviour
{
    public string Type { get; set; }
    public BonusType Bonus { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }

    // Init tile
    public Tile() {
        Bonus = BonusType.None;
    }

    // Compare this tile's type with others
    public bool IsSameType(Tile otherTile)
    {
        if ( (otherTile == null) || !(otherTile is Tile) )
            throw new System.ArgumentException("TileException");

        return string.Compare(this.Type, (otherTile as Tile).Type) == 0;
    }

    // Set a tile's properties
    public void Assign(string type, int row, int column)
    {
        if ( string.IsNullOrEmpty(type) )
            throw new System.ArgumentException("TypeException");

        Type = type;
        Row = row;
        Column = column;
    }

    // Swap two tiles by changing their row/col values
    public static void SwapColumnRow(Tile a, Tile b)
    {
        int temp = a.Row;
        a.Row = b.Row;
        b.Row = temp;

        temp = a.Column;
        a.Column = b.Column;
        b.Column = temp;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}