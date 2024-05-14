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
        var r = new BasicResources();
        r.m = a.m + b.m;
        r.d = a.d + b.d;
        return r;
    }
    public static BasicResources operator -(BasicResources a, BasicResources b)
    {
        var r = new BasicResources();
        r.m = a.m - b.m;
        r.d = a.d - b.d;
        return r;
    }

    public static BasicResources operator *(BasicResources a, int multi)
    {
        var r = new BasicResources();
        r.m = a.m * multi;
        r.d = a.d * multi;
        return r;
    }
    public static BasicResources operator *(BasicResources a, float multi)
    {
        var r = new BasicResources();
        r.m = (int)(a.m * multi);
        r.d = (int)(a.d * multi);
        return r;
    }
}