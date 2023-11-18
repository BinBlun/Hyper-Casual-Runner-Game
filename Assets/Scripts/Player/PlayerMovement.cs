using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();

    [SerializeField] float limitValue;
    [SerializeField] float sidewaySpeed;
    [SerializeField] Transform playerTransform;

    private float _finalPos;
    private float _currentPos;
    
    void OnEnable()
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
    }

    private void OnDisable()
    {
        subscriptions.Clear();
    }

    private void MovePlayer()
    {
        if (Input.GetMouseButton(0))
        {
            float percentageX = (Input.mousePosition.x - Screen.width / 2) / (Screen.width * 0.5f) * 2;
            percentageX = Mathf.Clamp(percentageX, -1.0f, 1.0f);
            _finalPos = percentageX * limitValue;
        }
        //Calculate the x position
        float delta = _finalPos - _currentPos;
        _currentPos += (delta * Time.deltaTime * sidewaySpeed);
        _currentPos = Mathf.Clamp(_currentPos, -limitValue, limitValue);
        playerTransform.localPosition = new Vector3(_currentPos, 0, 0);
    }
}
