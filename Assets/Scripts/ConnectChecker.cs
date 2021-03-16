using UnityEngine;

public class ConnectChecker
{

    public enum CONNECTSTATUS
    {
        NONE = -1,
        UNCHECKED = 0,
        CONNECTED,
        NUM,
    }

    private StackBlock[,] blocks;
    public CONNECTSTATUS[,] connectStatuses;
    public StackBlock.BlockIndex[] connectBlocks;

    public void Create(StackBlock[,] blocks)
    {
        this.blocks = blocks;
        connectStatuses = new CONNECTSTATUS[StackBlockControl.BlockNumX, StackBlockControl.BlockNumY];
        connectBlocks = new StackBlock.BlockIndex[StackBlockControl.BlockNumX * StackBlockControl.BlockNumY];
    }

    public void ClearAll()
    {
        for (int y = 0; y < StackBlockControl.BlockNumY; y++)
        {
            for (int x = 0; x < StackBlockControl.BlockNumX; x++)
            {
                connectStatuses[x, y] = CONNECTSTATUS.UNCHECKED;
            }
        }
    }

    public int CheckConnect(int x, int y)
    {
        //foreach (var item in blocks)
        //{
        //    Debug.LogError(item.Color);
        //}
        int num = ConnectRecurse(x, y, Block.COLORTYPE.NONE, 0);
        for (int i = 0; i < num; i++)
        {
            var index = connectBlocks[i];
            connectStatuses[index.x, index.y] = CONNECTSTATUS.CONNECTED;
        }
        return num;
    }

    private int ConnectRecurse(int x, int y, Block.COLORTYPE previousColor, int count)
    {
        StackBlock.BlockIndex index;
        do
        {
            //Debug.Log("Count " + count);
            if (count >= StackBlockControl.BlockNumX * StackBlockControl.BlockNumY)
            {
                break;
            }
            //if (!blocks[x, y].IsConnectable())
            //{
            //    break;
            //}
            if (connectStatuses[x, y] == CONNECTSTATUS.CONNECTED)
            {
                break;
            }
            index.x = x;
            index.y = y;
            if (IsChecked(index, count))
            {
                break;
            }
            if (previousColor == Block.COLORTYPE.NONE)
            {
                connectBlocks[0] = index;
                count = 1;
            }
            else if (previousColor == blocks[x, y].ColorType)
            {
                connectBlocks[count] = index;
                ++count;
            }
            //Debug.Log("previousColor " + previousColor + " currentColor " + blocks[x, y].Color);
            if (previousColor == Block.COLORTYPE.NONE || previousColor == blocks[x, y].ColorType)
            {
                if (x > 0)
                {
                    count = ConnectRecurse(x - 1, y, blocks[x, y].ColorType, count);
                }
                if (x < StackBlockControl.BlockNumX - 1)
                {
                    count = ConnectRecurse(x + 1, y, blocks[x, y].ColorType, count);
                }
                if (y > 0)
                {
                    count = ConnectRecurse(x, y - 1, blocks[x, y].ColorType, count);
                }
                if (y < StackBlockControl.BlockNumY - 1)
                {
                    count = ConnectRecurse(x, y + 1, blocks[x, y].ColorType, count);
                }
            }
        } while (false);
        return count;
    }

    private bool IsChecked(StackBlock.BlockIndex index, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (connectBlocks[i].Equals(index))
            {
                return true;
            }
        }
        return false;
    }
}
