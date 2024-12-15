namespace FinalGame.Develop.MainMenu.Features.LevelsMenu.LevelsMenuPopup
{
    public class LevelsMenuPopupPresenter
    {
        private const string TitleName = "LEVELS";
        
        //model
        private readonly LevelsMenuPopupFactory _factory;
        
        //view
        private readonly LevelsMenuPopupView _view;
        private LevelsTileListPresenter _levelsTileListPresenter;

        public LevelsMenuPopupPresenter(LevelsMenuPopupFactory factory, LevelsMenuPopupView view)
        {
            _factory = factory;
            _view = view;
        }

        public void Enable()
        {
            _view.SetTitle(TitleName);
            _levelsTileListPresenter = _factory.CreateLevelsTileListPresenter(_view.LevelsTileList);
            
            _levelsTileListPresenter.Enable();

            _view.CloseRequest += OnCloseRequest;
        }

        public void Disable()
        {
            _levelsTileListPresenter.Disable();
            _view.CloseRequest -= OnCloseRequest;
            
            UnityEngine.Object.Destroy(_view.gameObject);
        }
        
        private void OnCloseRequest()
        {
            Disable();
        }
    }
}