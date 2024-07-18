using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
   public class BackUpBl
    {
        BackUpDao backUpDao = BackUpDao.Instane;
        private BackUpBl() { }
        private static BackUpBl instane = null;
        public static BackUpBl Instane
        {
            get
            {
                if (instane == null)
                {
                    instane = new BackUpBl();
                }
                return instane;
            }
        }

        public int buildBackUpBl(String path,int partes)
        {

            return backUpDao.buildBackUp(path, partes);
         }

        public int restoreBackUpBl(List<String> paths)
        {


            return backUpDao.restoreBackup(paths);
        }
    }
}
