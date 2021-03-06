﻿using UnityEngine;
using System.Collections;

public class PlayerFront : MonoBehaviour {

    Animator anim;

	void Awake () {
        anim = GetComponentInParent<Animator>();
	}

    void UpdateSensorPosition()
    {
        if (anim.GetFloat("VerticalSpeed") > 0.1f)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (anim.GetFloat("VerticalSpeed") < -0.1f)
            transform.eulerAngles = new Vector3(0, 0, 180);
        else if (anim.GetFloat("HorizontalSpeed") > 0.1f)
            transform.eulerAngles = new Vector3(0, 0, 270);
        else if (anim.GetFloat("HorizontalSpeed") < -0.1f)
            transform.eulerAngles = new Vector3(0, 0, 90);
    }

    void Update () {
        UpdateSensorPosition();
	}
}
