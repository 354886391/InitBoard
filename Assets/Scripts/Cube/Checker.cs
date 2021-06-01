using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker
{
    Cube[,] cubes;
    ConnectStatus[,] statuses;
    List<CubePosition> connectedList;


    public void CheckConnect(int x, int y)
    {
        int num = ConnectRecurse(x, y, CubeColor.NONE, 0);

    }

    private int ConnectRecurse(int x, int y, CubeColor prevColor, int connectCount)
    {
        do
        {
            CubePosition position = new CubePosition(x, y);
            if (IsChecked(position)) break; //如果已经检测过break;
            if (statuses[x, y] == ConnectStatus.UNCHECKED) break;
            if (prevColor == CubeColor.NONE || prevColor == cubes[x, y].Color)  //如果当前颜色和前一个颜色相同
            {
                ++connectCount;
                connectedList.Add(position);
                if (x > 0)
                {
                    connectCount = ConnectRecurse(x - 1, y, cubes[x, y].Color, connectCount);
                }
                if (x < StackBlockControl.BlockNumX - 1)
                {
                    connectCount = ConnectRecurse(x + 1, y, cubes[x, y].Color, connectCount);
                }
                if (y > 0)
                {
                    connectCount = ConnectRecurse(x, y - 1, cubes[x, y].Color, connectCount);
                }
                if (y < StackBlockControl.BlockNumY - 1)
                {
                    connectCount = ConnectRecurse(x, y + 1, cubes[x, y].Color, connectCount);
                }
            }


        } while (false);
        return connectCount;
    }

    private bool IsChecked(CubePosition position)
    {

        for (int i = 0; i < connectedList.Count; i++)
        {
            if (connectedList[i].Equals(position))
            {
                return true;
            }
        }
        return false;

    }
}
