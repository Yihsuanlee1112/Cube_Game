using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopRedrawCube : MonoBehaviour
{
    public GameObject[] Cube;
    public Button myStopButton;

    private void Start()
    {
        myStopButton.onClick.AddListener(stopRedraw);
    }

    public void stopRedraw()
    {
        print("stopping");
        if (myStopButton.interactable == false)
        {
            //GetComponent<CubeDisappear>().enabled = false;
            //GetComponent<DragCube>().enabled = false;
            Cube = GameObject.FindGameObjectsWithTag("cube");
            /*Cube2 = GameObject.FindGameObjectsWithTag("cube2");
            Cuboid = GameObject.FindGameObjectsWithTag("cuboid");
            Cuboid3 = GameObject.FindGameObjectsWithTag("cuboid3");
            */
            foreach (GameObject cube in Cube)
            {
                cube.GetComponent<DragCube>().dragEnabled = false;
            }
            /*foreach (GameObject cube in Cube2)
            {
                cube.GetComponent<DragCube>().dragDisabled = false;
            }
            foreach (GameObject cube in Cuboid)
            {
                cube.GetComponent<DragCube>().dragDisabled = false;
            }
            foreach (GameObject cube in Cuboid3)
            {
                cube.GetComponent<DragCube>().dragDisabled = false;
            }*/
            print(this.name + " stopped");
        }
    }
}
