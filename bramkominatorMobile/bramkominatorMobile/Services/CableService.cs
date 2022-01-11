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

            return Solve(sr, sc, visited);
        }

        private List<Position> Solve(int sr, int sc, bool[,] visited)
        {
            rq = new Queue<int>();
            cq = new Queue<int>();

            isFound = false;
            moveCount = 0;
            Position position = new Position();
            Position[] prev = new Position[R * C];

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

                position.Set(r, c);
                if (position == _target.GetPosition())
                {
                    isFound = true;
                    break;
                }

                ExploreNeighbours(r, c, visited, prev, visitedElements);

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
                return ReconstructPath(new Position(sc, sr), prev, visitedElements);

            return new List<Position>();
        }

        private void ExploreNeighbours(int r, int c, bool[,] visited, Position[] prev, int visitedElements)
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
                prev[visitedElements] = new Position(c, r);
                nodesInNextLayer++;
            }
        }

        private List<Position> ReconstructPath(Position start, Position[] prev, int visitedElements)
        {
            List<Position> path = new List<Position>();

            for (var el = visitedElements-1; el > 0; el--)
            {
                if (prev[el] is null)
                    continue;

                path.Add(prev[el]);
            }

            path.Reverse();

            if (path.First() == start)
                return path;
            else
                return new List<Position>();
        }
    }
}
