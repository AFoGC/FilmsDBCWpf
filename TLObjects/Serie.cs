using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Attributes;

namespace TL_Objects
{
    [TableCell("Serie")]
    public class Serie : Cell
    {
        public int FilmId { get; private set; } = 0;
        
        private Film film;

        protected override void loadBody(Comand comand)
        {
            throw new NotImplementedException();
        }

        protected override void saveBody(StreamWriter streamWriter)
        {
            throw new NotImplementedException();
        }

        protected override void updateThisBody(Cell cell)
        {
            throw new NotImplementedException();
        }
    }
}
