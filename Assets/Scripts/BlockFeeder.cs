using System.Collections.Generic;
using UnityEngine;

public class BlockFeeder
{
    private StackBlockControl control;
    /// 开始时需要设置同色4方块排列的数量
    public int ConnectArrowNum = 1;
    /// 当前方块放置每种颜色时的连接数
    protected List<int> connectNums;
    /// 将要出现颜色的候选值
    protected List<Block.COLORTYPE> candidates;

    public void Create(StackBlockControl control)
    {
        this.control = control;
        connectNums = new List<int>();
        for (int i = 0; i < Block.NORMALNUM; i++)
            connectNums.Add(0);
        candidates = new List<Block.COLORTYPE>();
    }

    public Block.COLORTYPE GetNextColorStart(int x, int y)
    {
        var blocks = control.Blocks;
        var checker = control.Checker;
        var orginColor = blocks[x, y].ColorType;
        InitCandidates();
        for (int i = 0; i < candidates.Count; i++)
        {
            checker.ClearAll();
            blocks[x, y].SetColorType(candidates[i]);
            connectNums[i] = checker.CheckConnect(x, y);
        }
        int index;
        if (ConnectArrowNum > 0)
        {
            int maxConnectNum = GetMaxConnectNum();
            EraseCandidatesIfNot(maxConnectNum);
            index = Random.Range(0, candidates.Count);
            if (connectNums[(int)candidates[index]] >= 4)
            {
                --ConnectArrowNum;
            }
        }
        else
        {
            for (int i = candidates.Count - 1; i >= 0; i--)
            {
                if (connectNums[(int)candidates[i]] >= 4)
                {
                    candidates.RemoveAt(i);
                }
            }
            if (candidates.Count == 0)
            {
                InitCandidates();
                Debug.Log("give up");
            }
            int maxConnectNum = GetMaxConnectNum();
            EraseCandidatesIfNot(maxConnectNum);
            index = Random.Range(0, candidates.Count);
        }
        blocks[x, y].SetColorType(orginColor);
        return candidates[index];
    }

    private void InitCandidates()
    {
        candidates.Clear();
        for (int i = 0; i < Block.NORMALNUM; i++)
        {
            candidates.Add((Block.COLORTYPE)i);
        }
    }

    private int GetMaxConnectNum()
    {
        int sel = 0;
        for (int i = 1; i < candidates.Count; i++)
        {
            if (connectNums[(int)candidates[i]] > connectNums[(int)candidates[sel]])
            {
                sel = i;
            }
        }
        return connectNums[(int)candidates[sel]];

        //int max = 0;
        //for (int i = 0; i < Block.NORMALNUM; i++)
        //{
        //    max = System.Math.Max(max, connectNums[i]);
        //}
        //return max;
    }

    private void EraseCandidatesIfNot(int connectNum)
    {
        // ToDo 正反遍历结果不同? -> 原因在 RemoveAt()
        for (int i = candidates.Count - 1; i >= 0; i--)
        {
            if (connectNums[(int)candidates[i]] != connectNum)
            {
                candidates.RemoveAt(i);
            }
        }

        //for (int i = 0; i < candidates.Count; i++)
        //{
        //    if (connectNum != connectNums[(int)candidates[i]])
        //    {
        //        candidates.RemoveAt(i);
        //    }
        //}
    }

    private void EraseColorFromCandidates(Block.COLORTYPE color)
    {
        for (int i = candidates.Count - 1; i >= 0; i--)
        {
            if (candidates[i] == color)
            {
                candidates.RemoveAt(i);
            }
        }
    }
}
