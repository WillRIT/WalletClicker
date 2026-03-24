using UnityEngine;
using UnityEngine.InputSystem;


public class Wallet : MonoBehaviour
{
    private Collider walletCollider3D;
    private Collider2D walletCollider2D;

    public float walletValue = 1.0f;
    public GameObject gameManager;
    [SerializeField] private bool debugClicks = true;
    private void Awake()
    {
        walletCollider3D = GetComponent<Collider>();
        walletCollider2D = GetComponent<Collider2D>();

        if (debugClicks)
        {
            Debug.Log($"Wallet ready on '{name}'. 2D collider: {walletCollider2D != null}, 3D collider: {walletCollider3D != null}");
        }

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

        if (debugClicks)
        {
            Debug.Log($"Pointer down at screen position {screenPosition}");
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
            float depthToWallet = Mathf.Abs(transform.position.z - cameraToUse.transform.position.z);
            Vector3 worldPoint3 = cameraToUse.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, depthToWallet));
            Vector2 worldPoint = new Vector2(worldPoint3.x, worldPoint3.y);
            if (walletCollider2D.OverlapPoint(worldPoint))
            {
                clickedWallet = true;
            }
        }

        if (clickedWallet)
        {
            Debug.Log("Wallet clicked! Value: " + walletValue);
            gameManager.GetComponent<GameManager>().money += walletValue;
        }
        else if (debugClicks)
        {
            Debug.Log("Pointer detected, but wallet collider was not hit.");
        }
    }

    private bool TryGetPointerDownPosition(out Vector2 screenPosition)
    {
        if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
        {
            screenPosition = Pointer.current.position.ReadValue();
            return true;
        }

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

#if ENABLE_LEGACY_INPUT_MANAGER
        if (Input.GetMouseButtonDown(0))
        {
            screenPosition = Input.mousePosition;
            return true;
        }
#endif

        screenPosition = default;
        return false;
    }
}
