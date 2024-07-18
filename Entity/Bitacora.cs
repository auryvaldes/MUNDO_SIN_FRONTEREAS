using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Entity
{
    public class Bitacora:ICloneable,IComparer<Bitacora>
    {
        public Bitacora(DateTime fecha, string descripcion, string usuario, int codigoNivel)
        {
            this.fecha = fecha;
            this.descripcion = descripcion;
            this.usuario = usuario;
            this.codigoNivel = codigoNivel;
        }

        public Bitacora()
        {
           
        }
        public int codigo { get; set; }
        public DateTime fecha { get; set; }
        public String descripcion { get; set; }
        public String usuario { get; set; }
        public int codigoNivel { get; set; }
        public String dvh { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


        public int Compare( Bitacora x,  Bitacora y)
        {
            return x.fecha.CompareTo(y.fecha);
        }



        public class CompareCodigoNivel : IComparer<Bitacora>
        {
            int IComparer<Bitacora>.Compare(Bitacora x, Bitacora y)
            {
                return x.codigoNivel.CompareTo(y.codigoNivel);
            }

        }
            public class CompareFecha : IComparer<Bitacora>
            {
                int IComparer<Bitacora>.Compare(Bitacora x, Bitacora y)
                {
                    return x.fecha.CompareTo(y.fecha);
                }
            }

        public class CompareDescripcion : IComparer<Bitacora>
        {
            int IComparer<Bitacora>.Compare(Bitacora x, Bitacora y)
            {
                return x.descripcion.CompareTo(y.descripcion);
            }
        }
    }
}
