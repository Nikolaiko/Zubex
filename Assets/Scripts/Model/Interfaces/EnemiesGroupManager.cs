using System;

public interface EnemiesGroupManager
{
    void setWavesData(LevelWavesData data);
    EnemyGroup buildEnemyGroup(EnemyGroupType groupType);
}
