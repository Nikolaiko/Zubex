using System.Collections.Generic;
using UnityEngine;

public class ZubexGameCharacter : MonoBehaviour, BasicGameObject
{
    public delegate void HeroDeathHandler(ZubexGameCharacter character);
    public event HeroDeathHandler HeroDeathEvent;

    private SpriteRenderer cocpitRender;
    private SpriteRenderer endgineRender;

    private const int RESPAWN_SPEED_VALUE = 3;
    private const int SPEED_VALUE = 10;
    private const int STARTING_HEALTH = 0;
    private const int TOTAL_INVICIBILTY_TIME = 3;
    private const float INVICIBILTY_BLINK_INTERVAL = 0.15f;
    private const string INVINCIBILITY_STOP_FUNCTION = "stopInvincibilityFrames";

    private int health = 0;
    private float blinkStepTime = 0.0f;
    private float totalBlinkTime = 0.0f;

    private bool isActive = false;
    private bool isRespawning = false;
    private bool isInvincible = false;

    private Rigidbody2D heroBody;
    private WeaponArsenal weaponsArsenal;
    private BoxCollider2D boxCollider;

    private Vector3 characterStartingPosition;
    private Vector3 respawnPosition;
    private Vector3 respawnDirection;

    public void Awake() {
        health = STARTING_HEALTH;
        heroBody = GetComponent<Rigidbody2D>();

        weaponsArsenal = GetComponent<WeaponArsenal>();
        weaponsArsenal.initArsenal();

        boxCollider = GetComponent<BoxCollider2D>();

        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer renderer in spriteRenderers) {
            if (renderer.gameObject.name == "HeroCocpit") {
                cocpitRender = renderer;
            } else if (renderer.gameObject.name == "HeroEngine") {
                endgineRender = renderer;
            }
        }
    }

    public void initPositions(Vector3 respawn, Vector3 start)
    {
        characterStartingPosition = start;
        respawnPosition = respawn;
        respawnDirection = (characterStartingPosition - respawn).normalized;
    }

    public Vector2 getSize()
    {
        return boxCollider.size * transform.lossyScale;
    }

    public void respawnPlayer()
    {
        CancelInvoke(INVINCIBILITY_STOP_FUNCTION);

        transform.position = respawnPosition;
        isRespawning = true;
        isInvincible = true;
    }

    public bool isRespawnInProcess()
    {
        return isRespawning;
    }

    public void Update()
    {
        if (isRespawning) {
            transform.Translate(RESPAWN_SPEED_VALUE * respawnDirection * Time.deltaTime);
            if (Vector3.Distance(characterStartingPosition, transform.position) <= UtilConsts.DISTANCE_ALLOWED_DIFF) {
                isRespawning = false;
                Invoke(INVINCIBILITY_STOP_FUNCTION, TOTAL_INVICIBILTY_TIME);
            }
        }

        if (isInvincible) {
            blinkEffect();
        }    
    }

    public void setPosition(Vector3 newPosition) {
        transform.position = newPosition;
    }

    public void applyWeaponSettings(Dictionary<WeaponType, int> settings) {
        foreach(KeyValuePair<WeaponType, int> weaponData in settings) {
            weaponsArsenal.setWeaponTypeLevel(weaponData.Key, weaponData.Value);
        }
    }

    public void addToScene(GameObject sceneObject) {
        transform.SetParent(sceneObject.transform);
    }

    public void move(Vector2 movingVector) {
        if (isActive == true && !isRespawning) {
            if (movingVector.x > 0.5f || movingVector.x < -0.5f) {
                int moveSpeed = (movingVector.x > 0.5) ? SPEED_VALUE : -SPEED_VALUE;
                heroBody.velocity = new Vector2(moveSpeed, heroBody.velocity.y);
            } else {
                heroBody.velocity = new Vector2(0, heroBody.velocity.y);
            }

            if (movingVector.y > 0.5f || movingVector.y < -0.5f) {
                int moveSpeed = (movingVector.y > 0.5) ? SPEED_VALUE : -SPEED_VALUE;
                heroBody.velocity = new Vector2(heroBody.velocity.x, moveSpeed);
            } else {
                heroBody.velocity = new Vector2(heroBody.velocity.x, 0);
            }
        }           
    }

    public void applyDamage(int incomeDamage)
    {
        if (!isInvincible) {
            health -= incomeDamage;
            if (health <= 0) {
                HeroDeathEvent?.Invoke(this);
                health = STARTING_HEALTH;
            }            
        }        
    }

    public void activate() {
        isActive = true;

        weaponsArsenal.activate();
    }

    public void deactivate() {
        isActive = false;

        weaponsArsenal.deactivate();
        heroBody.velocity = Vector2.zero;        
    }

    public void becomeInvisible()
    {
        isRespawning = true;        
    }

    public WeaponType nextWeapon() {
        return weaponsArsenal.nextWeapon();
    }

    public WeaponType prevWeapon() {
        return weaponsArsenal.prevWeapon();
    }

    public WeaponType getActiveWeaponType()
    {
        return weaponsArsenal.getActiveWeaponType();
    }  

    private void blinkEffect()
    {
        blinkStepTime += Time.deltaTime;
        if (blinkStepTime >= INVICIBILTY_BLINK_INTERVAL) {
            if (endgineRender.enabled) {
                endgineRender.enabled = false;
                cocpitRender.enabled = false;
            } else {
                endgineRender.enabled = true;
                cocpitRender.enabled = true;
            }
            blinkStepTime = 0.0f;
        }   
    }

    private void stopInvincibilityFrames()
    {
        isInvincible = false;
        totalBlinkTime = 0.0f;
        blinkStepTime = 0.0f;

        cocpitRender.enabled = true;
        endgineRender.enabled = true;
    }
}
