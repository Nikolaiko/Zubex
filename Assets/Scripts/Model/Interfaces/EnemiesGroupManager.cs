using System;
using UnityEngine;

public interface EnemiesGroupManager
{
    void setSceneObject(GameObject sceneObject);
    void setWavesData(LevelWavesData data);
    EnemyGroup buildEnemyGroup(EnemyGroupType groupType);
}
