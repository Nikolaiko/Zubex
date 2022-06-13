using System.Collections.Generic;
using UnityEngine;

public class ZubexEnemyGroupBuilder : MonoBehaviour
{
    public int levelNumber = 0;

    private Dictionary<EnemyType, Object> loadedEnemies = new Dictionary<EnemyType, Object>();


    public EnemyGroup buildEnemyGroup(EnemyGroupData data)
    {
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
        case EnemyGroupType.METEORS_BELT: {
                buildedGroup = buildMeteorsBelt();
                break;
            }
        case EnemyGroupType.VERTICAL_SNAKE: {
                buildedGroup = buildVerticalSnake();
                break;
            }
        case EnemyGroupType.SINUSOID_LINES: {
                buildedGroup = buildSinusoidalGroup();
                break;
            }
        case EnemyGroupType.DIAGONAL_GROUP: {
                buildedGroup = buildDiagonalGroup(data);
                break;
            }
        case EnemyGroupType.VERTICAL_STATIC_CANNONS: {
                buildedGroup = buildVerticalStaticCannons();
                break;
            }
        case EnemyGroupType.STEP_GROUP: {
                buildedGroup = buildStepEnemyGroup(data);
                break;
            }
        }        
    
        buildedGroup.transform.position = Vector3.zero;
        return buildedGroup;
    }

    private EnemyGroup buildStepEnemyGroup(EnemyGroupData data)
    {
        GameObject groupObject = new GameObject("StepEnemyGroup");
        StepEnemyGroup group = groupObject.AddComponent<StepEnemyGroup>();

        for (int i = 0; i < EnemyGroupsConsts.STEP_ENEMY_COUNT; i++) {
            GameObject enemyObject = buildEnemy(EnemyType.STEP_ENEMY);
            group.setGroupDirection(data.movingDirection);
            group.AddEnemy(enemyObject.GetComponent<BaseEnemy>());
        }
        group.AlignEnenmies();
        return group;
    }

    private EnemyGroup buildVerticalStaticCannons()
    {
        GameObject groupObject = new GameObject("VerticalStationarEnemyGroup");
        VerticalStaticCannonsGroup group = groupObject.AddComponent<VerticalStaticCannonsGroup>();

        for (int i = 0; i < EnemyGroupsConsts.STATIC_VERTICAL_WALL_ENEMIES_COUNT; i++) {
            GameObject enemyObject = buildEnemy(EnemyType.STATIC_CANNON);
            group.AddEnemy(enemyObject.GetComponent<BaseEnemy>());
        }        
        group.AlignEnenmies();
        return group;
    }

    private EnemyGroup buildDiagonalGroup(EnemyGroupData data)
    {
        GameObject groupObject = new GameObject("DiagonalGroup");
        DiagonalGroup group = groupObject.AddComponent<DiagonalGroup>();

        for (int i = 0; i < EnemyGroupsConsts.DIAGONAL_ENEMIES_COUNT; i++) {
            GameObject enemyObject = buildEnemy(EnemyType.DIAGONAL_ENEMY);
            group.AddEnemy(enemyObject.GetComponent<BaseEnemy>());
            group.setGroupDirection(data.movingDirection);
        }
        group.AlignEnenmies();
        return group;
    }

    private EnemyGroup buildSinusoidalGroup()
    {
        GameObject groupObject = new GameObject("SinusoidalGroup");
        SinusoidLinesGroup group = groupObject.AddComponent<SinusoidLinesGroup>();

        for (int i = 0; i < EnemyGroupsConsts.SINUSOIDAL_LINE_ENEMIES_COUNT; i++) {
            GameObject enemyObject = buildEnemy(EnemyType.SINUSOIDAL_PART);
            group.AddEnemy(enemyObject.GetComponent<BaseEnemy>());
        }
        group.AlignEnenmies();
        return group;
    }

    private EnemyGroup buildVerticalSnake()
    {
        GameObject groupObject = new GameObject("VerticalSnake");
        SnakeGroup group = groupObject.AddComponent<SnakeGroup>();

        for (int i = 0; i < EnemyGroupsConsts.VERTICAL_SNAKE_ENEMIES_COUNT; i++) {
            GameObject enemyObject = buildEnemy(EnemyType.VERTICAL_SNAKE_PART);
            group.AddEnemy(enemyObject.GetComponent<BaseEnemy>());
        }
        group.AlignEnenmies();
        return group;
    }

    private EnemyGroup buildMeteorsBelt()
    {
        GameObject groupObject = new GameObject("MeteorsBelt");
        MeteorsBeltGroup group = groupObject.AddComponent<MeteorsBeltGroup>();

        for (int i = 0; i < EnemyGroupsConsts.VERTICAL_ENEMIES_COUNT; i++) {
            GameObject enemyObject = buildEnemy(EnemyType.METEOR);
            group.AddEnemy(enemyObject.GetComponent<BaseEnemy>());
        }       
        group.AlignEnenmies();
        return group;
    }

    private EnemyGroup buildStaticCannons(EnemyGroupData data)
    {
        GameObject groupObject = new GameObject("StationarEnemyGroup");
        StaticCannonsGroup group = groupObject.AddComponent<StaticCannonsGroup>();
        
        for (int i = 0; i < EnemyGroupsConsts.STATIC_WALL_ENEMIES_COUNT; i++) {
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
        for (int i = 0; i < EnemyGroupsConsts.VERTICAL_ENEMIES_COUNT; i++) {
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
        case EnemyType.METEOR:
            enemyName = "Meteor";
            break;
        case EnemyType.VERTICAL_SNAKE_PART:
            enemyName = "SnakePart";
            break;
        case EnemyType.SINUSOIDAL_PART:
            enemyName = "SinusoidalPart";
            break;
        case EnemyType.DIAGONAL_ENEMY:
            enemyName = "DiagonalEnemy";
            break;
        case EnemyType.STEP_ENEMY:
            enemyName = "StepEnemy";
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
