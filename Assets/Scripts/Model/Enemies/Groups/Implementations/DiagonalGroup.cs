using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalGroup : EnemyGroup
{
    private Vector3 startingPoint;
    private Vector3 enemiesDirection;
    private List<DiagonalEnemy> enemiesInGroup = new List<DiagonalEnemy>();

    public void setGroupDirection(GroupMovingDirection movingDirection)
    {
        if (movingDirection == GroupMovingDirection.DOWN) {
            startingPoint = new Vector3(
                ScreenHelper.getRightScreenBorder(),
                ScreenHelper.getTopScreenBorder(),
                0
            );

            Vector3 endPoint = new Vector3(
                ScreenHelper.getLeftScreenBorder(),
                ScreenHelper.getBottomScreenBorder(),
                0
            );
            enemiesDirection = (endPoint - startingPoint).normalized;
        } else if (movingDirection == GroupMovingDirection.UP) {
            startingPoint = new Vector3(
                ScreenHelper.getRightScreenBorder(),
                ScreenHelper.getBottomScreenBorder(),
                0
            );
            Vector3 endPoint = new Vector3(
                ScreenHelper.getLeftScreenBorder(),
                ScreenHelper.getTopScreenBorder(),
                0
            );
            enemiesDirection = (endPoint - startingPoint).normalized;
        }        
    }

    public void AddEnemy(BaseEnemy enemy)
    {
        if (enemiesInGroup.Count < EnemyGroupsConsts.DIAGONAL_ENEMIES_COUNT) {

            DiagonalEnemy diagonalShip = enemy as DiagonalEnemy;
            enemiesInGroup.Add(diagonalShip);
            diagonalShip.transform.SetParent(gameObject.transform);
            diagonalShip.EnemyDieEvent += onEnemyDie;            
        }
    }

    public override void AlignEnenmies()
    {
        if (enemiesInGroup.Count < EnemyGroupsConsts.DIAGONAL_ENEMIES_COUNT)
            throw new NotEnougthObjects("Diagonal Enemies");

        float delay = 0.0f;
        Vector2 enemySize = enemiesInGroup[0].getSize();
        foreach(DiagonalEnemy enemy in enemiesInGroup) {
            enemy.transform.position = new Vector3(
                startingPoint.x + enemySize.x,
                startingPoint.y,
                startingPoint.z
            );
            enemy.startMoving(enemiesDirection, delay);
            delay += 0.1f;
        }
    }

    private void onEnemyDie(BaseEnemy enemy)
    {
        DiagonalEnemy diagonalEnemy = enemy as DiagonalEnemy;
        if (enemiesInGroup.Contains(diagonalEnemy)) {
            enemiesInGroup.Remove(diagonalEnemy);
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
