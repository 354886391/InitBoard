using System.Collections.Generic;
using UnityEngine;

public class BlockFeeder
{
    private StackBlockControl control;
    public int ConnectArrowNum = 1;
    protected List<int> connectNum;
    protected List<Block.COLORTYPE> candidates;

    public void Create(StackBlockControl control)
    {
        this.control = control;
        connectNum = new List<int>();
        candidates = new List<Block.COLORTYPE>();      
        for (int i = 0; i < Block.NORMALNUM; i++)
            connectNum.Add(0);
    }

    public Block.COLORTYPE GetNextColorStart(int x, int y)
    {
        var blocks = control.Blocks;
        var checker = control.Checker;
        var color = blocks[x, y].Color;
        int sel = 0;

        InitCandidates();
        for (int i = 0; i < Block.NORMALNUM; i++)
        {
            blocks[x, y].SetColorType((Block.COLORTYPE)i);
            checker.ClearAll();
            connectNum[i] = checker.CheckConnect(x, y);
        }
        if (ConnectArrowNum > 0)
        {
            int maxNum = GetMaxConnectNum();
            EraseCandidatesIfNot(maxNum);
            sel = Random.Range(0, candidates.Count);
            if (connectNum[(int)candidates[sel]] >= 4)
            {
                --ConnectArrowNum;
            }
        }
        //else
        //{
        //    for (int i = 0; i < candidates.Count; i++)
        //    {
        //        if (connectNum[(int)candidates[i]] >= 4)
        //        {
        //            candidates.RemoveAt(i);
        //        }
        //    }
        //    if (candidates.Count == 0)
        //    {
        //        InitCandidates();
        //    }
        //    int maxNum = GetMaxConnectNum();
        //    EraseCandidatesIfNot(maxNum);
        //    sel = Random.Range(0, candidates.Count);
        //}
        blocks[x, y].SetColorType(color);
        return candidates[sel];
    }

    private void InitCandidates()
    {
        candidates.Clear();
        for (int i = 0; i < Block.NORMALNUM; i++)
        {
            if (!control.IsColorEnable[i])
                continue;
            candidates.Add((Block.COLORTYPE)i);
        }
    }

    private int GetMaxConnectNum()
    {
        int max = 0;
        for (int i = 0; i < Block.NORMALNUM; i++)
        {
            max = System.Math.Max(max, connectNum[i]);
        }
        return max;
    }

    private void EraseCandidatesIfNot(int num)
    {
        for (int i = 0; i < candidates.Count; i++)
        {
            if (connectNum[(int)candidates[i]] != num)
            {
                candidates.RemoveAt(i);
            }
        }
    }

    private void EraseColorFromCandidates(Block.COLORTYPE color)
    {
        for (int i = 0; i < candidates.Count; i++)
        {
            if (color == candidates[i])
            {
                candidates.RemoveAt(i);
            }
        }
    }
}
