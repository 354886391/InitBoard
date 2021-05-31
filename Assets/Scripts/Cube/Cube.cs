using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CubeColor
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
    NUM,
}

public struct CubePosition
{
    public int x;
    public int y;

    public CubePosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public enum ConnectStatus
{
    NONE = -1,
    UNCHECKED = 0,
    CONNECTED,
    NUM,
}

public class Cube : MonoBehaviour
{
    public CubeColor Color;
    public CubePosition Position;
    public Renderer ColorRenderer;
    public Material[] ColorMaterials;

    public void SetUnused()
    {
        SetColor(CubeColor.NONE);
        SetVisable(false);
    }

    public void SetColor(CubeColor color)
    {
        Color = color;
        switch (color)
        {
            case CubeColor.RED:
                ColorRenderer.material = ColorMaterials[(int)color];
                ColorRenderer.material.SetFloat("_BlendRate", 0.0f);
                break;
            case CubeColor.GRAY:
                ColorRenderer.material = ColorMaterials[(int)color];
                ColorRenderer.material.SetFloat("_BlendRate", 1.0f);
                break;
            default:
                if (color >= CubeColor.CYAN && color <= CubeColor.PINK)
                {
                    ColorRenderer.material = ColorMaterials[(int)color];
                    ColorRenderer.material.SetFloat("_BlendRate", 0.0f);
                }
                break;
        }
    }

    public void SetVisable(bool isVisable)
    {
        ColorRenderer.enabled = isVisable;
    }

    public void SetPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

}
