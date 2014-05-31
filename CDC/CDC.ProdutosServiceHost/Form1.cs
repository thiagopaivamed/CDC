using System;
using System.ServiceModel;
using System.Windows.Forms;

namespace CDC.ProdutosServiceHost
{
    public partial class Form1 : Form
    {
        private ServiceHost host;
        private const string executando = "Serviço em execução";
        private const string parado = "Serviço parado";

        public Form1()
        {
            InitializeComponent();
            host = new ServiceHost(typeof(ProdutosService.ConsultarProduto));
            host.Open();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            lblMessage.Text = executando;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            host = new ServiceHost(typeof(ProdutosService.ConsultarProduto));
            host.Open();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            lblMessage.Text = executando;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            host.Close();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            lblMessage.Text = parado;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            host.Close();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }
    }
}
