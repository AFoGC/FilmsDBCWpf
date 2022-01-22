using BO_Films;
using DAL_Films;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Films
{
    public static class ProfileBL
    {
        public static void SendProfiles(ProfileBO[] profiles, UserBO user)
        {
            ProfileBO[] DBProfiles = GetAllUserProfiles(user);

            bool hasProectionOnComputer = false;
            foreach (ProfileBO DBProfile in DBProfiles)
            {
                hasProectionOnComputer = false;
                foreach (ProfileBO profile in profiles)
                {
                    if (DBProfile.Name == profile.Name)
                    {
                        hasProectionOnComputer = true;
                        profile.Id = DBProfile.Id;
                        profile.UserId = DBProfile.UserId;
                        new ProfileDAL().Update(profile);
                    }
                }
                if (!hasProectionOnComputer)
                {
                    new ProfileDAL().DeleteByID(DBProfile.Id);
                }
            }

            foreach (ProfileBO profile in profiles)
            {
                if (profile.Id == 0)
                {
                    profile.UserId = user.Id;
                    new ProfileDAL().Add(profile);
                }
            }
        }

        public static ProfileBO[] GetAllUserProfiles(UserBO user)
        {
            return new ProfileDAL().GetAllUserProfiles(user.Id);
        }
    }
}
