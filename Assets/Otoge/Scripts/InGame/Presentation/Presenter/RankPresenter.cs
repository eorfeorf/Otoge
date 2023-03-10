
/// <summary>
/// ジャッジランク.
/// </summary>
public class RankPresenter
{
   private RankUseCase _useCase;
   private RankView _view;

   public RankPresenter(RankUseCase useCase, RankView view)
   {
      _useCase = useCase;
      _view = view;

      _view.Rank = _useCase.GetRank();
   }
   
}
