using IPDProductivityLibrary;
using IPDProductivityUI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPDProductivityUI
{
    public partial class MainFrm : Form
    {
        List<WardProduct> listIPDProductivity = new List<WardProduct>();
        public MainFrm()
        {
            InitializeComponent();
        }
        private void MainFrm_Load(object sender, EventArgs e)
        {
            Helper.GetCurrentSetWard();

            var dt4A = Helper.DtToWardProduct(GetData.GetBedRoom(Data._11W4L), Data._11W4L);
            var dt4R = Helper.DtToWardProduct(GetData.GetBedRoom(Data._11W4R), Data._11W4R);

            listIPDProductivity.Add(dt4A);
            listIPDProductivity.Add(dt4R);

            ShowDataOnDataGridView();
        }

        private void ShowDataOnDataGridView()
        {
            SetValuesToControls();
        }

        private void SetValuesToControls()
        {
            int i = 0;
            foreach (SetWardModel setWard in Constants.listSetWardModel)
            {
                foreach (WardProduct wardProduct in listIPDProductivity)
                {
                    if (setWard.WardCode.Trim() == wardProduct.WardCode.Trim())
                    {
                        Button btnx = this.Controls.Find("btn" + i, true).FirstOrDefault() as Button;
                        if (btnx != null)
                            btnx.Text = wardProduct.WardCode;

                        DataGridView gridx = this.Controls.Find("grid" + i, true).FirstOrDefault() as DataGridView;
                        if (gridx != null)
                        {
                            gridx.DataSource = Helper.GetDTForDisplayOnDataGrid(wardProduct.WardModel.PatientRoomClassList);
                            gridx.Columns[0].Width = 60;
                            gridx.Columns[1].Width = 40;
                        }

                        TextBox txtPerx = this.Controls.Find("txtPer" + i, true).FirstOrDefault() as TextBox;
                        if (txtPerx != null)
                        {
                            txtPerx.Text = wardProduct.WardCalculate.WardPercentage.ToString() + " %";
                            txtPerx.TextAlign = HorizontalAlignment.Center;
                        }


                        TextBox txtKardexx = this.Controls.Find("txtKardex" + i, true).FirstOrDefault() as TextBox;
                        if (txtKardexx != null)
                        {
                            txtKardexx.Text = wardProduct.WardCalculate.NumPatientKardex.ToString();
                            txtKardexx.TextAlign = HorizontalAlignment.Center;
                        }

                        TextBox txtTotx = this.Controls.Find("txtTot" + i, true).FirstOrDefault() as TextBox;
                        if (txtTotx != null)
                        {
                            txtTotx.Text = wardProduct.WardCalculate.NumPatientClass.ToString();
                            txtTotx.TextAlign = HorizontalAlignment.Center;
                        }

                        #region Staff Data

                        TextBox txtDayRnDx = this.Controls.Find("txtDayRnD" + i, true).FirstOrDefault() as TextBox;
                        if (txtDayRnDx != null)
                        {
                            txtDayRnDx.Text = wardProduct.Staffs.StaffData.DayRnData.ToString();
                        }

                        TextBox txtDayNrDx = this.Controls.Find("txtDayNrD" + i, true).FirstOrDefault() as TextBox;
                        if (txtDayNrDx != null)
                        {
                            txtDayNrDx.Text = wardProduct.Staffs.StaffData.DayNrData.ToString();
                        }

                        TextBox txtEveRnDx = this.Controls.Find("txtEveRnD" + i, true).FirstOrDefault() as TextBox;
                        if (txtEveRnDx != null)
                        {
                            txtEveRnDx.Text = wardProduct.Staffs.StaffData.EveRnData.ToString();
                        }

                        TextBox txtEveNrDx = this.Controls.Find("txtEveNrD" + i, true).FirstOrDefault() as TextBox;
                        if (txtEveNrDx != null)
                        {
                            txtEveNrDx.Text = wardProduct.Staffs.StaffData.EveNrData.ToString();
                        }

                        TextBox txtNigRnDx = this.Controls.Find("txtNigRnD" + i, true).FirstOrDefault() as TextBox;
                        if (txtNigRnDx != null)
                        {
                            txtNigRnDx.Text = wardProduct.Staffs.StaffData.NigRnData.ToString();
                        }

                        TextBox txtNigNrDx = this.Controls.Find("txtNigNrD" + i, true).FirstOrDefault() as TextBox;
                        if (txtNigNrDx != null)
                        {
                            txtNigNrDx.Text = wardProduct.Staffs.StaffData.NigNrData.ToString();
                        }

                        #endregion

                        #region Staff Real

                        TextBox txtDayRnRx = this.Controls.Find("txtDayRnR" + i, true).FirstOrDefault() as TextBox;
                        if (txtDayRnRx != null)
                        {
                            txtDayRnRx.Text = wardProduct.Staffs.StaffReal.DayRnReal.ToString();
                        }

                        TextBox txtDayNrRx = this.Controls.Find("txtDayNrR" + i, true).FirstOrDefault() as TextBox;
                        if (txtDayNrRx != null)
                        {
                            txtDayNrRx.Text = wardProduct.Staffs.StaffReal.DayNrReal.ToString();
                        }

                        TextBox txtEveRnRx = this.Controls.Find("txtEveRnR" + i, true).FirstOrDefault() as TextBox;
                        if (txtEveRnRx != null)
                        {
                            txtEveRnRx.Text = wardProduct.Staffs.StaffReal.EveRnReal.ToString();
                        }

                        TextBox txtEveNrRx = this.Controls.Find("txtEveNrR" + i, true).FirstOrDefault() as TextBox;
                        if (txtEveNrRx != null)
                        {
                            txtEveNrRx.Text = wardProduct.Staffs.StaffReal.EveNrReal.ToString();
                        }

                        TextBox txtNigRnRx = this.Controls.Find("txtNigRnR" + i, true).FirstOrDefault() as TextBox;
                        if (txtNigRnRx != null)
                        {
                            txtNigRnRx.Text = wardProduct.Staffs.StaffReal.NigRnReal.ToString();
                        }

                        TextBox txtNigNrRx = this.Controls.Find("txtNigNrR" + i, true).FirstOrDefault() as TextBox;
                        if (txtNigNrRx != null)
                        {
                            txtNigNrRx.Text = wardProduct.Staffs.StaffReal.NigNrReal.ToString();
                        }

                        #endregion

                    }
                }
                i++;
            }
        }

        private void btnSetWard_Click(object sender, EventArgs e)
        {
            SetWardFrm frm = new SetWardFrm();
            frm.ShowDialog();
            Helper.GetCurrentSetWard();
            ShowDataOnDataGridView();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }
    }
}
