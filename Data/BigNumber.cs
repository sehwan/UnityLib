
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class BigNumber
{
    public float n = 0; // number
    public int u = 0; // unit
    public int _ = 1; // Sign

    string GetSuffixText()
    {
        if (u == 0) return string.Empty;
        var unit = this.u - 1;
        var result = string.Empty;
        while (u >= 0)
        {
            result = chars[unit % 52] + result;
            unit /= 52;
            if (unit == 0) break;
            unit--;
        }
        return result;
    }
    static readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();



    void MakeSuffix()
    {
        if (n < 0) _ *= -1;
        n = Mathf.Abs(n);

        while (true)
        {
            if (n >= 1000)
            {
                n /= 1000;
                u++;
            }
            else break;
        }
        while (true)
        {
            if (u <= 0) break;
            if (n < 1)
            {
                n *= 1000;
                u--;
            }
            else break;
        }
    }

    public BigNumber()
    {
        n = 0;
        u = 0;
    }
    public BigNumber(BigNumber v)
    {
        n = v.n;
        u = v.u;
        _ = v._;
    }
    public BigNumber(int v = 0)
    {
        n = v;
        u = 0;
        MakeSuffix();
    }
    public BigNumber(float v = 0)
    {
        n = v;
        u = 0;
        MakeSuffix();
    }
    static public implicit operator BigNumber(int v) => new(v);
    static public implicit operator BigNumber(decimal v) => new(v);
    static public implicit operator BigNumber(double v) => new(v);
    static public implicit operator BigNumber(float v) => new(v);

    static public explicit operator float(BigNumber me)
    {
        for (int i = 0; i < me.u; i++) me.n *= 1000;
        return me.n * me._;
    }

    static public BigNumber operator +(BigNumber a, BigNumber b)
    {
        // 7단계 이상 작다면 사실 21자리 이상 차이므로 무시한다.
        if (a.u > b.u + 7) return new BigNumber(a);
        if (a.u + 7 < b.u) return new BigNumber(b);
        // 아니라면 계산 후 더한다.
        int i;
        BigNumber r = new()
        {
            u = a.u
        };
        float real_a = a.n * a._, real_b = b.n * b._;
        if (a.u > b.u)
        {
            int diff = a.u - b.u;
            for (i = 0; i < diff; i++)
                real_a *= 1000;
            r.u = b.u;    // 작은쪽 단위를 지정한다. 
        }
        else if (a.u < b.u)
        {
            int diff = b.u - a.u;
            for (i = 0; i < diff; i++)
                real_b *= 1000;
            r.u = a.u;
        }
        r.n = real_a + real_b;
        r.MakeSuffix();
        return r;
    }
    static public BigNumber operator -(BigNumber a, BigNumber b)
    {
        if (a.u > b.u + 7) return new BigNumber(a);
        if (a.u + 7 < b.u) return new BigNumber(b);

        int i;
        BigNumber r = new()
        {
            u = a.u
        };
        float real_a = a.n * a._, real_b = b.n * b._;
        if (a.u > b.u)
        {
            int diff = a.u - b.u;
            for (i = 0; i < diff; i++)
                real_a *= 1000;
            r.u = b.u;
        }
        else if (a.u < b.u)
        {
            int diff = b.u - a.u;
            for (i = 0; i < diff; i++)
                real_b *= 1000;
            r.u = a.u;
        }
        r.n = real_a - real_b;
        r.MakeSuffix();
        return r;
    }

    public static BigNumber operator -(BigNumber me)
    {
        BigNumber r = new(me);
        r._ *= -1;
        return r;
    }

    static public BigNumber operator *(BigNumber a, float b)
    {
        BigNumber r = new()
        {
            u = a.u,
            n = a.n * b
        };
        r.MakeSuffix();
        return r;
    }
    static public BigNumber operator *(BigNumber a, BigNumber b)
    {
        BigNumber r = new()
        {
            u = a.u + b.u,
            n = a.n * b.n
        };
        r.MakeSuffix();
        return r;
    }
    static public BigNumber operator /(BigNumber a, float b)
    {
        if (a == 0) return 0;
        if (b == 0) return a;
        BigNumber r = new()
        {
            u = a.u,
            n = a.n / b
        };
        r.MakeSuffix();
        return r;
    }
    static public BigNumber operator /(BigNumber a, BigNumber b)
    {
        if (a == 0) return 0;
        if (b == 0) return a;
        BigNumber r = new()
        {
            u = a.u - b.u
        };

        int diff, i;

        float real_b = b.n;
        if (r.u < 0)
        {
            diff = -r.u;

            for (i = 0; i < diff; i++)
                real_b *= 1000;

            r.u = 0;
        }

        float real_a = a.n;
        if (r.u > 0)
        {
            diff = r.u;
            for (i = 0; i < diff; i++)
                real_a *= 1000;
        }

        r.u = 0;
        r.n = real_a / real_b;
        r.MakeSuffix();
        return r;
    }


    public override int GetHashCode() => n.GetHashCode();
    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (obj is BigNumber)
        {
            BigNumber b = (BigNumber)obj;
            if (b.u != this.u) return false;
            if (b.n != this.n) return false;
            if (b._ != this._) return false;
            return true;
        }
        return false;
    }
    static public bool operator ==(BigNumber a, BigNumber b)
    {
        if (a is null && b is null) return true;
        if (a is null && b is not null) return false;
        if (a is not null && b is null) return false;
        if (a.n == b.n && a.u == b.u && a._ == b._) return true;
        else return false;
    }
    static public bool operator !=(BigNumber a, BigNumber b) => !(a == b);

    static public bool operator <(BigNumber a, BigNumber b)
    {
        if (a._ < b._) return true;
        if (a._ > b._) return false;
        if (a.u < b.u) return true;
        if (a.u > b.u) return false;
        return a.n * a._ < b.n * b._;
    }
    static public bool operator >(BigNumber a, BigNumber b)
    {
        if (a._ > b._) return true;
        if (a._ < b._) return false;
        if (a.u > b.u) return true;
        if (a.u < b.u) return false;
        return a.n * a._ > b.n * b._;
    }
    static public bool operator <(BigNumber a, float b)
    {
        if (a is null) return 0 < b;
        return a < new BigNumber(b);
    }
    static public bool operator >(BigNumber a, float b)
    {
        if (a is null) return 0 > b;
        return a > new BigNumber(b);
    }
    static public bool operator <(BigNumber a, int b)
    {
        if (a is null) return 0 < b;
        return a < new BigNumber(b);
    }
    static public bool operator >(BigNumber a, int b)
    {
        if (a is null) return 0 > b;
        return a > new BigNumber(b);
    }

    static public bool operator <=(BigNumber a, BigNumber b) => (a < b || a == b);
    static public bool operator >=(BigNumber a, BigNumber b) => (a > b || a == b);

    public BigNumber Abs()
    {
        if (_ >= 1) return this;
        BigNumber r = new(this)
        {
            _ = 1
        };
        return r;
    }

    public bool Chance()
    {
        if (this >= 100) return true;
        if (this <= 0) return false;
        return (UnityEngine.Random.value * 100 < (float)this);
    }
    public BigNumber IncreasePercent(float percent)
    {
        n *= 1 + percent * 0.01f;
        MakeSuffix();
        return this;
    }

    public string ToStringForSave() => n.ToString() + GetSuffixText();
    public override string ToString()
    {
        if (u == 0) return (n * _).ToString("n0");
        else return (n * _).ToString("f2") + GetSuffixText();
    }
}