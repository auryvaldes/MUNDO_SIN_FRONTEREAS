using BL;
using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
   public class BitacoraService 
    {
        BitacoraDAO bitacoraDAO = BitacoraDAO.Instane;
        IntegridadBl integridadBl = IntegridadBl.Instane;


        private BitacoraService() { }
        private static BitacoraService instane = null;
        public static BitacoraService Instane
        {
            get
            {
                if (instane == null)
                {
                    instane = new BitacoraService();
                }
                return instane;
            }
        }


        public List<Bitacora> consultarAll()
        {
            return bitacoraDAO.consultarAll();
               
        }

        public List<Bitacora> consultarbyCodigoNivel(int codigo, DateTime desde, DateTime hasta)
        {
            return bitacoraDAO.consultarByCodigoNivel(codigo, desde,hasta);

        }

        public object editar(object obj)
        {
            throw new NotImplementedException();
        }

        public bool eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public int guardar(object obj)
        {
            int res = bitacoraDAO.save((Bitacora)obj);

            if (res != 0)
            {
                Dvv dvv = integridadBl.getDvvByTabla("bitacora");
                dvv.valor = integridadBl.calcularDigitosVerificadoresVertical("bitacora");
                integridadBl.updateDvv(dvv);

            }

            return res;

        }
    }
}
