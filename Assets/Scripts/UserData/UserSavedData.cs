public class UserSavedData : System.Object
{
    public static UserSavedData instance = null;

    public static UserSavedData getInstance() {
        if (instance == null) {
            instance = new UserSavedData();
        }
        return instance;
    }

    public bool haveSavedData() {
        return false;
    }
}
