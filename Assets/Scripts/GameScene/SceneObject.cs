using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneObject : MonoBehaviour
{
    private SceneBackground sceneBackground;
    private SceneHeroBuilder sceneHeroBuilder;
    private ZubexGameControl userControl;
    private GUIManager guiManager;
    private EnemiesGroupManager enemiesGroupManager;

    private int characterLiveCount;
    private Vector3 characterStartingPosition;
    private ZubexGameCharacter character;
    private LevelWavesData wavesData;

    public void Awake() {
        InitVariables();
        initMainObjects();
    }

    public void Start() {
        if (sceneBackground != null) {
            sceneBackground.initBackground(1);
        }

        if (sceneHeroBuilder != null) {
            character = sceneHeroBuilder.buildHero();
            character.HeroDeathEvent += onHeroDeath;
            character.setPosition(characterStartingPosition);
            character.addToScene(gameObject);
            character.activate();

            character.becomeInvisible();

            guiManager.activeWeaponChange(character.getActiveWeaponType());
        }

        if (userControl != null) {
            userControl.OnMoveActionTrigger += userMoveActionCallback;
            userControl.OnWeaponChangeActionTrigger += userChangeWeaponCallback;
        }
        
        if (enemiesGroupManager != null) {            
            enemiesGroupManager.setWavesData(wavesData);            
        }        
    }

    public void Update() {
        if (ScreenHelper.isOutOfScreen(character.transform.position)) {
            onHeroDeath(character);            
        }
    }

    private void userChangeWeaponCallback(bool toNextWeapon) {
        WeaponType newType = WeaponType.NOT_SET;
        if (toNextWeapon) {
            newType = character.nextWeapon();
        } else {
            newType = character.prevWeapon();
        }
        guiManager.activeWeaponChange(newType);
    }

    private void userMoveActionCallback(Vector2 moveVector) {
        character.move(moveVector);
    }

    private void InitVariables() {
        JsonDecoder jsonDecoder = new JsonDecoder();
        wavesData = jsonDecoder.parseWavesData("Level1/Waves/WavesMap");

        characterStartingPosition = ScreenHelper.screenToCameraPosition(new Vector2(Screen.width / 4, Screen.height / 2));

        characterLiveCount = UtilConsts.INITIAL_LIVE_COUNT;
    }

    private void initMainObjects() {
        GameObject guiGameObject = GameObject.FindGameObjectWithTag(GameObjectTags.GUI_MANAGER_TAG);
        if (guiGameObject != null) {
            guiManager = guiGameObject.GetComponent<GUIManager>();
            guiManager.setLiveCount(characterLiveCount);
        }

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

        GameObject enemiesGroupObject = GameObject.FindGameObjectWithTag(GameObjectTags.ENEMIES_BUILDER_TAG);
        if (enemiesGroupObject != null) {
            enemiesGroupManager = enemiesGroupObject.GetComponent<EnemiesGroupManager>();
            enemiesGroupManager.setSceneObject(gameObject);
        }
    }

    private void onHeroDeath(ZubexGameCharacter deadCharacter)
    {
        characterLiveCount--;
        if (characterLiveCount <= 0) {
            SceneManager.LoadSceneAsync(SceneNumbers.GAME_OVER_SCENE_NUMBER);
        } else {
            guiManager.setLiveCount(characterLiveCount);
            deadCharacter.becomeInvisible();
            character.setPosition(characterStartingPosition);
        }        
    }
}
