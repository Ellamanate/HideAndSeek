using Zenject;

namespace HideAndSeek.AI
{
    public class Search : BaseAction
    {
        private readonly CharacterModel _characterModel;

        public Search()
        {

        }

        public override void Execute()
        {

        }

        public class Factory : PlaceholderFactory<Search> { }
    }
}
