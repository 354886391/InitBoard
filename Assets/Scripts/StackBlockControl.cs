using System.Collections.Generic;
using UnityEngine;

public class StackBlockControl
{
    public GameObject BlockPrefab = null;

    public const int BlockNumX = 9;
    public const int BlockNumY = 7;

    public StackBlock[,] Blocks;
    public bool[] IsColorEnable;
    public ConnectChecker Checker;
    public BlockFeeder Feeder;

    public void Create(Transform parentTrans)
    {
        Blocks = new StackBlock[BlockNumX, BlockNumY];
        for (int y = 0; y < BlockNumY; y++)
        {
            for (int x = 0; x < BlockNumX; x++)
            {
                var go = GameObject.Instantiate(BlockPrefab);
                go.name = x + " × " + y;
                go.transform.SetParent(parentTrans);
                var block = go.GetComponent<StackBlock>();
                block.Index.x = x;
                block.Index.y = y;
                block.SetUnused();
                Blocks[x, y] = block;
                block.transform.position = CalcBlockPosition(block.Index);
            }
        }

        IsColorEnable = new bool[Block.NORMALNUM];
        for (int i = 0; i < Block.NORMALNUM; i++)
            IsColorEnable[i] = true;
        IsColorEnable[(int)Block.COLORTYPE.PINK] = false;
        Checker = new ConnectChecker();
        Checker.Create(Blocks);

        Feeder = new BlockFeeder();
        Feeder.Create(this);

        SetColorToAllBlock();
    }

    public void SetColorToAllBlock()
    {
        var indices = new List<StackBlock.BlockIndex>();
        for (int y = 0; y < BlockNumY; y++)
        {
            for (int x = 0; x < BlockNumX; x++)
            {
                indices.Add(new StackBlock.BlockIndex(x, y));
            }
        }

        //for (int i = 0; i < indices.Count; i++)
        //{
        //    var temp = indices[i];
        //    var index = Random.Range(0, indices.Count);
        //    indices[i] = indices[index];
        //    indices[index] = temp;
        //}

        Feeder.ConnectArrowNum = 1;
        foreach (var index in indices)
        {
            var block = Blocks[index.x, index.y];
            var color = Feeder.GetNextColorStart(index.x, index.y);
            block.SetColorType(color);
            block.SetVisible(true);
        }
    }

    public static Vector3 CalcBlockPosition(StackBlock.BlockIndex index)
    {
        Vector3 position = Vector3.zero;
        position.x = (-(BlockNumX / 2.0f - 0.5f) + index.x) * Block.SizeX;
        position.y = (-(index.y) - 0.5f) * Block.SizeY;
        position.z = 0.0f;
        return position;
    }
}
