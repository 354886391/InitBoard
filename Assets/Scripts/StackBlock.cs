public class StackBlock : Block
{
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

    //private void Start()
    //{
    //    transform.position = StackBlockControl.CalcBlockPosition(Index);
    //}

    public void SetUnused()
    {
        SetColorType(COLORTYPE.NONE);
        SetVisible(false);
    }

    public bool IsConnectable()
    {
        var ret = true;
        if (Color < NORMALFIRST || Color > NORMALLAST)
        {
            ret = false;
        }
        if (Index.y >= StackBlockControl.BlockNumY - 1)
        {
            ret = false;
        }
        return ret;
    }

}
