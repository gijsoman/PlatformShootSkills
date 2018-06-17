using UnityEngine;

public class UIPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;

    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryPanel != null)
            {
                inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
            }
        }
	}
}
