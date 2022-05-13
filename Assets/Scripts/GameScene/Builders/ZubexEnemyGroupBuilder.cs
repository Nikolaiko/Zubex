using System.Collections.Generic;
using UnityEngine;

public class ZubexEnemyGroupBuilder : MonoBehaviour
{
    public int levelNumber = 0;

    private Dictionary<EnemyType, Object> loadedEnemies = new Dictionary<EnemyType, Object>();


    public EnemyGroup buildEnemyGroup(EnemyGroupType groupType)
    {
        EnemyGroup buildedGroup = null;

        switch(groupType) {
        case EnemyGroupType.STATIC_CANNONS: {
                buildedGroup = buildStaticCannons();
                break;
            }
        case EnemyGroupType.ROCKET_WALL: {
                buildedGroup = buildRocketWall();
                break;
            }
        }
        return buildedGroup;

    }

    private EnemyGroup buildStaticCannons()
    {
        GameObject groupObject = new GameObject("StationarEnemyGroup");
        StaticCannonsGroup group = groupObject.AddComponent<StaticCannonsGroup>();

        for (int i = 0; i < StaticCannonsGroup.ENEMIES_COUNT; i++) {
            GameObject enemyObject = buildEnemy(EnemyType.STATIC_CANNON);
            group.AddEnemy(enemyObject.GetComponent<BaseEnemy>());
        }
        group.AlignEnenmies();

        return group;
    }

    private EnemyGroup buildRocketWall()
    {
        return new StaticCannonsGroup();
    }

    #region Build Enemy Object

    private GameObject buildEnemy(EnemyType enemyType) 
    {
        Object enemyObject;
        
        if (!loadedEnemies.ContainsKey(enemyType)) {
            enemyObject = loadEnemyObject(enemyType);            
            loadedEnemies.Add(enemyType, enemyObject);
        } else {
            enemyObject = loadedEnemies[enemyType];
        }          
        return Instantiate(enemyObject) as GameObject;

    }

    private Object loadEnemyObject(EnemyType enemyType)
    {
        string name = getNameFromType(enemyType);
        string resourcePath = getResourcePath(name);
        Object loadedObject = Resources.Load(resourcePath);

        if (loadedObject == null) throw new ResourceObjectNotFound(resourcePath);
        return loadedObject;
    }

    private string getNameFromType(EnemyType type)
    {
        string enemyName = UtilConsts.EMPTY_STRING;

        switch (type) {
        case EnemyType.STATIC_CANNON:
            enemyName = "StationaryEnemy";
            break;
        case EnemyType.ROCKET_WALL_BRICK:
            enemyName = "RocketEnemy";
            break;
        }
        return enemyName;
    }

    private string getResourcePath(string enemyName)
    {
        return "Level" + levelNumber + "/Enemies/" + enemyName + levelNumber;
    }

    #endregion
}
