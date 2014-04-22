using System;
using UnityEngine;
using System.Collections;

public class BarManager : MonoBehaviour
{
    private BalanceSlider balance;
    private Bar energy;

    // Use this for initialization
	void Start () {
        balance = this.GetComponent<BalanceSlider>();
        energy = this.GetComponent<Bar>();
        energy.setAmount(1.0f);
        Bar.EmptyEvent += OnEmpty;
	}

    float adjVal(float val)
    {
        return (Mathf.PI - val) / Mathf.PI;
    }

    bool isIncrease = true;
    float depleteSpeed = 0.1f; // speed of enemy depletion
    float regenSpeed = 0.05f; // based on balance bar values

	// Update is called once per frame
	void Update ()
    {
        balance.setAmount(adjVal(this.GetComponent<Body>().GetBalance));
        //Debug.Log(balance.BarFill);
        energy.decreaseBar(Time.deltaTime * depleteSpeed);
        energy.increaseBar(Time.deltaTime * regenSpeed);
        /* // for testing
        if (!isIncrease)
        {
            energy.decreaseBar(0.01f);
            if (energy.Empty)
            {
                isIncrease = !isIncrease;
            }
        }
        else
        {
            energy.increaseBar(0.01f);
            if (energy.Full)
            {
                isIncrease = !isIncrease;
            }
        }
        //*/
	}

    void OnEmpty(object sender, EventArgs e)
    {
        Debug.Log("You're out of energy!");
    }
}
