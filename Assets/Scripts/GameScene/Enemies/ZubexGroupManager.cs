using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZubexGroupManager : MonoBehaviour, EnemiesGroupManager
{
    public ZubexEnemyGroupBuilder groupBuilder;

    private EnemyGroup currentGroup;
    private GameObject groupScene;
    private LevelWavesData wavesData;
    private int currentGroupIndex;

    public EnemyGroup buildEnemyGroup(EnemyGroupType groupType)
    {
        if (currentGroup != null) {
            Debug.LogWarning("Creating new group before destroying previous");
            Destroy(currentGroup.gameObject);
        }
        currentGroup = groupBuilder.buildEnemyGroup(wavesData.weaves[currentGroupIndex]);
        currentGroup.OnGroupDestroy += onGroupDestroy;
        currentGroup.addToScene(groupScene);
        return currentGroup;
    }

    public void setSceneObject(GameObject sceneObject)
    {
        groupScene = sceneObject;
    }

    public void setWavesData(LevelWavesData data)
    {
        wavesData = data;
        currentGroupIndex = 0;
    }

    private void onGroupDestroy(EnemyGroup group)
    {
        Destroy(group.gameObject);        
    }
}
