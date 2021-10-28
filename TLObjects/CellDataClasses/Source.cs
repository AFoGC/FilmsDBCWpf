using System;
using System.Collections.Generic;
using System.Text;

namespace TL_Objects.CellDataClasses
{
    public class Source
    {
        public String Name { get; set; }
        public String SourceUrl { get; set; }

        public Source()
        {

        }

        public static Source ToSource(String import)
        {
            int komaIndex = import.IndexOf(',');
            if (komaIndex == -1)
            {
                return new Source
                {
                    SourceUrl = import
                };
            }
            else
            {
                return new Source
                {
                    Name = import.Substring(0, komaIndex),
                    SourceUrl = import.Substring(komaIndex + 2)
                };
            }
        }

        public override string ToString()
        {
            if (Name == "")
            {
                return SourceUrl;
            }
            else
            {
                return Name + ", " + SourceUrl;
            }
        }
    }
}
