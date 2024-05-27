public struct BasicResources
{
    public int m, d;
    
    public BasicResources(int m, int d)
    {
        this.m = m;
        this.d = d;
    }
    public static BasicResources operator +(BasicResources a, BasicResources b)
    {
        var r = new BasicResources
        {
            m = a.m + b.m,
            d = a.d + b.d
        };
        return r;
    }
    public static BasicResources operator -(BasicResources a, BasicResources b)
    {
        var r = new BasicResources
        {
            m = a.m - b.m,
            d = a.d - b.d
        };
        return r;
    }

    public static BasicResources operator *(BasicResources a, int multi)
    {
        var r = new BasicResources
        {
            m = a.m * multi,
            d = a.d * multi
        };
        return r;
    }
    public static BasicResources operator *(BasicResources a, float multi)
    {
        var r = new BasicResources
        {
            m = (int)(a.m * multi),
            d = (int)(a.d * multi)
        };
        return r;
    }
}