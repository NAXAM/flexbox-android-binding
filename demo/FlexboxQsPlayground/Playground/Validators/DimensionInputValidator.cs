using System;
using Android.Text;

namespace FlexboxQsPlayground.Playground.Validators
{
    public class DimensionInputValidator : IInputValidator
    {
        public bool IsValid(string text)
		{
			// -1 represents match_parent, -2 represents wrap_content
			return !string.IsNullOrWhiteSpace(text) &&
                          TextUtils.IsDigitsOnly(text) ||
                          string.Equals(text, "-1", StringComparison.OrdinalIgnoreCase) ||
                          string.Equals(text, "-2", StringComparison.OrdinalIgnoreCase);
        }
    }

    public class FixedDimensionInputValidator : IInputValidator
    {
        public bool IsValid(string text)
        {
            return !string.IsNullOrWhiteSpace(text) &&
                          TextUtils.IsDigitsOnly(text);
        }
    }

    public class FlexBasisPercentInputValidator : IInputValidator
    {
        public bool IsValid(string text)
		{
			// -1 represents not set
			return !string.IsNullOrWhiteSpace(text) &&
                          TextUtils.IsDigitsOnly(text) ||
                          string.Equals(text, "-1", StringComparison.OrdinalIgnoreCase);
        }
    }

    public class IntegerInputValidator : IInputValidator
    {
        public bool IsValid(string text)
        {
            int x;
            return !string.IsNullOrWhiteSpace(text) &&
                          int.TryParse(text, out x);
        }
    }

    public class NonNegativeDecimalInputValidator : IInputValidator
    {
        public bool IsValid(string text)
		{
            float x;
			return !string.IsNullOrWhiteSpace(text) &&
                          float.TryParse(text, out x) &&
                          x >= 0;
        }
    }
}
