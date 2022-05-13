using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyGroup : MonoBehaviour
{
    public delegate void GroupDestroyHandler(EnemyGroup group);
    public event GroupDestroyHandler OnGroupDestroy;

    public abstract void AlignEnenmies();    

    public void addToScene(GameObject sceneObject)
    {
        transform.SetParent(sceneObject.transform);        
    }

    protected void destroyGroup()
    {
        OnGroupDestroy?.Invoke(this);
    }
}
