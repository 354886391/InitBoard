using UnityEngine;

public class Block : MonoBehaviour
{
    public enum COLORTYPE
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
        CAKE0,
        CAKE1,
        NUM,
    }

    public const float SizeX = 1.0f;
    public const float SizeY = 1.0f;

    public const int NORMALNUM = (int)COLORTYPE.RED;
    public const COLORTYPE NORMALFIRST = COLORTYPE.CYAN;
    public const COLORTYPE NORMALLAST = COLORTYPE.PINK;
    public const COLORTYPE CAKEFIRST = COLORTYPE.CAKE0;
    public const COLORTYPE CAKELAST = COLORTYPE.CAKE1;

    public Renderer blockRenderer;
    public COLORTYPE ColorType = 0;
    public static Material[] Materials;

    public void SetColorType(COLORTYPE colorType)
    {
        this.ColorType = colorType;
        switch (colorType)
        {
            case COLORTYPE.RED:
                blockRenderer.material = Materials[(int)colorType];
                blockRenderer.material.SetFloat("_BlendRate", 0.0f);
                break;
            case COLORTYPE.GRAY:
                blockRenderer.material = Materials[(int)colorType];
                blockRenderer.material.SetFloat("_BlendRate", 1.0f);
                break;
            case COLORTYPE.CAKE0:
                break;
            default:
                if (colorType >= NORMALFIRST && colorType <= NORMALLAST)
                {
                    blockRenderer.material = Materials[(int)colorType];
                    blockRenderer.material.SetFloat("_BlendRate", 0.0f);
                }
                break;
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetVisible(bool isVisible)
    {
        blockRenderer.enabled = isVisible;
    }

    public bool IsVisible()
    {
        return blockRenderer.enabled;
    }

}
