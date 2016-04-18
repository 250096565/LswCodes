using Abp.Web.Mvc.Views;

namespace StudyABP.Web.Views
{
    public abstract class StudyABPWebViewPageBase : StudyABPWebViewPageBase<dynamic>
    {

    }

    public abstract class StudyABPWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected StudyABPWebViewPageBase()
        {
            LocalizationSourceName = StudyABPConsts.LocalizationSourceName;
        }
    }
}