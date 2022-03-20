using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRIKInput
{


    [Tooltip("Reference to the VRIK component on the avatar.")] public VRIK ik;
    [Tooltip("The settings for VRIK calibration.")] public VRIKCalibrator.Settings settings;
    [Tooltip("The HMD.")] public Transform headTracker;
    [Tooltip("(Optional) A tracker or hand controller device placed anywhere on or in the player's left hand.")] public Transform leftHandTracker;
    [Tooltip("(Optional) A tracker or hand controller device placed anywhere on or in the player's right hand.")] public Transform rightHandTracker;



    public IEnumerator VRIKStart(GameObject VRCamera, GameObject player)
    {
        float npc = 1.5f;
        float userHeight = 1.5f;
        var npcHeight = (userHeight / npc);
        player.gameObject.transform.localScale = new Vector3(npcHeight, npcHeight, npcHeight);
        //生成
        var _camera = GameObject.Instantiate(VRCamera);
        GetIKTransform(_camera.transform);
        ik = player.AddComponent<VRIK>();
        yield return new WaitForSeconds(2);
        Debug.Log(headTracker);
        settings = new VRIKCalibrator.Settings();
        VRIKCalibrator.Calibrate(ik, settings, headTracker, null, leftHandTracker, rightHandTracker, null, null);
    }

    public void GetIKTransform(Transform VRtransform)
    {
        foreach (Transform item in VRtransform)
        {
            if (item.name == "Controller (left)")
            {
                foreach (Transform child in item)
                {
                    if (child.name == "LeftHand")
                    {
                        leftHandTracker = child;
                        Debug.Log(leftHandTracker.name);
                    }
                }
            }
            if (item.name == "Controller (right)")
            {
                foreach (Transform child in item)
                {
                    if (child.name == "RightHand")
                    {
                        rightHandTracker = child;
                        Debug.Log(rightHandTracker.name);
                    }
                }
            }
            if (item.name == "Camera")
            {
                foreach (Transform child in item)
                {
                    if (child.name == "Head")
                    {
                        headTracker = child;
                        Debug.Log(headTracker.name);
                    }
                }
            }
        }
    }

}
