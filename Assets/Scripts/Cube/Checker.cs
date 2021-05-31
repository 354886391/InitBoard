using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker
{
    Cube[,] cubes;
    CubePosition[] positions;
    ConnectStatus[,] statuses;
    List<CubePosition> connectedList;

    public int IsConnect(int x, int y, CubeColor prevColor, int connectCount)
    {
        do
        {
            CubePosition position = new(x, y);
            if (IsChecked(x, y)) break;
            if (prevColor == CubeColor.NONE || prevColor == cubes[x, y].Color)
            {
                ++connectCount;
                connectedList.Add(position);
                if (x > 0)
                {
                    connectCount = IsConnect(x - 1, y, cubes[x, y].Color, connectCount);
                }
                if (x < StackBlockControl.BlockNumX - 1)
                {
                    connectCount = IsConnect(x + 1, y, cubes[x, y].Color, connectCount);
                }
                if (y > 0)
                {
                    connectCount = IsConnect(x, y - 1, cubes[x, y].Color, connectCount);
                }
                if (y < StackBlockControl.BlockNumY - 1)
                {
                    connectCount = IsConnect(x, y + 1, cubes[x, y].Color, connectCount);
                }
            }


        } while (false);
        return connectCount;
    }

    private bool IsChecked(int x, int y)
    {
        return statuses[x, y] != ConnectStatus.UNCHECKED;
    }
}
