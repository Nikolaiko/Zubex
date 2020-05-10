using UnityEngine;

public class BackgroundBuilder : MonoBehaviour, SceneBackground
{
    public GameObject frontPartOfBackground;
    public GameObject backPartOfBackground;

    private Renderer frontBackgroundRenderer;

    public void Start() {
        if (frontPartOfBackground != null) {
            frontBackgroundRenderer = frontPartOfBackground.GetComponent<Renderer>();
        }
    }

    public void Update() {
        if (frontBackgroundRenderer != null) {
            frontBackgroundRenderer.material.mainTextureOffset = new Vector2(Time.time * 0.01f, 0f);
        }
    }

    public void initBackground(int levelNumber) {

    }
}
