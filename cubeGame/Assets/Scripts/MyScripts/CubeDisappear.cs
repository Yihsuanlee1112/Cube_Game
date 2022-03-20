using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDisappear : MonoBehaviour
{
    public GameObject cube;
    //public GameObject Initialize;
    private void Start()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            RespawnCube();
        }
    }//積木碰到界線後消失
    public void RespawnCube()
    {
        print("gone");
        //this.transform.position =  new Vector3(12, 2, Random.Range((float)-7.0, (float)7.0));
        this.GetComponent<Animator>().enabled = false;
        this.transform.SetPositionAndRotation(new Vector3(12, 2, Random.Range((float)-7.0, (float)7.0)), Quaternion.Euler(new Vector3(0, 0, 0)));
        //CheckWhichCube();
        //print("set");
    }

/*    public void CheckWhichCube()
    {
        if (this.tag == "cube")
        {
            print("cube");
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(this.tag == "cuboid")
        {
            print("cuboid");
            this.transform.localScale = new Vector3(2, 1, 1);
        }
        else if (this.tag == "cuboid3")
        {
            print("cuboid3");
            this.transform.localScale = new Vector3(3, 1, 1);
            print(this.transform.localScale);
            
        }
        if (this.tag == "cube2")
        {
            print("cube2");
            this.transform.localScale = new Vector3(1, 1, 1);
        }
*/
    }
    //IEnumerator RespwanCube()
    //{
    //   
    //Vector3 position = new Vector3((float)8.8, 2, Random.Range((float)-7.0, (float)7.0));
    //Quaternion rotation;
    //Instantiate(cube, position, Quaternion.identity);
    //cube.GetComponent<Instantiate_Cube>().Cubes.Remove(this.gameObject);
    //Destroy(this.gameObject);
    //    yield return null;
    //}//積木重新生成


