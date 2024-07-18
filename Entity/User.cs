using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class User : ICloneable
    {
        public User()
        {
        }

        public User(string contraseña, string mail, string user, string dniPersona, string dvh)
        {
            this.contraseña = contraseña;
            this.mail = mail;
            this.user = user;
            this.dniPersona = dniPersona;
            this.dvh = dvh;
        }

        public User(int codigo, string contraseña, string mail, string user, string dniPersona, string dvh, int rol)
        {
            this.codigo = codigo;
            this.contraseña = contraseña;
            this.mail = mail;
            this.user = user;
            this.dniPersona = dniPersona;
            this.dvh = dvh;
            this.rol = rol;
        }

        public int codigo { get; set; }

        public String contraseña { get; set; }
        public String mail { get; set; }
        public String user { get; set; }
        public String dniPersona { get; set; }
        public String dvh { get; set; }
        public int rol { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


    }

}
