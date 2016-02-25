﻿using System;
using Microsoft.Xna.Framework;

namespace MonoGameTest
{
    public class EasyTimer
    {
        private readonly TimeSpan _duration;
        private TimeSpan _startTime = new TimeSpan(0);

        public EasyTimer(TimeSpan duration)
        {
            _duration = duration;
        }

        public void Start(GameTime startTime)
        {
            _startTime = startTime.TotalGameTime;
        }

        public bool IsFinished(GameTime currentTime)
        {
            return currentTime.TotalGameTime - _startTime >= _duration;
        }
    }
}