using System;
using Logic;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class ModelAbstractApi
    {
        public abstract List<BallModel> balls { get; }

        public abstract void GenerateBalls(int counts);

        public static ModelAbstractApi CreateApi()
        {
            return new ModelLayerApi();
        }
    }

    internal class ModelLayerApi : ModelAbstractApi
    {
        private LogicAbstractAPI logicApi;

        public override List<BallModel> balls => copyBallList();

        public ModelLayerApi()
        {
            logicApi = logicApi ?? LogicAbstractAPI.CreateAPI();
        }

        public override void GenerateBalls(int counts)
        {
            logicApi.createBalls(counts);
            logicApi.start();

        }

        public List<BallModel> copyBallList()
        {
            List<BallModel> ballsModel = new List<BallModel>();

            foreach (Ball b in logicApi.getAllBalls())
            {
                ballsModel.Add(new BallModel(b));
            }
            return ballsModel;
        }
    }
}
