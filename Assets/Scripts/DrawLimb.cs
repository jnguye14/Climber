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
    private bool isFalling = false;

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
            if (value <= 0.0f)
            {
                ll += 360.0f;
            }
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
            if (value <= 0.0f)
            {
                ul += 360.0f;
            }
        }
    }

    public float Angle
    {
        get
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (angle < 0)
            {
                angle += 360.0f;
            }
            return angle;
        }
        set
        {
            float deg = value;

            float yVal = Mathf.Sin(deg * Mathf.Deg2Rad);
            float xVal = Mathf.Cos(deg * Mathf.Deg2Rad);
            Vector3 direction = new Vector3(xVal,yVal,0);
            direction = this.transform.InverseTransformDirection(direction);

            float currentDistance = Mathf.Clamp(magnitude, limbLength, limbLength + limbReach);
            //float currentDistance = Mathf.Clamp((this.transform.position - body.transform.position).magnitude, limbLength, limbLength + limbReach);
            this.transform.localPosition = body.transform.localPosition + direction.normalized * currentDistance;
            //this.transform.position = body.transform.position + direction.normalized * currentDistance;
        }
    }

    public Vector3 direction
    {
        get
        {
            Vector3 direction = this.transform.parent.InverseTransformDirection(this.transform.position - body.transform.position);
            //Vector3 direction = this.transform.localPosition - body.transform.localPosition;
            //Vector3 direction = this.transform.position - body.transform.position;
            direction = new Vector3(direction.x, direction.y, 0.0f);
            return direction.normalized;
        }
    }

    public float magnitude
    {
        get
        {
            Vector3 direction = this.transform.parent.InverseTransformDirection(this.transform.position - body.transform.position);
            //Vector3 direction = this.transform.localPosition - body.transform.localPosition;
            //Vector3 direction = this.transform.position - body.transform.position;
            direction = new Vector3(direction.x, direction.y, 0.0f);
            return direction.magnitude;
        }
    }

    private Vector3 initPos;
    private Quaternion initRot;
    void Restart()
    {
        isFalling = false;
        this.transform.position = initPos;
        this.transform.rotation = initRot;
        this.rigidbody.useGravity = false;
        this.rigidbody.isKinematic = true;
        //this.rigidbody.velocity = Vector3.zero;
    }

    // Use this for initialization
    void Start()
    {
        initPos = this.transform.position;
        initRot = this.transform.rotation;
        this.GetComponent<MeshRenderer>().material.color = nonSelectedColor;
        this.rigidbody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {            
        if (!isFalling)
        {
            // to prevent limb from being thrown everywhere
            this.rigidbody.useGravity = false;
            //this.rigidbody.velocity = Vector3.zero;

            if (hasRock)
            {
                // clamp position to rock's position
                if (target != null)
                {
                    this.transform.position = target.transform.position;
                }
                this.GetComponent<MeshRenderer>().material.color = grabColor;
            }
            else // mwahahaha
            {
                //Fall();
            }
        }

        // limb is limited within a certain reach
        if (magnitude < limbLength)
        {
            this.transform.localPosition = body.transform.localPosition + direction * limbLength;
            //this.transform.position = body.transform.position + direction * limbLength;
            //this.rigidbody.velocity = Vector3.zero;
        }
        else if(magnitude > limbLength + limbReach)
        {
            this.transform.localPosition = body.transform.localPosition + direction * (limbLength + limbReach);
            //this.transform.position = body.transform.position + direction * (limbLength + limbReach);
            //this.rigidbody.velocity = Vector3.zero;
        }

        //BindToLimits();
        
        // draw the limb from the body to the appendage
        if (body != null)
        {
            LineRenderer r = GetComponent<LineRenderer>();
            r.SetPosition(0, body.transform.position);
            r.SetPosition(1, this.transform.position);
        }
        
		this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0.0f);
        //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -1.8f);
        this.transform.localRotation = Quaternion.identity;
    }

    // clicked on appendage, change to selected color, release the rock if it's holding any
    void OnMouseDown()
    {
        //isFalling = false;
        this.GetComponent<MeshRenderer>().material.color = selectedColor;
        hasRock = false;
    }

    void OnMouseDrag()
    {
        //isFalling = false;
        // move the limb around
        float distanceFromCamera = Camera.main.WorldToScreenPoint(this.transform.position).z;
        // above line same as: body.transform.position.z - Camera.main.transform.position.z
        this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, distanceFromCamera));
        BindToLimits();
    }

    void BindToLimits()
    {
        // angle boundary limits
        if (upperLimit < lowerLimit) // special case, RArm, RLeg
        {
            Debug.Log("Upper: " + upperLimit + "; lower: " + lowerLimit + "; angle: " + Angle);
            if (Angle > upperLimit && Angle < lowerLimit)
            {
                float oneEighty = body.GetComponent<Body>().GetLeftAngle * Mathf.Rad2Deg;
                Angle = (Angle > oneEighty) ? lowerLimit : upperLimit;
            }
        }
        //else if (upperLimit == 360.0f) // special case RLeg
        //{
        //    Debug.Log(Angle);
        //    if (Angle < 90.0f)
        //    {
        //        Angle = upperLimit;
        //    }
        //    else if (Angle < lowerLimit)
        //    {
        //        Angle = lowerLimit;
        //    }
        //}
        else
        {
            if (Angle < lowerLimit)
            {
                Angle = lowerLimit;
            }
            else if (Angle > upperLimit)
            {
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
            isFalling = false;
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


    void Fall()
    {
        // float gravity = -1.0f;
        //Physics.gravity = new Vector3(0f, gravity, 0f);
        hasRock = false;
        this.rigidbody.useGravity = true;
        this.rigidbody.isKinematic = false;
        isFalling = true;
    }
}
