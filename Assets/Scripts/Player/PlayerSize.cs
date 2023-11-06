using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    private CompositeDisposable subcriptions = new CompositeDisposable();
    private float curentSize = 1.0f;

    private void OnEnable()
    {
        StartCoroutine(Subscribe());
    }

    private IEnumerator Subscribe()
    {
        yield return new WaitUntil(() =>  GameEvent.instance != null);
        GameEvent.instance.playerSize.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                float size = 1 + (value - 1) * 0.1f;
                
                if(size != curentSize)
                {
                    transform.GetChild(0).DOScale(new Vector3(size, size, size), 0.25f).SetEase(Ease.OutBack);
                    curentSize = size;
                }
            })
            .AddTo(subcriptions);
    }
}
