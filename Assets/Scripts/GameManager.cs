using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject SelectionWindow;
    [SerializeField] private GameObject GameOverWindow;
    [SerializeField] private float SelectionWaitTime;
    [SerializeField] private float SelectionCurrentTime;
    [SerializeField] private TMP_Text SelectionTimer;

    private void Awake()
    {
        SelectionCurrentTime = SelectionWaitTime;
        Invoke("ApperSelectionWindow", SelectionWaitTime);
    }

    private void Update()
    {
        SelectionTimer.text = "Timer : " + Mathf.Round(SelectionCurrentTime).ToString();
        SelectionCurrentTime -= Time.deltaTime;
        if (SelectionCurrentTime <= 0)
            SelectionCurrentTime = 0f;
    }

    private void ApperSelectionWindow()
    {
        Time.timeScale = 0f;
        SelectionWindow.SetActive(true);
    }

    public void ApperGameOverWindow()
    {
        Time.timeScale = 0f;
        GameOverWindow.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void DisapperSelectionWindow()
    {
        Time.timeScale = 1f;
        SelectionWindow.SetActive(false);
        SelectionCurrentTime = SelectionWaitTime;
        Invoke("ApperSelectionWindow", SelectionWaitTime);
    }

}
