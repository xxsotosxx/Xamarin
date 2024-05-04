using Game.Graphics;

namespace Game.Logic.Classes
{
    internal class Cat : Something
    {
        public override void MoveTo(MoveDirection direction)
        {
            var Enemyes = Engine.SearchEnamy(this);
            //TODO: Найти ближайшего врага 
            foreach (var enemy in Enemyes)
            {
                deltaX = enemy.
            }
            //base.MoveTo(direction);
        }
    }
}
