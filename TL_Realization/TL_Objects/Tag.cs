using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter.TableCell;

namespace TL_Objects
{
    [TableCell("Tag")]
    public class Tag : Cell
    {
        private string _name;

        public Tag()
        {
            _name = String.Empty;
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
                    _name = comand.Paramert;
                    break;
            }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
    }
}
