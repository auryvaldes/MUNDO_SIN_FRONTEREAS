using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace DAL
{
    public abstract class Crud
    {
        public DAO dao = DAO.Instane;
        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

        public DataSet select(String table, String where)
        {

            string mCommandText = "select * from [dbo].[" + table + "]" + where;

            DataSet dataset = dao.ExecuteDataSet(mCommandText);

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                return dataset;
            }
            else
            {
                return null;
            }
        }

        public Object select(String table, String where, Object ob)
        {

            string mCommandText = "select * from [dbo].[" + table + "]" + where;

            DataSet dataset = dao.ExecuteDataSet(mCommandText);

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                PropertyInfo[] properties = ob.GetType().GetProperties();


                foreach (PropertyInfo property in properties)
                {
                    try
                    {
                        if (property.PropertyType == typeof(String))
                        {
                            property.SetValue(ob, dataset.Tables[0].Rows[0][property.Name].ToString());

                        }
                        if (property.PropertyType == typeof(int))
                        {
                            property.SetValue(ob, int.Parse(dataset.Tables[0].Rows[0][property.Name].ToString()));
                        }

                        if (property.PropertyType == typeof(float))
                        {
                            property.SetValue(ob, float.Parse(dataset.Tables[0].Rows[0][property.Name].ToString()));
                        }

                        if (property.PropertyType == typeof(DateTime))
                        {
                            property.SetValue(ob, DateTime.Parse(dataset.Tables[0].Rows[0][property.Name].ToString()));
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }

                return ob;
            }
            else
            {
                return null;
            }
        }

        public DataSet select(String table)
        {

            string mCommandText = "select * from [dbo].[" + table + "]";

            DataSet dataset = dao.ExecuteDataSet(mCommandText);

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                return dataset;
            }
            else
            {
                return null;
            }
        }

        public List<Object> selectList(String table, String where, ICloneable ob)
        {
            string mCommandText = "select * from [dbo].[" + table + "]"+ where;
            

            DataSet dataset = dao.ExecuteDataSet(mCommandText);
            List<Object> list = new List<Object>();

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                PropertyInfo[] properties = ob.GetType().GetProperties();

                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    foreach (PropertyInfo property in properties)
                    {
                        try { 
                        if (property.PropertyType == typeof(String))
                        {
                            property.SetValue(ob, row[property.Name].ToString());

                        }
                        if (property.PropertyType == typeof(int))
                        {
                            property.SetValue(ob, int.Parse(row[property.Name].ToString()));
                        }

                        if (property.PropertyType == typeof(float))
                        {
                            property.SetValue(ob, float.Parse(row[property.Name].ToString()));
                        }

                        if (property.PropertyType == typeof(DateTime))
                        {
                            property.SetValue(ob, DateTime.Parse(row[property.Name].ToString()));
                        }
                        }catch(Exception e)
                        {

                        }
                    }
                    list.Add(ob.Clone());
                }               
                return list;
            }
            else
            {
                return new List<Object>(); 
            }
        }

        public int save(String table, Object obj, bool lastId=false)
        {
            if (!lastId)
            {
                string query = makeQuerySave(table, obj);
                return dao.ExecuteNonQuery(query);
            }
            else
            {
                string query = makeQuerySave(table, obj);
                DataSet dataset = dao.ExecuteDataSet(query + ";SELECT SCOPE_IDENTITY() as 'id' ;");
                int id = int.Parse(dataset.Tables[0].Rows[0][0].ToString());
                return id;
            }
        }

        public int getLastCodigo(String tabla)
        {
            String query = "SELECT codigo FROM " + tabla + " WHERE codigo = (SELECT max(codigo) FROM " + tabla + ")";

            DataSet d = dao.ExecuteDataSet(query);
            int lastCodigo =int.Parse(d.Tables[0].Rows[0][0].ToString());
            return lastCodigo;


        }

        public int delete(String table, String where)
        {
            string query = "DELETE FROM [dbo].["+ table + "] "+ where;
            return dao.ExecuteNonQuery(query);
        }

        public int logicDelete(String table, String where)
        {
            return updateStatus(table, where, 1);
        }

        public int updateStatus(String table, String where,int status)
        {
            string query = "UPDATE [dbo].[" + table + "]   SET[borrado] ="+ status + where;
            return dao.ExecuteNonQuery(query);
        }

        public int update(String table, Object obj, String pk = "")
        {
            string query = makeQueryUpdate(table, obj, pk);
            return dao.ExecuteNonQuery(query);
        }


        public String makeQuerySave(String table, Object obj)
        {
            String query = "";
            PropertyInfo[] properties = obj.GetType().GetProperties();
            string campos = "";
            string valores = "";

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "codigo")
                {
                    if (campos == "")
                    {
                        campos += "[" + property.Name + "]";
                    }
                    else
                    {
                        campos += ", [" + property.Name + "]";

                    }

                }
            }

            
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "codigo")
                {

                    if (valores == "")
                    {
                        String s = property.GetValue(obj).ToString();

                         s =   s.Replace("'", "''");

                        valores += "'" + s + "'";
                    }
                    else
                    {
                        String s = property.GetValue(obj).ToString();

                        s = s.Replace("'", "''");
                        valores += ", '" +s + "'";

                    }
                }

            }

            return query = "INSERT INTO  [dbo].[" + table + "] (" + campos + ")" + "VALUES(" + valores + ")";

        }


        public String makeQueryUpdate(String table, Object obj, String pk="")
        {
            String query = "";
            PropertyInfo[] properties = obj.GetType().GetProperties();
            string data = "";
            string codigo="";


            foreach (PropertyInfo property in properties)
            {
                if (property.Name !="codigo" && property.Name !=pk)
                {
                    if (data == "")
                    {
                        if (property.PropertyType != typeof(float))
                        {
                            data += "[" + property.Name + "]='" + property.GetValue(obj).ToString() + "'";
                        }
                        else
                        {
                            data += "[" + property.Name + "]=" + float.Parse(property.GetValue(obj).ToString(), nfi) + "";

                        }
                    }
                    else
                    {
                        if (property.PropertyType != typeof(float))
                        {
                            data += ",[" + property.Name + "]='" + property.GetValue(obj).ToString() + "'";
                        }
                        else
                        {

                            float a = 51.1F;

                            data += ",[" + property.Name + "]=" + float.Parse(property.GetValue(obj).ToString(),nfi) + "";

                        }
                    }

                }
                else
                {
                    codigo =property.GetValue(obj).ToString();
                }
            }

            if (pk != "")
            {

                foreach (PropertyInfo property in properties)
                {
                    if (property.Name == pk)
                    {
                        codigo = property.GetValue(obj).ToString();

                    }
                }

                return query = "UPDATE "+ table + " SET "+ data + " WHERE "+pk+"='" + codigo+"'";
            }
            else
            {
              return query = "UPDATE " + table + " SET " + data + " WHERE codigo='" + codigo+"'";

            }
        }

    }
}
