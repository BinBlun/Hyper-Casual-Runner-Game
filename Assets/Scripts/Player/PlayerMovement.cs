using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();

    [SerializeField] Transform playerTransform;
    [SerializeField] float limitValue;
    [SerializeField] float sidewaySpeed;

    private bool lockControls;
    private float _finalPos;
    private float _currentPos;
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Subcribe());
    }

    private IEnumerator Subcribe()
    {
        yield return new WaitUntil(() => GameEvent.instance != null);
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButton(0))
            .Subscribe(x =>
            {
                if (GameEvent.instance.gameStarted.Value && !GameEvent.instance.gameLost.Value
                && !GameEvent.instance.gameWon.Value)
                {
                    MovePlayer();
                }
            })
            .AddTo(subscriptions);

        GameEvent.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                {
                    lockControls = true;
                }
            })
            .AddTo(subscriptions);

        GameEvent.instance.gameLost.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if(value)
                {
                    lockControls = true;
                }
            })
            .AddTo(subscriptions);
    }

    private void OnDisable()
    {
        subscriptions.Clear();
    }

    void MovePlayer()
    {
        if (Input.GetMouseButton(0))
        {
            float halfScreen = Screen.width / 2;
            float xPos = ((Input.mousePosition.x - halfScreen) / halfScreen) * 2;
            xPos = Mathf.Clamp(xPos, -1.0f, 1.0f);
            _finalPos = xPos * limitValue;
        }
        //Calculate the x position

        float delta = _finalPos - _currentPos;
        _currentPos += delta * Time.deltaTime * sidewaySpeed;
        _currentPos = Mathf.Clamp(_currentPos,-limitValue, limitValue);
        playerTransform.localPosition = new Vector3(_currentPos, 0, 0);
    }
}
