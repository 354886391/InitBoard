
public class StackBlock : Block
{
    // 方块的位置
    public struct BlockIndex
    {
        public int x;
        public int y;

        public BlockIndex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public BlockIndex Index;

    public void SetUnused()
    {
        SetColorType(COLORTYPE.NONE);
        SetVisible(false);
    }

    public bool IsConnectable()
    {
        var ret = true;
        if (ColorType < NORMALFIRST || ColorType > NORMALLAST)
        {
            ret = false;
        }
        //if (Index.y >= StackBlockControl.BlockNumY - 1)
        //{
        //    ret = false;
        //}
        return ret;
    }

}
