namespace miniMarketSolid.Domain.Entities
{
    public interface Descuento
    {
        decimal AplicarDescuento(decimal montoTotal);
    }
}
