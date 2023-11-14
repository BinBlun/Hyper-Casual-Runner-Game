using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    private CompositeDisposable subcriptions = new CompositeDisposable();
    [SerializeField] private TextMeshPro sizeText;
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
                    sizeText.text = value.ToString();
                    sizeText.transform.parent.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.25f);
                    transform.GetChild(0).DOScale(new Vector3(size, size, size), 0.25f).SetEase(Ease.OutBack);
                    curentSize = size;
                }
            })
            .AddTo(subcriptions);

        GameEvent.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                {
                    sizeText.transform.parent.DOScale(Vector3.zero, 0.25f);
                }
            })
            .AddTo(subcriptions);

        GameEvent.instance.gameLost.ObserveEveryValueChanged (x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                {
                    sizeText.transform.parent.DOScale(Vector3.zero, 0.25f);
                }
            })
            .AddTo(subcriptions);
    }

    private void OnDisable()
    {
        subcriptions.Clear();
    }
}
