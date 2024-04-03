namespace Data
{
    public abstract class AbstractDataAPI
    {
        public static AbstractDataAPI createAPI()
        {
            return new DataLayerAPI();
        }
        internal class DataLayerAPI : AbstractDataAPI
        {

        }
    }
}

