using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : ScreenUI
{
    [SerializeField] Button[] listBtnEmote;
    [SerializeField] Animator animator;
    private const string AnimEmoteState =  "Emote";

    void Start()
    {
        SetBtnEmote();
    }
    public void SetBtnEmote(){
        for(int i=0; i< listBtnEmote.Count(); i++){
            int idx = i;
            listBtnEmote[i].onClick.AddListener(()=>ChangeEmote(idx));
        }
    }
    public void ChangeEmote(int idEmote){
        StartCoroutine(SetAnim(idEmote));
        
    }
    public IEnumerator SetAnim(int idEmote){
        animator.SetInteger(AnimEmoteState,idEmote);
        yield return null;
        animator.SetInteger(AnimEmoteState,-1);
    }
}
