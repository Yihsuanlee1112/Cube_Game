using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CubeExamination : MonoBehaviour
{   
    public Button myStopButton;
    public GameObject Instantiate;
    public List<GameObject> myCubeList;
    public List<Vector3> CubePosList;
    public List<Color> CubeColorList;

    //public List<MyClass> Layer = new List<MyClass>();
    public static int Row = 20;
    public static int Col = 20;
    public GameObject[,] block = new GameObject[Row,Col];

    public static Vector3 CubeScale = new Vector3(1, 1, 1);
    public static Vector3 CuboidScale = new Vector3(2, 1, 1);
    public int[] sequence = new int[] { 11, 12, 21, 22, 31, 32 };

 //   private void Start()
 //   {
 //       print("START");
 //       test();

//        for(int i = 0; i < Layer.Count; i++)
 //       {
 //           print("NUM: " + Layer[i].num_layer);
 //           for (int j = 0; j < Layer[i].layer.Count; j++)
 //           {
  //              print(Layer[i].layer[j]);


 //           }
 //       }
 //   }

    public void ListCubes()
    {
        myStopButton = GetComponent<Button>();
        //myCubes.Add(GameObject.);// 所有 tag 為 cube 的物件，都會被抓出來存到變數(陣列) myCubes` 中
        myCubeList =  Instantiate.GetComponent<Instantiate_Cube>().Cubes;
        foreach (GameObject cube in myCubeList)
        {
            print(cube.name);
            Vector3 CubePos = cube.transform.position * 100;
            Color cubeColor = cube.gameObject.GetComponent<MeshRenderer>().material.color;
            CubePosList.Add(CubePos);//add the transform in the list
            CubeColorList.Add(cubeColor);
        }//列出所有積木的座標
    }

 //   void test()
 //   {
 //       MyClass my = new MyClass();
 //       for(int i = 0; i < 5; i++)
 //       {
 //           my.layer.Add(i);
 //       }
 //       my.num_layer = 1;
 //       Layer.Add(my);

 //       MyClass my2 = new MyClass();
 //       for (int i = 4; i >= 0; i--)
 //       {
 //           my2.layer.Add(i);
 //       }
 //       my2.num_layer = 2;
 //       Layer.Add(my2);
 //   }

    //檢查積木數量
    public void ExamineCubesNum()
    {
        if (myCubeList.Count < 5)
            print("Fail");
        else
            print("good");
    }

    
    //檢查積木顏色
    public int ExamineCubeColor(GameObject gameObject)
    {
        Color obj = gameObject.GetComponent<MeshRenderer>().material.color;
        //if (gameObject.color == Color.blue)
        if (obj == Color.blue)
        {
            print("blue");
            return 1;
        }
        if (obj == Color.red)
        {
            print("red");
            return 2;
        }
        if (obj == new Color32(0, 255,0, 255))
        {
            print("green");
            return 3;
        }
        else
        {
            print("wrong color");
            return 0;
        }
    }

    //檢查積木形狀
      public int ExamineCubeShape(GameObject gameObject)
    {
        print("check shape");
        if (gameObject.transform.localScale == CubeScale)
        {
            print("A cube");
            return 1;
        }
        if (gameObject.transform.localScale == CuboidScale)
        {
            print("A cuboid");
            return 2;
        }
        else
        {
            return 0;
        }
    }

    //public int ExaminePosition_Y(GameObject gameObject)
    public void ExaminePosition()
    {
        double Coordinate_Y = gameObject.transform.position.y;//得到Ｙ座標
        Coordinate_Y = Math.Round(Coordinate_Y, 1);//四捨五入到小數後一位
        double Coordinate_X = gameObject.transform.position.x;//得到X座標
        Coordinate_X = Math.Round(Coordinate_X, 1);//四捨五入到小數後一位
        print("check pos_Y");
        int layer = (int)(Coordinate_Y - 0.5) / 1;
       // int Level =InsertIntoLevel(layer, gameObject);
    }
    /* public int ExaminePosition_X(GameObject gameObject)
    {
        double Coordinate_X = gameObject.transform.position.x;//得到X座標
        Coordinate_X = Math.Round(Coordinate_X, 1);//四捨五入到小數後一位
        print("check pos_X");
        int i = (int)(Coordinate_X - 0.5) / 1;
        return i;
        //if (i){}
    }
    */
   // public int InsertIntoLevel(int layer, GameObject gameObject)
   // {
   //     for(int i = 0; i < myCubeList.Count; i++)
   //     {

   //     }
   // }

    //檢查積木座標(一對多)
    //public int ExamineCubeCoordinate(int i)
    //{
        
    //}
    public void CheckColorAndShape()
    {
        int[] result= new int[]{ 11, 12, 21, 22, 31, 32}; 
        
        for (int i= 0; i<myCubeList.Count; i++)//比顏色跟形狀
        {
            int rtn1 = ExamineCubeColor(myCubeList[i])*10;//i*10
            int rtn2 = ExamineCubeShape(myCubeList[i]);
            //int rtn3 = ExaminePosition_Y(CubePosList[i]);

            //cube
            if (rtn1 + rtn2 == 11 )
            {
                print("congrats its blue cube");
                
            }
            if (rtn1 + rtn2 == 21)
            {
               print("congrats its red cube");
            }
            if (rtn1 + rtn2 == 31)
            {
                print("congrats its green cube");
            }

            //cuboid
            if (rtn1 + rtn2 ==  12)
            {
                print("congrats its blue cuboid");
            }
            if (rtn1 + rtn2 == 22)
                    
            {
                print("congrats its red cuboid");
            }
            if (rtn1 + rtn2 == 22)
            {
                print("congrats its green cuboid");
            }
            if (result[i] != sequence[i])
                print("Checked");
            break;
        }
    }


    //使Stop不能按
    public void ClickStopButton()
    {
        myStopButton.interactable = false;

        if(myStopButton.interactable == false)
        {
            print("disabled");
        }
    }

}

/* public class MyClass
{
   public int[,] layer = new int [20,20];
   //public int num_layer;
}
*/
