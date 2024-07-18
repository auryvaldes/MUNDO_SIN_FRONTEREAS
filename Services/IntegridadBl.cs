using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Utils;

namespace BL
{
    public class IntegridadBl
    {
        IntegridadDAO integridadDAO = IntegridadDAO.Instane;
        private IntegridadBl() { }
        private static IntegridadBl instane = null;
        public static IntegridadBl Instane
        {
            get
            {
                if (instane == null)
                {
                    instane = new IntegridadBl();
                }
                return instane;
            }
        }
        public String calcularDigitosVerificadoresVertical(String table)
        {
            int count = integridadDAO.selectCountRow(table);

            return Encriptar.encriptarNoReversible(count.ToString());
        }

        public String calcularDigitosVerificadoresHorizontal(Object objeto)
        {
            PropertyInfo[] properties = objeto.GetType().GetProperties();
            string str = "";

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "dvh" && property.Name !="codigo")
                {
                str += property.GetValue(objeto).ToString();
                }
            }
            return Encriptar.encriptarNoReversible(str);
            
        }

        public List<Object> getAllDvv()
        {
            return integridadDAO.getAllDvv();
        }

        public Dvv getDvvByTabla(String tabla)
        {
            return integridadDAO.getDvvByTabla(tabla);

        }
        public int updateDvv(Dvv dvv)
        {
            return integridadDAO.updateDvvById(dvv);
        }


        public bool verificarIntegridad()
        {
            bool integridad = true;
            List<Object> ldvv = getAllDvv();
            foreach (Dvv dvv in ldvv)
            {
                String digitoV = calcularDigitosVerificadoresVertical(dvv.tabla);

                if (!digitoV.Equals(dvv.valor))
                {
                    integridad = false;
                    return integridad;
                }

                List<Object> l = integridadDAO.getAllObj(dvv);
                if (l == null)
                {
                    l = new List<object>();
                }

                foreach (object obj in l)
                {
                    String digitoh = calcularDigitosVerificadoresHorizontal(obj);
                    PropertyInfo[] properties = obj.GetType().GetProperties();
                    String dvhObj = "";
                    foreach (PropertyInfo property in properties)
                    { 
                    
                        if(property.Name == "dvh")
                        {
                            dvhObj = property.GetValue(obj).ToString();
                        }
                    }
                        if (!digitoh.Equals(dvhObj))
                    {
                        integridad = false;
                        return integridad;
                    }
                }
            }

            return integridad; 
        }


        public bool RecalcularIntegridad()
        {
            bool integridad = true;
            List<Object> ldvv = getAllDvv();
            foreach (Dvv dvv in ldvv)
            {
                String digitoV = calcularDigitosVerificadoresVertical(dvv.tabla);

                if (!digitoV.Equals(dvv.valor))
                {
                    dvv.valor = digitoV;
                    integridadDAO.updateDvvById(dvv);
                }

                List<Object> l = integridadDAO.getAllObj(dvv);

                if(l== null)
                {
                    l = new List<object>();
                }

                foreach (object obj in l)
                {
                    String digitoh = calcularDigitosVerificadoresHorizontal(obj);
                    PropertyInfo[] objProperties = obj.GetType().GetProperties();
                    String dvhObj = "";
                    String pk = "";
                    foreach (PropertyInfo property in objProperties)
                    {

                        if (property.Name == "dvh")
                        {
                            dvhObj = property.GetValue(obj).ToString();

                            if (!digitoh.Equals(dvhObj))
                            {
                                integridadDAO.updateDvh(dvv.tabla, obj, digitoh);
                            }
                        }
                    }
                }
            }

            return integridad;
        }

    }
}
