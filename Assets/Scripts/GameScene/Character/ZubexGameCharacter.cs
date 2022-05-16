using System.Collections.Generic;
using UnityEngine;

public class ZubexGameCharacter : MonoBehaviour, BasicGameObject
{
    private const int SPEED_VALUE = 10;
    private const int STARTING_HEALTH = 0;

    private int health = 0;
    private bool isActive = false;    

    private Rigidbody2D heroBody;
    private WeaponArsenal weaponsArsenal;

    public void Awake() {
        health = STARTING_HEALTH;
        heroBody = GetComponent<Rigidbody2D>();

        weaponsArsenal = GetComponent<WeaponArsenal>();
        weaponsArsenal.initArsenal();
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
        if (isActive == true) {
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
        health -= incomeDamage;
        print("Hero hit");
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
}
