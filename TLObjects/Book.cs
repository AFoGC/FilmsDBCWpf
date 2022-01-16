using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Attributes;
using TL_Objects.CellDataClasses;

namespace TL_Objects
{
	[TableCell("Book")]
	public class Book : Cell
	{
		private String name = "";
		private String author = "";
		private BookGenre bookGenre;
		private int bookGenreId = 0;
		private int publicationYear = 0;
		private bool readed = false;
		private DateTime fullReadDate = new DateTime();
		private sbyte mark = -1;
		private List<Source> sources = new List<Source>();

		private int countOfReadings = 0;
		private String bookmark = "";

		private int franshiseId = 0;
		private sbyte franshiseListIndex = -1;

		public Book() : base() { }
		public Book(int id) : base(id) { }

		protected override void loadBody(Comand comand)
		{
			switch (comand.Paramert)
			{
				case "name":
					this.name = comand.Value;
					break;
				case "author":
					this.author = comand.Value;
					break;
				case "bookGenreId":
					this.bookGenreId = Convert.ToInt32(comand.Value);
					break;
				case "publicationYear":
					this.publicationYear = Convert.ToInt32(comand.Value);
					break;
				case "readed":
					this.readed = Convert.ToBoolean(comand.Value);
					break;
				case "fullReadDate":
					this.fullReadDate = Convert.ToDateTime(comand.Value);
					break;
				case "mark":
					this.mark = Convert.ToSByte(comand.Value);
					break;
				case "sourceUrl":
					this.sources.Add(Source.ToSource(comand.Value));
					break;
				case "bookmark":
					this.bookmark = comand.Value;
					break;
				case "franshiseId":
					this.franshiseId = Convert.ToInt32(comand.Value);
					break;
				case "franshiseListIndex":
					this.franshiseListIndex = Convert.ToSByte(comand.Value);
					break;
			}
		}

		private Source defSource = new Source();
		protected override void saveBody(StreamWriter streamWriter)
		{
			streamWriter.Write(FormatParam("name", name, "", 2));
			streamWriter.Write(FormatParam("author", author, "", 2));
			streamWriter.Write(FormatParam("bookGenreId", bookGenreId, 0, 2));
			streamWriter.Write(FormatParam("publicationYear", publicationYear, 0, 2));
			streamWriter.Write(FormatParam("readed", readed, false, 2));
			streamWriter.Write(FormatParam("fullReadDate", fullReadDate, new DateTime(), 2));
			streamWriter.Write(FormatParam("mark", mark, -1, 2));
			streamWriter.Write(FormatParam("bookmark", bookmark, "", 2));
			streamWriter.Write(FormatParam("franshiseId", franshiseId, 0, 2));
			streamWriter.Write(FormatParam("franshiseListIndex", franshiseListIndex, -1, 2));

			foreach (Source source in sources)
			{
				streamWriter.Write(FormatParam("sourceUrl", source, defSource, 2));
			}
		}

		protected override void updateThisBody(Cell cell)
		{
			Book book = (Book)cell;

			this.name = book.name;
			this.author = book.author;
			this.BookGenre = book.BookGenre;
			this.publicationYear = book.publicationYear;
			this.readed = book.readed;
			this.fullReadDate = book.fullReadDate;
			this.mark = book.mark;
			this.sources = book.sources;
			this.bookmark = book.bookmark;
		}


		public String Name
		{
			get { return name; }
			set { name = value; }
		}
		public String Author
		{
			get { return author; }
			set { author = value; }
		}
		public BookGenre BookGenre
		{
			get
			{
				if (bookGenre != null)
				{
					return bookGenre;
				}
				else
				{
					return new BookGenre(0);
				}
			}
			set
			{
				bookGenre = value;
				bookGenreId = bookGenre.ID;
			}
		}
		public int BookGenreId
		{
			get { return bookGenreId; }
		}
		public int PublicationYear
		{
			get { return publicationYear; }
			set { publicationYear = value; }
		}
		public bool Readed
		{
			get { return readed; }
			set { readed = value; }
		}
		public DateTime FullReadDate
		{
			get { return fullReadDate; }
			set { fullReadDate = value; }
		}
		public sbyte Mark
		{
			get { return mark; }
			set { mark = value; }
		}
		public List<Source> Sources
		{
			get { return sources; }
			set { sources = value; }
		}

		public int CountOfReadings
		{
			get { return countOfReadings; }
			set { countOfReadings = value; }
		}

		public int FranshiseId
		{
			get { return franshiseId; }
			set { franshiseId = value; }
		}

		public sbyte FranshiseListIndex
		{
			get { return franshiseListIndex; }
			set { franshiseListIndex = value; }
		}

		public String Bookmark
		{
			get { return bookmark; }
			set { bookmark = value; }
		}
	}
}
