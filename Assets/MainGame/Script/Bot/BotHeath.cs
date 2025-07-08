using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BotHeath : BaseHealth
{
    [SerializeField] Transform transParent;
    [SerializeField] BotShooterMode botShooterMode;
    [SerializeField] Slider health;
    [SerializeField] Slider armor;
    protected override async void Revide()
    {
        base.Revide();
        shooterModeUI.UpdateScore(botShooterMode.tagTarget);
        Debug.Log(transParent.name+" dead");
        transParent.gameObject.SetActive(false);
        await Task.Delay(1000);
        ResetUI();
        botShooterMode.SetPowerByGun();
        transParent.position = RevidePosition;
        transParent.gameObject.SetActive(true);
        botShooterMode.agent.enabled = true;
        botShooterMode.SetNewPos();

    }

    protected override void UpdateHealthUI(int CurrentArmor, int CurrentHp)
    {
        base.UpdateHealthUI(CurrentArmor, CurrentHp);
        health.value = (float)CurrentHp/100;
        armor.value = (float)CurrentArmor/100;
    }
    public void ResetUI(){
        health.value = 1;
        armor.value = 1;
    }
}
