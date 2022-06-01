using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeGroup : EnemyGroup
{
    private static float GROUP_INITIAL_SPEED = 0.7f;

    private float groupYPosition = 0.0f;
    private List<SnakePart> enemiesInGroup = new List<SnakePart>();


    public void AddEnemy(BaseEnemy enemy)
    {
        if (enemiesInGroup.Count < EnemyGroupsConsts.VERTICAL_SNAKE_ENEMIES_COUNT) {
            SnakePart meteorEnemy = enemy as SnakePart;            
            meteorEnemy.transform.SetParent(gameObject.transform);
            enemiesInGroup.Add(meteorEnemy);
        }
    }

    public override void AlignEnenmies()
    {
        if (enemiesInGroup.Count < EnemyGroupsConsts.VERTICAL_SNAKE_ENEMIES_COUNT)
            throw new NotEnougthObjects("Vertical Snake");

        Vector2 enemySize = enemiesInGroup[0].getSize();        
        float pathPart = (ScreenHelper.getScreenWidthInCoors() * 3) / 100;

        transform.position = Vector3.zero;

        Vector3 verticalPosition = Vector3.zero; //new Vector3(ScreenHelper.getLeftScreenBorder(), 0, 0);        

        enemiesInGroup = enemiesInGroup.OrderBy(a => Random.Range(0, 1000)).ToList();
        enemiesInGroup[4].transform.position = verticalPosition;
        
        for (int i = 3; i >= 0; i--) {
            verticalPosition.y += enemySize.y * 1.5f;
            enemiesInGroup[i].transform.position = verticalPosition;            
        }

        verticalPosition = Vector3.zero;
        for (int i = 5; i < enemiesInGroup.Count; i++) {
            verticalPosition.y -= enemySize.y * 1.5f;
            enemiesInGroup[i].transform.position = verticalPosition;            
        }

        float moveDelay = 0.0f;
        for (int i = 0; i < enemiesInGroup.Count; i++) {
            enemiesInGroup[i].startMovingRepeatly(pathPart, moveDelay);
            moveDelay += 0.2f;
        }                  
    }    
}
