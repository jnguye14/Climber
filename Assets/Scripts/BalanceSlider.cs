using UnityEngine;
using System.Collections;

public class BalanceSlider : MonoBehaviour
{
	public Rect area = new Rect(10f,10f,50f,10f);
	public Texture2D bgTexture;
	public Texture2D fillTexture;
	private float buffer;

    private float barFill = 0.5f;
    public float BarFill
    {
        get
        {
            return barFill;
        }
        set
        {
            barFill = Mathf.Clamp(value, 0.0f, 1.0f);
        }
    }

    public bool Full
    {
        get
        {
            return barFill == 1.0f;
        }
    }

    public bool Empty
    {
        get
        {
            return barFill == 0.0f;
        }
    }

	// Use this for initialization
	void Start ()
	{
        if (bgTexture == null)
        {
            bgTexture = new Texture2D(1,1);
            bgTexture.SetPixel(0, 0, Color.white);
            bgTexture.Apply();
        }
        if (fillTexture == null)
        {
            int resolution = 256; // power of two > 0
            fillTexture = new Texture2D(resolution, resolution);
            for (int i = 0; i < fillTexture.width; i++)
            {
                for (int j = 0; j < fillTexture.height; j++)
                {
                    int someVal = (i - resolution / 2) * (i - resolution / 2) + (j - resolution / 2) * (j - resolution / 2);
                    if (someVal < resolution * resolution / 4)
                    {
                        float numerator = Mathf.Sqrt(someVal);
                        float denominator = (float)resolution / 2.0f;
                        fillTexture.SetPixel(i, j, Color.Lerp(Color.red, Color.yellow, numerator / denominator));
                    }
                    else
                    {
                        fillTexture.SetPixel(i, j, Color.clear);
                    }
                }
            }
            fillTexture.Apply();
        }

        buffer = area.height*0.75f;
	}

	void OnGUI()
	{
		GUI.depth = -1; // draw on top of everything

		// draw the background:
        GUI.DrawTexture(adjRect(area), bgTexture);

		// draw the filled-in part
        Rect temp = adjRect(new Rect(area.x + area.width * barFill, area.y - buffer * 0.25f, 0,0));
        temp.x -= buffer * Screen.height / 100.0f;
        temp.width = buffer * 2 * Screen.height/100.0f;
        temp.height = buffer * 2 * Screen.height/100.0f;
		GUI.DrawTexture  (temp, fillTexture);
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

    public void increaseBar(float amount)
    {
        BarFill += amount;
    }

    public void decreaseBar(float amount)
    {
        BarFill -= amount;
    }

    public void setAmount(float amount)
    {
        BarFill = amount;
    }
}