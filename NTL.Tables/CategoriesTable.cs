using NewTablesLibrary;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TL_Objects;
using TL_Tables.Interfaces;

namespace TL_Tables
{
	public class CategoriesTable : Table<Category>, IHasMarkSystem
	{
        [SaveField("markSystem")]
        private int _markSystem;

        [SaveField("newMarkSystem")]
        private bool _newMarkSystem;

        public CategoriesTable()
        {
            _newMarkSystem = false;
            MarkSystem = 6;
        }

        public bool NewMarkSystem => _newMarkSystem;
        public int MarkSystem
        {
            get => _markSystem;
            set
            {
                _markSystem = value;
                foreach (Category category in this)
                    category.FormatedMark.MarkSystem = _markSystem;
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Category category = (Category)e.NewItems[0];
                category.FormatedMark.MarkSystem = MarkSystem;
            }

            base.OnCollectionChanged(e);
        }

        protected override void OnLoaded()
        {
            MarkSystem = _markSystem;

            if (NewMarkSystem == false)
                ChangeToNewMarkSystem();

            base.OnLoaded();
        }

        private void ChangeToNewMarkSystem()
        {
            foreach (Category category in this)
                category.Mark *= 50;

            _newMarkSystem = true;
        }
    }
}
