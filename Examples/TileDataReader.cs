using System.Collections.Generic;
using Tyd;

namespace Example
{
    public class TileDataReader
    {
        public static TydFile LoadTilesConfiguration(string filePath)
        {
            // Load the file
            TydFile file = TydFile.FromFile(filePath);

            // Resolve inheritance
            Inheritance.Initialize();
            try
            {
                TydDocument doc = file.DocumentNode;

                Inheritance.RegisterAllFrom(doc);

                Inheritance.ResolveAll();
            }
            finally
            {
                Inheritance.Complete();
            }

            return file;
        }

        public static List<TileData> LoadTileData(string filePath)
        {
            List<TileData> tilesData = new List<TileData>();

            TydFile file = LoadTilesConfiguration(filePath);

            TileData data;
            TydTable tydTable;

            foreach(TydNode node in file.DocumentNode.Nodes)
            {
                tydTable = node as TydTable;

                // Only read the data if the node is a table and is not abstract
                if(tydTable != null
                    && !tydTable.AttributeAbstract)
                {
                    data = new TileData();

                    data.ReadData(tydTable);

                    tilesData.Add(data);
                }
            }

            return tilesData;
        }
    }
}