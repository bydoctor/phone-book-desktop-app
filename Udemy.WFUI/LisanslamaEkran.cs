using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Udemy.WFUI
{
    public partial class LisanslamaEkran : Form
    {
        public LisanslamaEkran()
        {
            InitializeComponent();
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if(txt_lisanskey.Text == "644e1dd7-2a7f-18fb-b8ed-ed78c3f92c2b")
            {
                string HarddiskSeriNumarasi = string.Empty;
                string MacAddress = string.Empty;

                string Surucu = "C";
                ManagementObject Disk = new ManagementObject("Win32_LogicalDisk.DeviceID=\"" + Surucu + ":\"");
                Disk.Get();

                HarddiskSeriNumarasi =  Disk["VolumeSerialNumber"].ToString();

                ManagementClass MACADD = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection NAL = MACADD.GetInstances();
                foreach (ManagementObject item in NAL)
                {
                    if((bool)item["IPEnabled"])
                    {
                        MacAddress = item["MacAddress"].ToString();
                    }
                }

                if(!string.IsNullOrEmpty(HarddiskSeriNumarasi) && !string.IsNullOrEmpty(MacAddress))
                {
                    RegistryKey Key = Registry.CurrentUser.CreateSubKey("TelefonRehberi", true);
                    Key.SetValue("HardDiskSeriNumarasi", HarddiskSeriNumarasi);
                    Key.SetValue("MACAddress", MacAddress);

                    MessageBox.Show("Lisanslama Isleminiz Tamamlanmıştır. Lütfen Uygulamayı Kapatıp Açınız", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Girmiş olduğunuz Lisans Numarası Hatalıdır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
