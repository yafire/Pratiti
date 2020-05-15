using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pratiti
{   //Pratiti基本資料設定
    public static int counter = 0;
    public int index;
    public string pratitiName;
    public PratitiBrand pratitiBrand;
    public int sexDetermination , characteristic , IV;
    public int baseStatistic;
    public int pressure;
    public float maxHp,maxAtk,maxDef,maxMAtk,maxMDef,maxAgi;
    public bool sex;
    public float hp;
    public float atk;
    public float def;
    public float mAtk;
    public float mDef;
    public float agi;

    public Sprite PratitiSprite()
    {
        switch (pratitiBrand.pratitiName)
        {
            case PratitiBrand.PratitiName.山羊: return Resources.Load<Sprite>("PratitiAssets/山羊");
            case PratitiBrand.PratitiName.山豬: return Resources.Load<Sprite>("PratitiAssets/山豬");
            case PratitiBrand.PratitiName.幼仔: return Resources.Load<Sprite>("PratitiAssets/幼仔");
            case PratitiBrand.PratitiName.羽毛: return Resources.Load<Sprite>("PratitiAssets/羽毛");
            case PratitiBrand.PratitiName.蝸牛: return Resources.Load<Sprite>("PratitiAssets/蝸牛");
            case PratitiBrand.PratitiName.雲朵: return Resources.Load<Sprite>("PratitiAssets/雲朵");
            default:
                Debug.Log("error");
                return null;
        }
    }

    public static Sprite PratitiSprite(Pratiti pratiti)
    {
        switch (pratiti.pratitiBrand.pratitiName)
        {
            case PratitiBrand.PratitiName.山羊: return Resources.Load<Sprite>("PratitiAssets/山羊");
            case PratitiBrand.PratitiName.山豬: return Resources.Load<Sprite>("PratitiAssets/山豬");
            case PratitiBrand.PratitiName.幼仔: return Resources.Load<Sprite>("PratitiAssets/幼仔");
            case PratitiBrand.PratitiName.羽毛: return Resources.Load<Sprite>("PratitiAssets/羽毛");
            case PratitiBrand.PratitiName.蝸牛: return Resources.Load<Sprite>("PratitiAssets/蝸牛");
            case PratitiBrand.PratitiName.雲朵: return Resources.Load<Sprite>("PratitiAssets/雲朵");
            default:
                Debug.Log("error");
                return null;
        }
    }
}

    // public void RandomData(PratitiBrand pratitiBrand)
    //     {
    //         int randomSexDetermination;
    //         int randomCharacteristic;
    //         int randomIV;
         
    //         randomIV = Random.Range(1,101);//決定個體值
    //         randomCharacteristic = Random.Range(1,22);//決定性格
    //         randomSexDetermination=Random.Range(1,101);//決定性別,true爲雄性
    //         PratitiInitialize(500,pratitiBrand,randomSexDetermination,randomCharacteristic,randomIV);
    //     }

        // public void PratitiInitialize(int baseStatistic,PratitiBrand pratitiBrand,int sexDetermination,int characteristic,int IV)
        // {
        //     this.pratitiBrand = pratitiBrand;
        //     this.baseStatistic = baseStatistic;
        //     this.sexDetermination = sexDetermination;
        //     this.characteristic = characteristic;
        //     this.IV = IV;
        //     pressure = 0;
        //     if(sexDetermination<=pratitiBrand.sexCoefficient)
        //     {
        //         sex=true;
        //     }
        //     else
        //     {
        //         sex=false;
        //     }    
        //     //計算最大能力值；性格修正；基礎能力值
        //     maxHp = pratitiBrand.hpBuff*baseStatistic*(IV/5+90)/100;
        //     maxAtk = pratitiBrand.atkBuff*baseStatistic*(IV/5+90)/100;
        //     maxDef = pratitiBrand.defBuff*baseStatistic*(IV/5+90)/100;
        //     maxMAtk = pratitiBrand.matkBuff*baseStatistic*(IV/5+90)/100;
        //     maxMDef = pratitiBrand.mdefBuff*baseStatistic*(IV/5+90)/100;
        //     maxAgi = pratitiBrand.agiBuff*baseStatistic*(IV/5+90)/100;
        //     if(characteristic<5){    //性格值1~4 => atkup
        //         maxAtk=maxAtk*11/10;
        //         switch(characteristic/4){
        //             case 1:
        //                 maxDef=maxDef*9/10;
        //                 break;
        //             case 2:
        //                 maxMAtk=maxMAtk*9/10;
        //                 break;
        //             case 3:
        //                 maxMDef=maxMDef*9/10;
        //                 break;
        //             case 4:
        //                 maxAgi=maxAgi*9/10;
        //                 break;
        //         }
        //     }
        //     else if(characteristic<9){  //性格值5~8 => defup；以此類推；性格值==21，則不增減
        //         maxDef=maxDef*11/10;
        //         switch(characteristic%4){
        //             case 1:
        //                 maxAtk=maxAtk*9/10;  
        //                 break;
        //             case 2:
        //                 maxMAtk=maxMAtk*9/10;
        //                 break;
        //             case 3:
        //                 maxMDef=maxMDef*9/10;
        //                 break;
        //             case 4:
        //                 maxAgi=maxAgi*9/10;
        //                 break;
        //         }
        //     }      
        //     else if(characteristic<13){
        //         maxMAtk=maxMAtk*11/10;
        //         switch(characteristic%4){
        //             case 1:
        //                 maxAtk=maxAtk*9/10;
        //                 break;
        //             case 2:
        //                 maxDef=maxDef*9/10;
        //                 break;
        //             case 3:
        //                 maxMDef=maxMDef*9/10;
        //                 break;
        //             case 4:
        //                 maxAgi=maxAgi*9/10;
        //                 break;
        //         }
        //     }
        //     else if(characteristic<17){
        //         maxMDef=maxMDef*11/10;
        //         switch(characteristic%4){
        //             case 1:
        //                 maxAtk=maxAtk*9/10;
        //                 break;
        //             case 2:
        //                 maxDef=maxDef*9/10;
        //                 break;
        //             case 3:
        //                 maxMAtk=maxMAtk*9/10;
        //                 break;
        //             case 4:
        //                 maxAgi=maxAgi*9/10;
        //                 break;
        //         }
        //     }
        //     else if(characteristic<21){     //性格值21不進行修正
        //         maxAgi=maxAgi*11/10;
        //         switch(characteristic%4){
        //             case 1:
        //                 maxAtk=maxAtk*9/10;
        //                 break;
        //             case 2:
        //                 maxDef=maxDef*9/10;
        //                 break;
        //             case 3:
        //                 maxMAtk=maxMAtk*9/10;
        //                 break;
        //             case 4:
        //                 maxMDef=maxMDef*9/10;
        //                 break;
        //         }
        //     }
        //     hp = maxHp/20;
        //     atk = maxAtk/20;
        //     def = maxDef/20;
        //     mAtk = maxMAtk/20;
        //     mDef = maxMDef/20;
        //     agi = maxAgi/20;   

        //     index = Pratiti.counter++;   
        // }


