using System;
using bramkominatorMobile.Exceptions;

namespace bramkominatorMobile.Models
{
    public class Position
    {
        public int Column { get; private set; }
        public int Row { get; private set; }

        public Position()
        {
            Set(0, 0);
        }

        public Position(int column, int row)
        {
            Set(column, row);
        }

        public void Set(int column, int row)
        {
            if (column < 0 || row < 0)
                throw new InvalidPositionException("Bad position cordinates given");
            else
                {
                    Column = column;
                    Row = row;
                }
        }

        public static bool operator ==(Position a, Position b)
        {
            if (a is null || b is null)
                return false;

            return a.Column == b.Column && a.Row == b.Row;
        }

        public static bool operator !=(Position a, Position b)
        {
            return !(a == b);
        }

        public override bool Equals(Object obj)
        {
            return Equals(obj as Position);
        }

        public bool Equals(Position pos)
        {
            return pos != null && pos.Column == Column && pos.Row == Row;
        }

        public override int GetHashCode()
        {
            return Column.GetHashCode() ^ Row.GetHashCode();
        }
    }
}
