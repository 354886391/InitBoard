using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eColor
{
    NONE = -1,
    CYAN = 0,
    YELLOW,
    ORANGE,
    MAGENTA,
    GREEN,
    PINK,

    RED,
    GRAY,
}

public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private Material[] _materials;

    public eColor FirstColor = eColor.CYAN;
    public eColor LastColor = eColor.PINK;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetColor(eColor color)
    {
        switch (color)
        {
            case eColor.RED:
                _renderer.material = _materials[(int)color];
                _renderer.material.SetFloat("_BlendRate", 0.0f);
                break;
            case eColor.GRAY:
                _renderer.material = _materials[(int)color];
                _renderer.material.SetFloat("_BlendRate", 1.0f);
                break;
            default:
                if (color >= FirstColor && color <= LastColor)
                {
                    _renderer.material = _materials[(int)color];
                }
                break;
        }
    }

    public void SetVisible(bool visible)
    {
        _renderer.enabled = visible;
    }


}
