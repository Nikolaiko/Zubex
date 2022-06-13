using UnityEngine;

public class ScreenHelper
{
    public static float getScreenWidthInCoors()
    {
        return Mathf.Abs(getLeftScreenBorder()) + Mathf.Abs(getRightScreenBorder());
    }

    public static float getRightScreenBorder()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
    }

    public static float getTopScreenBorder()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
    }

    public static float getBottomScreenBorder()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
    }

    public static float getLeftScreenBorder()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
    }

    public static bool isOutOfScreen(Vector3 position) {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(position);
        return screenPosition.y > Screen.height ||
            screenPosition.y < 0 ||
            screenPosition.x > Screen.width ||
            screenPosition.x < 0;            
    }

    public static bool isOnScreen(Vector3 position)
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(position);
        return screenPosition.y < Screen.height &&
            screenPosition.y > 0 &&
            screenPosition.x < Screen.width &&
            screenPosition.x > 0;
    }

    public static bool isEnemyOutOfScreen(Vector3 position)
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(position);
        return screenPosition.y > Screen.height ||
            screenPosition.y < 0 ||            
            screenPosition.x < 0;
    }


    public static Vector3 screenToCameraPosition(Vector2 screenPosition) {
        Vector3 screenPositionInThreeDimensions = new Vector3(screenPosition.x, screenPosition.y, 0.0f);
        return Camera.main.ScreenToViewportPoint(screenPositionInThreeDimensions);        
    }
}
