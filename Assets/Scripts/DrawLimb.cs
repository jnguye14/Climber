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

    public float Angle
    {
        get
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
        set
        {
            float deg = value;
            //deg = Mathf.Clamp(deg, lowerLimit, upperLimit) * Mathf.Deg2Rad;

            //deg -= Mathf.Acos(Vector3.Dot(Vector3.right, body.transform.right)) * Mathf.Rad2Deg;
            //deg += Mathf.Acos(Vector3.Dot(Vector3.right, body.transform.right)) * Mathf.Rad2Deg;

            //Vector3 direction = new Vector3(Mathf.Cos(deg), Mathf.Sin(deg), 0);
            float theta = Mathf.Acos(body.transform.right.x);
            float theta2 = Mathf.Asin(body.transform.right.y);
            Vector3 direction = new Vector3(Mathf.Cos(deg + theta), Mathf.Sin(deg + theta2), 0);
            float currentDistance = Mathf.Clamp((this.transform.position - body.transform.position).magnitude, limbLength, limbLength + limbReach);
            this.transform.position = body.transform.position + direction.normalized * currentDistance;

            /*if (ll < ul)
            {
                angle = Mathf.Clamp(value, ll, ul);
            }
            else // upper limit less than lower limit
            {
                angle = Mathf.Clamp(value, ll, ul);
            }//*/
        }
    }

    public Vector3 direction
    {
        get
        {
            Vector3 direction = this.transform.position - body.transform.position;
            direction = new Vector3(direction.x, direction.y, 0.0f);
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
            this.rigidbody.velocity = Vector3.zero;
            //Physics.gravity = new Vector3(0f, gravity, 0f);
            //this.rigidbody.useGravity = true;
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
		this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,-1.8f);
        this.transform.rotation = Quaternion.identity;
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
        //Debug.Log(Angle);

        if (upperLimit < lowerLimit) // special case, LArm
        {
            if (Angle > upperLimit && Angle < lowerLimit)
            {
                //SetAngle((Angle < 0) ? lowerLimit : upperLimit);
                Angle = (Angle < 0) ? lowerLimit : upperLimit;
            }
        }
        else if (lowerLimit == -179.0f) // special case, LLeg
        {
            if (Angle > 90.0f)
            {
                //SetAngle(lowerLimit);
                Angle = lowerLimit;
            }
            else if (Angle > upperLimit)
            {
                //SetAngle(upperLimit);
                Angle = upperLimit;
            }
        }
        else
        {
            if (Angle < lowerLimit)
            {
                //SetAngle(lowerLimit);
                Angle = lowerLimit;
            }
            else if (Angle > upperLimit)
            {
                //SetAngle(upperLimit);
                Angle = upperLimit;
            }
        }
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

    /*void SetAngle(float deg)
    {
        Angle = deg;
    }//*/

    private bool isUnder()
    {
        // y = mx + b; // where b is at the body's center (localPosition 0,0,0)
        float y = body.transform.right.y;
        float x = body.transform.right.x;
        float m = y / x;

        Vector3 localPos = body.transform.InverseTransformDirection(this.transform.position - body.transform.position);

        float y2 = m * localPos.x;
        if(y2 - localPos.y < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
