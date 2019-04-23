using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
 using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroManagerScript : MonoBehaviour
{
    public Text PlayText;
    float FlashIndex = 0;
    public Image TransitionImage;
    SceneManager sceneManager;
    bool ChangingScene = false;
    // Start is called before the first frame update
    void Start()
    {
        sceneManager = new SceneManager();
    }

    // Update is called once per frame
    void Update()
    {
        flashPlayButton();
    }

    void flashPlayButton()
    {
        Color c = new Color(1, 1, 1, (float)((0.5 * Mathf.Cos(FlashIndex*Mathf.PI)) + 0.5f));
        PlayText.color = c;
        FlashIndex += 0.02f;
        if (FlashIndex >= 2)
        {
            FlashIndex = 0;
        }
    }

    public IEnumerator GoToGameScene()
    {
        

        for (float i = 0; i <= 1; i += 0.04f)
        {
            Color c = new Color(1, 1, 1, i);
            TransitionImage.color = c;
            yield return new WaitForSeconds(0.002f);
        }
        SceneManager.LoadScene("New scene ofzo idk");
        SceneManager.UnloadSceneAsync("IntroScene");
          
    }

    public void NextScene()
    {
        Debug.Log("NextScene");
        if (ChangingScene == false)
        {
            ChangingScene = true;
            StartCoroutine("GoToGameScene");
        }
    }

}
