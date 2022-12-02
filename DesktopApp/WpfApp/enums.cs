namespace WpfApp.ViewModels
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
}

namespace WpfApp.Services
{
    public enum StatusEnum
    {
        Normal,
        Saved,
        UnSaved
    }
}
