using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCannonsGroup : EnemyGroup
{   
    public static int ENEMIES_COUNT = 5;
    private static float GROUP_SPEED = 0.5f;    

    private List<BaseEnemy> enemiesInGroup = new List<BaseEnemy>();

    public void AddEnemy(BaseEnemy enemy)
    {
        if (enemiesInGroup.Count < ENEMIES_COUNT) {
            enemiesInGroup.Add(enemy);
            enemy.transform.SetParent(gameObject.transform);
            enemy.EnemyDieEvent += onEnemyDie;
        }
    }

    override public void AlignEnenmies()
    {
        if (enemiesInGroup.Count < ENEMIES_COUNT) throw new NotEnougthObjects("Static Cannons");

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
