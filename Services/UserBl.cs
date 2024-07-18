using DAL;
using Entity;
using Services;
using System;
using System.Collections.Generic;
using Utils;

namespace BL
{
    public class UserBl
    {

        private UserBl() { }
        private static UserBl instane = null;
        public static UserBl Instane
        {
            get
            {
                if (instane == null)
                {
                    instane = new UserBl();
                }
                return instane;
            }
        }
        UserDao userDao = UserDao.Instane;
        BitacoraService bitacoraBl = BitacoraService.Instane;
        IntegridadBl integridadBl = IntegridadBl.Instane;

        public User login(String user, String pass)
        {

            Bitacora b = new Bitacora(
                DateTime.Now,
                "login",
                user,
               1);
           b.dvh = integridadBl.calcularDigitosVerificadoresHorizontal(b);
           int i = bitacoraBl.guardar(b);
           Console.WriteLine(i);

            return userDao.getUserByUserAndPass(user, pass);
        }

        public int updatecontraseña(String user, String pass)
        {

            return userDao.updateContraseña(user, Encriptar.encriptarNoReversible(pass));
        }

    }




}
