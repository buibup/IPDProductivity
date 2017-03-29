using IPDProductivityLibrary;
using IPDProductivityUI.Common;
using IPDProductivityUI.DA.SqlServer;
using MetroFramework.Forms;
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
    public partial class SetWardFrm : MetroForm
    {
        public SetWardFrm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<SetWardModel> setWardList = new List<SetWardModel>();
            foreach (var item in listboxWard.Items)
            {
                var setWard = new SetWardModel();
                setWard.WardCode = (item as SetWardModel).WardCode;
                setWard.WardDesc = (item as SetWardModel).WardDesc;
                setWardList.Add(setWard);
            }
            
            if (SqlServerDA.ExecuteNonQuery(QueryString.TruncateTable("SetWard"), Constants.Svh21CHKConnectionString))
            {
                Helper.SetWardToDb(setWardList);
            }

            Helper.GetCurrentSetWard();
            this.Close();
        }

        private void SetWard_Load(object sender, EventArgs e)
        {

            listboxWard.DisplayMember = "WardDesc";
            listboxWard.ValueMember = "WardCode";

            foreach (var item in Constants.listSetWardModel)
            {
                listboxWard.Items.Add(new SetWardModel
                {
                    WardCode = item.WardCode,
                    WardDesc = item.WardDesc
                });
            }
        }

        public void MoveItem(int direction)
        {
            // Checking selected item
            if (listboxWard.SelectedItem == null || listboxWard.SelectedIndex < 0)
                return;

            // Calculate new index using move direction
            int newIndex = listboxWard.SelectedIndex + direction;

            // Checking bounds of the range
            if (newIndex < 0 || newIndex >= listboxWard.Items.Count)
                return; // Index out of range - nothing to do

            object selected = listboxWard.SelectedItem;

            // Removing removable element
            listboxWard.Items.Remove(selected);

            // Insert it in new position
            listboxWard.Items.Insert(newIndex, selected);

            // Restore selection
            listboxWard.SetSelected(newIndex, true);
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            MoveItem(-1);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            MoveItem(1);
        }
    }
}
