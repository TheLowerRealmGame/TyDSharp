using Tyd;

namespace Example
{

    public class TileData
    {
        public int Id { get; private set; }

        public string Tag { get; private set; }
        public string Name { get; private set; }

        public bool BackTile { get; private set; }
        public bool Transparent { get; private set; }
        public bool Solid { get; private set; }
        public bool Climbable { get; private set; }
        public bool Mineable { get; private set; }

        public TileData() { }

        public void ReadData(TydTable data)
        {
            // Get id
            Id = GetTydString(data, "id").IntValue;
            
            // Get tag
            Tag = GetTydString(data, "tag").Value;
            // Get Name
            Name = GetTydString(data, "name").Value;

            // Get back tile flag
            BackTile = GetTydString(data, "backTile").BoolValue;
            // Get transparent flag
            Transparent = GetTydString(data, "transparent").BoolValue;
            // Get solid flag
            Solid = GetTydString(data, "solid").BoolValue;
            // Get climbable flag
            Climbable = GetTydString(data, "climbable").BoolValue;
            // Get mineable flag
            Mineable = GetTydString(data, "mineable").BoolValue;
        }

        private TydString GetTydString(TydTable tydTable, string name)
        {
            return tydTable[name] as TydString;
        }
    }
}