using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private CompositeDisposable subcriptions = new CompositeDisposable();
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(Subcribe());
    }

    private IEnumerator Subcribe()
    {
        yield return new WaitUntil(() => GameEvent.instance != null);
        GameEvent.instance.gameStarted.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                anim.SetBool("moving", value);
            })
            .AddTo(subcriptions);

        GameEvent.instance.gameLost.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                {
                    anim.SetTrigger("die");
                }
            })
            .AddTo(subcriptions);

        GameEvent.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    anim.SetTrigger("win");
            })
            .AddTo(subcriptions);
    }
    private void OnDisable()
    {
        subcriptions.Clear();
    }

}
