using System;
using System.Collections.Generic;
using System.Linq;

namespace bramkominatorMobile.Models
{
    public class Cable : List<Position>
    {
        private Position _start;
        private Position _target;
        private List<Position> _route;
        private string[] images;

        public Cable(List<Position> route, Position start, Position target)
        {
            if (start != null && target != null)
            {
                _start = start;
                _target = target;
            }

            if (route.Count > 0)
            {
                _route = route;
                images = new string[_route.Count-1];
            }

            SetImages();
        }

        public string GetImage(int i)
        {
            return images[i];
        }

        private void SetImages()
        {
            if (_route[1].Row == _route[0].Row)
            {
                if (_route[1].Column == _route[0].Column)
                    images[0] = "cableVertical.svg";
                else
                    images[0] = "cableHorizontal.svg";
            }
            else
            {
                if (_route[1].Row < _route[0].Row)
                {
                    images[0] = "cableFromTopLeft.svg";
                }
                else if (_route[1].Row > _route[0].Row)
                {
                    images[0] = "cableFromDownLeft.svg";
                }
            }

            for (int i = 1; i <= _route.Count - 2; i++)
            {
                if (_route[i + 1].Row == _route[i].Row)
                {
                    if (_route[i - 1].Row < _route[i].Row)
                        images[i] = "cableFromTopRight.svg";
                    else if (_route[i - 1].Row > _route[i].Row)
                    {
                        if (_route[i - 1].Row < _route[i].Row)
                            images[i] = "cableVertical.svg";
                        else
                            images[i] = "cableFromTopLeft.svg";
                    }
                    else
                        images[i] = "cableHorizontal.svg";
                }
                else
                {
                    if (_route[i + 1].Column > _route[i].Column)
                    {
                        if (_route[i - 1].Row < _route[i].Row)
                            images[i] = "cableFromTopRight.svg";
                    }
                    else if (_route[i + 1].Column == _route[i].Column)
                    {
                        if (_route[i - 1].Column < _route[i].Column)
                            images[i] = "cableFromDownLeft.svg";
                        else if (_route[i - 1].Column > _route[i].Column)
                            images[i] = "cableFromDownRight.svg";
                        else if (_route[i-1].Column == _route[i].Column)
                            images[i] = "cableVertical.svg";
                    }
                }
            }

            if (_start.Row < _route[0].Row)
                images[0] = "cableFromTopRight.svg";
            else if (_start.Row > _route[0].Row)
                images[0] = "cableFromTopLeft.svg";
            else
                images[0] = "cableHorizontal.svg";

            var last = _route.Count - 2;
            if (_target.Row < _route[last].Row)
                images[last] = "cableFromDownRight.svg";
            else if (_target.Row > _route[last].Row)
                images[last] = "cableFromTopRight.svg";
            else
                images[last] = "cableHorizontal.svg";
        }
    }
}
