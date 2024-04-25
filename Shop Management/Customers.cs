using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace Shop_Management
{
    public partial class Customers : Form
    {
        private Functions Con;

        public Customers()
        {
            InitializeComponent();
            Con = new Functions();
            ShowCustomers();
        }

        private void ShowCustomers()
        {
            try
            {
                string Query = "SELECT CustCode as ID,Name,Gender,Phone FROM CustomerTbl";
                CustomersList.DataSource = Con.GetData(Query);
                CustomersList.ColumnHeadersHeight = 30;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (NameCus.Text == "" || GenderCus.Text == "" || PhoneCus.Text == "")
            {
                MessageBox.Show("Missing Data !!");
            }
            else
            {
                try
                {
                    string itemName = NameCus.Text;
                    string gender = GenderCus.Text;
                    string phoneNumber = PhoneCus.Text;

                    string query = "UPDATE CustomerTbl SET Name='{0}', Gender='{1}', Phone='{2}' WHERE CustCode={3}";
                    query = string.Format(query, itemName, gender, phoneNumber, key);

                    Con.SetData(query);
                    ShowCustomers();
                    MessageBox.Show("Customer Updated !!");

                    // Clear the textboxes after successful update
                    NameCus.Text = "";
                    GenderCus.Text = "";
                    PhoneCus.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void AddBtn_Click_1(object sender, EventArgs e)
        {
            if (NameCus.Text == "" || GenderCus.Text == "" || PhoneCus.Text == "")
            {
                MessageBox.Show("Missing Data !!");
            }
            else
            {
                try
                {
                    string itemName = NameCus.Text;
                    string gender = GenderCus.Text;
                    string phoneNumber = PhoneCus.Text;

                    string query = "INSERT INTO CustomerTbl (Name, Gender, Phone) VALUES ('{0}', '{1}', '{2}')";
                    query = string.Format(query, itemName, gender, phoneNumber);

                    Con.SetData(query);
                    ShowCustomers();
                    MessageBox.Show("Customer Added !!");

                    NameCus.Text = "";
                    GenderCus.Text = "";
                    PhoneCus.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    

       int key = 0;
        private void CustomersList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

           
            NameCus.Text = CustomersList.SelectedRows[0].Cells[1].Value.ToString();
            GenderCus.Text = CustomersList.SelectedRows[0].Cells[2].Value.ToString();
            PhoneCus.Text = CustomersList.SelectedRows[0].Cells[3].Value.ToString();
            
            if (NameCus.Text == "" || GenderCus.Text == "" || PhoneCus.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CustomersList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
 

        private void DeleteBtn_Click_1(object sender, EventArgs e)
        {
            if (NameCus.Text == "" || GenderCus.Text == "" || PhoneCus.Text == "")
            {
                MessageBox.Show("Missing Data !!");
            }
            else
            {
                try
                {
                    string query = "DELETE FROM CustomerTbl WHERE CustCode={0}";
                    query = string.Format(query, key);

                    Con.SetData(query);
                    ShowCustomers();
                    MessageBox.Show("Customer Deleted !!");

                    // Clear the textboxes after successful deletion
                    NameCus.Text = "";
                    GenderCus.Text = "";
                    PhoneCus.Text = "";
                    //key = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            items It = new items();
            It.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Categories Cat = new Categories();
            Cat.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billing Bill = new Billing();
            Bill.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            items It = new items();
            It.Show();
            this.Hide();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (NameCus.Text == "" || GenderCus.Text == "" || PhoneCus.Text == "")
            {
                MessageBox.Show("Missing Data !!");
            }
            else
            {
                try
                {
                    string itemName = NameCus.Text;
                    string gender = GenderCus.Text;
                    string phoneNumber = PhoneCus.Text;

                    string query = "UPDATE CustomerTbl SET Name='{0}', Gender='{1}', Phone='{2}' WHERE CustCode={3}";
                    query = string.Format(query, itemName, gender, phoneNumber, key);

                    Con.SetData(query);
                    ShowCustomers();
                    MessageBox.Show("Customer Updated !!");

                    // Clear the textboxes after successful update
                    NameCus.Text = "";
                    GenderCus.Text = "";
                    PhoneCus.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select a customer to delete !!");
            }
            else
            {
                try
                {
                    string query = "DELETE FROM CustomerTbl WHERE CustCode={0}";
                    query = string.Format(query, key);

                    Con.SetData(query);
                    ShowCustomers();
                    MessageBox.Show("Customer Deleted !!");

                    // Clear the textboxes after successful deletion
                    NameCus.Text = "";
                    GenderCus.Text = "";
                    PhoneCus.Text = "";
                    key = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }

}
