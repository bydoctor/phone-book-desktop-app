using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Udemy.WFUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool LK =  LisansKontrol();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (LK)
            {
                Application.Run(new Form1());
            }
            else
            {
                Application.Run(new LisanslamaEkran());
            }
        }

        static bool LisansKontrol()
        {
            RegistryKey RK = Registry.CurrentUser.OpenSubKey("TelefonRehberi");
            if (RK != null)
            {
                string HarddiskSeriNumarasi = string.Empty;
                string MacAddress = string.Empty;

                string Surucu = "C";
                ManagementObject Disk = new ManagementObject("Win32_LogicalDisk.DeviceID=\"" + Surucu + ":\"");
                Disk.Get();

                HarddiskSeriNumarasi = Disk["VolumeSerialNumber"].ToString();

                ManagementClass MACADD = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection NAL = MACADD.GetInstances();
                foreach (ManagementObject item in NAL)
                {
                    if ((bool)item["IPEnabled"])
                    {
                        MacAddress = item["MacAddress"].ToString();
                    }
                }

                string HDDSNSTR = RK.GetValue("HardDiskSeriNumarasi").ToString();
                string MACADDSTR = RK.GetValue("MACAddress").ToString();

                if (HDDSNSTR == HarddiskSeriNumarasi && MACADDSTR == MacAddress)
                {
                    return true;
                }
                else
                {
                    return false;
                }

               
            }
            else
            {
                return false;
            }
        }
    }
}
