using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RocketWallGroup : EnemyGroup
{    
    private static string START_FUNCTION_NAME = "startRockets";

    private List<RocketShip> enemiesInGroup = new List<RocketShip>();  

    public void AddEnemy(BaseEnemy enemy)
    {
        if (enemiesInGroup.Count < EnemyGroupsConsts.VERTICAL_ENEMIES_COUNT) {
            float top = ScreenHelper.getTopScreenBorder();
            float bottom = ScreenHelper.getBottomScreenBorder();
           
            Vector3 initialPosition = new Vector3(
                ScreenHelper.getLeftScreenBorder() + ScreenHelper.getLeftScreenBorder() / 2,
                Random.Range(bottom, top),
                0
            );

            RocketShip rocketShip = enemy as RocketShip;            
            enemiesInGroup.Add(rocketShip);
            rocketShip.transform.SetParent(gameObject.transform);
            rocketShip.EnemyDieEvent += onEnemyDie;
            rocketShip.RocketReadyEvent += onRocketReady;
            rocketShip.transform.position = initialPosition;
        }
    }

    public override void AlignEnenmies()
    {
        if (enemiesInGroup.Count < EnemyGroupsConsts.VERTICAL_ENEMIES_COUNT)
            throw new NotEnougthObjects("RocketWall");

        Vector2 size = enemiesInGroup[0].getSize();
        Vector3 startPosition = new Vector3(ScreenHelper.getLeftScreenBorder() - size.x / 2, 0, 0);
        Vector3 verticalPosition = startPosition;        

        enemiesInGroup = enemiesInGroup.OrderBy(a => Random.Range(0, 1000)).ToList();

        enemiesInGroup[0].goToStartLocation(startPosition);
        for (int i = 1; i < 5; i++) {
            verticalPosition.y += size.y / 2.5f;
            enemiesInGroup[i].goToStartLocation(verticalPosition);
        }

        verticalPosition = startPosition;
        for (int i = 5; i < enemiesInGroup.Count; i++) {
            verticalPosition.y -= size.y / 2.5f;
            enemiesInGroup[i].goToStartLocation(verticalPosition);
        }

    }

    private void onEnemyDie(BaseEnemy enemy)
    {
        RocketShip rocketShip = enemy as RocketShip;

        if (enemiesInGroup.Contains(rocketShip)) {
            enemiesInGroup.Remove(rocketShip);
            enemy.destroyEnemy();
            checkEnemiesInGroup();
        } else {
            throw new WrongEnemy("Trying to delete enemy" + enemy.name + "what not in group : " + name);
        }
    }

    private void checkEnemiesInGroup()
    {
        if (enemiesInGroup.Count == 0) {
            destroyGroup();
        }
    }

    private void onRocketReady()
    {
        int notReadyRockets = enemiesInGroup.FindAll(
            rocket => rocket.isRocketReady() == false
        ).Count;

        if (notReadyRockets == 0) {
            Invoke(START_FUNCTION_NAME, 1.2f);
        }
    }

    private void startRockets()
    {
        float top = ScreenHelper.getTopScreenBorder();
        float bottom = ScreenHelper.getBottomScreenBorder();
        float right = ScreenHelper.getRightScreenBorder();
        Vector2 size = enemiesInGroup[0].getSize();

        float endX = right - size.x;

        foreach (RocketShip ship in enemiesInGroup) {
            ship.attack();
        }
    }
}
