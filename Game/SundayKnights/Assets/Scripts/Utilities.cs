using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public static class Utilities
{
    // Helper method to animate potential matches
    public static IEnumerator AnimatePotentialMatches(IEnumerable<GameObject> potentialMatches)
    {
        for (float i = 1f; i >= 0.3f; i -= 0.1f)
        {
            foreach (var item in potentialMatches)
            {
                Color c = item.GetComponent<SpriteRenderer>().color;
                c.a = i;
                item.GetComponent<SpriteRenderer>().color = c;
            }
            yield return new WaitForSeconds(Const.OpacityAnimationFrameDelay);
        }
        for (float i = 0.3f; i <= 1f; i += 0.1f)
        {
            foreach (var item in potentialMatches)
            {
                Color c = item.GetComponent<SpriteRenderer>().color;
                c.a = i;
                item.GetComponent<SpriteRenderer>().color = c;
            }
            yield return new WaitForSeconds(Const.OpacityAnimationFrameDelay);
        }
    }

    // Check if a shape is next to another one either horizontally or vertically
    public static bool AreVerticalOrHorizontalNeighbors(Tile s1, Tile s2)
    {
        return (s1.Column == s2.Column ||
                        s1.Row == s2.Row)
                        && Mathf.Abs(s1.Column - s2.Column) <= 1
                        && Mathf.Abs(s1.Row - s2.Row) <= 1;
    }

    // Will check for potential matches vertically and horizontally
    public static IEnumerable<GameObject> GetPotentialMatches(TilesArray tiles)
    {
        // list that will contain all the matches we find
        List<List<GameObject>> matches = new List<List<GameObject>>();

        for (int row = 0; row < Const.Rows; row++)
        {
            for (int column = 0; column < Const.Columns; column++)
            {

                var matches1 = CheckHorizontal1(row, column, tiles);
                var matches2 = CheckHorizontal2(row, column, tiles);
                var matches3 = CheckHorizontal3(row, column, tiles);
                var matches4 = CheckVertical1(row, column, tiles);
                var matches5 = CheckVertical2(row, column, tiles);
                var matches6 = CheckVertical3(row, column, tiles);

                if (matches1 != null) matches.Add(matches1);
                if (matches2 != null) matches.Add(matches2);
                if (matches3 != null) matches.Add(matches3);
                if (matches4 != null) matches.Add(matches4);
                if (matches5 != null) matches.Add(matches5);
                if (matches6 != null) matches.Add(matches6);

                // if we have >= 3 matches, return a random one
                if (matches.Count >= 3)
                    return matches[UnityEngine.Random.Range(0, matches.Count - 1)];

                // if we are in the middle of the calculations/loops
                // and we have less than 3 matches, return a random one
                if(row >= Const.Rows / 2 && matches.Count > 0 && matches.Count <=2)
                    return matches[UnityEngine.Random.Range(0, matches.Count - 1)];
            }
        }
        return null;
    }

    public static List<GameObject> CheckHorizontal1(int row, int column, TilesArray tiles)
    {
        if (column <= Const.Columns - 2)
        {
            if (tiles[row, column].GetComponent<Tile>().
                IsSameType(tiles[row, column + 1].GetComponent<Tile>()))
            {
                if (row >= 1 && column >= 1)
                    if (tiles[row, column].GetComponent<Tile>().
                    IsSameType(tiles[row - 1, column - 1].GetComponent<Tile>()))
                        return new List<GameObject>()
                                {
                                    tiles[row, column],
                                    tiles[row, column + 1],
                                    tiles[row - 1, column - 1]
                                };

                /* example *\
                 * * * * *
                 * * * * *
                 * * * * *
                 * & & * *
                 & * * * *
                \* example  */

                if (row <= Const.Rows - 2 && column >= 1)
                    if (tiles[row, column].GetComponent<Tile>().
                    IsSameType(tiles[row + 1, column - 1].GetComponent<Tile>()))
                        return new List<GameObject>()
                                {
                                    tiles[row, column],
                                    tiles[row, column + 1],
                                    tiles[row + 1, column - 1]
                                };

                /* example *\
                 * * * * *
                 * * * * *
                 & * * * *
                 * & & * *
                 * * * * *
                \* example  */
            }
        }
        return null;
    }

    public static List<GameObject> CheckHorizontal2(int row, int column, TilesArray tiles)
    {
        if (column <= Const.Columns - 3)
        {
            if (tiles[row, column].GetComponent<Tile>().
                IsSameType(tiles[row, column + 1].GetComponent<Tile>()))
            {

                if (row >= 1 && column <= Const.Columns - 3)
                    if (tiles[row, column].GetComponent<Tile>().
                    IsSameType(tiles[row - 1, column + 2].GetComponent<Tile>()))
                        return new List<GameObject>()
                                {
                                    tiles[row, column],
                                    tiles[row, column + 1],
                                    tiles[row - 1, column + 2]
                                };

                /* example *\
                 * * * * *
                 * * * * *
                 * * * * *
                 * & & * *
                 * * * & *
                \* example  */

                if (row <= Const.Rows - 2 && column <= Const.Columns - 3)
                    if (tiles[row, column].GetComponent<Tile>().
                    IsSameType(tiles[row + 1, column + 2].GetComponent<Tile>()))
                        return new List<GameObject>()
                                {
                                    tiles[row, column],
                                    tiles[row, column + 1],
                                    tiles[row + 1, column + 2]
                                };

                /* example *\
                 * * * * *
                 * * * * *
                 * * * & *
                 * & & * *
                 * * * * *
                \* example  */
            }
        }
        return null;
    }

    public static List<GameObject> CheckHorizontal3(int row, int column, TilesArray tiles)
    {
        if (column <= Const.Columns - 4)
        {
            if (tiles[row, column].GetComponent<Tile>().
               IsSameType(tiles[row, column + 1].GetComponent<Tile>()) &&
               tiles[row, column].GetComponent<Tile>().
               IsSameType(tiles[row, column + 3].GetComponent<Tile>()))
            {
                return new List<GameObject>()
                                {
                                    tiles[row, column],
                                    tiles[row, column + 1],
                                    tiles[row, column + 3]
                                };
            }

            /* example *\
              * * * * *
              * * * * *
              * * * * *
              * & & * &
              * * * * *
            \* example  */
        }
        if (column >= 2 && column <= Const.Columns - 2)
        {
            if (tiles[row, column].GetComponent<Tile>().
               IsSameType(tiles[row, column + 1].GetComponent<Tile>()) &&
               tiles[row, column].GetComponent<Tile>().
               IsSameType(tiles[row, column - 2].GetComponent<Tile>()))
            {
                return new List<GameObject>()
                                {
                                    tiles[row, column],
                                    tiles[row, column + 1],
                                    tiles[row, column -2]
                                };
            }

            /* example *\
              * * * * *
              * * * * *
              * * * * *
              * & * & &
              * * * * *
            \* example  */
        }
        return null;
    }

    public static List<GameObject> CheckVertical1(int row, int column, TilesArray tiles)
    {
        if (row <= Const.Rows - 2)
        {
            if (tiles[row, column].GetComponent<Tile>().
                IsSameType(tiles[row + 1, column].GetComponent<Tile>()))
            {
                if (column >= 1 && row >= 1)
                    if (tiles[row, column].GetComponent<Tile>().
                    IsSameType(tiles[row - 1, column - 1].GetComponent<Tile>()))
                        return new List<GameObject>()
                                {
                                    tiles[row, column],
                                    tiles[row + 1, column],
                                    tiles[row - 1, column -1]
                                };

                /* example *\
                  * * * * *
                  * * * * *
                  * & * * *
                  * & * * *
                  & * * * *
                \* example  */

                if (column <= Const.Columns - 2 && row >= 1)
                    if (tiles[row, column].GetComponent<Tile>().
                    IsSameType(tiles[row - 1, column + 1].GetComponent<Tile>()))
                        return new List<GameObject>()
                                {
                                    tiles[row, column],
                                    tiles[row + 1, column],
                                    tiles[row - 1, column + 1]
                                };

                /* example *\
                  * * * * *
                  * * * * *
                  * & * * *
                  * & * * *
                  * * & * *
                \* example  */
            }
        }
        return null;
    }

    public static List<GameObject> CheckVertical2(int row, int column, TilesArray tiles)
    {
        if (row <= Const.Rows - 3)
        {
            if (tiles[row, column].GetComponent<Tile>().
                IsSameType(tiles[row + 1, column].GetComponent<Tile>()))
            {
                if (column >= 1)
                    if (tiles[row, column].GetComponent<Tile>().
                    IsSameType(tiles[row + 2, column - 1].GetComponent<Tile>()))
                        return new List<GameObject>()
                                {
                                    tiles[row, column],
                                    tiles[row + 1, column],
                                    tiles[row + 2, column -1]
                                };

                /* example *\
                  * * * * *
                  & * * * *
                  * & * * *
                  * & * * *
                  * * * * *
                \* example  */

                if (column <= Const.Columns - 2)
                    if (tiles[row, column].GetComponent<Tile>().
                    IsSameType(tiles[row + 2, column + 1].GetComponent<Tile>()))
                        return new List<GameObject>()
                                {
                                    tiles[row, column],
                                    tiles[row+1, column],
                                    tiles[row + 2, column + 1]
                                };

                /* example *\
                  * * * * *
                  * * & * *
                  * & * * *
                  * & * * *
                  * * * * *
                \* example  */

            }
        }
        return null;
    }

    public static List<GameObject> CheckVertical3(int row, int column, TilesArray tiles)
    {
        if (row <= Const.Rows - 4)
        {
            if (tiles[row, column].GetComponent<Tile>().
               IsSameType(tiles[row + 1, column].GetComponent<Tile>()) &&
               tiles[row, column].GetComponent<Tile>().
               IsSameType(tiles[row + 3, column].GetComponent<Tile>()))
            {
                return new List<GameObject>()
                                {
                                    tiles[row, column],
                                    tiles[row + 1, column],
                                    tiles[row + 3, column]
                                };
            }
        }

        /* example *\
          * & * * *
          * * * * *
          * & * * *
          * & * * *
          * * * * *
        \* example  */

        if (row >= 2 && row <= Const.Rows - 2)
        {
            if (tiles[row, column].GetComponent<Tile>().
               IsSameType(tiles[row + 1, column].GetComponent<Tile>()) &&
               tiles[row, column].GetComponent<Tile>().
               IsSameType(tiles[row - 2, column].GetComponent<Tile>()))
            {
                return new List<GameObject>()
                                {
                                    tiles[row, column],
                                    tiles[row + 1, column],
                                    tiles[row - 2, column]
                                };
            }
        }

        /* example *\
          * * * * *
          * & * * *
          * & * * *
          * * * * *
          * & * * *
        \* example  */
        return null;
    }
}
