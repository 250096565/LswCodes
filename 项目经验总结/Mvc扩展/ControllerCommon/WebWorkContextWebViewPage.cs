namespace MvcCustommade.ControllerCommon
{
    public abstract class WebWorkContextWebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public WebWorkContext WorkContext;

        public sealed override void InitHelpers()
        {
            base.InitHelpers();
            var baseController = ((this.ViewContext.Controller) as InfrastructureController);
            if (baseController != null)
            {
                WorkContext = baseController.WorkContext;
            }
        }
    }
}