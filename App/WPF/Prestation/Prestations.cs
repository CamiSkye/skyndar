using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TestSkyndar
{
    public partial class Prestations: UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jpegl\source\repos\TestSkyndar\TestSkyndar\Bdd.mdf;Integrated Security=True");
        public Prestations()
        {
            InitializeComponent();

            PrestationData();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void titre_Click(object sender, EventArgs e)
        {

        }

        public void PrestationData()
        {
            Prestations ed = new Prestations();
            List<Prestations> liste = ed.prestations();

            dataGridView1.DataSource = liste;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (titre_pres.Text == ""
                || duree_pres.Text == ""
                || tarif_pres.Text == ""
                || descip_pres.Text == "")
            {
                MessageBox.Show("Veuillez remplir les champs de texte s'il vous plaît"
                    , "Message erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    connect.Open();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur" + ex
                   , "Message erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }
    }
}
