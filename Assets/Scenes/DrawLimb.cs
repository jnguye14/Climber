using UnityEngine;
using System.Collections;

[RequireComponent (typeof(LineRenderer))]
public class DrawLimb : MonoBehaviour
{
    public GameObject body;
    public float limbLength = 2.0f;
    private Vector3 limbPos;

	// Use this for initialization
	void Start () {
        limbPos = Camera.main.WorldToScreenPoint(this.transform.position);
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 bodyPos = Camera.main.WorldToScreenPoint(body.transform.position);
        Vector3 direction = limbPos - bodyPos;
        this.transform.position = body.transform.position + direction.normalized * limbLength;

        if (body != null)
        {
            LineRenderer r = GetComponent<LineRenderer>();
            r.SetPosition(0, body.transform.position);
            r.SetPosition(1, this.transform.position);
        }
	}

    void OnMouseDrag()
    {
        limbPos = Input.mousePosition;
    }
}
