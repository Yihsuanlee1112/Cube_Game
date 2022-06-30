using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabData;
using DataSync;
namespace lab317
{
    public class StartPage : LabDataBase
    {
        public string userID;

        public StartPage(string Id)
        {
            userID = Id;
        }
    }
}
/*
public class Lab317DataBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/