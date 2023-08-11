using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Animator))]
public class AnimatedView : View {
    [SerializeField] private Animator animator;
    [SerializeField] private string closeTrigger = "disappear";
    public event Action ClosedEvent ;

    public override void Close()=>
        ClosePlay();
    
    public void ForceClose() =>
        base.Close();

    private void ClosePlay(){
        if (!animator){
            base.Close();
            return;
        }

        var closeHash = Animator.StringToHash(closeTrigger);
        animator.SetTrigger(closeHash);
    }
    
    private void Handle_OnAnimationEnd() { // Animation event
        ForceClose();
        ClosedEvent?.Invoke();
    }
}