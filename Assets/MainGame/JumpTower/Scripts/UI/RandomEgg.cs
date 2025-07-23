using UnityEngine;

public class RandomEgg
{
    public static int RandomWithRaito(int i1,int i2, int i3, int i4){
        int ran = Random.Range(0,100);
        if(ran<=i1){
            return 0;
        }
        else if(ran<=i1+i2){
            return 1;
        }
        else if(ran<=i1+i2+i3){
            return 2;
        }
        else if(ran<=i1+i2+i3+i4){
            return 3;
        }
        else{
            return 4;
        }
    } 
}
