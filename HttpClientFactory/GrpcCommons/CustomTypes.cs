namespace CustomTypes
{
    public partial class Decimal
    {
        private const decimal NanoFactor = 1_000_000_000;
        public static implicit operator decimal(Decimal grpcDecimal)
        {
            return grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor;
        }

        public static implicit operator Decimal(decimal value)
        {
            var units = decimal.ToInt64(value);
            var nanos = decimal.ToInt32((value - units) * NanoFactor);
            return new Decimal() { Units = units, Nanos = nanos };
        }
    }
}