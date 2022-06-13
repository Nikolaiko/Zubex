using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalStaticCannonsGroup : EnemyGroup
{
    private static float GROUP_SPEED = 1.0f;

    private bool isMoving = false;
    private List<BaseEnemy> enemiesInGroup = new List<BaseEnemy>();

    public void AddEnemy(BaseEnemy enemy)
    {
        if (enemiesInGroup.Count < EnemyGroupsConsts.STATIC_VERTICAL_WALL_ENEMIES_COUNT) {
            enemiesInGroup.Add(enemy);
            enemy.transform.SetParent(gameObject.transform);
            enemy.EnemyDieEvent += onEnemyDie;
        }
    }

    override public void AlignEnenmies()
    {
        if (enemiesInGroup.Count < EnemyGroupsConsts.STATIC_VERTICAL_WALL_ENEMIES_COUNT)
            throw new NotEnougthObjects("Static Cannons");

        Vector2 enemySize = enemiesInGroup[0].getSize();        
        Vector3 verticalPosition = new Vector3(
            ScreenHelper.getRightScreenBorder() + enemySize.x * 2,
            0,
            0
        );
        
        enemiesInGroup[4].transform.position = verticalPosition;
        for (int i = 3; i >= 0; i--) {
            verticalPosition.y += enemySize.y * 1.5f;
            enemiesInGroup[i].transform.position = verticalPosition;
        }

        verticalPosition = new Vector3(
            ScreenHelper.getRightScreenBorder() + enemySize.x * 2,
            0,
            0
        );
        for (int i = 5; i < enemiesInGroup.Count; i++) {
            verticalPosition.y -= enemySize.y * 1.5f;
            enemiesInGroup[i].transform.position = verticalPosition;
        }        
    }

    public override void addToScene(GameObject sceneObject)
    {
        base.addToScene(sceneObject);
        isMoving = true;
    }

    public void Update()
    {
        if (isMoving) {
            gameObject.transform.Translate(Vector3.left * GROUP_SPEED * Time.deltaTime);
        }        
    }

    private void onEnemyDie(BaseEnemy enemy)
    {
        if (enemiesInGroup.Contains(enemy)) {
            enemiesInGroup.Remove(enemy);
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
