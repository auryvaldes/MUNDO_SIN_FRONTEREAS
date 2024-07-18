using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Utils;

namespace DAL
{
    public class BitacoraDAO : Crud
    {

        private BitacoraDAO() { }
        private static BitacoraDAO instane = null;
        public static BitacoraDAO Instane
        {
            get
            {
                if (instane == null)
                {
                    instane = new BitacoraDAO();
                }
                return instane;
            }
        }
        public int save(Bitacora bitacora)
        {
            return base.save("bitacora",bitacora);
        }

        public List<Bitacora> consultarAll()
        {
            List<Bitacora> ll = new List<Bitacora>();
            List<Object> l = base.selectList("bitacora", "", new Bitacora());

            foreach (Bitacora b in l)
            {
                ll.Add(b);
            }

            return ll;
        }

        public List<Bitacora> consultarByCodigoNivel(int codigonivel, DateTime desde, DateTime hasta)
        {
            List<Bitacora> ll = new List<Bitacora>();
            String where = "where codigonivel=" + codigonivel
                + "and  fecha BETWEEN {ts '"+desde.ToString(Constants.formatDBDate) + "'} AND {ts '"+hasta.ToString(Constants.formatDBDate) + "'}";
           List < Object > l = base.selectList("bitacora",where, new Bitacora());

            foreach(Bitacora b in l)
            {
                ll.Add(b);
            }

            return ll;

        }

    }
}
