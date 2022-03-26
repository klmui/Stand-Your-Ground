using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool key1 = Input.GetKey("1");
        if (key1) {
            SceneManager.LoadScene(0);
        }

        bool key2 = Input.GetKey("2");
        if (key2) {
            SceneManager.LoadScene(1);
        }
    }
}
