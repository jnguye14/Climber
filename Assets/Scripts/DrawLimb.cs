using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(LineRenderer))]
public class DrawLimb : MonoBehaviour
{
    public GameObject body;
    private GameObject target;

    public float limbLength = 2.0f;
    public float limbReach = 2.0f;
    public bool isArm = false;
    private bool hasRock = true; // used to release rock
    public bool isGrabbingRock = false;

    public Color selectedColor = Color.green;
    public Color nonSelectedColor = Color.gray;
    public Color grabColor = Color.blue;

    // how far counter-clockwise player can go
    private float ll;
    public float lowerLimit
    {
        get
        {
            return ll;
        }
        set
        {
            ll = value;
        }
    }

    // how far clockwise limb can go
    private float ul;
    public float upperLimit
    {
        get
        {
            return ul;
        }
        set
        {
            ul = value;
        }
    }

    public Vector3 direction
    {
        get
        {
            Vector3 direction = this.transform.position - body.transform.position;
            return direction.normalized;
        }
    }

    public float magnitude
    {
        get
        {
            Vector3 direction = this.transform.position - body.transform.position;
            return direction.magnitude;
        }
    }

    //public float gravity = -1.0f;
    // Use this for initialization
    void Start()
    {
        this.GetComponent<MeshRenderer>().material.color = nonSelectedColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasRock)
        {
            // clamp position to rock's position
            if (target != null)
            {
                this.transform.position = target.transform.position;
            }
            this.GetComponent<MeshRenderer>().material.color = grabColor;

            // to prevent limb from being thrown everywhere
            this.rigidbody.velocity = Vector3.zero;
            this.rigidbody.useGravity = false;
        }
        else // mwahahaha
        {
            //Physics.gravity = new Vector3(0f, gravity, 0f);
            this.rigidbody.useGravity = true;
        }

        
        // limb is limited within a certain reach
        if (magnitude < limbLength)
        {
            this.transform.position = body.transform.position + direction * limbLength;
        }
        else if(magnitude > limbLength + limbReach)
        {
            this.transform.position = body.transform.position + direction * (limbLength + limbReach);
        }
        
        // draw the limb from the body to the appendage
        if (body != null)
        {
            LineRenderer r = GetComponent<LineRenderer>();
            r.SetPosition(0, body.transform.position);
            r.SetPosition(1, this.transform.position);
        }
    }

    // clicked on appendage, change to selected color, release the rock if it's holding any
    void OnMouseDown()
    {
        this.GetComponent<MeshRenderer>().material.color = selectedColor;
        hasRock = false;
    }

    void OnMouseDrag()
    {
        // move the limb around
        this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, body.transform.position.z - Camera.main.transform.position.z));

        // certain limbs are limited by their upper and lower angles
        if (upperLimit < lowerLimit) // special case, LArm
        {
            if (GetAngle() > upperLimit && GetAngle() < lowerLimit)
            {
                SetAngle((GetAngle() < 0) ? lowerLimit : upperLimit);
            }
        }
        else
        {
            if (GetAngle() < lowerLimit)
            {
                SetAngle(lowerLimit);
            }
            else if (GetAngle() > upperLimit)
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
        }//*/
    }

    // mouse released, unselect limb
    void OnMouseUp()
    {
        this.GetComponent<MeshRenderer>().material.color = nonSelectedColor;
        if (target != null) // JIC
        {
            hasRock = true;
        }
        else
        {
            hasRock = false;
        }
    }

    // Entered a collision, check if able to grab rock
    void OnTriggerEnter(Collider other)
    {
        if ((isArm && other.gameObject.tag == "Handhold") ||
                (!isArm && other.gameObject.tag == "Foothold") ||
                (other.gameObject.tag == "Multihold") ||
                (other.gameObject.tag == "Checkpoint"))
        {
            target = other.gameObject;
            hasRock = true;
        }
    }

    // exited a collision, release the rock
    void OnTriggerExit(Collider other)
    {
        this.GetComponent<MeshRenderer>().material.color = nonSelectedColor;
        target = null;
        hasRock = false;
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
        deg = Mathf.Clamp(deg, lowerLimit, upperLimit) * Mathf.Deg2Rad;

        //deg -= Mathf.Acos(Vector3.Dot(Vector3.right, body.transform.right)) * Mathf.Rad2Deg;
        //deg += Mathf.Acos(Vector3.Dot(Vector3.right, body.transform.right)) * Mathf.Rad2Deg;

        //Vector3 direction = new Vector3(Mathf.Cos(deg), Mathf.Sin(deg), 0);
        float theta = Mathf.Acos(body.transform.right.x);
        float theta2 = Mathf.Asin(body.transform.right.y);
        Vector3 direction = new Vector3(Mathf.Cos(deg+theta), Mathf.Sin(deg+theta2), 0);
        float currentDistance = Mathf.Clamp((this.transform.position - body.transform.position).magnitude, limbLength, limbLength + limbReach);
        this.transform.position = body.transform.position + direction.normalized * currentDistance;
    }//*/



    // returns an angle in degrees between -180 and 180 with 0 being directly right
    private float GetAngle()
    {
        if (isUnder())//this.transform.localPosition.y < 0)
        {
            return -Mathf.Acos(Vector3.Dot(direction, body.transform.right)) * Mathf.Rad2Deg;
        }
        else
        {
            return Mathf.Acos(Vector3.Dot(direction, body.transform.right)) * Mathf.Rad2Deg;
        }
    }

    private bool isUnder()
    {
        Vector3 line = body.transform.right; // i.e. plane
        Vector3 point = this.transform.position;
        if(line.x * point.y - line.y * point.y < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
