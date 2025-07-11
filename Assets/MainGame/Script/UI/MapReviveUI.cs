using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapReviveUI : MonoBehaviour
{
    [SerializeField] TMP_Text txtRank;
    [SerializeField] GameObject loseUI;
    [SerializeField] GameObject winUI;
    [SerializeField] TMP_Text textTime;
    [SerializeField] int timeCooldown = 30;
    [SerializeField] int objectiveRank = 3;
    bool isWin = false;
    int rank;

    void OnEnable()
    {
        // GameEvent.eventLoseGame+=LoseGame;
        GameEvent.eventWinGame+=WinGame;
        GameEvent.eventFinish+=UpdateRank;
    }
    void Start()
    {
        rank = 0;
        loseUI.SetActive(false);
        winUI.SetActive(false);
        StartCoroutine(CoolDownTime());
    }
    void OnDisable()
    {
        GameEvent.eventLoseGame-=LoseGame;
        GameEvent.eventWinGame-=WinGame;
        GameEvent.eventFinish-=UpdateRank;
    }
    private void WinGame()
    {
        winUI.SetActive(true);
        isWin = true;
    }
    private void LoseGame()
    {
        if(!isWin){
            loseUI.SetActive(true);
        }
    }
    public void UpdateRank()
    {
        Debug.Log("Updaterank");
        rank+=1;
        txtRank.text = $"{rank}/{objectiveRank}";
    }
    private IEnumerator CoolDownTime(){
        while(timeCooldown>0){
            textTime.text = timeCooldown.ToString();
            yield return new WaitForSeconds(1f);
            timeCooldown--;
        }
        textTime.text = timeCooldown.ToString();
        GameEvent.eventLoseGame?.Invoke();
    }
    public void PlayAgain(){
        int idScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(idScene);
    }
    public void Home(){
        SceneManager.LoadScene(0);
    }
    public void PlayMap(int index){
        SceneManager.LoadScene(index);
    }
}
