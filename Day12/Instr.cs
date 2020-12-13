namespace Day12
{
    internal class Instr
    {
        public ShipAction Action { get; init; }

        public int Distance { get; init; }

        public Instr(ShipAction action, int distance)
        {
            Action = action;
            Distance = distance;
        }
    }
}
