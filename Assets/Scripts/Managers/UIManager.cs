using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject UpgradePanel;

    private void OnEnable()
    {
        UpgradePanel.SetActive(false);
    }

    private void Update()
    {
        UpgradePanel.SetActive(ShipUpgradeManager.CanUpgradeAny());
    }
}
