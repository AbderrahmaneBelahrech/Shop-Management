using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_Management
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UserName.Text == "" || Password.Text == "")
            {
                MessageBox.Show("Missing Data !!");
            }
            else if (UserName.Text != "Admin" || Password.Text != "123")
            {
                MessageBox.Show("User Name ou Password est incorrecte !!");
            }
            else
            {
                // Authentification réussie, redirigez vers la fenêtre items.cs
                items itemsForm = new items();
                itemsForm.Show();

                // Fermez la fenêtre de connexion (login.cs) si nécessaire
                this.Hide();
            }
        
    }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            UserName.Text = "";
            Password.Text = "";
        }
    }
}
