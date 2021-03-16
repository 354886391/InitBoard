using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimNode
{
    public float Time;
    public float Length;
    public UnityAction Callback;
    public AnimNode Next;

    public AnimNode() { }

    public AnimNode(float length, AnimNode next = null)
    {
        Time = 0.0f;
        Length = length;
        Callback = null;
        Next = next;
    }
}

[RequireComponent(typeof(Animation))]
public class AnimationObject : MonoBehaviour
{
    private string[] _animNames;
    [SerializeField]
    private AnimNode _headNode;
    private AnimNode _tailNode;
    private Animation _animation;
    private Queue<AnimNode> _animQueue;

    private void Awake()
    {
        _animation = GetComponent<Animation>();
        _animQueue = new Queue<AnimNode>();
        _animNames = GetAnimationNames();
        _headNode = _tailNode = new AnimNode();
    }

    private System.Collections.IEnumerator RunTimer()
    {
        while (_headNode != null)
        {
            yield return new WaitForAnimDone(_headNode);
            _headNode.Callback?.Invoke();
            _headNode = _headNode.Next;
        }
        yield return null;
    }

    private void AddNode(AnimNode node)
    {
        if (node != null)
        {
            _tailNode.Next = node;
            _tailNode = _tailNode.Next;
        }
    }

    private void RemoveNode()
    {
        if (_headNode.Next != null)
        {
            _headNode = _headNode.Next;
        }
    }

    public AnimationObject Animate(int animIndex)
    {
        AddNode(new AnimNode(GetLength(animIndex)));
        _animation.Play(_animNames[animIndex]);
        return this;
    }

    public AnimationObject NextPlay(int animIndex)
    {
        _headNode.Callback = () => Animate(animIndex);
        return this;
    }

    public AnimationObject OnComplete(UnityAction complete)
    {
        return this;
    }

    #region Utility
    private string[] GetAnimationNames()
    {
        List<string> strList = new List<string>();
        foreach (AnimationState item in _animation)
        {
            strList.Add(item.name);
        }
        return strList.ToArray();
    }

    public AnimationState GetState(int animIndex)
    {
        return _animation[_animNames[animIndex]];
    }

    public AnimationClip GetClip(int animIndex)
    {
        return _animation[_animNames[animIndex]].clip;
    }

    public float GetLength(int animIndex)
    {
        return _animation[_animNames[animIndex]].length;
    }

    public void SetSpeed(int animIndex, float speed = 1.0f)
    {
        _animation[_animNames[animIndex]].speed = speed;
    }

    public bool IsAnimating(int animIndex = 0)
    {
        return _animation.IsPlaying(_animNames[animIndex]);
    }

    public void SetAnimation(int animIndex = 0, float time = 0.0f)
    {
        _animation.Stop();
        GetClip(animIndex).SampleAnimation(gameObject, time);
    }
    #endregion

}

