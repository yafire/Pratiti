using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PratitiBrand
{   
    public static PratitiBrand feather = new PratitiBrand(PratitiBrand.PratitiName.羽毛, 1.1f, 1.1f, 1f, 1f, 1f, 1f, 60);
    public static PratitiBrand pig = new PratitiBrand(PratitiBrand.PratitiName.山豬, 1.1f, 1.1f, 1.1f, 1f, 1f, 1f, 40);

    public PratitiName pratitiName;
    public float atkBuff;
    public float matkBuff;
    public float defBuff;
    public float mdefBuff;
    public float hpBuff;
    public float agiBuff;
    public int sexCoefficient;

    public enum PratitiName
    {
        羽毛,
        山豬,
        山羊,
        幼仔,
        蝸牛,
        雲朵
    }

    public PratitiBrand(PratitiName pratitiName,float atkBuff, float matkBuff, float defBuff, float mdefBuff, float hpBuff, float agiBuff,int sexCoefficient) 
    {
        this.pratitiName = pratitiName;
        this.atkBuff = atkBuff;
        this.matkBuff = matkBuff;
        this.defBuff = defBuff;
        this.mdefBuff = mdefBuff;
        this.hpBuff = hpBuff;
        this.agiBuff = agiBuff;
        this.sexCoefficient = sexCoefficient;
    }
}
