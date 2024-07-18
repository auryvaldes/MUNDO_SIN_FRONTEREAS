using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace DAL
{
    public class IntegridadDAO :Crud
    {

        IDictionary<String, ICloneable> tablaObjt = new Dictionary<String, ICloneable>();
        IDictionary<String, List<string>> tablaPk = new Dictionary<String, List<string>>();

        private IntegridadDAO() {
            tablaObjt.Add("usuario", new User());
            tablaObjt.Add("bitacora", new Bitacora());
      

            tablaPk.Add("usuario", new List<string>() {"codigo"});
            tablaPk.Add("bitacora", new List<string>() { "codigo" });
           
        }
        private static IntegridadDAO instane = null;
        public static IntegridadDAO Instane
        {
            get
            {
                if (instane == null)
                {
                    instane = new IntegridadDAO();
                }
                return instane;
            }
        }

        public int selectCountRow(String table)
        {

            string mCommandText = "SELECT COUNT(*) as rows FROM [dbo].[" + table + "]" ;

            DataSet dataset = base.dao.ExecuteDataSet(mCommandText);

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dataset.Tables[0].Rows[0]["rows"].ToString());
            }
            else
            {
                return 0;
            }
        }

        public List<Object> getAllDvv()
        {
            return base.selectList("dvv", "", new Dvv());
        }

        public List<Object> getAllObj(Dvv dvv)
        {
            return base.selectList(dvv.tabla, "", tablaObjt[dvv.tabla]);
        }

        public Dvv getDvvByTabla(String tabla)
        {
            String where = "WHERE [dvv].tabla ='" + tabla+ "'";
            return (Dvv) base.select("dvv", where, new Dvv());
        }

        public int updateDvvById(Dvv dvv)
        {
            return base.update("dvv", dvv,"id");

        }

        public int updateDvh(String tabla, Object obj, String newDVH)
        {

            PropertyInfo[] properties = obj.GetType().GetProperties();
            String where = "";
            List<String> s = tablaPk[tabla];
            foreach (PropertyInfo property in properties)
            {
               if( s.FindAll(e => e == property.Name).Count >0)
                {
                    if (where == "") { 
                    where += property.Name + "='" + property.GetValue(obj)+"'";
                    }
                    else
                    {
                        where +=" and "+ property.Name + "='" + property.GetValue(obj)+"'";

                    }
                }

            }

             String query = "UPDATE [dbo].["+ tabla +"] SET [dvh] ='"+ newDVH + "' WHERE "+ where+"";
            return dao.ExecuteNonQuery(query);
        }

    }
}
