namespace Calc.Web.Services
{
    public interface ICalcService
    {
        float Add(float[] summands);

        float Multiplicate(float[] multipliers);
    }
}