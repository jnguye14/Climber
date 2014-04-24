using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour
{
    private GameObject LArm;
    private GameObject RArm;
    private GameObject LLeg;
    private GameObject RLeg;
    private GameObject head;
    private float speed = 0.5f;

    // returns a value between 0 and pi
    public float GetBalance
    {
        get
        {
            Vector3 direction = head.transform.position - this.transform.position;
            return Mathf.Atan2(direction.y, direction.x);
        }
    }

    public float GetRightAngle
    {
        get
        {
            Vector3 direction = this.transform.right;
            return Mathf.Atan2(direction.y, direction.x);
        }
    }

    public float GetDownAngle
    {
        get
        {
            return GetBalance - Mathf.PI;
        }
    }

    public float GetLeftAngle
    {
        get
        {
            return GetRightAngle + Mathf.PI;
        }
    }

    // Use this for initialization
    void Start()
    {
        LArm = GameObject.Find("LArm");
        LArm.SendMessage("SetUpperLimit",-135f); // update sets to LLeg angle
        LArm.SendMessage("SetLowerLimit",90f);
        RArm = GameObject.Find("RArm");
        RArm.SendMessage("SetUpperLimit",90f);
        RArm.SendMessage("SetLowerLimit",-45f); // update sets to RLeg angle
        LLeg = GameObject.Find("LLeg");
        LLeg.SendMessage("SetUpperLimit",-90f);
        LLeg.SendMessage("SetLowerLimit",-180f);
        RLeg = GameObject.Find("RLeg");
        RLeg.SendMessage("SetUpperLimit",0f);
        RLeg.SendMessage("SetLowerLimit",-90f);
        head = GameObject.Find("Head");
    }

    // Update is called once per frame
    void Update()
    {
        CenterBody();
        TurnBody();

        //Debug.Log(GetAngle(LArm));
        LArm.SendMessage("SetLowerLimit", GetBalance * Mathf.Rad2Deg); // cannot go farther clockwise than head
        LArm.SendMessage("SetUpperLimit", GetAngle(LLeg)); // cannot go farther counter-clockwise than left leg

        RArm.SendMessage("SetLowerLimit", GetAngle(RLeg)); // cannot go farther clockwise than right leg 
        RArm.SendMessage("SetUpperLimit", GetBalance * Mathf.Rad2Deg); // cannot go farther counter-clockwise than head

        RLeg.SendMessage("SetLowerLimit", GetDownAngle * Mathf.Rad2Deg); // cannot go farther clockwise than directly down
        float val = Mathf.Max(GetAngle(RArm), GetRightAngle * Mathf.Rad2Deg);
        if (GetAngle(RArm) < 90.0f && GetRightAngle * Mathf.Rad2Deg < 90.0f)
        {
            val = Mathf.Min(GetAngle(RArm), GetRightAngle * Mathf.Rad2Deg);
        }
        RLeg.SendMessage("SetUpperLimit", val); // cannot go farther counter-clockwise than directly right or to right arm

        val = Mathf.Max(GetAngle(LArm), GetLeftAngle * Mathf.Rad2Deg);
        LLeg.SendMessage("SetLowerLimit", val); // cannot go farther clockwise than directly left or to left arm
        LLeg.SendMessage("SetUpperLimit", GetDownAngle * Mathf.Rad2Deg); // cannot go farther counter-clockwise than directly down

        //*// basic translations for testing
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.up * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.down * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * speed);
        }
        //*/

        //* // basic rotations for testing
        if (Input.GetKey(KeyCode.J))
        {
            this.transform.Rotate(Vector3.forward, 2.0f * speed);
        }
        if (Input.GetKey(KeyCode.L))
        {
            this.transform.Rotate(Vector3.forward, -2.0f * speed);
        }//*/
    }

    // rotate this based on angle of limbs
    private void TurnBody()
    {
        // Create two vectors in the form of an X across the this's limbs
        Vector3 positiveLine = RArm.transform.position - LLeg.transform.position;
        Vector3 negativeLine = LArm.transform.position - RLeg.transform.position;
        positiveLine.Normalize();
        negativeLine.Normalize();
        
        // find the angle of the vectors and the bodies up vector
        float leftAngle = Mathf.Acos(Vector3.Dot(positiveLine, this.transform.up)) * Mathf.Rad2Deg;
        float rightAngle = Mathf.Acos(Vector3.Dot(negativeLine, this.transform.up)) * Mathf.Rad2Deg;
        
        //Debug.Log("Left: " + leftAngle + "; Right: " + rightAngle);

        float buffer = 5.0f; // degrees

        //*
        // rotate left/right if the difference is greater on one side over the other
        if (rightAngle - leftAngle > buffer)
        {
            this.transform.Rotate(Vector3.forward, 2.0f * speed); // rotate left
        }
        else if (leftAngle - rightAngle > buffer)
        {
            this.transform.Rotate(Vector3.forward, -2.0f * speed); // rotate right
        }//*/

        this.transform.eulerAngles = new Vector3(0, 0, this.transform.eulerAngles.z);

        /*
        float leftAngle = Mathf.Atan2(positiveLine.y, positiveLine.x) * Mathf.Rad2Deg;
        float rightAngle = Mathf.Atan2(negativeLine.y, negativeLine.x) * Mathf.Rad2Deg;
        this.transform.eulerAngles = new Vector3(0,0,(leftAngle + rightAngle) * 0.5f - 90.0f);
        //*/
    }

    // centers this between limbs
    private void CenterBody()
    {
        float buffer = 0.5f;
        //Debug.Log("Distances: " + GetDistance(RArm) + " " + GetDistance(LLeg) + " " + GetDistance(LArm) + " " + GetDistance(RLeg));

        // translate this as best as it can between its four limbs
        if (GetDistance(RArm) + buffer < GetDistance(LLeg))
        {
            this.transform.Translate((LLeg.transform.position - this.transform.position).normalized * 0.01f);
        }
        if (GetDistance(LLeg) + buffer < GetDistance(RArm))
        {
            this.transform.Translate((RArm.transform.position - this.transform.position).normalized * 0.01f);
        }
        if (GetDistance(LArm) + buffer < GetDistance(RLeg))
        {
            this.transform.Translate((RLeg.transform.position - this.transform.position).normalized * 0.01f);
        }
        if (GetDistance(RLeg) + buffer < GetDistance(LArm))
        {
            this.transform.Translate((LArm.transform.position - this.transform.position).normalized * 0.01f);
        }
    }

    // used in Centerthis(), get's the distance from the limb
    private float GetDistance(GameObject limb)
    {
        Vector3 direction = limb.transform.position - this.transform.position;
        return direction.magnitude;
    }

    //*// returns an angle in degrees between -180 and 180 with 0 being directly right
    private float GetAngle(GameObject limb)
    {
        return limb.GetComponent<DrawLimb>().Angle;
        /*
        Vector3 direction = limb.transform.position - this.transform.position;
        direction.Normalize();
        if (limb.transform.position.y < this.transform.position.y)
        {
            return -Mathf.Acos(Vector3.Dot(direction, this.transform.right)) * Mathf.Rad2Deg;
        }
        else
        {
            return Mathf.Acos(Vector3.Dot(direction, this.transform.right)) * Mathf.Rad2Deg;
        }//*/
    }//*/

    void Fall()
    {
        this.rigidbody.useGravity = true;
    }
}
