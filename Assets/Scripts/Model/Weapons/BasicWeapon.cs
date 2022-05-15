public abstract class BasicWeapon : System.Object, BasicGameObject
{
    private bool isActive = false;
    private int level = 0;

    protected WeaponType weaponType = WeaponType.NOT_SET;

    public void activate() {
        isActive = true;
    }

    public void deactivate() {
        isActive = false;
    }

    public int getLevel() {
        return level;
    }

    public WeaponType getType() {
        return weaponType;
    }

    public void levelDown() {
        level--;
        if (level < 0) {
            level = 0;
        }
    }

    public void levelUp() {
        level++;
    }

    public void setLevel(int levelValue) {
        level = levelValue;
    }

    protected bool isWeaponActive() {
        return isActive;
    }

    abstract public void updateWeapon(float deltaTime);
    abstract public void prepareWeapon();
}
