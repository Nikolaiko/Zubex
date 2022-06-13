using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeGroup : EnemyGroup
{
    private static float GROUP_INITIAL_SPEED = 5.0f;
    private static float GROUP_NORMAL_SPEED = 0.7f;

    private bool movingInAttackMode = false;
    private bool movingToAttackPosition = false;
    private Vector3 groupTargetPosition = Vector3.zero;
    private List<SnakePart> enemiesInGroup = new List<SnakePart>();


    public void AddEnemy(BaseEnemy enemy)
    {
        if (enemiesInGroup.Count < EnemyGroupsConsts.VERTICAL_SNAKE_ENEMIES_COUNT) {
            SnakePart snakeEnemy = enemy as SnakePart;
            snakeEnemy.EnemyDieEvent += onEnemyDie;
            snakeEnemy.transform.SetParent(gameObject.transform);
            enemiesInGroup.Add(snakeEnemy);
        }
    }

    public override void AlignEnenmies()
    {
        if (enemiesInGroup.Count < EnemyGroupsConsts.VERTICAL_SNAKE_ENEMIES_COUNT)
            throw new NotEnougthObjects("Vertical Snake");

        Vector2 enemySize = enemiesInGroup[0].getSize();        
        float pathPart = ScreenHelper.getScreenWidthInCoors() / 40;

        Vector3 verticalPosition = Vector3.zero;

        enemiesInGroup = enemiesInGroup.OrderBy(a => Random.Range(0, 1000)).ToList();
        enemiesInGroup[3].transform.position = verticalPosition;
        
        for (int i = 2; i >= 0; i--) {
            verticalPosition.y += enemySize.y * 1.5f;
            enemiesInGroup[i].transform.position = verticalPosition;            
        }

        verticalPosition = Vector3.zero;
        for (int i = 4; i < enemiesInGroup.Count; i++) {
            verticalPosition.y -= enemySize.y * 1.5f;
            enemiesInGroup[i].transform.position = verticalPosition;            
        }

        float moveDelay = 0.0f;
        for (int i = 0; i < enemiesInGroup.Count; i++) {
            enemiesInGroup[i].startMovingRepeatly(pathPart, moveDelay);
            moveDelay += 0.1f;
        }                  
    }

    public override void addToScene(GameObject sceneObject)
    {
        base.addToScene(sceneObject);

        Vector2 enemySize = enemiesInGroup[0].getSize();
        Vector3 startPosition = new Vector3(
            ScreenHelper.getRightScreenBorder() + enemySize.x * 4,
            transform.position.y,
            transform.position.z
        );
        transform.position = startPosition;

        groupTargetPosition = new Vector3(
            ScreenHelper.getRightScreenBorder() -  enemySize.x * 6,
            transform.position.y,
            transform.position.z
        );
      
        movingToAttackPosition = true;      
    }

    public void Update()
    {
        if (movingToAttackPosition) {
            gameObject.transform.Translate(Vector3.left * GROUP_INITIAL_SPEED * Time.deltaTime);
            if (Vector3.Distance(transform.position, groupTargetPosition) <= 0.2f) {
                movingToAttackPosition = false;
                movingInAttackMode = true;
            }
        } else if (movingInAttackMode) {
            gameObject.transform.Translate(Vector3.left * GROUP_NORMAL_SPEED * Time.deltaTime);
        }
    }

    private void onEnemyDie(BaseEnemy enemy)
    {
        SnakePart snakePart = enemy as SnakePart;

        if (enemiesInGroup.Contains(snakePart)) {
            enemiesInGroup.Remove(snakePart);
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
