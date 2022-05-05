using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menucontrol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void city()
    {
        SceneManager.LoadScene("city");
    }
    public void Base()
    {
        SceneManager.LoadScene("base");
    }
    public void farm()
    {
        SceneManager.LoadScene("farm");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
