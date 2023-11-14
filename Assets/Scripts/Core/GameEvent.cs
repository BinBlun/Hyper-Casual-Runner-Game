using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();
    public static GameEvent instance { get; private set; }
    public BoolReactiveProperty gameStarted { get; set; } = new BoolReactiveProperty(false);
    public BoolReactiveProperty gameWon {  get; set;}
    public BoolReactiveProperty gameLost { get; set; } = new BoolReactiveProperty(false);
    public IntReactiveProperty playerSize {  get; set; } = new IntReactiveProperty(1);

    private void Awake()
    {
        instance = this; 
    }

    private void OnEnable()
    {
        playerSize.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value <= 0)
                {
                    gameLost.SetValueAndForceNotify(true);
                }
            })
            .AddTo(subscriptions);
    }

    private void OnDisable()
    {
        subscriptions.Clear();
    }
}
