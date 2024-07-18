using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class UserDao :Crud
    {

        private UserDao() { }
        private static UserDao instane = null;
        public static UserDao Instane
        {
            get
            {
                if (instane == null)
                {
                    instane = new UserDao();
                }
                return instane;
            }
        }

        public  User getUserByUserAndPass(String user, String pass)
        {
            pass = pass.Replace("'", "''");
            user = user.Replace("'", "''");
            String where = " where contraseña='" + pass + "' and [user]='" + user + "'";
            DataSet dataSetUser = base.select("usuario", where);

            if (dataSetUser != null)
            {

            return new User(
                int.Parse(dataSetUser.Tables[0].Rows[0]["codigo"].ToString()),
                dataSetUser.Tables[0].Rows[0]["contraseña"].ToString(),
                dataSetUser.Tables[0].Rows[0]["mail"].ToString(),
                dataSetUser.Tables[0].Rows[0]["user"].ToString(),
                dataSetUser.Tables[0].Rows[0]["dnipersona"].ToString(),
                dataSetUser.Tables[0].Rows[0]["dvh"].ToString(),
                int.Parse(dataSetUser.Tables[0].Rows[0]["rol"].ToString())


                );;
            }
            else { 
                return null;
            }
                    
        }
        public int save(User user)
        {
            return base.save("usuario", user);
        }

        public List<Object> getAllUsersEnabled()
        {

            return base.selectList("usuario", "where borrado!=1", new User());
        }

        public List<Object> getAllUsersdisabled()
        {

            return base.selectList("usuario", "where borrado=1", new User());
        }


        public User getUserByDni(String dni)
        {
            return (User)base.select("usuario", "where dnipersona='" + dni+"'", new User());
        }

        public int deleteAllUser(User user)
        {
            return base.logicDelete("usuario", "where [user]!='" + user.user+"'");
        }

        public int deleteUserByCodigo(int codigo, User user)
        {
            return base.logicDelete("usuario", "where codigo=" + codigo+ " and  [user]!='" + user.user + "'");
        }


        public int enableByCode(int codigo, User user)
        {
            return base.updateStatus("usuario", "where codigo=" + codigo + " and  [user]!='" + user.user + "'",0);
        }


        public int update(User user)
        {
            return base.update("usuario", user);
        }

        public int updateContraseña(String user, String pass)
        {
            String s = "update usuario set Contraseña='" + pass + "' where User='" + user+"'";
            return dao.ExecuteNonQuery(s);
        }


    }
}
