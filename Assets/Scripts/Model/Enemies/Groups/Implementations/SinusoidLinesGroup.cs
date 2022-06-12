using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidLinesGroup : EnemyGroup
{
    private List<SinusoidalElement> enemiesInGroup = new List<SinusoidalElement>();

    public void AddEnemy(BaseEnemy enemy)
    {
        if (enemiesInGroup.Count < EnemyGroupsConsts.SINUSOIDAL_LINE_ENEMIES_COUNT) {
            SinusoidalElement element = enemy as SinusoidalElement;
            enemiesInGroup.Add(element);
            element.transform.SetParent(gameObject.transform);
            element.EnemyDieEvent += onEnemyDie;            
        }
    }

    public override void AlignEnenmies()
    {
        
    }

    public override void addToScene(GameObject sceneObject)
    {
        base.addToScene(sceneObject);

        Vector2 enemySize = enemiesInGroup[0].getSize();
        Vector3 startPosition = new Vector3(
            ScreenHelper.getRightScreenBorder() + enemySize.x * 5,
            transform.position.y,
            transform.position.z
        );
        transform.position = startPosition;        

        foreach (SinusoidalElement element in enemiesInGroup) {
            element.transform.position = startPosition;
        }


        int half = EnemyGroupsConsts.SINUSOIDAL_LINE_ENEMIES_COUNT / 2;
        List<SinusoidalElement> upPart = enemiesInGroup.GetRange(0, half);
        List<SinusoidalElement> downPart = enemiesInGroup.GetRange(half, enemiesInGroup.Count - half);

        int maxCount = Math.Max(upPart.Count, downPart.Count);
        float delay = 0.0f;

        for (int index = 0; index < maxCount; index++) {
            if (index < upPart.Count) {
                upPart[index].startMoving(Vector3.up, delay, delay);
            }
            if (index < downPart.Count) {
                downPart[index].startMoving(Vector3.down, delay, delay);
            }
            delay += 0.1f;
        }
    }

    private void onEnemyDie(BaseEnemy enemy)
    {
        SinusoidalElement element = enemy as SinusoidalElement;

        if (enemiesInGroup.Contains(element)) {
            enemiesInGroup.Remove(element);
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
