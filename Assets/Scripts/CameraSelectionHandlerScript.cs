using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using Vuforia;

public class CameraSelectionHandlerScript : MonoBehaviour
{
    private TMP_Dropdown CameraDropdown;
    private string cameraName;

    void Start()
    {
        CameraDropdown = GameObject.Find("CameraDropdown").GetComponent<TMP_Dropdown>();
        if (CameraDropdown)
        {
            CameraDropdown.ClearOptions();
            foreach(var device in WebCamTexture.devices) {
                Debug.Log($"Device is found with name:\"{device.name}\". The device is front facing:{device.isFrontFacing}");
            }
            CameraDropdown.AddOptions(WebCamTexture.devices.Select(device => new TMP_Dropdown.OptionData(device.name)).ToList());
            CameraDropdown.onValueChanged.AddListener(CameraSelectionChanged);
        }
    }

    public void CameraSelectionChanged(int selectedIndex)
    {
        cameraName = CameraDropdown.options[selectedIndex].text;
    }

    public void CameraSelected()
    {
        RestartCamera();
        SceneManager.LoadScene(1);
    }

    private void RestartCamera()
    {
        CameraDevice.Instance.Stop();
        CameraDevice.Instance.Deinit();
        VuforiaConfiguration.Instance.WebCam.DeviceNameSetInEditor = cameraName;
        CameraDevice.Instance.Init();
        CameraDevice.Instance.Start();
    }
}
