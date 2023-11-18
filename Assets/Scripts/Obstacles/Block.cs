using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("Size and Color")]
    [SerializeField] int startingSize;
    [SerializeField] Material[] blockColor;
    [SerializeField] MeshRenderer blockMesh;

    [Header("References")]
    [SerializeField] GameObject completeBlock;
    [SerializeField] GameObject brokenBlock;
    [SerializeField] TextMeshPro blockSizeText;

    private void Awake()
    {
        completeBlock.SetActive(true);
        brokenBlock.SetActive(false);
        blockSizeText.text = startingSize.ToString();
        AssignColor();
    }

    private void AssignColor()
    {
        int colorIndex = (startingSize - 1)/3;
        colorIndex = Mathf.Clamp(colorIndex, 0, blockColor.Length - 1);
        blockMesh.material = blockColor[colorIndex];
    }

    public void CheckHit()
    {
        Handheld.Vibrate();
        Camera.main.transform.DOShakePosition(0.1f, 0.5f, 5);

        if (GameEvent.instance.playerSize.Value > startingSize)
        {
            ParticleManager.instance.PlayParticle(0, transform.position);
            GameEvent.instance.playerSize.Value -= startingSize;
            completeBlock.SetActive(false);
            brokenBlock.SetActive(true);
            blockSizeText.gameObject.SetActive(false);
        } else
        {
            GameEvent.instance.gameLost.SetValueAndForceNotify(true);
        }
    }
}
