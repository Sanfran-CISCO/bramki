using System;
using System.Collections.Generic;
using System.Linq;
using bramkominatorMobile.Models;

namespace bramkominatorMobile.Services
{
    public class CableService
    {
        private CircutElement[,] _matrix;
        private CircutElement _target;

        private bool isFound;
        int moveCount;

        private readonly int R;
        private readonly int C;

        private readonly int[] dr = { -1, 1, 0, 0 };
        private readonly int[] dc = { 0, 0, 1, -1 };

        private Queue<int> rq;
        private Queue<int> cq;

        private int nodesLeftInLayer;
        private int nodesInNextLayer;

        private CableService()
        {
        }

        public CableService(CircutElement[,] matrix, int r, int c)
        {
            if (matrix != null)
                _matrix = matrix;

            if (r > 0 && c > 0)
            {
                R = r;
                C = c;
            }
        }

        public List<Position> FindPath(CircutElement start, CircutElement target)
        {
            Position pos = start.GetPosition();
            int sr = pos.Row;
            int sc = pos.Column;
            bool[,] visited = new bool[R, C];
            _target = target;

            return Solve(sr, sc, target.GetPosition(), visited);
        }

        private List<Position> Solve(int sr, int sc, Position target, bool[,] visited)
        {
            rq = new Queue<int>();
            cq = new Queue<int>();

            isFound = false;
            moveCount = 0;

            Position position = new Position();
            Position[] prev = new Position[R * C];

            int[,] prevX = new int[R,C];
            int[,] prevY = new int[R,C];

            int visitedElements = 0;

            nodesInNextLayer = 0;
            nodesLeftInLayer = 1;

            rq.Enqueue(sr);
            cq.Enqueue(sc);

            visited[sr,sc] = true;
            visitedElements++;

            while (rq.Count > 0)
            {
                int r = rq.Dequeue();
                int c = cq.Dequeue();

                position.Set(c, r);
                if (position == _target.GetPosition())
                {
                    isFound = true;
                    break;
                }

                ExploreNeighbours(r, c, visited, visitedElements, prevX, prevY);

                nodesLeftInLayer--;
                visitedElements++;

                if (nodesLeftInLayer == 0)
                {
                    nodesLeftInLayer = nodesInNextLayer;
                    nodesInNextLayer = 0;
                    moveCount++;
                }
            }

            if (isFound)
                return ReconstructPath(new Position(sc, sr), target, prevX, prevY);

            return new List<Position>();
        }

        private void ExploreNeighbours(int r, int c, bool[,] visited, int visitedElements, int[,] prevX, int[,] prevY)
        {
            for (int i=0; i<4; i++)
            {
                int rr = r + dr[i];
                int cc = c + dc[i];

                if (rr < 0 || cc < 0)
                    continue;

                if (rr >= R || cc >= C)
                    continue;

                if (visited[rr,cc])
                    continue;

                if (_matrix[rr,cc].GetType() != typeof(EmptyElement))
                {
                    if (_matrix[rr, cc].GetType() != _target.GetType())
                        continue;
                }

                rq.Enqueue(rr);
                cq.Enqueue(cc);
                visited[rr,cc] = true;
                prevX[rr, cc] = c;
                prevY[rr, cc] = r;
                nodesInNextLayer++;
            }
        }

        private List<Position> ReconstructPath(Position start, Position end, int[,] prevX, int[,] prevY)
        {
            List<Position> path = new List<Position>();

            int curX = end.Column;
            int curY = end.Row;

            int tempX;
            int tempY;

            while (curX != start.Column || curY != start.Row)
            {
                path.Add(new Position(curX, curY));

                tempX = prevX[curY, curX];
                tempY = prevY[curY, curX];

                curX = tempX;
                curY = tempY;
            }

            path.Reverse();

            return path;
        }
    }
}
