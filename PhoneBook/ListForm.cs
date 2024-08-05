using MetroFramework.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhoneBook.Models;
using System.Data;
using System.Data.SqlClient;

namespace PhoneBook;

public partial class ListForm : MetroForm
{
    private readonly PhoneBookContext _DbContext;
    public ListForm()
    {
        InitializeComponent();
        _DbContext = new();
    }

    #region Load Function
    void Refresh()
    {
        lstPeople.Items.Clear();

        var people = _DbContext.People.Select(p => new
        {
            p.Id,
            p.FirstName,
            p.LastName,
            p.Phone,
            p.Mail
        }).ToList();

        foreach (var person in people)
        {
            ListViewItem lvi = new ListViewItem(person.Id.ToString());
            lvi.SubItems.Add(person.FirstName);
            lvi.SubItems.Add(person.LastName);
            lvi.SubItems.Add(person.Phone);
            lvi.SubItems.Add(person.Mail);
            lstPeople.Items.Add(lvi);
        }

         
    }



    #endregion



    private void ListForm_Load(object sender, EventArgs e)
    {
        Refresh();
    }
    private void cmsRefresh_Click(object sender, EventArgs e)
    {
        Refresh();
    }


    private void cmsSil_Click(object sender, EventArgs e)
    {
        if (lstPeople.SelectedItems.Count == 0)
        {
            MessageBox.Show(
                "Please select a record to delete.",
                "Delete Item",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        DialogResult dialogResult = MessageBox.Show(
            "Are you sure you want to delete the selected record?",
            "Delete Item",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (dialogResult == DialogResult.No)
        {
            return;
        }

        int personId;
        if (int.TryParse(lstPeople.SelectedItems[0].Text, out personId))
        {
            var selectedPerson = _DbContext.People.Find(personId);
            if (selectedPerson != null)
            {
                _DbContext.People.Remove(selectedPerson);
                _DbContext.SaveChanges();

                lstPeople.SelectedItems[0].Remove();
                 
                MessageBox.Show(
                    "Record deleted successfully.",
                    "Delete Notification",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "The selected record was not found.",
                    "Delete Item",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        else
        {
            MessageBox.Show(
                "Invalid ID.",
                "Delete Item",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }


    private void ListForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        Program.MainFormInstance.Show();
    }

    private void cmsEdit_Click(object sender, EventArgs e)
    {
        if (lstPeople.SelectedItems.Count == 0)
        {
            MessageBox.Show(
                "Please select a record to update.",
                "Update Item",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            return;
        }



        string selectedId = lstPeople.SelectedItems[0].Text;
        EditForm frm = new(selectedId);
        frm.ShowDialog();
    }
}

