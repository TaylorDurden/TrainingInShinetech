using Microsoft.AspNet.SignalR;

namespace MoveShapeDemo
{
    public class MoveShapeHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void UpdateModel(ShapeModel clientModel)
        {
            clientModel.LastUpdatedBy = Context.ConnectionId;
            Clients.AllExcept(clientModel.LastUpdatedBy).updateShape(clientModel);
        }
    }
}