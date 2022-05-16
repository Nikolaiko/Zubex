using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCannon : BaseEnemy
{
    private static string SHOT_METHOD_NAME = "shot";

    private int MIN_SHOT_DELAY = 3;
    private int MAX_SHOT_DELAY = 5;

    private int lastShotDelay = UtilConsts.INITIAL_LOW_INT_VALUE;  

    public GameObject bulletInstance;
      
    override public void Update()
    {
        base.Update();
        if (lastShotDelay == UtilConsts.INITIAL_LOW_INT_VALUE && appearedOnScreen) {
            lastShotDelay = Random.Range(MIN_SHOT_DELAY, MAX_SHOT_DELAY);
            Invoke(SHOT_METHOD_NAME, lastShotDelay);
        }
    } 

    private void shot()
    {        
        lastShotDelay = Random.Range(MIN_SHOT_DELAY, MAX_SHOT_DELAY);
        Instantiate(bulletInstance, gameObject.transform);
        Invoke(SHOT_METHOD_NAME, lastShotDelay);
    }
}
