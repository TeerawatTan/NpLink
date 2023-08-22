using EndoscopicSystem.Constants;
using EndoscopicSystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndoscopicSystem
{
    public partial class EndoscopyRoomForm : Form
    {
        readonly EndoscopicEntities db = new EndoscopicEntities();
        private readonly int UserID;

        public EndoscopyRoomForm(int userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        private void EndoscopyRoomForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtNameEN;
            txtNameEN.Focus();
            GetDate();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtNameEN.Text) || !string.IsNullOrWhiteSpace(txtNameTH.Text))
                {
                    if (!string.IsNullOrWhiteSpace(txtRoomID.Text))
                    {
                        int id = Convert.ToInt32(txtRoomID.Text);
                        var data = db.Rooms.Where(x => x.RoomID == id).FirstOrDefault();
                        data.NameTH = txtNameTH.Text;
                        data.NameEN = txtNameEN.Text;
                        data.IsActive = chkEnable.Checked;
                    }
                    else
                    {
                        Room room = new Room
                        {
                            NameEN = txtNameEN.Text,
                            NameTH = txtNameTH.Text,
                            CreateDate = DateTime.Now,
                            IsActive = chkEnable.Checked,
                            CreateBy = UserID,
                        };

                        db.Rooms.Add(room);
                    }
                    if (db.SaveChanges() > 0)
                    {
                        MessageBox.Show("Saved successfully.", "Save form", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Controls.ClearControls();

                        GetDate();
                    }
                    else
                    {
                        MessageBox.Show("There was a problem saving the data.", "Error save form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetDate()
        {
            var data = db.Rooms.ToList();
            LoadData(data);
        }

        private void LoadData(List<Room> data)
        {
            var list = new List<RoomModel>();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    var model = new RoomModel();
                    model.RoomID = item.RoomID;
                    model.NameTH = item.NameTH;
                    model.NameEN = item.NameEN;
                    model.IsActive = item.IsActive ?? false;
                    list.Add(model);
                }
            }
            gridRoom.DataSource = list;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var data = db.Rooms.ToList();
            if (!string.IsNullOrWhiteSpace(txtFullname.Text))
            {
                data = data.Where(x => (x.NameTH ?? "").Contains(txtFullname.Text) || (x.NameEN ?? "").Contains(txtFullname.Text)).ToList();
            }
            LoadData(data);
        }

        private void gridRoom_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string id = gridRoom.Rows[e.RowIndex].Cells["RoomID"].FormattedValue.ToString();
            string nameTh = gridRoom.Rows[e.RowIndex].Cells["NameTH"].FormattedValue.ToString();
            string nameEn = gridRoom.Rows[e.RowIndex].Cells["NameEN"].FormattedValue.ToString();
            bool isActive = Convert.ToBoolean(gridRoom.Rows[e.RowIndex].Cells["IsActive"].Value);
            if (!string.IsNullOrWhiteSpace(id))
            {
                txtRoomID.Text = id;
                txtNameTH.Text = nameTh;
                txtNameEN.Text = nameEn;
                chkEnable.Checked = isActive;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.Controls.ClearControls();
            txtNameEN.Focus();
            GetDate();
        }
    }

    public class RoomModel
    {
        public int RoomID { get; set; }
        public string NameEN { get; set; }
        public string NameTH { get; set; }
        public bool IsActive { get; set; }
    }
}
