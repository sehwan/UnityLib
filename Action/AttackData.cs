using UnityEngine;
using UnityEditor;
public enum AttackElementalType
{ Normal, Magic }

[System.Serializable]
public class AttackData
{
    public BigNumber atk = new(0);
    public BigNumber mag = new(0);
    public BigNumber acc = new(0);
    public BigNumber cri = new(0);

    public static AttackData operator *(AttackData a, float m)
    {
        if (a == null) return a;
        if (m == 0) return new AttackData();

        AttackData r = new();
        r.atk = a.atk * m;
        r.mag = a.mag * m;
        r.acc = a.acc;
        r.cri = a.cri;
        return r;
    }

    public (BigNumber dmg, bool isCri) GetResisted(ResistData res)
    {
        var ad = new BigNumber((atk * atk / (res.def + 1)));
        var ap = new BigNumber((mag * mag / (res.res + 1)));
        if (atk * 1.2f < ad) ad = atk * 1.2f;
        if (mag * 1.2f < ap) ap = mag * 1.2f;
        var sum = ad + ap;
        if (sum < 1) sum = 1;

        if (res.crr == 0) return (sum, false);
        if ((15 * cri / (res.crr + 1)).Chance() == false) return (sum, false);

        var cr = new BigNumber(1.5f);
        cr += (cri / (res.crr + 1));
        return (sum * cr, true);
    }

    public AttackElementalType BiggestElement()
    {
        return AttackElementalType.Normal;
        // if (atk > mag) return AttackElementalType.Normal;
        // return AttackElementalType.Magic;
    }
    public BigNumber Total()
    {
        return atk + mag;
    }
}


[System.Serializable]
public class ResistData
{
    public BigNumber def;
    public BigNumber res;
    public BigNumber eva;
    public BigNumber crr;
}