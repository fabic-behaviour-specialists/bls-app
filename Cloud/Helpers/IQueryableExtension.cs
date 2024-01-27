using BodyLifeSkillsPlatform.Data.Models;
using System;
using System.Linq;

namespace BodyLifeSkillsPlatform.Data.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> PerUserFilter<T>(this IQueryable<T> query, string userid)
        {
            if (query is IQueryable<BehaviourScale>)
            {
                IQueryable<BehaviourScale> result = ((IQueryable<BehaviourScale>)query).Where(item => item.UserID.Equals(userid));
                if (result != null)
                {
                    return (IQueryable<T>)result;
                }
                return (IQueryable<T>)((IQueryable<BehaviourScale>)query).Where(item => item.Id == string.Empty);
            }
            else if (query is IQueryable<BehaviourScaleItem>)
            {
                IQueryable<BehaviourScaleItem> result = ((IQueryable<BehaviourScaleItem>)query).Where(item => item.UserID.Equals(userid) || item.UserID == string.Empty);
                if (result != null)
                {
                    return (IQueryable<T>)result;
                }
                return (IQueryable<T>)((IQueryable<BehaviourScaleItem>)query).Where(item => item.Id == string.Empty);
            }
            else if (query is IQueryable<IChooseChart>)
            {
                IQueryable<IChooseChart> result = ((IQueryable<IChooseChart>)query).Where(item => item.UserID.Equals(userid));
                if (result != null)
                {
                    return (IQueryable<T>)result;
                }
                return (IQueryable<T>)((IQueryable<IChooseChart>)query).Where(item => item.Id == string.Empty);
            }
            else if (query is IQueryable<IChooseChartItem>)
            {
                IQueryable<IChooseChartItem> result = ((IQueryable<IChooseChartItem>)query).Where(item => item.UserID.Equals(userid));
                if (result != null)
                {
                    return (IQueryable<T>)result;
                }
                return (IQueryable<T>)((IQueryable<IChooseChartItem>)query).Where(item => item.Id == string.Empty);
            }
            else if (query is IQueryable<ItemHighlight>)
            {
                IQueryable<ItemHighlight> result = ((IQueryable<ItemHighlight>)query).Where(item => item.UserID.Equals(userid));
                if (result != null)
                {
                    return (IQueryable<T>)result;
                }
                return (IQueryable<T>)((IQueryable<ItemHighlight>)query).Where(item => item.Id == string.Empty);
            }
            else
                return query;
        }
    }
}
