using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShooterModeUI : MonoBehaviour
{
    [SerializeField] GameObject WinUI;
    [SerializeField] GameObject LoseUI;
    [SerializeField] GameObject OptionGun;
    [SerializeField] Button gunLight;
    [SerializeField] Button gunMedium;
    [SerializeField] Button gunWeight;
    [SerializeField] Button btnGo;
    [SerializeField] Slider Armor;
    [SerializeField] Slider Health;
    [SerializeField] Slider TimePickGun;
    [SerializeField] TMP_Text scoreLeftTeam;
    [SerializeField] TMP_Text scoreRightTeam;
    [SerializeField] TMP_Text textTime;
    [SerializeField] int timePlay = 120;
    private int scoreL;
    private int scoreR;
    private PlayerShootMode playerShootMode;

    public Action onJump;
    public Action onShoot;
    async void Start()
    {
        Time.timeScale = 1;
        playerShootMode = FindFirstObjectByType<PlayerShootMode>();
        gunLight.onClick.AddListener(()=>playerShootMode.PickGun(TypeGun.Light));
        gunMedium.onClick.AddListener(()=>playerShootMode.PickGun(TypeGun.Medium));
        gunWeight.onClick.AddListener(()=>playerShootMode.PickGun(TypeGun.Weight));
        btnGo.onClick.AddListener(()=>ActiveOptionGun(false));
        ResetUI();
        ActiveOptionGun(false);

        scoreLeftTeam.text = "0";
        scoreRightTeam.text = "0";
        scoreL = 0;
        scoreR = 0;
        WinUI.SetActive(false);
        LoseUI.SetActive(false);
        StartCoroutine(CooldownTime(timePlay));

        await Task.Delay(1000);
        ActiveOptionGun(true);
    }
    public void ActiveOptionGun(bool value){
        OptionGun.SetActive(value);
        if(value){
            StartCoroutine(CooldownPickGun(5f));
        }
    }
    
    public void OnJump(){
        onJump?.Invoke();
    }
    public void OnShoot(){
        onShoot?.Invoke();
    }
    public void UpdateUI(int armor, int health){
        if(armor>0){
            Armor.value = (float)armor/100;
        }
        else{
            Armor.value = 0;
        }

        if(health>0){
            Health.value = (float)health/100;
        }
        else{
            Health.value = 0;
        }
        
    }
    
    public void UpdateScore(TagTarget tag){
        if(tag.Equals(TagTarget.Player)){
            scoreL ++;
            scoreLeftTeam.text = scoreL.ToString();
        }
        else{
            scoreR++;
            scoreRightTeam.text = scoreR.ToString();
        }
    }
    public void ResetUI(){
        Armor.value = 1;
        Health.value = 1;
    }

    public IEnumerator CooldownTime(int time){
        while(time>0){
            yield return new WaitForSeconds(1f);
            time--;
            textTime.text = time.ToString();
        }
        textTime.text = "0";
        Time.timeScale = 0;
        if(scoreL>scoreR){
            WinUI.SetActive(true);
        }
        else if(scoreL==scoreR){
            scoreL ++;
            scoreLeftTeam.text = scoreL.ToString();
            WinUI.SetActive(true);
        }
        else{
            LoseUI.SetActive(true);
        }
    }
    public IEnumerator CooldownPickGun(float time){
        float elap = time/100;
        float maxTime = time;
        while(time>0){
            yield return new WaitForSeconds(elap);
            time -= elap;
            TimePickGun.value = time/maxTime;
        }
        ActiveOptionGun(false);
    }
    public void PlayAgain(){
        int idScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(idScene);
    }
    public void Home(){
        SceneManager.LoadScene(0);
    }
}
