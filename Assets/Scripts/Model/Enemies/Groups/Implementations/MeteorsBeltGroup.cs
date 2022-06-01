using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeteorsBeltGroup : EnemyGroup
{
    private List<Meteor> enemiesInGroup = new List<Meteor>();

    public void AddEnemy(BaseEnemy enemy)
    {
        if (enemiesInGroup.Count < EnemyGroupsConsts.VERTICAL_ENEMIES_COUNT) {
            Meteor meteorEnemy = enemy as Meteor;
            meteorEnemy.EnemyDieEvent += onEnemyDie;
            meteorEnemy.transform.SetParent(gameObject.transform);
            enemiesInGroup.Add(meteorEnemy);            
        }
    }

    public override void AlignEnenmies()
    {
        if (enemiesInGroup.Count < EnemyGroupsConsts.VERTICAL_ENEMIES_COUNT)
            throw new NotEnougthObjects("MeteorsBelt");

        float horizontalStart = ScreenHelper.getRightScreenBorder() + ScreenHelper.getRightScreenBorder() / 2;
        Vector2 size = enemiesInGroup[0].getSize();
        Vector3 startPosition = new Vector3(
            horizontalStart,
            0,
            0
        );
        Vector3 verticalPosition = startPosition;

        enemiesInGroup = enemiesInGroup.OrderBy(a => Random.Range(0, 1000)).ToList();

        enemiesInGroup[0].transform.position = verticalPosition;
        for (int i = 1; i < 5; i++) {
            verticalPosition.y += size.y * 1.3f;
            enemiesInGroup[i].transform.position = verticalPosition;
        }

        verticalPosition = startPosition;
        for (int i = 5; i < enemiesInGroup.Count; i++) {
            verticalPosition.y -= size.y * 1.3f;
            enemiesInGroup[i].transform.position = verticalPosition;
        }

        foreach(Meteor meteor in enemiesInGroup) {
            meteor.fly();
        }
    }

    private void onEnemyDie(BaseEnemy enemy)
    {
        Meteor meteorEnemy = enemy as Meteor;

        if (enemiesInGroup.Contains(meteorEnemy)) {
            enemiesInGroup.Remove(meteorEnemy);
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
