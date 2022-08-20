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
        private string _name;

        public BookGenre()
        {
            _name = String.Empty;
        }

        protected override void updateThisBody(Cell cell)
        {
            BookGenre bookGenre = (BookGenre)cell;

            _name = bookGenre._name;
        }

        protected override void saveBody(StreamWriter streamWriter, Cell defaultCell)
        {
            streamWriter.Write(FormatParam("name", _name, String.Empty, 2));
        }
        protected override void loadBody(Comand comand)
        {

            switch (comand.Paramert)
            {
                case "name":
                    _name = comand.Value;
                    break;

                default:
                    break;
            }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
