﻿namespace WpfApp.ViewModels
{
    public enum BookInfoMenuCondition
    {
        Closed,
        BookInfo,
        BookUpdate,
        CategoryUpdate
    }

    public enum FilmInfoMenuCondition
    {
        Closed,
        FilmInfo,
        FilmUpdate,
        SerieInfo,
        SerieUpdate,
        CategoryUpdate
    }

    public enum FilmsMenuMode
    {
        Categories,
        Films,
        Series,
        Priorities
    }

    public enum BooksMenuMode
    {
        Categories,
        Books,
        Priorities
    }
}

namespace WpfApp.Services
{
    public enum StatusEnum
    {
        Normal,
        Saved,
        UnSaved
    }

    public enum ScaleEnum
    {
        Small = 0,
        Medium = 1
    }
}

namespace WpfApp.Factories
{
    public enum SortDirection
    {
        Ascending = 0,
        Descending = 1,
    }
}
