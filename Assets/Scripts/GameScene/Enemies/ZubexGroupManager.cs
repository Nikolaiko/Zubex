using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZubexGroupManager : MonoBehaviour, EnemiesGroupManager
{
    private static string BUILD_GROUP_FUNCTION_NAME = "buildEnemyGroup";

    public ZubexEnemyGroupBuilder groupBuilder;

    private EnemyGroup currentGroup;
    private GameObject groupScene;
    private LevelGroupsData wavesData;
    private int currentGroupIndex;

    private EnemyGroup buildEnemyGroup()
    {
        if (currentGroup != null) {
            Debug.LogWarning("Creating new group before destroying previous");
            Destroy(currentGroup.gameObject);
        }
        currentGroup = groupBuilder.buildEnemyGroup(wavesData.groups[currentGroupIndex]);
        currentGroup.OnGroupDestroy += onGroupDestroy;
        currentGroup.addToScene(groupScene);
        return currentGroup;
    }

    public void setSceneObject(GameObject sceneObject)
    {
        groupScene = sceneObject;
    }

    public void setWavesData(LevelGroupsData data)
    {
        wavesData = data;
        currentGroupIndex = 0;
        Invoke(BUILD_GROUP_FUNCTION_NAME, wavesData.initialDelay);
    }

    private void onGroupDestroy(EnemyGroup group)
    {        
        Destroy(group.gameObject);
        currentGroupIndex += 1;
        Invoke(BUILD_GROUP_FUNCTION_NAME, wavesData.delayBetweenWaves);        
    }
}
