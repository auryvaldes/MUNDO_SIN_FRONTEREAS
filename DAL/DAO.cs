using System;
using System.Data;
using System.Data.SqlClient;
using Utils;

namespace DAL
{
    public class DAO
    {

        private DAO() { }
        private static DAO instane = null;
        public static DAO Instane
        {
            get
            {
                if (instane == null)
                {
                    instane = new DAO();
                }
                return instane;
            }
        }

        SqlConnection mCon = null;


        public int ExecuteNonQuery(string pCommandText)
        {
            if (mCon == null)
            {
                crearConexion();
            }

            try
            {
                SqlCommand mCom = new SqlCommand(pCommandText, mCon);
                mCon.Open();
                return mCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (mCon.State != ConnectionState.Closed)
                    mCon.Close();
            }
        }

        public DataSet ExecuteDataSet(string pCommandText)
        {
            if (mCon==null)
            {
                crearConexion();
            }

            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(pCommandText, mCon);

                DataSet data = new DataSet();

                sqlDataAdapter.Fill(data);

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (mCon.State != ConnectionState.Closed)
                    mCon.Close();
            }

        }

        public bool comprobarConexion()
        {
            Txt.readTxt();
            String cnn = Txt.readTxt("./cnn.txt");
            if (cnn != null) {
                try
                {
                    mCon = new SqlConnection(Encriptar.desencriptar(cnn));
                    mCon.Open();
                    if (mCon.State > 0)
                    {
                        mCon.Close();

                        return true;
                    }
                    else return false;

                }
                catch(Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
     }


        public string crearConexion()
        {
            String cnn = "Data Source=.;Initial Catalog=mundoSinFronteras;Integrated Security=True";
            try
            {
                mCon = new SqlConnection(cnn);
                mCon.Open();
                if (!(mCon.State > 0))
                {
                    throw new Exception("Base de datos no encontrada");
                }
                mCon.Close();

                return cnn;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

       
    }
}
