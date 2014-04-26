using UnityEngine;
using System.Collections;

public class GifCursorControl : MonoBehaviour
{
    public Texture2D[] frames;
    int curFrame = 0;

	// Update is called once per frame
	void Update ()
    {
        curFrame = (int)(Time.time * frames.Length) % frames.Length;
        Cursor.SetCursor(frames[curFrame], Vector2.zero, CursorMode.Auto);
	}
}
