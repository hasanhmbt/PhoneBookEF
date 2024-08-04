using MetroFramework.Forms;
using Microsoft.Extensions.Configuration;
using PhoneBook.Models;
using System.Data;
using System.Data.SqlClient;

namespace PhoneBook
{
    public partial class EditForm : MetroForm
    {

        private string _id;
        private readonly PhoneBookContext db;

        public EditForm(string Id)
        {
            db = new();
            _id = Id;
            InitializeComponent();
        }


         
        
        private void EditForm_Load(object sender, EventArgs e)
        {
           var person =  db.People.Find(Convert.ToInt32(_id));

            txtFirstName.Text = person.FirstName;
            txtLastName.Text = person.LastName;
            txtPhone.Text = person.Phone;
            txtMail.Text = person.Mail;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           var editedPerson = db.People.Find(Convert.ToInt32(_id));

            editedPerson.FirstName = txtFirstName.Text;
            editedPerson.LastName = txtLastName.Text;
            editedPerson.Phone = txtPhone.Text;
            editedPerson.Mail = txtMail.Text;

            db.People.Update(editedPerson);
            db.SaveChanges();

            MessageBox.Show(
                "Person has been updated.",
                "Update Person",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            Close();




        }
    }
}
