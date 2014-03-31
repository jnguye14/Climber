using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour
{
    private GameObject LArm;
    private GameObject RArm;
    private GameObject LLeg;
    private GameObject RLeg;
    private GameObject body;
    private float speed = 0.5f;

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
        LLeg.SendMessage("SetLowerLimit",-179f);
        RLeg = GameObject.Find("RLeg");
        RLeg.SendMessage("SetUpperLimit",0f);
        RLeg.SendMessage("SetLowerLimit",-90f);
        body = GameObject.Find("Body");
    }

    // Update is called once per frame
    void Update()
    {
        CenterBody();
        TurnBody();

        //Debug.Log(GetAngle(LArm));
        LArm.SendMessage("SetUpperLimit", GetAngle(LLeg));
        RArm.SendMessage("SetLowerLimit", GetAngle(RLeg));

        /*// basic translations for testing
        if (Input.GetKey(KeyCode.W))
        {
            body.transform.Translate(Vector3.up * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            body.transform.Translate(Vector3.down * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            body.transform.Translate(Vector3.left * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            body.transform.Translate(Vector3.right * speed);
        }
        //*/

        /* // basic rotations for testing
        if (Input.GetKey(KeyCode.J))
        {
            body.transform.Rotate(Vector3.forward, 2.0f * speed);
        }
        if (Input.GetKey(KeyCode.L))
        {
            body.transform.Rotate(Vector3.forward, -2.0f * speed);
        }//*/
    }

    // rotate body based on angle of limbs
    private void TurnBody()
    {
        // Create two vectors in the form of an X across the body's limbs
        Vector3 positiveLine = RArm.transform.position - LLeg.transform.position;
        Vector3 negativeLine = LArm.transform.position - RLeg.transform.position;
        positiveLine.Normalize();
        negativeLine.Normalize();
        
        // find the angle of the vectors and the bodies up vector
        float leftAngle = Mathf.Acos(Vector3.Dot(positiveLine, body.transform.up)) * Mathf.Rad2Deg;
        float rightAngle = Mathf.Acos(Vector3.Dot(negativeLine, body.transform.up)) * Mathf.Rad2Deg;
        //Debug.Log("Left: " + leftAngle + "; Right: " + rightAngle);

        float buffer = 5.0f; // degrees

        // rotate left/right if the difference is greater on one side over the other
        if (rightAngle - leftAngle > buffer)
        {
            body.transform.Rotate(Vector3.forward, 2.0f * speed); // rotate left
        }
        else if (leftAngle - rightAngle > buffer)
        {
            body.transform.Rotate(Vector3.forward, -2.0f * speed); // rotate right
        }
    }

    // centers body between limbs
    private void CenterBody()
    {
        float buffer = 0.5f;
        //Debug.Log("Distances: " + GetDistance(RArm) + " " + GetDistance(LLeg) + " " + GetDistance(LArm) + " " + GetDistance(RLeg));

        // translate body as best as it can between its four limbs
        if (GetDistance(RArm) + buffer < GetDistance(LLeg))
        {
            body.transform.Translate((LLeg.transform.position - body.transform.position).normalized * 0.01f);
        }
        if (GetDistance(LLeg) + buffer < GetDistance(RArm))
        {
            body.transform.Translate((RArm.transform.position - body.transform.position).normalized * 0.01f);
        }
        if (GetDistance(LArm) + buffer < GetDistance(RLeg))
        {
            body.transform.Translate((RLeg.transform.position - body.transform.position).normalized * 0.01f);
        }
        if (GetDistance(RLeg) + buffer < GetDistance(LArm))
        {
            body.transform.Translate((LArm.transform.position - body.transform.position).normalized * 0.01f);
        }
    }

    // used in CenterBody(), get's the distance from the limb
    private float GetDistance(GameObject limb)
    {
        Vector3 direction = limb.transform.position - body.transform.position;
        return direction.magnitude;
    }

    //*// returns an angle in degrees between -180 and 180 with 0 being directly right
    private float GetAngle(GameObject limb)
    {
        return limb.GetComponent<DrawLimb>().Angle;
        /*
        Vector3 direction = limb.transform.position - body.transform.position;
        direction.Normalize();
        if (limb.transform.position.y < body.transform.position.y)
        {
            return -Mathf.Acos(Vector3.Dot(direction, body.transform.right)) * Mathf.Rad2Deg;
        }
        else
        {
            return Mathf.Acos(Vector3.Dot(direction, body.transform.right)) * Mathf.Rad2Deg;
        }//*/
    }//*/
}
