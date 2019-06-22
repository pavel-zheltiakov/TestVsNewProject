using GalaSoft.MvvmLight;

namespace VSNewProjectDialogExample.ViewModel
{
    public class StarViewModel : ViewModelBase
    {
        public StarViewModel(double value)
        {
            Value = value;
        }

        public double Value { get; }
    }

    public class MultiStarViewModel : ViewModelBase
    {
        public MultiStarViewModel(double value, int starCount)
        {
            Stars = new StarViewModel[starCount];
            for (var i = 1; i <= starCount; i++)
            {
                Stars[i - 1] = new StarViewModel(
                    value >= i
                        ? 1
                        : value > i - 1
                            ? value - i
                            : 0);
            }
        }

        public StarViewModel[] Stars { get; }
    }
}