using UnityEngine;
using UnityEngine.InputSystem;


public class Wallet : MonoBehaviour
{
    private Collider walletCollider3D;
    private Collider2D walletCollider2D;

    public float walletValue = 1.0f;
    public GameObject gameManager;
    private void Awake()
    {
        walletCollider3D = GetComponent<Collider>();
        walletCollider2D = GetComponent<Collider2D>();

        if (walletCollider3D == null && walletCollider2D == null)
        {
            Debug.LogWarning("Wallet needs a Collider or Collider2D to detect clicks.");
        }
    }

    private void Update()
    {
        if (!TryGetPointerDownPosition(out Vector2 screenPosition))
        {
            return;
        }

        Camera cameraToUse = Camera.main;
        if (cameraToUse == null)
        {
            return;
        }

        bool clickedWallet = false;

        if (walletCollider3D != null)
        {
            Ray ray = cameraToUse.ScreenPointToRay(screenPosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider == walletCollider3D)
            {
                clickedWallet = true;
            }
        }

        if (!clickedWallet && walletCollider2D != null)
        {
            Vector2 worldPoint = cameraToUse.ScreenToWorldPoint(screenPosition);
            RaycastHit2D hit2D = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit2D.collider != null && hit2D.collider == walletCollider2D)
            {
                clickedWallet = true;
            }
        }

        if (clickedWallet)
        {
            Debug.Log("Wallet clicked! Value: " + walletValue);
            gameManager.GetComponent<GameManager>().money += walletValue;

        }
    }

    private bool TryGetPointerDownPosition(out Vector2 screenPosition)
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            screenPosition = Mouse.current.position.ReadValue();
            return true;
        }

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            screenPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            return true;
        }

        screenPosition = default;
        return false;
    }
}
