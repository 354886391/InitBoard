using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForAnimDone : CustomYieldInstruction
{
    private AnimNode _animNode;

    public WaitForAnimDone(AnimNode node)
    {
        _animNode = node;       
    }

    public override bool keepWaiting
    {
        get
        {
            if (_animNode.Time < _animNode.Length)
            {
                _animNode.Time += Time.deltaTime;
                return true;
            }
            _animNode.Time = 0;
            return false;
        }

    }
}
