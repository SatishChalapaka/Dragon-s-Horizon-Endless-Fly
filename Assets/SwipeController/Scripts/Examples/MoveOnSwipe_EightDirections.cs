using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG.Infrastructure.Utils.Swipe;
using DanielLochner.Assets.SimpleScrollSnap;

public class MoveOnSwipe_EightDirections : MonoBehaviour
{
    [Header("Available movements:")]

    [SerializeField] private bool _up = true;
    [SerializeField] private bool _down = true;
    [SerializeField] private bool _left = true;
    [SerializeField] private bool _right = true;
    [SerializeField] private bool _upLeft = true;
    [SerializeField] private bool _upRight = true;
    [SerializeField] private bool _downLeft = true;
    [SerializeField] private bool _downRight = true;

    [SerializeField] private SimpleScrollSnap _simpleScrollSnap;

    public void OnSwipeHandler(string id)
    {
        switch(id)
        {
            case DirectionId.ID_UP:
                MoveUp();
                break;

            case DirectionId.ID_DOWN:
                MoveDown();
                break;

            case DirectionId.ID_LEFT:
                _simpleScrollSnap.GoToNextPanel(2);

                //MoveLeft();
                break;

            case DirectionId.ID_RIGHT:
                _simpleScrollSnap.GoToPreviousPanel(2);

                //MoveRight();
                break;

            case DirectionId.ID_UP_LEFT:
                MoveUpLeft();
                break;

            case DirectionId.ID_UP_RIGHT:
                MoveUpRight();
                break;

            case DirectionId.ID_DOWN_LEFT:
                MoveDownLeft();
                break;

            case DirectionId.ID_DOWN_RIGHT:
                MoveDownRight();
                break;
        }
    }

    private void MoveDownRight()
    {
        if (_downRight)
        {
        }
    }

    private void MoveDownLeft()
    {
        if (_downLeft)
        {
        }
    }

    private void MoveUpRight()
    {
        if (_upRight)
        {
            
        }
    }

    private void MoveUpLeft()
    {
        if (_upLeft)
        {
            
        }
    }

    private void MoveRight()
    {
        if (_right)
        {
            
        }
    }

    private void MoveLeft()
    {
        if (_left)
        {
            
        }
    }

    private void MoveDown()
    {
        if (_down)
        {
            
        }
    }

    private void MoveUp()
    {
        if (_up)
        {
            
        }
    }
}
