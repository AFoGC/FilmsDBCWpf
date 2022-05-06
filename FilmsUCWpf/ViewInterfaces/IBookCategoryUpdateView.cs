﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsUCWpf.ViewInterfaces
{
    public interface IBookCategoryUpdateView
    {
        string ID { set; }
        string Name { get; set; }
        string HideName { get; set; }
        string Mark { get; set; }
        IList Marks { get; }
    }
}
