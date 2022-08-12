using System;
using System.Collections.Generic;

namespace TL_Tables.MarkSystems
{
    public class BaseMarkSystem
    {
        public const int MaxMark = 300;
        public int MaxMarkInSystem { get; private set; }
        public BaseMarkSystem(int maxMark)
        {
            MaxMarkInSystem = maxMark;
        }

        public List<String> GetAllMarksString()
        {
            List<String> strs = new List<String>();

            int i = MaxMarkInSystem;
            while (i > 0)
            {
                strs.Add($"{i}/{MaxMarkInSystem}");
                i--;
            }

            return strs;
        }

        public String GetFormatedMark(int mark)
        {
            int outMark = GetMark(mark);
            if (outMark != 0)
            {
                return $"{outMark}/{MaxMarkInSystem}";
            }
            else
            {
                return String.Empty;
            }
        }

        public int GetMark(int inMark)
        {
            int modifier = MaxMark / MaxMarkInSystem;
            int outMark = 0;

            for (int i = 0; i < MaxMarkInSystem; i++)
            {
                outMark = modifier * i;
                if (outMark <= inMark)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}
