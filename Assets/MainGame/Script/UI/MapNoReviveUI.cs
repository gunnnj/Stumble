using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapNoRevive : MonoBehaviour
{
    [SerializeField] GameObject loseUI;
    [SerializeField] GameObject winUI;
    [SerializeField] GameObject cubeStart;
    [SerializeField] TMP_Text textTime;
    [SerializeField] int timeStart = 8;
    [SerializeField] int timeSurvival = 30;
    bool isWin = false;

    void OnEnable()
    {
        GameEvent.eventLoseGame+=LoseGame;
        GameEvent.eventWinGame+=WinGame;
    }

    async void Start()
    {
        loseUI.SetActive(false);
        winUI.SetActive(false);
        await Task.Delay(timeStart*1000);
        cubeStart.SetActive(false);
        StartCoroutine(CoolDownTime());
    }
    void OnDisable()
    {
        GameEvent.eventLoseGame-=LoseGame;
        GameEvent.eventWinGame-=WinGame;
    }
    private void WinGame()
    {
        isWin = true;
        winUI.SetActive(true);
    }
    private void LoseGame()
    {
        if(!isWin){
            loseUI.SetActive(true);
        }
    }
    private IEnumerator CoolDownTime(){
        while(timeSurvival>0){
            textTime.text = timeSurvival.ToString();
            yield return new WaitForSeconds(1f);
            timeSurvival--;
        }
        textTime.text = timeSurvival.ToString();
        GameEvent.eventWinGame?.Invoke();
    }
    public void PlayAgain(){
        int idScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(idScene);
    }
    public void Home(){
        SceneManager.LoadScene(0);
    }
}
