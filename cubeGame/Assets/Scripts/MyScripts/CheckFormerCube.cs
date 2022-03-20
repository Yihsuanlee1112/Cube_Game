using UnityEngine;

// 確認積木擺放是否正確，播放 correct sound (播放失敗)
public class CheckFormerCube : MonoBehaviour
{
    // 這行可以拿掉
    //public GameObject checkFormerCube;

    //public AudioClip correctSound;
    //public AudioClip wrongSound;
    //Animator animator;
    private int i, j;

    AudioSource checkAudioSource;
    AudioClip clip;
    //putCube putcube;
    //public AudioClip[] Sound;

    void Start()
    {
        checkAudioSource = GetComponent<AudioSource>();
        //checkAudioSource = putcube.GetComponent<AudioSource>();

        //correctSound = Resources.Load<AudioClip>("AudioClip/correctSound");
        //wrongSound = Resources.Load<AudioClip>("AudioClip/wrongSound");
    }
    public bool CheckQ1Former(Collider Cube)
    //public IEnumerator CheckQ1Former(Collider Cube)
    {
        j = 1;
        Collider[] QuadQ1 = {
            GameObject.Find("RedCube(Quad0Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("BlueCube(Quad1Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("YellowCuboid(Quad2Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("GreenCuboid(Quad3Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("RedCuboid(Quad4Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("BlueCuboid(Quad5Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("YellowCuboid3(Quad6Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("GreenCuboid3(Quad7Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("RedCuboid3(Quad8Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("BlueCuboid3(Quad9Q1)").GetComponent<BoxCollider>(),
        };

        bool flag = true;
        Debug.Log("I'm checking");
        print("fucking bitch");
        if (Cube == GameObject.Find("RedCube(Clone)").GetComponent<BoxCollider>()) { i = 0; }
        if (Cube == GameObject.Find("BlueCube(Clone)").GetComponent<BoxCollider>()) { i = 1; }
        if (Cube == GameObject.Find("YellowCuboid(Clone)").GetComponent<BoxCollider>()) { i = 2; }
        if (Cube == GameObject.Find("GreenCuboid(Clone)").GetComponent<BoxCollider>()) { i = 3; }
        if (Cube == GameObject.Find("RedCuboid(Clone)").GetComponent<BoxCollider>()) { i = 4; }
        if (Cube == GameObject.Find("BlueCuboid(Clone)").GetComponent<BoxCollider>()) { i = 5; }
        if (Cube == GameObject.Find("YellowCuboid3(Clone)").GetComponent<BoxCollider>()) { i = 6; }
        if (Cube == GameObject.Find("GreenCuboid3(Clone)").GetComponent<BoxCollider>()) { i = 7; }
        if (Cube == GameObject.Find("RedCuboid3(Clone)").GetComponent<BoxCollider>()) { i = 8; }
        if (Cube == GameObject.Find("BlueCuboid3(Clone)").GetComponent<BoxCollider>()) { i = 9; }


        while (true)
        {
            Debug.Log("in while " + Cube);
            Debug.Log(QuadQ1[i]);
            if (i == 9)
            {
                print("good job!!");

                clip = Resources.Load<AudioClip>("AudioClip/correctSound");
                checkAudioSource.PlayOneShot(clip);

                // 共用 putcube 的 audioSource
                //putcube.GetComponent<AudioSource>().PlayOneShot(clip);

                // 睿哲
                //audioSource.clip = correctSound;
                //audioSource.Play();

                // 框架
                //GameAudioController.Instance.PlayOneShot(clip);

                // 讓putCube播

                //flag = true;
                //Debug.Log(clip);
                Cube.GetComponent<Rigidbody>().isKinematic = true;
                print("this " + Cube.name + "iskinematic");
                flag = true;
                break;
            }
            else if (QuadQ1[i].isTrigger == false && QuadQ1[i + 1].isTrigger == false)
            {
                print("great job!!");
                clip = Resources.Load<AudioClip>("AudioClip/correctSound");
                checkAudioSource.PlayOneShot(clip);
                /*
                clip = Resources.Load<AudioClip>("AudioClip/wrongSound");
                audioSource.PlayOneShot(clip);
                */
                //audioSource.clip = correctSound;
                //audioSource.Play();
                //audioSource.PlayOneShot(correctSound);
                //Debug.Log(clip);
                //GameAudioController.Instance.PlayOneShot(clip);
                Cube.GetComponent<Rigidbody>().isKinematic = true;
                print("this " + Cube.name + "iskinematic");
                flag = true;
                break;
            }
            else if (QuadQ1[i].isTrigger == false && QuadQ1[i + 1].isTrigger == true)
            {
                Debug.Log("wrong");
                print("put former first!!");
                clip = Resources.Load<AudioClip>("AudioClip/wrongSound");
                checkAudioSource.PlayOneShot(clip);
                //audioSource.PlayOneShot(clip);
                //Debug.Log(clip);
                //GameAudioController.Instance.PlayOneShot(clip);
                print("music");
                Cube.transform.SetPositionAndRotation(new Vector3(12, 2, UnityEngine.Random.Range((float)-7.0, (float)7.0)), Quaternion.Euler(new Vector3(0, 0, 0)));
                flag = false;
                break;
            }
            else if (QuadQ1[i].isTrigger == true && QuadQ1[i + 1].isTrigger == true)
            {
                print("try it again");
                clip = Resources.Load<AudioClip>("AudioClip/wrongSound");
                checkAudioSource.PlayOneShot(clip);
               // Debug.Log(clip);
                //audioSource.PlayOneShot(clip);
                //Debug.Log(clip);
                //GameAudioController.Instance.PlayOneShot(clip);
                Cube.transform.position = new Vector3(12, 2, UnityEngine.Random.Range((float)-7.0, (float)7.0));
                print("you can do it");
                flag = false;
                break;
            }
            print("out of while loop");
            break;
        }
        return flag;
        //StartCoroutine(ActionOne());
        //StartCoroutine(ActionTwo());
        //yield return flag;
    }
    public bool CheckQ2Former(Collider Cube)
    //public IEnumerator CheckQ1Former(Collider Cube)
    {
        Collider[] QuadQ2 = {
            GameObject.Find("RedCube(Quad0Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("BlueCube(Quad1Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("YellowCuboid(Quad2Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("GreenCuboid(Quad3Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("RedCuboid(Quad4Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("BlueCuboid(Quad5Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("YellowCuboid3(Quad6Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("GreenCuboid3(Quad7Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("RedCuboid3(Quad8Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("BlueCuboid3(Quad9Q1)").GetComponent<BoxCollider>(),
        };

        bool flag = true;
        Debug.Log("I'm checking");
        print("fucking bitch");
        if (Cube == GameObject.Find("Q2RedCube(Clone)").GetComponent<BoxCollider>()) { i = 0; }
        if (Cube == GameObject.Find("Q2BlueCube(Clone)").GetComponent<BoxCollider>()) { i = 1; }
        if (Cube == GameObject.Find("Q2YellowCuboid(Clone)").GetComponent<BoxCollider>()) { i = 2; }
        if (Cube == GameObject.Find("Q2GreenCuboid(Clone)").GetComponent<BoxCollider>()) { i = 3; }
        if (Cube == GameObject.Find("Q2RedCuboid(Clone)").GetComponent<BoxCollider>()) { i = 4; }
        if (Cube == GameObject.Find("Q2BlueCuboid(Clone)").GetComponent<BoxCollider>()) { i = 5; }
        if (Cube == GameObject.Find("Q2YellowCuboid3(Clone)").GetComponent<BoxCollider>()) { i = 6; }
        if (Cube == GameObject.Find("Q2GreenCuboid3(Clone)").GetComponent<BoxCollider>()) { i = 7; }
        if (Cube == GameObject.Find("Q2RedCuboid3(Clone)").GetComponent<BoxCollider>()) { i = 8; }
        if (Cube == GameObject.Find("Q2BlueCuboid3(Clone)").GetComponent<BoxCollider>()) { i = 9; }


        while (true)
        {
            Debug.Log("in while " + Cube);
            Debug.Log(QuadQ2[i]);
            if (i == 9)
            {
                print("good job!!");

                clip = Resources.Load<AudioClip>("AudioClip/correctSound");
                checkAudioSource.PlayOneShot(clip);

                // 共用 putcube 的 audioSource
                //putcube.GetComponent<AudioSource>().PlayOneShot(clip);

                // 睿哲
                //audioSource.clip = correctSound;
                //audioSource.Play();

                // 框架
                //GameAudioController.Instance.PlayOneShot(clip);

                // 讓putCube播

                //flag = true;
                //Debug.Log(clip);
                Cube.GetComponent<Rigidbody>().isKinematic = true;
                print("this " + Cube.name + "iskinematic");
                flag = true;
                break;
            }
            else if (QuadQ2[i].isTrigger == false && QuadQ2[i + 1].isTrigger == false)
            {
                print("great job!!");
                clip = Resources.Load<AudioClip>("AudioClip/correctSound");
                checkAudioSource.PlayOneShot(clip);
                /*
                clip = Resources.Load<AudioClip>("AudioClip/wrongSound");
                audioSource.PlayOneShot(clip);
                */
                //audioSource.clip = correctSound;
                //audioSource.Play();
                //audioSource.PlayOneShot(correctSound);
                //Debug.Log(clip);
                //GameAudioController.Instance.PlayOneShot(clip);
                Cube.GetComponent<Rigidbody>().isKinematic = true;
                print("this " + Cube.name + "iskinematic");
                flag = true;
                break;
            }
            else if (QuadQ2[i].isTrigger == false && QuadQ2[i + 1].isTrigger == true)
            {
                Debug.Log("wrong");
                print("put former first!!");
                clip = Resources.Load<AudioClip>("AudioClip/wrongSound");
                checkAudioSource.PlayOneShot(clip);
                //audioSource.PlayOneShot(clip);
                //Debug.Log(clip);
                //GameAudioController.Instance.PlayOneShot(clip);
                print("music");
                Cube.transform.SetPositionAndRotation(new Vector3(12, 2, UnityEngine.Random.Range((float)-7.0, (float)7.0)), Quaternion.Euler(new Vector3(0, 0, 0)));
                flag = false;
                break;
            }
            else if (QuadQ2[i].isTrigger == true && QuadQ2[i + 1].isTrigger == true)
            {
                print("try it again");
                clip = Resources.Load<AudioClip>("AudioClip/wrongSound");
                checkAudioSource.PlayOneShot(clip);
                // Debug.Log(clip);
                //audioSource.PlayOneShot(clip);
                //Debug.Log(clip);
                //GameAudioController.Instance.PlayOneShot(clip);
                Cube.transform.position = new Vector3(12, 2, UnityEngine.Random.Range((float)-7.0, (float)7.0));
                print("you can do it");
                flag = false;
                break;
            }
            print("out of while loop");
            break;
        }
        return flag;
        //StartCoroutine(ActionOne());
        //StartCoroutine(ActionTwo());
        //yield return flag;
    }
    public bool CheckQ3Former(Collider Cube)
    //public IEnumerator CheckQ1Former(Collider Cube)
    {
        Collider[] QuadQ3 = {
            GameObject.Find("RedCube(Quad0Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("BlueCube(Quad1Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("YellowCuboid(Quad2Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("GreenCuboid(Quad3Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("RedCuboid(Quad4Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("BlueCuboid(Quad5Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("YellowCuboid3(Quad6Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("GreenCuboid3(Quad7Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("YellowCubeQuad8Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("BlueCuboid3(Quad9Q1)").GetComponent<BoxCollider>(),
        };

        bool flag = true;
        Debug.Log("I'm checking");
        print("fucking bitch");
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 0; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 1; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 2; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 3; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 4; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 5; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 6; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 7; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 8; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 9; }


        while (true)
        {
            Debug.Log("in while " + Cube);
            Debug.Log(QuadQ3[i]);
            if (i == 9)
            {
                print("good job!!");

                clip = Resources.Load<AudioClip>("AudioClip/correctSound");
                checkAudioSource.PlayOneShot(clip);

                // 共用 putcube 的 audioSource
                //putcube.GetComponent<AudioSource>().PlayOneShot(clip);

                // 睿哲
                //audioSource.clip = correctSound;
                //audioSource.Play();

                // 框架
                //GameAudioController.Instance.PlayOneShot(clip);

                // 讓putCube播

                //flag = true;
                //Debug.Log(clip);
                Cube.GetComponent<Rigidbody>().isKinematic = true;
                print("this " + Cube.name + "iskinematic");
                flag = true;
                break;
            }
            else if (QuadQ3[i].isTrigger == false && QuadQ3[i + 1].isTrigger == false)
            {
                print("great job!!");
                clip = Resources.Load<AudioClip>("AudioClip/correctSound");
                checkAudioSource.PlayOneShot(clip);
                /*
                clip = Resources.Load<AudioClip>("AudioClip/wrongSound");
                audioSource.PlayOneShot(clip);
                */
                //audioSource.clip = correctSound;
                //audioSource.Play();
                //audioSource.PlayOneShot(correctSound);
                //Debug.Log(clip);
                //GameAudioController.Instance.PlayOneShot(clip);
                Cube.GetComponent<Rigidbody>().isKinematic = true;
                print("this " + Cube.name + "iskinematic");
                flag = true;
                break;
            }
            else if (QuadQ3[i].isTrigger == false && QuadQ3[i + 1].isTrigger == true)
            {
                Debug.Log("wrong");
                print("put former first!!");
                clip = Resources.Load<AudioClip>("AudioClip/wrongSound");
                checkAudioSource.PlayOneShot(clip);
                //audioSource.PlayOneShot(clip);
                //Debug.Log(clip);
                //GameAudioController.Instance.PlayOneShot(clip);
                print("music");
                Cube.transform.SetPositionAndRotation(new Vector3(12, 2, UnityEngine.Random.Range((float)-7.0, (float)7.0)), Quaternion.Euler(new Vector3(0, 0, 0)));
                flag = false;
                break;
            }
            else if (QuadQ3[i].isTrigger == true && QuadQ3[i + 1].isTrigger == true)
            {
                print("try it again");
                clip = Resources.Load<AudioClip>("AudioClip/wrongSound");
                checkAudioSource.PlayOneShot(clip);
                // Debug.Log(clip);
                //audioSource.PlayOneShot(clip);
                //Debug.Log(clip);
                //GameAudioController.Instance.PlayOneShot(clip);
                Cube.transform.position = new Vector3(12, 2, UnityEngine.Random.Range((float)-7.0, (float)7.0));
                print("you can do it");
                flag = false;
                break;
            }
            print("out of while loop");
            break;
        }
        return flag;
        //StartCoroutine(ActionOne());
        //StartCoroutine(ActionTwo());
        //yield return flag;
    }
    public bool CheckQ4Former(Collider Cube)
    //public IEnumerator CheckQ1Former(Collider Cube)
    {
        Collider[] QuadQ4 = {
            GameObject.Find("RedCube(Quad0Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("BlueCube(Quad1Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("YellowCuboid(Quad2Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("GreenCuboid(Quad3Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("RedCuboid(Quad4Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("BlueCuboid(Quad5Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("YellowCuboid3(Quad6Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("GreenCuboid3(Quad7Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("RedCuboid3(Quad8Q1)").GetComponent<BoxCollider>(),
            GameObject.Find("BlueCuboid3(Quad9Q1)").GetComponent<BoxCollider>(),
        };

        bool flag = true;
        Debug.Log("I'm checking");
        print("fucking bitch");
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 0; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 1; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 2; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 3; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 4; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 5; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 6; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 7; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 8; }
        if (Cube == GameObject.Find("(Clone)").GetComponent<BoxCollider>()) { i = 9; }


        while (true)
        {
            Debug.Log("in while " + Cube);
            Debug.Log(QuadQ4[i]);
            if (i == 9)
            {
                print("good job!!");

                clip = Resources.Load<AudioClip>("AudioClip/correctSound");
                checkAudioSource.PlayOneShot(clip);

                // 共用 putcube 的 audioSource
                //putcube.GetComponent<AudioSource>().PlayOneShot(clip);

                // 睿哲
                //audioSource.clip = correctSound;
                //audioSource.Play();

                // 框架
                //GameAudioController.Instance.PlayOneShot(clip);

                // 讓putCube播

                //flag = true;
                //Debug.Log(clip);
                Cube.GetComponent<Rigidbody>().isKinematic = true;
                print("this " + Cube.name + "iskinematic");
                flag = true;
                break;
            }
            else if (QuadQ4[i].isTrigger == false && QuadQ4[i + 1].isTrigger == false)
            {
                print("great job!!");
                clip = Resources.Load<AudioClip>("AudioClip/correctSound");
                checkAudioSource.PlayOneShot(clip);
                /*
                clip = Resources.Load<AudioClip>("AudioClip/wrongSound");
                audioSource.PlayOneShot(clip);
                */
                //audioSource.clip = correctSound;
                //audioSource.Play();
                //audioSource.PlayOneShot(correctSound);
                //Debug.Log(clip);
                //GameAudioController.Instance.PlayOneShot(clip);
                Cube.GetComponent<Rigidbody>().isKinematic = true;
                print("this " + Cube.name + "iskinematic");
                flag = true;
                break;
            }
            else if (QuadQ4[i].isTrigger == false && QuadQ4[i + 1].isTrigger == true)
            {
                Debug.Log("wrong");
                print("put former first!!");
                clip = Resources.Load<AudioClip>("AudioClip/wrongSound");
                checkAudioSource.PlayOneShot(clip);
                //audioSource.PlayOneShot(clip);
                //Debug.Log(clip);
                //GameAudioController.Instance.PlayOneShot(clip);
                print("music");
                Cube.transform.SetPositionAndRotation(new Vector3(12, 2, UnityEngine.Random.Range((float)-7.0, (float)7.0)), Quaternion.Euler(new Vector3(0, 0, 0)));
                flag = false;
                break;
            }
            else if (QuadQ4[i].isTrigger == true && QuadQ4[i + 1].isTrigger == true)
            {
                print("try it again");
                clip = Resources.Load<AudioClip>("AudioClip/wrongSound");
                checkAudioSource.PlayOneShot(clip);
                // Debug.Log(clip);
                //audioSource.PlayOneShot(clip);
                //Debug.Log(clip);
                //GameAudioController.Instance.PlayOneShot(clip);
                Cube.transform.position = new Vector3(12, 2, UnityEngine.Random.Range((float)-7.0, (float)7.0));
                print("you can do it");
                flag = false;
                break;
            }
            print("out of while loop");
            break;
        }
        return flag;
        //StartCoroutine(ActionOne());
        //StartCoroutine(ActionTwo());
        //yield return flag;
    }
}

