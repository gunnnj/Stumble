using System.Threading.Tasks;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    [SerializeField] Transform transParent;
    [SerializeField] PlayerShootMode playerShoot;
    protected override void Start()
    {
        base.Start();
    }

    protected override async void Revide()
    {
        base.Revide();
        shooterModeUI.UpdateScore(TagTarget.Bot);
        transParent.gameObject.SetActive(false);
        await Task.Delay(1000);
        playerShoot.PickGun(TypeGun.Light);
        transParent.position = RevidePosition;
        transParent.gameObject.SetActive(true);
        shooterModeUI.ResetUI();
        shooterModeUI.ActiveOptionGun(true);

    }
    protected override void UpdateHealthUI(int CurrentArmor, int CurrentHp)
    {
        base.UpdateHealthUI(CurrentArmor, CurrentHp);
        shooterModeUI.UpdateUI(CurrentArmor,CurrentHp);
    }

}
