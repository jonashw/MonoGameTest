﻿namespace MonoGameTest
{
    public class CircularArray<T>
    {
        private readonly T[] _items;
        private int _index;

        public CircularArray(params T[] items)
        {
            _items = items;
        }

        public T GetCurrent()
        {
            return _items[_index];
        }

        public void Next()
        {
            _index++;
            if (_index >= _items.Length)
            {
                _index = 0;
            }
        }

        public void Prev()
        {
            _index--;
            if (_index < 0)
            {
                _index = _items.Length - 1;
            }
        }
    }
}