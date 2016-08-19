using Imposto.Core.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Imposto.Domain;
using System.Configuration;
using Imposto.Core.Interfaces;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        private Pedido pedido;
        private INotaFiscalServices service;
        private List<string> EstadosValidos;
        public FormImposto()
        {
            InitializeComponent();
            dataGridViewPedidos.AutoGenerateColumns = true;
            IniciarGrid();
            service = new NotaFiscalService();
            EstadosValidos = new List<string>() {"AC","AL","AP","AM","BA","CE","DF","ES","GO","MA",
                "MT","MS","MG","PA","PB","PR","PE","PI","RJ","RN","RS","RO","RR","SC","SP","SE","TO"};
        }
        private void IniciarGrid()
        {
            dataGridViewPedidos.DataSource = GetTablePedidos();
            ResizeColumns();
        }

        private void ResizeColumns()
        {
            double mediaWidth = dataGridViewPedidos.Width / dataGridViewPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }
        }

        private object GetTablePedidos()
        {
            DataTable table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(decimal)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));

            return table;
        }

        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {
            if (!VerificarCampos())
                return;

            pedido = new Pedido();
            pedido.EstadoOrigem = txtEstadoOrigem.Text;
            pedido.EstadoDestino = txtEstadoDestino.Text;
            pedido.NomeCliente = txtNomeCliente.Text;

            DataTable table = (DataTable)dataGridViewPedidos.DataSource;

            foreach (DataRow row in table.Rows)
            {
                pedido.ItensDoPedido.Add(
                    new PedidoItem()
                    {
                        Brinde = Convert.ToString(row["Brinde"]) != "",
                        CodigoProduto = row["Codigo do produto"].ToString(),
                        NomeProduto = row["Nome do produto"].ToString(),
                        ValorItemPedido = Convert.ToDouble(row["Valor"].ToString())
                    });
            }
            var caminho = ConfigurationManager.AppSettings["CaminhoXMLNotaFiscal"];
            var NotaFiscal = service.GerarNotaFiscal(pedido);
            service.GerarXML(NotaFiscal, caminho);
            service.PersistirNotaBanco(NotaFiscal);

            MessageBox.Show("Operação efetuada com sucesso");
            LimparTela();


        }

        private bool VerificarCampos()
        {
            if (txtNomeCliente.Text == "")
            {
                if (MessageBox.Show("VocÊ realmente deseja salvar a nota sem nome de cliente !!!", "Nota Fiscal - Gerador",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
                    return false;
            }
            if (txtEstadoOrigem.Text == "")
            {
                if (MessageBox.Show("VocÊ realmente deseja salvar a nota sem preencher o campo estado origem !!!", "Nota Fiscal - Gerador",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
                    return false;
            }
            if (txtEstadoDestino.Text == "")
            {
                if (MessageBox.Show("VocÊ realmente deseja salvar a nota sem preencher o campo estado destino !!!", "Nota Fiscal - Gerador",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
                    return false;
            }
            if (((DataTable)dataGridViewPedidos.DataSource).Rows.Count == 0)
            {
                if (MessageBox.Show("VocÊ realmente deseja salvar a nota sem nenhum item !!!", "Nota Fiscal - Gerador",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
                    return false;
            }
            return true;
        }

        private void LimparTela()
        {
            IniciarGrid();
            txtEstadoDestino.Text = "";
            txtEstadoOrigem.Text = "";
            txtNomeCliente.Text = "";
        }

        private void txtEstadoOrigem_Leave(object sender, EventArgs e)
        {
            if (txtEstadoOrigem.Text != "")
            {
                var estado = txtEstadoOrigem.Text;

                if (EstadosValidos.FindAll(x => x == estado).Count() == 0)
                {
                    MessageBox.Show("Estado invalido !");
                    txtEstadoOrigem.Focus();
                }
            }
        }

        private void txtEstadoDestino_Leave(object sender, EventArgs e)
        {
            if (txtEstadoDestino.Text != "")
            {
                var estado = txtEstadoDestino.Text;
                if (EstadosValidos.FindAll(x => x == estado).Count() == 0)
                {
                    MessageBox.Show("Estado invalido !");
                    txtEstadoDestino.Focus();
                }
            }
        }

        private void txtEstadoOrigem_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
