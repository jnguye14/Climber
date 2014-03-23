using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class DrawLimb : MonoBehaviour
{
    public GameObject body;
    public float limbLength = 2.0f;
    public bool hasRock = false;
    private float lowerLimit = 180f;
    private float upperLimit = -180f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = this.transform.position - body.transform.position;
        this.transform.position = body.transform.position + direction.normalized * limbLength;

        if (body != null)
        {
            LineRenderer r = GetComponent<LineRenderer>();
            r.SetPosition(0, body.transform.position);
            r.SetPosition(1, this.transform.position);
        }
    }

    void OnMouseDown()
    {
        body.SendMessage("SelectLimb",this.gameObject);
    }

    void OnMouseDrag()
    {
        this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, body.transform.position.z - Camera.main.transform.position.z));
        if (upperLimit < lowerLimit) // special case, LArm
        {
            if (GetAngle() > upperLimit && GetAngle() < lowerLimit)
            {
                SetAngle((GetAngle() < 0) ? upperLimit : lowerLimit);
            }
        }
        else
        {
            if (GetAngle() < lowerLimit)
            {
                SetAngle(lowerLimit);
            }
            if (GetAngle() > upperLimit)
            {
                if (lowerLimit == -180.0f && GetAngle() > 0) // special case, LLeg
                {
                    SetAngle(lowerLimit);
                }
                else
                {
                    SetAngle(upperLimit);
                }
            }
        }
    }

    void SetUpperLimit(float l)
    {
        upperLimit = l;
    }

    void SetLowerLimit(float l)
    {
        lowerLimit = l;
    }

    // sets the angle from counter clockwise around the z-axis
    void SetAngle(float deg)
    {
        deg *= Mathf.Deg2Rad;
        Vector3 direction = new Vector3(Mathf.Cos(deg), Mathf.Sin(deg), 0);
        this.transform.position = body.transform.position + direction.normalized * limbLength;
    }

    // returns an angle in degrees between -180 and 180 with 0 being directly right
    private float GetAngle()
    {
        Vector3 direction = this.transform.position - body.transform.position;
        direction.Normalize();
        if (this.transform.localPosition.y < 0)
        {
            return -Mathf.Acos(Vector3.Dot(direction, body.transform.right)) * Mathf.Rad2Deg;
        }
        else
        {
            return Mathf.Acos(Vector3.Dot(direction, body.transform.right)) * Mathf.Rad2Deg;
        }
    }

    void GrabRock(GameObject rock)
    {
        // get direction from rock to limb
        Vector3 direction = rock.transform.position - this.transform.position;
        //float distance = direction.magnitude;
        direction.Normalize();

        // if too far away, hasRock = false;
        // else, was able to grab, hasRock = true;
        hasRock = true;

        // actually grab the rock
        //body.SendMessage("MoveTowards",rock);
        //set angle limb to direction of rock
        //translate body towards rock until selected limb touches rock
    }
}
