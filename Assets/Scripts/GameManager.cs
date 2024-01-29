using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text clearText;
    [SerializeField] Text titleText;
    [SerializeField] Button titleButton;
    GameObject[] tagObjects;

    private float timer = 0.0f;
    float interval = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        clearText.enabled = false;
        titleText.enabled = false;
        titleButton.enabled = false;
        titleButton.image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            Check("Enemy");
            timer = 0.0f;
        }
    }

    private void Check(string tagName)
    {
        tagObjects = GameObject.FindGameObjectsWithTag(tagName);
        if(tagObjects.Length == 0)
        {
            clearText.enabled = true;
            titleText.enabled = true;
            titleButton.enabled = true;
            titleButton.image.enabled = true;
        }
    }

    // Startボタン
    public void SrartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Titleボタン
    public void TitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    // Quitボタン
    public void QuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
