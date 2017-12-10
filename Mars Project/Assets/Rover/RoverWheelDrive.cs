using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverWheelDrive : MonoBehaviour {

    private WheelCollider[] wheels;

    public float maxAngle = 30;
    public float maxTorque = 300;



    [Header("Forward Friction")]
    public float FextrenumSlip = 0.4f;
    public float FextrenumValue = 1;
    public float FasymptoteSlip = 0.8f;
    public float FasymptoteValue = 0.5f;
    public float forwardStiffness = 1f;


    [Header("Sideways Friction")]
    public float SextrenumSlip = 0.2f;
    public float SextrenumValue = 1;
    public float SasymptoteSlip = 0.5f;
    public float SasymptoteValue = 0.75f;
    public float sidewayStiffness = 1f;

    public GameObject wheelShape;

    // here we find all the WheelColliders down in the hierarchy
    public void Start()
    {
        wheels = GetComponentsInChildren<WheelCollider>();

        print(wheels.Length);


        for (int i = 0; i < wheels.Length; ++i)
        {
            var wheel = wheels[i];

            wheel.ConfigureVehicleSubsteps(5, 12, 15);

            WheelFrictionCurve wfFriction = wheel.forwardFriction;

            WheelFrictionCurve wsFriction = wheel.sidewaysFriction;

            wfFriction.stiffness = forwardStiffness;
            wsFriction.stiffness = sidewayStiffness;

            wfFriction.asymptoteSlip = FasymptoteSlip;
            wfFriction.asymptoteValue = FasymptoteValue;
            wfFriction.extremumSlip = FextrenumSlip;
            wfFriction.extremumValue = FextrenumValue;

            wsFriction.asymptoteSlip = SasymptoteSlip;
            wsFriction.asymptoteValue = SasymptoteValue;
            wsFriction.extremumSlip = SextrenumSlip;
            wsFriction.extremumValue = SextrenumValue;

            wheel.forwardFriction = wfFriction;
            wheel.sidewaysFriction = wsFriction;


            // create wheel shapes only when needed
            if (wheelShape != null)
            {
                var ws = Instantiate(wheelShape);
                ws.transform.parent = wheel.transform;
            }
        }
    }

    // this is a really simple approach to updating wheels
    // here we simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero
    // this helps us to figure our which wheels are front ones and which are rear
    public void FixedUpdate()
    {
        float angle = maxAngle * Input.GetAxis("Horizontal");
        float torque = maxTorque * Input.GetAxis("Vertical");

        foreach (WheelCollider wheel in wheels)
        {
            // a simple car where front wheels steer while rear ones drive
            if (wheel.transform.localPosition.z > 0)
                wheel.steerAngle = angle;

            //if (wheel.transform.localPosition.z <= 0)
              //  wheel.motorTorque = torque;

            wheel.motorTorque = torque;

            // update visual wheels if any
            if (wheelShape)
            {
                Quaternion q;
                Vector3 p;
                wheel.GetWorldPose(out p, out q);

                // assume that the only child of the wheelcollider is the wheel shape
                Transform shapeTransform = wheel.transform.GetChild(0);
                shapeTransform.position = p;
                shapeTransform.rotation = q;
            }

        }
    }
}
