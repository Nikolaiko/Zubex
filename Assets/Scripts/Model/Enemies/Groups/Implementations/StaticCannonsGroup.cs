using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCannonsGroup : EnemyGroup
{       
    private static float GROUP_SPEED = 0.7f;    

    private float groupYPosition = 0.0f;
    private List<BaseEnemy> enemiesInGroup = new List<BaseEnemy>();

    public void setGroupXPosition(float y)
    {
        groupYPosition = y;
    }

    public void AddEnemy(BaseEnemy enemy)
    {
        if (enemiesInGroup.Count < EnemyGroupsConsts.STATIC_WALL_ENEMIES_COUNT) {
            enemiesInGroup.Add(enemy);
            enemy.transform.SetParent(gameObject.transform);
            enemy.EnemyDieEvent += onEnemyDie;
        }
    }

    override public void AlignEnenmies()
    {
        if (enemiesInGroup.Count < EnemyGroupsConsts.STATIC_WALL_ENEMIES_COUNT)
            throw new NotEnougthObjects("Static Cannons");

        Vector2 enemySize = enemiesInGroup[0].getSize();

        enemiesInGroup[0].transform.position = Vector3.zero;
        enemiesInGroup[1].transform.position = new Vector3(
            enemySize.x / 2,
            enemySize.y * 1.5f
        );
        enemiesInGroup[2].transform.position = new Vector3(
            -enemySize.x / 2,
            -enemySize.y * 1.5f
        );
        enemiesInGroup[3].transform.position = new Vector3(
            -enemySize.x / 2,
            enemySize.y
        );
        enemiesInGroup[4].transform.position = new Vector3(
            enemySize.x / 2,
            -enemySize.y
        );
    }

    public override void addToScene(GameObject sceneObject)
    {
        base.addToScene(sceneObject);

        float top = ScreenHelper.getTopScreenBorder();
        float bottom = ScreenHelper.getBottomScreenBorder();
        Vector2 enemySize = enemiesInGroup[0].getSize();

        Vector3 startPosition = new Vector3(
            ScreenHelper.getLeftScreenBorder() + enemySize.x,
            bottom + ((top - bottom) * groupYPosition),
            transform.position.z
        );
        transform.position = startPosition;
    }

    public void Update()
    {
        gameObject.transform.Translate(Vector3.left * GROUP_SPEED * Time.deltaTime);
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
