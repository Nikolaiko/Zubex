using UnityEngine;

public class BasicBullet : MonoBehaviour, BasicGameObject
{
    public void activate() {
        gameObject.SetActive(true);
    }

    public void deactivate() {
        gameObject.SetActive(false);
    }

    public void Update() {
        if (ScreenHelper.isOutOfScreen(transform.position)) {
            Destroy(gameObject);
        }
    }
}
