using bramkominatorMobile.Exceptions;

namespace bramkominatorMobile.Models
{
    public class OutputElement : CircutElement
    {
        public override bool InputA { get => Node.Left.Content.Output; }
        public override bool Output { get => Node.Left.Content.Output; }
        public string Image { get; private set; }

        public OutputElement(Position position) : base()
        {
            if (Node.Left is null || !Output)
                Image = "outputOff.png";
            else
                Image = "ouputOn.png";

            if (position is null)
                throw new BadElementInputException("Position cannot be null!");

            Position = new Position(position.Column, position.Row);
        }

        public void Disconnect()
        {
            Node.Left.Next = null;
            Node.Left = null;
        }

        public bool IsConnected()
        {
            if (Node.Left is null)
                return false;
            else
                return true;
        }
    }
}
