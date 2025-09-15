namespace miniMarketSolid.Domain.Entities
{
    public interface IDescuento
    {
        decimal AplicarDescuento(decimal montoTotal);
    }
}
