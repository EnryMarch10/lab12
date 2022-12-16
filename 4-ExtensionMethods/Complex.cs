namespace ExtensionMethods
{
    using System;

    /// <inheritdoc cref="IComplex"/>
    public class Complex : IComplex
    {
        private readonly double _re;
        private readonly double _im;

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> class.
        /// </summary>
        /// <param name="re">the real part.</param>
        /// <param name="im">the imaginary part.</param>
        public Complex(double re, double im)
        {
            this._re = re;
            this._im = im;
        }

        /// <inheritdoc cref="IComplex.Real"/>
        public double Real => _re;

        /// <inheritdoc cref="IComplex.Imaginary"/>
        public double Imaginary => _im;

        /// <inheritdoc cref="IComplex.Modulus"/>
        public double Modulus =>
            Math.Sqrt(Math.Pow(Real, 2) + Math.Pow(Imaginary, 2));

        /// <inheritdoc cref="IComplex.Phase"/>
        public double Phase => Math.Atan2(Imaginary, Real);

        /// <inheritdoc cref="IComplex.ToString"/>
        public override string ToString()
        {
            if (Imaginary == 0.0) return Real.ToString();
            var imAbs = Math.Abs(Imaginary);
            var imValue = imAbs == 1.0 ? "" : imAbs.ToString();
            string sign;
            if (Real == 0d)
            {
                sign = Imaginary > 0 ? "" : "-";
                return sign + "i" + imValue;
            }

            sign = Imaginary > 0 ? "+" : "-";
            return $"{Real} {sign} i{imValue}";
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(IComplex other) =>
            Real.Equals(other.Real) && Imaginary.Equals(other.Imaginary);

        /// <inheritdoc cref="object.Equals(object?)"/>
        public override bool Equals(object obj) =>
            obj is Complex && Equals((Complex) obj);

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode() =>
            HashCode.Combine(Real, Imaginary);
    }
}
