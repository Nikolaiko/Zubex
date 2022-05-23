using System.Collections.Generic;
using UnityEngine;

public class ZubexEnemyGroupBuilder : MonoBehaviour
{
    public int levelNumber = 0;

    private Dictionary<EnemyType, Object> loadedEnemies = new Dictionary<EnemyType, Object>();


    public EnemyGroup buildEnemyGroup(EnemyGroupData data)
    {
        print(data.type);
        EnemyGroup buildedGroup = null;
        switch(data.type) {
        case EnemyGroupType.STATIC_CANNONS: {
                buildedGroup = buildStaticCannons(data);
                break;
            }
        case EnemyGroupType.ROCKET_WALL: {
                buildedGroup = buildRocketWall();
                break;
            }
        }

        buildedGroup.transform.position = Vector3.zero;
        return buildedGroup;
    }

    private EnemyGroup buildStaticCannons(EnemyGroupData data)
    {
        GameObject groupObject = new GameObject("StationarEnemyGroup");
        StaticCannonsGroup group = groupObject.AddComponent<StaticCannonsGroup>();
        
        for (int i = 0; i < StaticCannonsGroup.ENEMIES_COUNT; i++) {
            GameObject enemyObject = buildEnemy(EnemyType.STATIC_CANNON);
            group.AddEnemy(enemyObject.GetComponent<BaseEnemy>());
        }
        group.setGroupXPosition(data.positionY);
        group.AlignEnenmies();        
        return group;
    }

    private EnemyGroup buildRocketWall()
    {     
        GameObject groupObject = new GameObject("RocketWallEnemyGroup");
        RocketWallGroup group = groupObject.AddComponent<RocketWallGroup>();
        for (int i = 0; i < RocketWallGroup.ENEMIES_COUNT; i++) {
            GameObject enemyObject = buildEnemy(EnemyType.ROCKET_WALL_BRICK);
            group.AddEnemy(enemyObject.GetComponent<BaseEnemy>());
        }
        group.AlignEnenmies();

        return group;
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
