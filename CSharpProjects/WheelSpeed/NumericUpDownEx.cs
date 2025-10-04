using System.ComponentModel;
using System.Windows.Forms;

namespace WheelSpeed
{
    public class NumericUpDownEx : NumericUpDown
    {
        private decimal _wheelIncrement;

        [Description("Mouse Wheel Increment"), DefaultValue(1)]
        public decimal WheelIncrement
        {
            get { return _wheelIncrement; }
            set
            {
                if (value > 0)
                {
                    _wheelIncrement = value;
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            HandledMouseEventArgs hme = e as HandledMouseEventArgs;
            if (hme != null)
                hme.Handled = true;

            if (e.Delta > 0 && Value < Maximum)
            {
                if (Value + WheelIncrement >= Maximum)
                {
                    Value = Maximum;
                }
                else
                {
                    Value += WheelIncrement;
                }
            }
            else if (e.Delta < 0 && Value > Minimum)
            {
                if (Value - WheelIncrement < Minimum)
                {
                    Value = Minimum;
                }
                else
                {
                    Value -= WheelIncrement;
                }
            }
        }

        public NumericUpDownEx()
            : base()
        {
            //_wheelIncrement = 1;
        }
    }
}