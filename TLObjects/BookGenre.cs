using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects.Interfaces;

namespace TL_Objects
{
    [TableCell("BookGenre")]
    public class BookGenre : Cell, IGenre
    {
        private string name = "";

        public BookGenre() : base() { }
        public BookGenre(int id) : base(id) { }

        protected override void updateThisBody(Cell cell)
        {
            BookGenre bookGenre = (BookGenre)cell;

            name = bookGenre.name;
        }

        protected override void saveBody(StreamWriter streamWriter, Cell defaultCell)
        {
            streamWriter.Write(FormatParam("name", name, "", 2));
        }
        protected override void loadBody(Comand comand)
        {

            switch (comand.Paramert)
            {
                case "name":
                    name = comand.Value;
                    break;

                default:
                    break;
            }
        }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        public override string ToString()
        {
            return name;
        }
    }
}
