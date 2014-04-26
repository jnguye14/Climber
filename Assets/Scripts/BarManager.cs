using System;
using UnityEngine;
using System.Collections;

public class BarManager : MonoBehaviour
{
    private BalanceSlider balance;
    private Bar energy;
    private bool outOfEnergy = false;
    float depleteSpeed = 0.75f; // speed of enemy depletion
    float regenSpeed
    {
        get
        {
            float toReturn = 1.0f * (0.5f - Mathf.Abs(balance.BarFill - 0.5f)) / 0.5f;;
            return toReturn;
        }
    }
    
    public string feedbackText = "";
    private Rect feedbackBox = new Rect(70.0f, 0.0f, 30.0f, 20.0f); // upper right of screen
    private Rect windowRect = new Rect(40.0f, 40.0f, 20.0f, 20.0f);

    public GameObject Body;
    public GameObject cameraSwitch;

    // Use this for initialization
	void Start ()
    {
        balance = this.GetComponent<BalanceSlider>();
        energy = this.GetComponent<Bar>();
        energy.setAmount(1.0f);
        Bar.EmptyEvent += OnEmpty;
	}

	// Update is called once per frame
	void Update ()
    {
        if (!outOfEnergy)
        {
            balance.setAmount(adjVal(Body.GetComponent<Body>().GetBalance));
            energy.decreaseBar(Time.deltaTime * depleteSpeed);
            energy.increaseBar(Time.deltaTime * regenSpeed);
        }
	}

    float adjVal(float val)
    {
        return (Mathf.PI - val) / Mathf.PI;
    }

    void OnEmpty(object sender, EventArgs e)
    {
        feedbackText = "You're out of energy!";
        outOfEnergy = true;

        // enable physics
        this.BroadcastMessage("Fall");
        
        // stop camera from being locked
        Camera.main.gameObject.SendMessage("UnLock");
    }

    void SetFeedback(string text)
    {
        feedbackText = text;
    }

    void OnGUI()
    {
        string text = "Energy: " + (energy.BarFill * 100.0f) + "\n";
        text += feedbackText;
        GUI.Box(adjRect(feedbackBox), text);

        if (outOfEnergy)
        {
            GUI.Window(0, adjRect(windowRect), windowFunc, "You have fallen");
        }
    }

    void windowFunc(int id)
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Climb again?"))
        {
            feedbackText = "";
            energy.setAmount(1.0f);
            outOfEnergy = false;
            Camera.main.gameObject.SendMessage("LockCameraTo", Body);
            this.gameObject.BroadcastMessage("Restart");
            //Debug.Log("Restart at last monkey checkpoint");
        }
        if (GUILayout.Button("Return to Bottom"))
        {
            feedbackText = "";
            energy.setAmount(1.0f);
            outOfEnergy = false;
            Camera.main.gameObject.SendMessage("LockCameraTo", Body);
            this.gameObject.BroadcastMessage("Restart");
            //Debug.Log("Restart to first person camera");
            if (cameraSwitch != null)
            {
                GameObject.Find("Music").SendMessage("SwapMusic");
                cameraSwitch.SendMessage("SwitchCam");
            }
        }
        GUILayout.EndHorizontal();
    }

    // returns Rectangle adjusted to screen size
    Rect adjRect(Rect r)
    {
        return new Rect(
                r.x * Screen.width / 100.0f,
                r.y * Screen.height / 100.0f,
                r.width * Screen.width / 100.0f,
                r.height * Screen.height / 100.0f);
    }
}
