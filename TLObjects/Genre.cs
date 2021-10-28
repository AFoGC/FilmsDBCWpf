using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Attributes;

namespace TL_Objects
{
    [TableCell("Genre")]
    public class Genre : Cell
    {
        public String Name { get; set; }        = "";
        public bool IsSerialGenre { get; set; } = false;

        public Genre() : base() { }
        public Genre(int id) : base(id) { }

        protected override void loadBody(Comand comand)
        {
            switch (comand.Paramert)
            {
                case "name":
                    Name = comand.Value;
                    break;
                case "isSerialGenre":
                    IsSerialGenre = Convert.ToBoolean(comand.Value);
                    break;

                default:
                    break;
            }
        }

        protected override void saveBody(StreamWriter streamWriter)
        {
            streamWriter.Write(FormatParam("name", Name, "", 2));
            streamWriter.Write(FormatParam("isSerialGenre", IsSerialGenre, false, 2));
        }

        protected override void updateThisBody(Cell cell)
        {
            Genre genre = (Genre)cell;

            Name = genre.Name;
            IsSerialGenre = genre.IsSerialGenre;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
