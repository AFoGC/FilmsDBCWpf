using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects.Interfaces;

namespace TL_Objects
{
    [TableCell("Genre")]
    public class Genre : Cell, IGenre
    {
        private string _name;
        private bool _isSerialGenre;

        public Genre()
        {
            _name = String.Empty;
            _isSerialGenre = false;
        }

        protected override void updateThisBody(Cell cell)
        {
            Genre genre = (Genre)cell;

            _name = genre._name;
            _isSerialGenre = genre._isSerialGenre;
        }

        protected override void saveBody(StreamWriter streamWriter, Cell defaultCell)
        {
            streamWriter.Write(FormatParam("name", _name, String.Empty, 2));
            streamWriter.Write(FormatParam("isSerialGenre", _isSerialGenre, false, 2));
        }
        protected override void loadBody(Comand comand)
        {

            switch (comand.Paramert)
            {
                case "name":
                    _name = comand.Value;
                    break;
                case "isSerialGenre":
                    _isSerialGenre = Convert.ToBoolean(comand.Value);
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

        public bool IsSerialGenre
        {
            get { return _isSerialGenre; }
            set { _isSerialGenre = value; OnPropertyChanged(nameof(IsSerialGenre)); }
        }

        public override string ToString()
        {
            return this._name;
        }
    }
}
