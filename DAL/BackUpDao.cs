using System;
using System.Collections.Generic;
using System.Text;
using Entity;

namespace DAL
{
    public class BackUpDao : Crud
    {
        private BackUpDao() { }
        private static BackUpDao instane = null;
        public static BackUpDao Instane
        {
            get
            {
                if (instane == null)
                {
                    instane = new BackUpDao();
                }
                return instane;
            }
        }
        public int buildBackUp(String path,int partes)
        {
            String p = "";
            for(int i = 0; i < partes; i++)
            {
                if (i == 0)
                {
                p += " DISK = '" + path + "/backUp"+i+".bak" + "'" ;

                }
                else
                {
                    p += ", DISK = '" + path + "/backUp" + i + ".bak" + "'" ;

                }
            }

            string query = "BACKUP DATABASE mundoSinFronteras TO "
                + p 
                +"WITH FORMAT,MEDIANAME = 'SQLServerBackups'," 
                + "NAME = 'Full Backup of mundoSinFronteras';";
          
            return dao.ExecuteNonQuery(query);
        }

    
        public int restoreBackup(List<String> paths)
        {
            String p = "";

            foreach(String path in paths)
            {
                if (p.Equals(""))
                {
                p += "DISK = '" + path + "'";
                }
                else
                {
                    p += ",DISK = '" + path + "'";

                }
            }
            string query = "RESTORE DATABASE LUG1Parcial2021 FROM " 
                + p
                +"WITH FILE = 1, NOUNLOAD, STATS = 5; ";

            return dao.ExecuteNonQuery(query);
        }
    }
}
