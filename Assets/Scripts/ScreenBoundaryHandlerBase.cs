using UnityEngine;

public abstract class ScreenBoundaryHandlerBase : MonoBehaviour
{
    private float _screenTopY;
    private float _screenBottomY;
    private float _screenLeftX;
    private float _screenRightX;
    
    private void Start()
    {
        Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        _screenTopY = screenTopRight.y;
        _screenBottomY = screenBottomLeft.y;
        _screenRightX = screenTopRight.x;
        _screenLeftX = screenBottomLeft.x;
    }

    public void CheckScreenBoundaries()
    {
        Vector3 newPosition = transform.position;

        if (transform.position.y > _screenTopY)
            newPosition.y = _screenBottomY;
        
        if (transform.position.y < _screenBottomY)
            newPosition.y = _screenTopY;

        if (transform.position.x > _screenRightX)
            newPosition.x = _screenLeftX;
        
        if (transform.position.x < _screenLeftX)
            newPosition.x = _screenRightX;

        transform.position = newPosition;
    }
}