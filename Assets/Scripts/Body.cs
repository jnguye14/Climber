using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour
{
    private GameObject LArm;
    private GameObject RArm;
    private GameObject LLeg;
    private GameObject RLeg;
    private float speed = 0.5f;

    private GameObject selectedLimb = null;

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
    }

    void SelectLimb(GameObject limb)
    {
        if (selectedLimb != null)
        {
            selectedLimb.GetComponent<MeshRenderer>().material.color = Color.grey;
        }
        selectedLimb = limb;
        if (selectedLimb != null)
        {
            selectedLimb.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }

    void UnSelectLimb()
    {
        if (selectedLimb != null)
        {
            selectedLimb.GetComponent<MeshRenderer>().material.color = Color.grey;
        }
        selectedLimb = null;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GetAngle(LArm));
        LArm.SendMessage("SetUpperLimit",GetAngle(LLeg));
        RArm.SendMessage("SetLowerLimit",GetAngle(RLeg));

        //Debug.Log(GetAngle(RLeg));

        // basic translations for testing
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

        // basic rotations for testing
        if (Input.GetKey(KeyCode.J))
        {
            this.transform.Rotate(Vector3.forward, 2.0f * speed);
        }
        if (Input.GetKey(KeyCode.L))
        {
            this.transform.Rotate(Vector3.forward, -2.0f * speed);
        }
        //    LArm.SendMessage("SetAngle",90f);
    }

    // returns an angle in degrees between -180 and 180 with 0 being directly right
    private float GetAngle(GameObject limb)
    {
        Vector3 direction = limb.transform.position - this.transform.position;
        direction.Normalize();
        if (limb.transform.localPosition.y < 0)
        {
            return -Mathf.Acos(Vector3.Dot(direction, this.transform.right)) * Mathf.Rad2Deg;
        }
        else
        {
            return Mathf.Acos(Vector3.Dot(direction, this.transform.right)) * Mathf.Rad2Deg;
        }
    }
}
