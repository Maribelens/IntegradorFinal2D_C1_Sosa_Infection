using UnityEngine;
public class PlayerAim : MonoBehaviour
{
    public Camera mainCamera;
    [SerializeField] GameManager gameManager;
    void Update()
    {
        if(gameManager.CurrentState == GameManager.GameState.Playing)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
