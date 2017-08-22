namespace _4Gewinnt
{
    public class Position
    {
        public Position(int row, int column)
        {
            Column = column;
            Row = row;
        }

        public int Column { get; private set; }

        public int Row { get; private set; }
    }
}
