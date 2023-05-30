using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject UpgradeWindow;
    [SerializeField] private GameObject UpgradeNotification;
    
    private void Start()
    {
        UpgradeWindow.SetActive(false);
        UpgradeNotification.SetActive(true);
    }

    void Update()
    {
        var shiftHeld = Input.GetKey(KeyCode.LeftShift);

        Time.timeScale = shiftHeld ? 0 : 1;
        
        UpgradeWindow.SetActive(shiftHeld);
        UpgradeNotification.SetActive(!shiftHeld);
    }
}
