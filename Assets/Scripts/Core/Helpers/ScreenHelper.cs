using UnityEngine;

public class ScreenHelper
{
    public static bool isOutOfScreen(Vector3 position) {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(position);
        return screenPosition.y > Screen.height ||
            screenPosition.y < 0 ||
            screenPosition.x > Screen.width ||
            screenPosition.x < 0;            
    }

    public static bool isEnemyOutOfScreen(Vector3 position)
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(position);
        return screenPosition.y > Screen.height ||
            screenPosition.y < 0 ||            
            screenPosition.x < 0;
    }


    public static Vector2 screenToCameraPosition(Vector2 screenPosition) {
        Vector3 screenPositionInThreeDimensions = new Vector3(screenPosition.x, screenPosition.y, 0.0f);
        Vector3 convertedPosition = Camera.main.ScreenToViewportPoint(screenPositionInThreeDimensions);
        return new Vector2(convertedPosition.x, convertedPosition.y);
    }
}
