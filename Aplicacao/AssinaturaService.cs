using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Aplicacao.DTOs.Assinatura;
using AutoMapper;
using Dominio.Entidades;

namespace Aplicacao;
public class AssinaturaService : ServiceBase<AssinaturaRetornoDto, AssinaturaDto , Assinatura, IAssinaturaRepository>, IAssinaturaService
{
    public AssinaturaService(IAssinaturaRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }

    protected override void ValidarValores(AssinaturaDto dto)
    {
       if(dto == null) _erros.Add("Informe os dados da Assinatura");
        if (dto.TipoAssinatura == 0) _erros.Add("Informe o tipo de assinatura");
        if(dto.TipoPlano == 0) _erros.Add("Informe o Tipo Plano de assinatura");
        if (_erros.Any()) throw new Exception(string.Join("\n", _erros));
    }

    protected override async Task<Assinatura> DefinirEntidadeAlteracao(Assinatura entidade, AssinaturaDto dto)
    {
       if(entidade == null)
            throw new ArgumentNullException("Assinatura não informada");

        entidade.AlterarTipoAssinatura(dto.TipoAssinatura);
        entidade.CalculaPeriodicidade(dto.TipoPlano);
        entidade.CalculaPreco(dto.Preco);

       return entidade;
    }

    protected override async Task<Assinatura> DefinirEntidadeInclusaoAsync(AssinaturaDto dto)
    {

        if (dto == null) throw new ArgumentNullException("Não foi encontrado o Dto de assinatura");

        return new Assinatura(dto.TipoAssinatura, dto.Preco, dto.TipoPlano);
    }

    protected override void ValidarDelecao(Assinatura entidade)
    {
        if (entidade == null)
            throw new ArgumentNullException(nameof(entidade), "Assinatura informada não encontrada.");
    }

}
