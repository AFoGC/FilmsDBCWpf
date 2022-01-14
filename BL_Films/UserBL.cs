﻿using BO_Films;
using DAL_Films;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Films
{
    public class UserBL
    {
        public static int Add_User(string email, string password)
        {
            UserBO userBO = new UserBO();
            userBO.Email = email;
            userBO.Password = password;
            int row = (new UserDAL()).Add(userBO);
            return row;
        }

        public static UserBO LogIn(string email, string password)
        {
            return new UserDAL().LogIn(email, password);
        }
    }
}