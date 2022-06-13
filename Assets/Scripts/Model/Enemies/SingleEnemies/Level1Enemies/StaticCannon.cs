using UnityEngine;

public class StaticCannon : BaseEnemy
{
    private static string SHOT_METHOD_NAME = "shot";

    private int MIN_SHOT_DELAY = 1;
    private int MAX_SHOT_DELAY = 4;

    private int lastShotDelay = UtilConsts.INITIAL_LOW_INT_VALUE;  

    public GameObject bulletInstance;

    public void Awake()
    {
        health = MachineGunBullet.DAMAGE * 2;
    }

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
        Instantiate(bulletInstance, transform.position, new Quaternion(), bulletsParent);
        Invoke(SHOT_METHOD_NAME, lastShotDelay);
    }
}
