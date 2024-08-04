using MetroFramework.Forms;
using PhoneBook.Models;

namespace PhoneBook
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        void Clear(Control ctrl)
        {
            foreach (Control item in ctrl.Controls)
            {
                if (item is TextBox txt)
                {
                    txt.Clear();
                }
                else if (item is NumericUpDown nmr)
                {
                    nmr.Value = nmr.Minimum;
                }
                else if (item is ComboBox cmb)
                {
                    cmb.SelectedIndex = -1;
                }
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            PhoneBookContext db = new();


            if (string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtMail.Text))
            {
                MessageBox.Show(
                    "Please fill in all fields.",
                    "Save Person",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Person person = new()
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Phone = txtPhone.Text,
                Mail = txtMail.Text
            };

            db.People.Add(person);
            db.SaveChanges();

            Clear(grbSavePerson);

            MessageBox.Show(
                "Person saved successfully.",
                "Save Person",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           Program.MainFormInstance.Show();
        } 
    }
}
