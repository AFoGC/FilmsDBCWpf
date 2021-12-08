using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO_Films;
using DAL_Films;
namespace BL_Films
{
    public class FilmsBL
    {
        public static int Add_User(int id, string username, string password)
        {
            FilmsBO filmsBO = new FilmsBO();
            filmsBO.id = id;
            filmsBO.username = username;
            filmsBO.password = password;
            int row = (new FilmsDAL()).Add(filmsBO);
            return row;
        }
    }
}
