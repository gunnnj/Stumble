using System.Threading.Tasks;
using DiasGames.Abilities;
using DiasGames.Controller;
using UnityEngine;

public class BotJump : MonoBehaviour
{
    [SerializeField] public CSPlayerController bot;
    [SerializeField] public ClimbLadderAbility climbLadderAbility;


    async void Start()
    {
        climbLadderAbility = GetComponent<ClimbLadderAbility>();
        bot.OnJump(true);
        climbLadderAbility.autoClimb = true;
        climbLadderAbility.climbSpeed = 2f;
        climbLadderAbility.energy = 45f;
        await Task.Delay(5000);
        bot.OnDrop(true);
        await Task.Delay(2000);
        bot.OnJump(true);
        climbLadderAbility.autoClimb = true;
        await Task.Delay(5000);
        bot.OnDrop(true);
        
    }
    void Update()
    {
        
    }
}
