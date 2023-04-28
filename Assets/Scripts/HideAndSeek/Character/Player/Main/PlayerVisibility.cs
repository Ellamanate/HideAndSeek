namespace HideAndSeek
{
    public class PlayerVisibility
    {
        private readonly PlayerModel _model;

        private PlayerBody _body;

        public bool Available => !_model.Destroyed && _body != null;

        public PlayerVisibility(PlayerModel model)
        {
            _model = model;
        }
        
        public void Initialize(PlayerBody body)
        {
            _body = body;
        }

        public void SetVisible()
        {
            if (Available)
            {
                _model.Visible = true;
                _body.SetVisible(true);
            }
        }

        public void SetInvisible()
        {
            if (Available)
            {
                _model.Visible = false;
                _body.SetVisible(false);
            }
        }
    }
}