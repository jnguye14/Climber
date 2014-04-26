using System; // for EventArgs
using UnityEngine;
using System.Collections;

public class GorillaTalk : MonoBehaviour
{
    bool clicked = false;
    private Tutorial dialogue;

    void Start()
    {
        dialogue = this.GetComponent<Tutorial>();
        dialogue.HideText();
    }

    void Update()
    {
        if (!clicked)
        {
            return;
        }

        if (!dialogue.isShowing())
        {
            GameObject.Find("Music").SendMessage("SwapMusic");
            this.SendMessage("SwitchCam");
            clicked = false;
        }
    }

    void OnMouseDown()
    {
        if (clicked == true) // already clicked once
        {
            return;
        }
        clicked = true;
        dialogue.ShowText();
    }
}
