using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulAidatSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        private bool IsFormActivated(Form form)
        {
            bool IsOpened = false;
            if (MdiChildren.Count()>0)
            {
                foreach(var item in MdiChildren)
                {
                    if (form.Name == item.Name)
                    {
                        xtraTabbedMdiManager1.Pages[item].MdiChild.Activate();
                        IsOpened= true;
                    }

                }
            }
            return IsOpened;
        }

        private void ViewForm(Form _form)
        {
            if(!IsFormActivated(_form))
            {
                _form.MdiParent = this;
                _form.Show();
            }
        }

        FrmOgrenciler fr1;
        private void BtnOgr_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr1 = new FrmOgrenciler();
            ViewForm(fr1);
        }

        FrmVeliler fr2;
        private void BtnVeli_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr2 = new FrmVeliler();
            ViewForm(fr2);
        }

        FrmKirtasiye fr3;
        private void BtnKirtasiye_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr3= new FrmKirtasiye();
            ViewForm(fr3);
        }

        FrmOgretmen fr4;
        private void BtnOgretmen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr4= new FrmOgretmen();
            ViewForm(fr4);
        }

        FrmKirtasiyeUrunleri fr12;
        private void BtnKırtasiyeurun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr12 = new FrmKirtasiyeUrunleri();
            ViewForm(fr12);
        }

        FrmPersonel fr5;
        private void BtnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr5 = new FrmPersonel();
            ViewForm(fr5);
        }

        FrmRehber fr11;
        private void BtnRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr11 = new FrmRehber();
            ViewForm(fr11);
        }

        OdemeSekliGirisi fr6;
        private void BtnOdemesekli_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr6 = new OdemeSekliGirisi();
            fr6.ShowDialog();
        }

        FrmOdemePlani fr7;
        private void BtnOdemePlanı_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr7 = new FrmOdemePlani();
            ViewForm(fr7);
        }

        FrmBanka fr8;
        private void BtnBankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr8 = new FrmBanka();
            ViewForm(fr8);
        }

        FrmGiderler fr9;
        private void BtnGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr9 = new FrmGiderler();
            ViewForm(fr9);
        }

        FrmKasa fr10;
        private void BtnKasa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr10 = new FrmKasa();
            ViewForm(fr10);
        }


        FrmAyarlar fr13;
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr13 = new FrmAyarlar();
            fr13.ShowDialog();
        }

        FrmAcilklama fr14;
        private void BtnAcıklama_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr14 = new FrmAcilklama();
            ViewForm(fr14);
        }

        void anasayfa()
        {
            FrmAnaSayfa fr15;
            fr15 = new FrmAnaSayfa();
            ViewForm(fr15);
        }

        private void BtnAnaSayfa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            anasayfa();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            anasayfa();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        FrmSehirBilgileri fr16;
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr16 = new FrmSehirBilgileri();
            ViewForm(fr16);
        }

        FrmEgitimYili fr17;
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fr17 = new FrmEgitimYili();
            fr17.ShowDialog();
        }


        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }


    }
}
