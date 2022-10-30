using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DungeonButton : MonoBehaviour
{
    [SerializeField] private Sprite spriteChange;

    [Serializable]
    public class SlidingWall
    {
        [SerializeField] public GameObject wallGameObject;
        [SerializeField] public Transform finalPosition;

        public void MoveWall(float interpolationTime)
        {
            wallGameObject.transform.DOMove(finalPosition.position, interpolationTime);
        }
    }

    [SerializeField] private List<SlidingWall> slidingWalls;
    [SerializeField] private float interpolationTime;
    private bool isActive = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive)
        {
            foreach (var slidingWall in slidingWalls)
            {
                slidingWall.MoveWall(interpolationTime);
                GetComponent<SpriteRenderer>().sprite = spriteChange;
            }
            isActive = false;
        }
    }
}
