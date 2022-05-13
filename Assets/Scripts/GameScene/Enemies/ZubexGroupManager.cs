using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZubexGroupManager : MonoBehaviour, EnemiesGroupManager
{
    public ZubexEnemyGroupBuilder groupBuilder;

    private EnemyGroup currentGroup;

    public EnemyGroup buildEnemyGroup(EnemyGroupType groupType)
    {
        if (currentGroup != null) {
            Debug.LogWarning("Creating new group before destroying previous");
            Destroy(currentGroup.gameObject);
        }
        currentGroup = groupBuilder.buildEnemyGroup(groupType);
        currentGroup.OnGroupDestroy += onGroupDestroy;
        return currentGroup;
    }

    private void onGroupDestroy(EnemyGroup group)
    {
        Destroy(group.gameObject);        
    }
}
