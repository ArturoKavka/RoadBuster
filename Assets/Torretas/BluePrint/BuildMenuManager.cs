using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class BuildMenuManager : MonoBehaviour
{
    public static BuildMenuManager Instance;

    [Header("Referencias de UI")]
    public GameObject buildMenuPanel;
    public Transform buttonsParent;
    public GameObject turretButtonPrefab;
    public Button closeButton;

    private RectTransform panelRect;

    [Header("Blueprints Disponibles")]
    public TurretBlueprint[] availableTurrets;

    private BuildableSpot selectedSpot;
    private Camera mainCam;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        panelRect = buildMenuPanel.GetComponent<RectTransform>();
        mainCam = Camera.main;

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(HideBuildMenu);
        }

        HideBuildMenu();
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = mainCam.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
            {
                BuildableSpot spot = hit.collider.GetComponent<BuildableSpot>();
                if (spot != null && !spot.isBuilt)
                {
                    ShowBuildMenu(spot);
                }
                else
                {
                    HideBuildMenu();
                }
            }
            else
            {
                HideBuildMenu();
            }
        }
    }

    public void ShowBuildMenu(BuildableSpot spot)
    {
        selectedSpot = spot;

        Vector3 screenPos = mainCam.WorldToScreenPoint(spot.transform.position);
        panelRect.position = screenPos;
        panelRect.anchoredPosition += new Vector2(0, 100);

        buildMenuPanel.SetActive(true);
        PopulateButtons();
    }

    public void HideBuildMenu()
    {
        buildMenuPanel.SetActive(false);
        selectedSpot = null;
    }

    private void PopulateButtons()
    {
        if (buttonsParent == null || turretButtonPrefab == null)
        {
            Debug.LogError("Faltan referencias en el BuildMenuManager (Parent o Prefab).");
            return;
        }

        foreach (Transform child in buttonsParent) { Destroy(child.gameObject); }

        foreach (TurretBlueprint bp in availableTurrets)
        {
            if (bp == null) continue;

            GameObject buttonGO = Instantiate(turretButtonPrefab, buttonsParent);

            // Comprobación de texto
            TextMeshProUGUI txt = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
            if (txt != null)
            {
                txt.text = $"{bp.turretName}\n{bp.cost}G";
            }
            else
            {
                Debug.LogWarning("El prefab de botón no tiene un componente TextMeshProUGUI.");
            }

            // Comprobación de botón
            Button btn = buttonGO.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() => {
                    if (LevelManager.main.GastarDinero(bp.cost))
                    {
                        selectedSpot.BuildTurret(bp);
                        HideBuildMenu();
                    }
                });
            }
            else
            {
                Debug.LogError("El prefab asignado NO tiene un componente Button.");
            }
        }
    }
}