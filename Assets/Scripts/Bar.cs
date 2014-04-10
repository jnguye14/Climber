using UnityEngine;
using System.Collections;

public class Bar : MonoBehaviour
{
	public Rect area = new Rect(20f,40f,100f,50f);
	public Texture2D bgTexture;
	public Texture2D fillTexture;
	public float buffer = 5f;
	
	public bool full = false;
	public float barFill = 0.5f;
	public bool empty = false;

	// Use this for initialization
	void Start ()
	{
		barFill = 0.5f; // start game half way
        if (bgTexture == null)
        {
            bgTexture = new Texture2D(1,1);
            bgTexture.SetPixel(0, 0, Color.white);
            bgTexture.Apply();
        }
        if (fillTexture == null)
        {
            fillTexture = new Texture2D(1, 1);
            fillTexture.SetPixel(0, 0, Color.white);
            fillTexture.Apply();
        }
	}
	
	// Update is called once per frame
	void Update () { }

	void OnGUI()
	{
		GUI.depth = -1; // draw on top of everything

		//GUIStyle style;

		// draw the background:
		GUI.BeginGroup (area);
		GUI.DrawTexture(new Rect (0f,0f, area.width, area.height), bgTexture);
		
		// change color of fill based on current amount
		if(barFill < 0.33f)
		{
			GUI.color = Color.red;
		}
		else if(barFill < 0.66f)
		{
			GUI.color = Color.yellow;
		}
		else
		{
			GUI.color = Color.green;
		}

		// draw the filled-in part
		GUI.DrawTexture  (new Rect (buffer,buffer, (area.width-2*buffer)*barFill, area.height - 2*buffer), fillTexture);
		GUI.EndGroup ();
	} 

	public void increaseBar(float amount)
	{
		empty = false;
		barFill += amount;
		if(barFill >= 1f)
		{
			barFill = 1f;
			full = true; // or send bar filled event
		}
	}
	
	public void decreaseBar(float amount)
	{
		full = false;
		barFill -= amount;
		if(barFill <= 0f)
		{
			barFill = 0f;
			empty = true; // or send bar empty event
		}
	}
	
	public void setAmount(float amount)
	{
		barFill = Mathf.Clamp(amount, 0f, 1f);
	}
}