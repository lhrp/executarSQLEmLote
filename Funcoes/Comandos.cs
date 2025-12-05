using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace executarSQLEmLote.Funcoes
{
    public class Comandos
    {

        private readonly BancoDados bancoDados;
        private string comandoSQL;

        public Comandos(BancoDados bd)
        {
            bancoDados = bd;
        }


        public DataTable ListarBases()
        {
            comandoSQL = "SELECT Bases.database_id" +
                         "     , Bases.name" +
                         "  FROM sys.databases Bases" +
                         " WHERE Bases.name NOT IN ('master', 'tempdb', 'model', 'msdb')";

            return bancoDados.Consultar(comandoSQL);
        }

        public bool ValidarTabelaExiste(string nomeTabela)
        {
            comandoSQL = "IF OBJECT_ID('" + nomeTabela + "', 'U') IS NOT NULL " +
                         "   SELECT 1 AS TabelaValida " +
                         "ELSE " +
                         "   SELECT 0 AS TabelaValida ";
            return bancoDados.Consultar(comandoSQL).Rows[0].Field<int>("TabelaValida") == 1;


        }

        public async Task CriarProceduresAsync(Action<string>? logAcao = null)
        {
            // Pasta "Arquivos/Procedure" relativa ao executável
            string pastaProcedures = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Arquivos",
                "Procedure"
            );

            if (!Directory.Exists(pastaProcedures))
            {
                logAcao?.Invoke($"Pasta de procedures não encontrada: {pastaProcedures}");
                return;
            }

            // Somente arquivos .sql (ajuste se precisar)
            string[] arquivos = Directory.GetFiles(pastaProcedures, "*.sql", SearchOption.TopDirectoryOnly);

            if (arquivos.Length == 0)
            {
                logAcao?.Invoke("Nenhum arquivo .sql encontrado na pasta de procedures.");
                return;
            }

            foreach (var arquivo in arquivos)
            {
                string nomeArquivo = Path.GetFileName(arquivo);

                try
                {
                    // Leitura assíncrona do arquivo
                    string script = await File.ReadAllTextAsync(arquivo, Encoding.UTF8);

                    if (string.IsNullOrWhiteSpace(script))
                    {
                        logAcao?.Invoke($"Arquivo vazio ou inválido: {nomeArquivo}");
                        continue;
                    }

                    // Execução do script no banco (método síncrono rodando em Task.Run para não travar a UI)
                    await Task.Run(() => bancoDados.ExecutarComando(script));

                    logAcao?.Invoke($"✅ Script executado com sucesso: {nomeArquivo}");
                }
                catch (Exception ex)
                {
                    logAcao?.Invoke($"❌ Erro ao executar {nomeArquivo}: {ex.Message}");
                }
            }
        }



    }
}
