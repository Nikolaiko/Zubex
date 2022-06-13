using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepEnemyGroup : EnemyGroup
{
    private static float GROUP_SPEED = 0.7f;

    private Vector3 startingPosition = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private List<StepEnemy> enemiesInGroup = new List<StepEnemy>();

    public void setGroupDirection(GroupMovingDirection groupMovingDirection)
    {
        switch(groupMovingDirection) {
        case GroupMovingDirection.UP: {
                direction = Vector3.right + Vector3.up;
                startingPosition = new Vector3(
                    ScreenHelper.getLeftScreenBorder(),
                    ScreenHelper.getBottomScreenBorder(),
                    0
                );
                break;
            }
        case GroupMovingDirection.DOWN: {
                direction = Vector3.right + Vector3.down;
                startingPosition = new Vector3(
                    ScreenHelper.getLeftScreenBorder(),
                    ScreenHelper.getTopScreenBorder(),
                    0
                );
                break;
            }
        }
    }

    public void AddEnemy(BaseEnemy enemy)
    {
        if (enemiesInGroup.Count < EnemyGroupsConsts.STEP_ENEMY_COUNT) {
            StepEnemy stepEnemy = enemy as StepEnemy;
            enemiesInGroup.Add(stepEnemy);
            enemy.transform.SetParent(gameObject.transform);
            enemy.EnemyDieEvent += onEnemyDie;
        }
    }

    override public void AlignEnenmies()
    {
        if (enemiesInGroup.Count < EnemyGroupsConsts.STEP_ENEMY_COUNT)
            throw new NotEnougthObjects("Step Group");

        Vector2 enemySize = enemiesInGroup[0].getSize();
        startingPosition = new Vector3(
            startingPosition.x + enemySize.x,
            startingPosition.y + (enemySize.y / 3) * direction.y,
            startingPosition.z
        );

        foreach(StepEnemy enemy in enemiesInGroup) {
            enemy.transform.position = startingPosition;
            startingPosition.x += enemySize.x;
        }
    }

    public override void addToScene(GameObject sceneObject)
    {
        base.addToScene(sceneObject);

        float delay = 0.5f;
        for (int i = enemiesInGroup.Count - 1; i >= 0; i--) {
            enemiesInGroup[i].startMoving(direction, delay);
            delay += 1.2f;
        }        
    }
    
    private void onEnemyDie(BaseEnemy enemy)
    {
        StepEnemy stepEnemy = enemy as StepEnemy;
        if (enemiesInGroup.Contains(stepEnemy)) {
            enemiesInGroup.Remove(stepEnemy);
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
}
