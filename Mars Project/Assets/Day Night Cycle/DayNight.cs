using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour {

    public float cycleMins = 15f;
    float cycleCalc;

    public void Awake() {
        cycleCalc = 0.1f / cycleMins * -1;
    }

    public void Update() {

        transform.Rotate(-cycleCalc, 0, 0, Space.Self);

    }

}
