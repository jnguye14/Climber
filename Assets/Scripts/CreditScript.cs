using UnityEngine;
using System.Collections;

public class CreditScript : MonoBehaviour
{
    public GUISkin skin;
    public Rect windowRect = new Rect(0f, 0f, 100f, 100f);

    // credit variables
    public TextAsset credits;
    public float creditSpeed = 50.0f;

    // Private Credit Scrolling variables
    private string creditText = "";
    private int creditLength = 0;
    private float startTime = 0.0f;
    private float amount; // credit offset amount
    private int repeatNum = 0;

	// Use this for initialization
	void Start ()
    {
        windowRect = adjRect(windowRect);
        creditText = credits.text;
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
        amount = (Time.time - startTime) * creditSpeed - (float)((creditLength + windowRect.height) * repeatNum);
        if(amount >= creditLength + windowRect.height)
		{
			amount = 0;
			repeatNum++;
		}
    }

    void OnGUI()
    {
        GUI.skin = skin;
        windowRect = GUI.Window(0, windowRect, windowFunc, "");
    }

    void windowFunc(int id)
    {
        // window buffer values
        float leftBuffer = 50; // should be same as rightBuffer
        float topBuffer = 100;
        float bottomBuffer = 65;

        FancyTop(windowRect.width);

        // Title Label
        float width = windowRect.width - 2 * leftBuffer;
        float height = GUI.skin.label.CalcHeight(new GUIContent("CREDITS"), width);
        Rect tempRect = new Rect(leftBuffer, topBuffer, width, height);
        GUI.Label(tempRect, "CREDITS");

        // height for replay button, compute now so scroll area can be in the middle
        float height3 = GUI.skin.button.CalcHeight(new GUIContent("Replay?"), width);

        // Begin Area
        float height2 = windowRect.height - topBuffer - height - height3 - bottomBuffer;
        tempRect = new Rect(leftBuffer, topBuffer + height, width, height2);
        GUI.Box(tempRect, "");
        GUILayout.BeginArea(tempRect);

        // Draw the Scrolling Label
        creditLength = (int)GUI.skin.GetStyle("PlainText").CalcHeight(new GUIContent(creditText), width);
        tempRect = new Rect(0, windowRect.height - amount, width, creditLength);
        TextAnchor temp = GUI.skin.GetStyle("PlainText").alignment;
        GUI.skin.GetStyle("PlainText").alignment = TextAnchor.MiddleCenter;
        GUI.Label(tempRect, creditText, "PlainText");
        GUI.skin.GetStyle("PlainText").alignment = temp;

        GUILayout.EndArea();

        // Replay Button
        tempRect = new Rect(leftBuffer, topBuffer + height + height2, width, height3);
        if(GUI.Button(tempRect,"Replay?"))
        {
            Application.LoadLevel("Title");
        }

        // make window draggable
        GUI.DragWindow();
    }

    void FancyTop(float topX)
    {
        float leafOffset = (topX / 2) - 64;
        float frameOffset = (topX / 2) - 27;
        float skullOffset = (topX / 2) - 20;

        GUI.Label(new Rect(leafOffset + 35, 35, 0, 0), "", "RibbonBlue"); // added
        GUI.Label(new Rect(leafOffset, 18, 0, 0), "", "GoldLeaf");
        GUI.Label(new Rect(frameOffset, 3, 0, 0), "", "IconFrame");
        //GUI.Label(new Rect(skullOffset, 12, 0, 0), "", "Skull");
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