using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apteka
{
    public static class Admin
    {
        #region Lek
        public static void AddLek(Lek lek)
        {
            using (AptekaDataDataContext con = new AptekaDataDataContext())
            {
                con.Leks.InsertOnSubmit(lek);
                con.SubmitChanges();
            }
        }

        public static void UpdateLek(Lek lek)
        {
            using (AptekaDataDataContext con = new AptekaDataDataContext())
            {
                Lek lk = (from l in con.Leks where l.LekID == lek.LekID select l).FirstOrDefault();
                lk.Nazwa = lek.Nazwa;
                lk.M_nazwa = lek.M_nazwa;
                lk.KategorieID = lek.KategorieID;
                lk.PostacID = lek.PostacID;
                lk.Cena = lek.Cena;
                lk.Cena_hutowa = lek.Cena_hutowa;
                lk.Refundacja = lek.Refundacja;
                lk.Data_waznosci = lek.Data_waznosci;
                lk.Dawka = lek.Dawka;
                lk.Opakowanie = lek.Opakowanie;
                lk.Promocja = lek.Promocja;
                lk.Ilosc = lek.Ilosc;
                con.SubmitChanges();
            }
        }
        public static void DeleteLek(Lek lek)
        {
            using (AptekaDataDataContext con = new AptekaDataDataContext())
            {
                Lek lk = (from l in con.Leks where l.LekID == lek.LekID select l).FirstOrDefault();
                con.Leks.DeleteOnSubmit(lk);
                con.SubmitChanges();
            }
        }
        #endregion
        #region User
        public static void AddUser(User user)
        {
            using (AptekaDataDataContext con = new AptekaDataDataContext())
            {
                con.Users.InsertOnSubmit(user);
                con.SubmitChanges();
            }
        }
        public static void EditUser(User user)
        {
            using (AptekaDataDataContext con = new AptekaDataDataContext())
            {
                User us = (from u in con.Users where u.UsersId == user.UsersId select u).FirstOrDefault();
                us.Nazwa_uzytkownika = user.Nazwa_uzytkownika;
                us.Haslo = user.Haslo;
                us.Prawo = user.Prawo;
                con.SubmitChanges();
            }
        }
         public static void DeleteUser(User user)
        {
            using (AptekaDataDataContext con = new AptekaDataDataContext())
            {
                User us = (from u in con.Users where u.UsersId == user.UsersId select u).FirstOrDefault();
                con.Users.DeleteOnSubmit(us);
                con.SubmitChanges();
            }
        }
        #endregion
    }
}
