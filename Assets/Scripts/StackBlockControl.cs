using System.Collections.Generic;
using UnityEngine;

public class StackBlockControl
{
    public GameObject BlockPrefab;

    public const int BlockNumX = 9;
    public const int BlockNumY = 7;

    public BlockFeeder Feeder;
    public ConnectChecker Checker;
    public StackBlock[,] Blocks;


    public void Create(Transform parentTrans)
    {
        Blocks = new StackBlock[BlockNumX, BlockNumY];
        for (int y = 0; y < BlockNumY; y++)
        {
            for (int x = 0; x < BlockNumX; x++)
            {
                var go = Object.Instantiate(BlockPrefab);
                var block = go.GetComponent<StackBlock>();
                block.name = x + " × " + y;
                block.transform.SetParent(parentTrans);
                block.Index.x = x;
                block.Index.y = y;
                block.SetUnused();
                block.SetPosition(CalcBlockPosition(block.Index));
                Blocks[x, y] = block;
            }
        }
        Feeder = new BlockFeeder();
        Checker = new ConnectChecker();
        Feeder.Create(this);
        Checker.Create(Blocks);

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
