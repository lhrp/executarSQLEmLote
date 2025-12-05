using executarSQLEmLote.Funcoes;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;


namespace executarSQLEmLote
{
    public partial class TelaPrincipal : Form
    {

        public string IpServidor { get; set; }
        public string BaseDados { get; set; }
        private readonly string pastaDados = @"C:\executarSQLEmLote";
        private readonly string arquivoTabelasJson;


        public BancoDados bancoDados;
        public Comandos comandos;


        public TelaPrincipal()
        {
            InitializeComponent();
            arquivoTabelasJson = Path.Combine(pastaDados, "tabelas.json");

            // Carrega o JSON (se existir) ao abrir a tela
            CarregarTabelasDoJson();

        }

        public void montaConexao()
        {
            IpServidor = txtIpServidor.Text;
            BaseDados = cboxBaseDados.Text;
            bancoDados = new BancoDados(IpServidor, BaseDados);
            comandos = new Comandos(bancoDados);

        }

        private void CarregarTabelasDoJson()
        {
            try
            {
                if (!Directory.Exists(pastaDados))
                    return; // primeira execução, nada salvo ainda

                if (!File.Exists(arquivoTabelasJson))
                    return;

                string json = File.ReadAllText(arquivoTabelasJson);

                // Aqui estou salvando como lista de strings (apenas nome da tabela)
                List<string>? tabelas = JsonSerializer.Deserialize<List<string>>(json);

                if (tabelas == null || tabelas.Count == 0)
                    return;

                // Limpa o grid antes de popular
                gridListaTabelas.Rows.Clear();

                foreach (var nomeTabela in tabelas)
                {
                    int index = gridListaTabelas.Rows.Add();
                    gridListaTabelas.Rows[index].Cells["nomeTabela"].Value = nomeTabela;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar as tabelas do JSON: {ex.Message}");
            }
        }


        private void btnListarBases_Click(object sender, EventArgs e)
        {
            montaConexao();

            if (string.IsNullOrWhiteSpace(txtIpServidor.Text))
            {
                MessageBox.Show("Informe o IP do Servidor");
                return;
            }

            DataTable basesDisponiveis = comandos.ListarBases();

            if (basesDisponiveis.Rows.Count > 0)
            {
                foreach (DataRow row in basesDisponiveis.Rows)
                {
                    string nomeBase = row["name"].ToString();

                    cboxBaseDados.Items.Add(nomeBase);
                }
                cboxBaseDados.DroppedDown = true;
                cboxBaseDados.SelectedIndex = 0;
            }
        }

        public void ProcessarTabelasGrid()
        {
            List<string> listaTabelas = new List<string>();

            // Salvar lista de tabelas em JSON
            if (listaTabelas.Count > 0)
            {
                try
                {
                    // Garante que a pasta existe (se não existir, cria)
                    Directory.CreateDirectory(pastaDados);

                    string json = JsonSerializer.Serialize(
                        listaTabelas,
                        new JsonSerializerOptions { WriteIndented = true }
                    );

                    File.WriteAllText(arquivoTabelasJson, json);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar JSON das tabelas: {ex.Message}");
                }
            }

            foreach (DataGridViewRow row in gridListaTabelas.Rows)
            {
                if (row.IsNewRow) continue; // Pula a linha nova

                string nomeTabela = row.Cells["nomeTabela"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(nomeTabela))
                {
                    listaTabelas.Add(nomeTabela);

                    txtLog.Text += $"Processamendo Tabela: {nomeTabela}{Environment.NewLine}";

                }
            }

            
        }

        public async Task ProcessarTabelasGridAsync()
        {
            montaConexao();
            txtLog.Clear();
            List<string> listaTabelas = new List<string>();

            foreach (DataGridViewRow row in gridListaTabelas.Rows)
            {
                if (row.IsNewRow) continue; // Pula a linha nova

                string nomeTabela = row.Cells["nomeTabela"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(nomeTabela))
                {

                    if (comandos.ValidarTabelaExiste(nomeTabela))
                    {
                        listaTabelas.Add(nomeTabela);

                        // Aqui seria o ponto para processar de fato a tabela
                        // Exemplo (sincrono):
                        // comandos.ProcessarTabela(nomeTabela);
                        //
                        // Ou, se você criar algo assíncrono no Comandos:
                        // await comandos.ProcessarTabelaAsync(nomeTabela);

                        // Atualiza o log
                        txtLog.AppendText($"Processando Tabela: {nomeTabela}{Environment.NewLine}");

                        // Pequeno delay opcional para não travar a UI e ver o log "andando"
                        await Task.Delay(50);
                    }
                    //else
                    //{
                    //    MessageBox.Show($"A tabela '{nomeTabela}' não existe na base de dados '{BaseDados}'.");
                    //}

                    
                }
            }

            // Salvar lista de tabelas em JSON (se houver algo)
            if (listaTabelas.Count > 0)
            {
                try
                {
                    Directory.CreateDirectory(pastaDados);

                    string json = JsonSerializer.Serialize(
                        listaTabelas,
                        new JsonSerializerOptions { WriteIndented = true }
                    );

                    await File.WriteAllTextAsync(arquivoTabelasJson, json);

                    txtLog.AppendText($"Lista de tabelas salva em: {arquivoTabelasJson}{Environment.NewLine}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar JSON das tabelas: {ex.Message}");
                }
            }
            else
            {
                txtLog.AppendText("Nenhuma tabela válida encontrada para processar." + Environment.NewLine);
            }
        }



        private void btnExecutar_Click(object sender, EventArgs e)
        {

            //ProcessarTabelasGrid();
            ProcessarTabelasGridAsync();

        }

        private async void btnProcedures_Click(object sender, EventArgs e)
        {

            try
            {
                // Garante que a conexão está montada
                montaConexao();

                txtLog.AppendText("Iniciando execução das procedures..." + Environment.NewLine);

                await comandos.CriarProceduresAsync(msg =>
                {
                    // Atualiza o log na tela
                    txtLog.AppendText(msg + Environment.NewLine);
                });

                txtLog.AppendText("Finalizado processamento dos arquivos de procedure." + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao executar procedures: {ex.Message}");
            }

        }


    }
}
