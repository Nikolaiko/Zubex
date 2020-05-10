using UnityEngine;

public class SceneObject : MonoBehaviour
{
    private SceneBackground sceneBackground;
    private SceneHeroBuilder sceneHeroBuilder;
    private ZubexGameControl userControl;

    private ZubexGameCharacter character;

    public void Awake() {
        initMainObjects();
    }

    public void Start() {
        if (sceneBackground != null) {
            sceneBackground.initBackground(1);
        }

        if (sceneHeroBuilder != null) {
            character = sceneHeroBuilder.buildHero();
            character.addToScene(gameObject);
            character.activate();
        }

        if (userControl != null) {
            userControl.OnMoveActionTrigger += userMoveActionCallback;
            userControl.OnWeaponChangeActionTrigger += userChangeWeaponCallback;
        }
    }

    private void userChangeWeaponCallback(bool toNextWeapon) {
        Debug.Log("CHANGE WEAPON!");
        if (toNextWeapon) {
            character.nextWeapon();
        } else {
            character.prevWeapon();
        }
    }

    private void userMoveActionCallback(Vector2 moveVector) {
        character.move(moveVector);
    }

    private void initMainObjects() {
        GameObject backgroundObject = GameObject.FindGameObjectWithTag(GameObjectTags.SCENE_BACKGROUND_TAG);
        if (backgroundObject != null) {
            sceneBackground = backgroundObject.GetComponent<SceneBackground>();
        }

        GameObject heroObject = GameObject.FindGameObjectWithTag(GameObjectTags.SCENE_HERO_BUILDER_TAG);
        if (heroObject != null) {
            sceneHeroBuilder = heroObject.GetComponent<SceneHeroBuilder>();
        }

        GameObject controlObject = GameObject.FindGameObjectWithTag(GameObjectTags.SCENE_CONTROL_TAG);
        if (controlObject != null) {            
            userControl = controlObject.GetComponent<ZubexGameControl>();
        }
    }
}
