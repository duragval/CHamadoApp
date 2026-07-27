using System;
using System.Collections.Generic;
using System.Text;

namespace ChamadoApp
{
    internal class ChamadoService
    {
        private readonly string _diretorio;

        public ChamadoService(string diretorio = "Chamados")
        {
            _diretorio = diretorio;
            Directory.CreateDirectory(_diretorio);
        }

        private string CaminhoDoArquivo(int id)
        {
            return Path.Combine(_diretorio, $"chamado_{id:D3}.txt");
        }

        public int ObterProximoId()
        {
            var arquivos = Directory.GetFiles(_diretorio, "chamado_*.txt");

            var ultimoId = arquivos
                .Select(caminho => Path.GetFileNameWithoutExtension(caminho))
                .Select(nome => nome.Split('_'))
                .Where(partes => partes.Length == 2 && int.TryParse(partes[1], out _))
                .Select(partes => int.Parse(partes[1]))
                .DefaultIfEmpty(0)
                .Max();

            return ultimoId + 1;
        }

        public void Salvar (Chamado chamado)
        {
            try
            {
                var caminho = CaminhoDoArquivo(chamado.Id);
                File.WriteAllText(caminho, chamado.GerarConteudo());
            }
            catch ( Exception ex)
            {
                throw new IOException($"Erro ao salvar o chamado {chamado.Id} : {ex.Message}", ex);
            }

        }

        public void Atualizar (Chamado chamado)
        {
            Salvar(chamado);
        }

        public List<Chamado> ListarChamados()
        {
            var chamados = new List<Chamado>();
            var arquivos = Directory.GetFiles(_diretorio, "chamado_*.txt");

            foreach (var arquivo in arquivos)
            {
                try
                {
                    var conteudo = File.ReadAllText(arquivo);
                    var chamado = Chamado.CarregarDeTexto(conteudo);
                    chamados.Add(chamado);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Aviso: não foi possivel ler o arquivo {Path.GetFileName(arquivo)}. {ex.Message}");
                }
            }

            return chamados.OrderBy(c => c.Id).ToList();
        }
    }
}
