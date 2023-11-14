using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Size")
        {
            GameEvent.instance.playerSize.Value += 1;
            other.GetComponent<Collider>().enabled = false;
            other.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                Destroy(other.gameObject);
            });
        }
        if(other.tag == "Gate")
        {
            other.GetComponent<Gate>().ExecuteOperation();
        }
        if (other.tag == "Obstacle")
        {
            other.GetComponent<Block>().CheckHit();
        }
        if (other.tag == "Finish")
        {
            GameEvent.instance.gameWon.SetValueAndForceNotify(true);
        }
    }
}
