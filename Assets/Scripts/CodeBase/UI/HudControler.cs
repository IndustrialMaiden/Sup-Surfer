using System;
using System.Collections.Generic;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Factory;
using CodeBase.Utils.Disposables;
using GamePush;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class HudControler : MonoBehaviour
    {
        [SerializeField] private TMP_Text _pointsText;

        private Player _player;

        private CompositeDisposables _trash = new CompositeDisposables();

        private void Awake()
        {
            _player = Game.Player;
            _trash.Retain(_player.Score.Subscribe(OnScoreChanged));
        }

        private void OnScoreChanged(int newvalue, int oldvalue)
        {
            _pointsText.text = _player.Score.Value.ToString();
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}