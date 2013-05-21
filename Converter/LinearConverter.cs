using System;
using Converter.Interfaces;

namespace Converter
{
    /// <summary>
    /// Basic implementation for linear unit Converter.
    /// 
    /// DestUnit = SrcUnit * Factor + Deltha.
    /// 
    /// Instances should always have an inverse, except for Factor==0, but what's the point. Factor cannot be 0, 
    /// ArgumentException will protect you from harm yourself
    /// </summary>
    public class LinearConverter : ILinearConverter
    {
        #region Fields & Properties
        private double _dblFactor = 1d;
        public double Factor
        {
            get { return _dblFactor; }
            set { _dblFactor = value; }
        }

        private double _dblDeltha;
        public double Deltha
        {
            get { return _dblDeltha; }
            set { _dblDeltha = value; }
        }
        #endregion

        #region Creation & Initialization

        public LinearConverter(double factor, double deltha)
        {
            if (factor == 0)
                throw new ArgumentException("Factor cannot be 0");
            this._dblFactor = factor;
            this._dblDeltha = deltha;
        }

        public LinearConverter(double factor)
        {
            if (factor == 0)
                throw new ArgumentException("Factor cannot be 0");
            this._dblFactor = factor;
        }
        #endregion

        #region Converter Overrides
        public float Convert(float source)
        {
            return (float)(source * _dblFactor + _dblDeltha);
        }
        public bool AllowInverse { get { return true; } }
        public ILinearConverter Inverse { get { return new LinearConverter(1 / _dblFactor, -_dblDeltha / _dblFactor); } }
        #endregion
    }
}
