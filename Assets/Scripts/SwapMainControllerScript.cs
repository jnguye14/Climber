using UnityEngine;
using System.Collections;

// better named as camera switch, but whatever
public class SwapMainControllerScript : MonoBehaviour
{
    public GameObject fpc;
    public GameObject clim;
    private bool isWallCam = false;

    void SwitchCam()
    {
        isWallCam = !isWallCam;
        //fpc = GameObject.FindGameObjectsWithTag("FPC")[0];
        fpc.SetActive(!isWallCam);
        clim.SetActive(isWallCam);
    }
}
